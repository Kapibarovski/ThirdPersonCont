using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GUIBasic : MonoBehaviour
{
    private void OnGUI()
    {
        int posX = 10;
        int posY = 10;
        int width = 100;
        int height = 50;
        int buffer = 110;
        List<string> items = Managers.Inventory.GetItemList();
        if (items.Count == 0)
        {
            GUI.Box(new Rect(posX, posY, width, height), "No items");
        }

        foreach (var item in items)
        {
            int count = Managers.Inventory.GetItemCount(item);
            Texture2D texture = Resources.Load<Texture2D>($"Icon/{item}");
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent($"({count})", texture));
            posX += buffer;
        }

        string equip = Managers.Inventory.EquippedItem;

       

        posX = 10;
        posY = buffer;

        foreach (var item in items)
        {
            if (GUI.Button(new Rect(posX, posY, width, height), $"Equip {item}"))
            {
                Managers.Inventory.EquipItem(item);
            }
            posX += buffer; 
        }
        
        posY = 10;

        if (equip != null)
        {
            posX = Screen.width - buffer;
            Texture2D image = Resources.Load($"Icons/{equip}") as Texture2D;
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent("Equipped", image));
        }
    }
}
