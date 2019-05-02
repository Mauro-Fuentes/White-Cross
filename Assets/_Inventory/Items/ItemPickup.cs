using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Transform player;

    bool hasInterected = false;

    public Item item;

    void Update()
    {
        float distance = Vector3.Distance(player.position, interactionTransform.position);

        if (distance <= radius && !hasInterected)
        {
            hasInterected = true;
            Interact();
        }
        else if (distance >= radius)
        {
            hasInterected = false;
        }
    }

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {

        Debug.Log("Picking up " + item.name);

        bool wasPickedUp = Inventory.instance.Add ( item );

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}

