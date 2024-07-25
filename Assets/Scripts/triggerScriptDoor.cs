using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerScriptDoor : MonoBehaviour
{
    [SerializeField] private GameObject[] _doors;
    public bool RequreKey;

    private void OnTriggerEnter(Collider other)
    {
        if(RequreKey && Managers.Inventory.EquippedItem == "key")
        {
            foreach(var door in _doors)
            {
                door.SendMessage("Activate");
            }
        }
        else if(RequreKey && Managers.Inventory.EquippedItem != "key")
        {
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach(GameObject door in _doors)
        {
            door.SendMessage("Disactivate");
        }
    }
}
