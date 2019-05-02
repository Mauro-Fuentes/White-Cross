using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleron
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log ("More than one instance of Inventory found!");
            return;
        }
        instance = this;    // we get this by Inventory.instance from everywhere
    }
    #endregion

    // Delegate to know if the list has changed
    public delegate void OnItemChanged ();
    public OnItemChanged onItemChangedCallBack;

    public int space = 5;

    // List of type Item
    public List<Item> items = new List<Item>();

    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log ("Not enough room");
                return false;
            }

            items.Add (item);

            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
            
        }

        return true;
        
    }    

    public void Remove (Item item)
    {
        items.Remove (item);

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
}
