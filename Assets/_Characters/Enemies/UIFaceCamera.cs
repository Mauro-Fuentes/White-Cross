using UnityEngine;

namespace RPG.Characters
{   

    public class UIFaceCamera : MonoBehaviour 
    {
        public GameObject cameraToSearch;
        public Camera cameraToLookAt;

        void Start()
        {
            cameraToSearch = GameObject.FindGameObjectWithTag ("MainCamera");
            cameraToLookAt = cameraToSearch.GetComponent<Camera>();
            
            //cameraToLookAt = Camera.main;
        }

        void LateUpdate()
        {
            // hacemos que el GUI mire a la camara todo el tiempo
            transform.LookAt(cameraToLookAt.transform);
        }

    }

}