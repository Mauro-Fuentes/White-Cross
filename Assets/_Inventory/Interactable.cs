using UnityEngine;
using UnityEditor;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;


    public virtual void Interact () // Interact is designed to be overwritten
    {
        Debug.Log ("Interacting with " + transform.name);
    }

    void OnDrawGizmosSelected ()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
