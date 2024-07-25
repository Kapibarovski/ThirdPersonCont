using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IgameManager
{
    public ManagerStatus status { get; private set; }

    private Dictionary<string, int> _items;

    public string EquippedItem { get; private set; }
    public void Startup()
    {
        Debug.Log("Inventory manager starting...");

        _items = new();

        status = ManagerStatus.Started;
    }

    public void Inventory()
    {
        string NameOfObject = "Object: ";
        foreach (var item in _items)
        {
            NameOfObject += item.Key + "(" + item.Value + ") ";
        }
        Debug.Log($"Inventory: {NameOfObject}");
    }

    public void AddItem(string name)
    {
        if (_items.ContainsKey(name))
        {
            _items[name] += 1;
        }
        else 
        {
            _items[name] = 1;
        }
        Inventory();
    }

    public List<string> GetItemList()
    {
        List<string> list = new List<string>(_items.Keys);
        return list;
    }

    public int GetItemCount(string name)
    {
        if(_items.ContainsKey(name))
        {
            return _items[name];
        }
        return 0;
    }

    public bool EquipItem(string name)
    {
        if(_items.ContainsKey(name) && EquippedItem != name)
        {
            EquippedItem = name;
            Debug.Log($"Equipped: {name}");
            return true;
        }
        EquippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }
}
