using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(PlayerManager))]

public class Managers : MonoBehaviour
{
    public static PlayerManager Player { get; private set; }
    public static InventoryManager Inventory { get; private set; }

    private List<IgameManager> _managers;

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();

        _managers = new List<IgameManager>();

        _managers.Add(Inventory);
        _managers.Add(Player);

        StartCoroutine(StartUpManager());
    }

    private IEnumerator StartUpManager()
    {
        foreach (var manager in _managers)
        {
            manager.Startup();
        }

        yield return null;

        int moduleReady = 0;
        int moduls = _managers.Count;

        while (moduleReady < moduls)
        {
            int lastReady = moduleReady;
            moduleReady = 0;

            foreach (var manager in _managers)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    moduleReady++;
                }
                if (moduleReady > lastReady)
                {
                    Debug.Log($"Progress: {moduleReady}/{moduls}");
                    yield return null;
                }
            }
        }

        Debug.Log("All managers set up");
    }
}
