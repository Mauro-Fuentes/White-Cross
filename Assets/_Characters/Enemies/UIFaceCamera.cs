using UnityEngine;

namespace RPG.Characters
{   

    public class UIFaceCamera : MonoBehaviour 
    {

        Camera cameraToLookAt;

        void Start()
        {
            // Buscamos la camara principal
            cameraToLookAt = Camera.main;

        }

        void LateUpdate()
        {
            // hacemos que el GUI mire a la camara todo el tiempo
            transform.LookAt(cameraToLookAt.transform);
        }

    }

}