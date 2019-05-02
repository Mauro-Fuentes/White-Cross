using UnityEngine;

namespace RPG.Characters
{   

    public class UIFaceCamera : MonoBehaviour 
    {
        public GameObject cameraToSearch;
        public Camera cameraToLookAt;

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