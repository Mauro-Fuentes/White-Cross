using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    public string ItemName = "New Item";

    public Sprite icon = null;

    public bool isDefaultItem = false;


    // LEt's do it virtual so every object has its own way of implementing Use()
    public virtual void Use()
    {
        //Use the item
        // somethin might happen

        Debug.Log ("Using " + name);
    }
}

