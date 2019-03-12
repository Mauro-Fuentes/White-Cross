using UnityEngine;

namespace RPG.Characters
{   

    public class UIFaceCamera : MonoBehaviour 
    {
        public GameObject cameraToSearch;
        public Camera cameraToLookAt;


        // public static UIFaceCamera uIFaceCameraSingleton {get; private set;}


        void Awake()
        {
            while (cameraToSearch == null)
            {
                cameraToSearch = GameObject.FindGameObjectWithTag ("MainCamera");
                if (cameraToSearch != null)
                {
                    break;
                }
            }

            //cameraToSearch = GameObject.FindGameObjectWithTag ("MainCamera");
      
            
            // if (uIFaceCameraSingleton == null)				// Si el tipo MyGameManager uIFaceCameraSingleton no está
            // {	

            //     uIFaceCameraSingleton = this;				// Hacer una instancia this (MyGameManager)

            //     //DontDestroyOnLoad (uIFaceCameraSingleton);	            // Este gameObject
            // }
            // else
            // {
            //     Destroy(gameObject);	
            // }

            cameraToLookAt = cameraToSearch.GetComponent<Camera>();
            
            cameraToLookAt = Camera.main;
        }

        void LateUpdate()
        {
            if (cameraToLookAt != null)
            {
                cameraToLookAt.Reset();

                transform.LookAt(cameraToLookAt.transform);
            }
        }

    }

}