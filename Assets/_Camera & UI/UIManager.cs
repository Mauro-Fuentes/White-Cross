using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	Scene nextScene;

	[SerializeField] Button beginButton;

	bool beginButtonHasBeenPressed = false;

	bool CoroutineIsRunning = true;

	void Start()
	{
		StartCoroutine (LoadNextSceneInTheBackground());
	
		beginButton.onClick.AddListener(OnClickBeginButton);
	}

	void Update()
	{
		if (CoroutineIsRunning == false)
		{
			SetActiveScene();
		}
	}


    IEnumerator LoadNextSceneInTheBackground()
    {
		AsyncOperation loadSceneInTheBackground = SceneManager.LoadSceneAsync ( 1 , LoadSceneMode.Additive);

		loadSceneInTheBackground.allowSceneActivation = false;

		// wait until it loads
		while (!loadSceneInTheBackground.isDone)
		{
			if (loadSceneInTheBackground.progress <= 0.9f)
			{
				if (beginButtonHasBeenPressed)
				{
                    //Activate the Scene
                    loadSceneInTheBackground.allowSceneActivation = true;

					// print (loadSceneInTheBackground.progress);
					CoroutineIsRunning = false;

            	}
			}
			yield return  null;
		}
    }

	public void OnClickBeginButton()
	{
		print ("BUTTON PRESSED");
		beginButtonHasBeenPressed = true;
	}

    public void SetActiveScene () 
	{
		nextScene = SceneManager.GetSceneByBuildIndex(1);

		AsyncOperation UnloadScene = SceneManager.UnloadSceneAsync(0);

		if (UnloadScene.isDone)
		{
			print ("unload finished");
			SceneManager.SetActiveScene(nextScene);
		}

	}

}
