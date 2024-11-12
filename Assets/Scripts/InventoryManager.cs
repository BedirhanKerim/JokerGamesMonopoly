using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace InventorySystem{
public class InventoryManager : Singleton<InventoryManager>
{
    public ItemUnit[] inventoryValues = new ItemUnit[3];

    [System.Serializable]
    public class ItemUnit
    {
        public ItemType type;
        public int collectedValue;
        public Sprite itemSprite;
        public TextMeshProUGUI itemTextBox;
        public Image itemImage;
    }

    public enum ItemType
    {
        Apple,
        Pear,
        Strawberry
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadInventoryData();
    }

    private void LoadInventoryData()
    {
        for (int i = 0; i < inventoryValues.Length; i++)
        {
            inventoryValues[i].itemImage.sprite = inventoryValues[i].itemSprite;

            int storedValue = PlayerPrefs.GetInt(inventoryValues[i].type.ToString(), 0);
            inventoryValues[i].collectedValue = storedValue;
            inventoryValues[i].itemTextBox.text = inventoryValues[i].collectedValue.ToString(); // UI'yi güncelle
        }
    }

    public void AddItem(ItemType itemType, int count)
    {
        // Mevcut miktarı al
        int currentAmount = PlayerPrefs.GetInt(itemType.ToString(), 0);

        int newAmount = currentAmount + count;
        PlayerPrefs.SetInt(itemType.ToString(), newAmount);
        PlayerPrefs.Save();

        for (int i = 0; i < inventoryValues.Length; i++)
        {
            if (itemType == inventoryValues[i].type)
            {
                inventoryValues[i].collectedValue = newAmount;
                inventoryValues[i].itemTextBox.text = inventoryValues[i].collectedValue.ToString();
            }
        }
    }
[NaughtyAttributes.Button()]
    public void TestAddApple()
    {
        AddItem(ItemType.Apple, 5);
    }
}
}