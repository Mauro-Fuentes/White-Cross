using UnityEngine;

public class BookController : MonoBehaviour
{
    // serialized
    [SerializeField] AudioClip clip;
    //[SerializeField] int layerFilter = 11;
    [SerializeField] float playerDistanceThreshold = 1f;
    [SerializeField] bool isOneTimeOnly = true;

    [SerializeField] public Canvas myCanvas;


    bool hasPlayed = false;
    AudioSource audioSource;
    GameObject player;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");

        audioSource.playOnAwake = false;
        audioSource.clip = clip;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (distanceToPlayer <= playerDistanceThreshold)
            {
                RequestPlayAudioClip();
            }

        }
    }

    void RequestPlayAudioClip()
    {
        if (isOneTimeOnly && hasPlayed)
        {
            return;
        }
        else if (audioSource.isPlaying == false)
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 255f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, playerDistanceThreshold);
    }
}