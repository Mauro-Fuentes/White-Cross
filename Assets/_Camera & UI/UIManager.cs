using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
	Scene nextScene;
	Scene currentScene;

	[SerializeField] Button beginButton;
	[SerializeField] Image loadingBar;

	public Text loadingText;

	bool startButtonHasBeenPressed;
	bool LoadSceneBackgroundCoroutineFinished;

	[SerializeField] AudioClip audioClickButton;
	[SerializeField] AudioSource audioSource;

	AsyncOperation asyncloadSceneInTheBackground;

	void Start()
	{
        //loadingBar.gameObject.SetActive(false);
		loadingBar.gameObject.SetActive(true);

		currentScene = SceneManager.GetActiveScene();
	
		beginButton.onClick.AddListener(OnClickButton);

		loadingText.text = "Loading...";

		audioSource = GetComponent<AudioSource>();

		// the coroutine hangs until 90% and waits for permission: buttonHasBeenPressed 
		StartCoroutine (LoadNextSceneInTheBackground());
	}

	void Update()
    {
        UpdateLoadingBar();

		// Once asyncLoad is finished, check if Start button is pressed
		if (LoadSceneBackgroundCoroutineFinished == true)
        {
            SetActiveScene();
        }
    }

    public void OnClickButton()
	{
		StartCoroutine ( WaitForTheSound() );
			
	}

    private void UpdateLoadingBar()
    {
        float progress = Mathf.Clamp01(asyncloadSceneInTheBackground.progress / 0.9f);
        loadingBar.fillAmount = progress;
        loadingText.text = progress * 100f + "%";
    }



    IEnumerator LoadNextSceneInTheBackground()
    {
		asyncloadSceneInTheBackground = SceneManager.LoadSceneAsync ( GetNextSceneName() , LoadSceneMode.Additive);
		asyncloadSceneInTheBackground.allowSceneActivation = false;

		// wait until it loads
		while (!asyncloadSceneInTheBackground.isDone)
		{	
			if (asyncloadSceneInTheBackground.progress <= 0.9f)
			{
				break;
			}
		}
		yield return  null;
    }

    public void SetActiveScene () 
	{
		AsyncOperation UnloadOldScene = SceneManager.UnloadSceneAsync(currentScene);

		if (UnloadOldScene.isDone)
		{
			SceneManager.SetActiveScene(nextScene);
		}
	}

	IEnumerator WaitForTheSound()
    {
		audioSource.PlayOneShot(audioClickButton);

		yield return new WaitWhile(audioFinished);

		startButtonHasBeenPressed = true;

		if (startButtonHasBeenPressed)
		{
			//Activate the Scene
			asyncloadSceneInTheBackground.allowSceneActivation = true;

			LoadSceneBackgroundCoroutineFinished = true;
		}	
	}

	public bool audioFinished()
	{
		if (audioSource.isPlaying)
		{
			return true;
		}
		else 
		{	
			return false;
		}
	}


	// "Level +1"
	public string GetNextSceneName()
	{
		var nextSceneIndex = SceneManager.GetActiveScene().buildIndex +1;

		if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
		{
			return GetSceneNameByBuildIndex(nextSceneIndex);
		}
		return string.Empty;
	}

	public string GetSceneNameByBuildIndex(int buildIndex)
	{
		return GetSceneNameFromScenePath (SceneUtility.GetScenePathByBuildIndex (buildIndex));
	}

	private string GetSceneNameFromScenePath (string scenePath)
	{
		var sceneNameStart = scenePath.LastIndexOf ("/", StringComparison.Ordinal ) +1;
		var sceneNameEnd = scenePath.LastIndexOf (".", StringComparison.Ordinal);
		var sceneNameLength = sceneNameEnd - sceneNameStart;
		return scenePath.Substring (sceneNameStart, sceneNameLength);
	}
}
