using UnityEngine;

namespace RPG.Characters
{   
    // Add a UI SOCKET transform to your EnemyAI
    // Attach this script to the socket
    // Link to a canvas prefab that contains NPC UI
    public class EnemyUI : MonoBehaviour 
    {

        // Works around Unity 5.5's lack of nested prefabs
        [Tooltip("The UI canvas prefab")]
        [SerializeField] GameObject enemyCanvasPrefab = null;

        Camera cameraToLookAt;


        void Start()
        {
            // Buscamos la camara principal
            cameraToLookAt = Camera.main;

            // instanciamos el prefab del canvas en la posición en la que esta el SOCKET
            Instantiate(enemyCanvasPrefab, transform.position, Quaternion.identity, transform);
        }


        void LateUpdate()
        {
            // hacemos que el GUI mire a la camara todo el tiempo
            transform.LookAt(cameraToLookAt.transform);
            transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
        }
    }

}