using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Класс-пример. Его надо будет переписать под базовый
/// </summary>
public class ExtraBulletsItem : MonoBehaviour, IItem
{
    string itemName = "bullet";
public void OnTake()
    {
        TestPlayerScript.bullets ++;

        if (TestPlayerScript.itemDictionary.TryGetValue(itemName, out var itemData))
        {
            TestPlayerScript.itemDictionary[itemName] = (itemData.count + 1, itemData.receiptNumber);
        }
        else
        {
            int maxReceiptNumber = TestPlayerScript.itemDictionary.Values.Select(item => item.receiptNumber).DefaultIfEmpty(0).Max();
            int newReceiptNumber = maxReceiptNumber != 0 ? maxReceiptNumber + 1 : 1;
            TestPlayerScript.itemDictionary[itemName] = (1, newReceiptNumber);
        }
    }


    public void OnLoss()
    {   
        if (TestPlayerScript.itemDictionary.TryGetValue(itemName, out var itemData))
        {
            TestPlayerScript.itemDictionary[itemName] = (itemData.count - 1, itemData.receiptNumber);
            TestPlayerScript.bullets --;
            if(itemData.count <=0)
            {
                TestPlayerScript.itemDictionary.Remove(itemName);

                foreach (var item in TestPlayerScript.itemDictionary.ToList())
                {
                    if (item.Value.receiptNumber > TestPlayerScript.itemDictionary[itemName].receiptNumber)
                    {
                        var updatedValue = item.Value;
                        updatedValue.receiptNumber -= 1;
                        TestPlayerScript.itemDictionary[item.Key] = updatedValue;
                    }
                }
            }
        }
    }

    public void OnTriggerEnter()
    {
        OnTake();
        Destroy(gameObject);
    }
}
