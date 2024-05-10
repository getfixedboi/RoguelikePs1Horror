using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IItem
{
    void OnTake();
    void OnLoss();
    void OnTriggerEnter();
}
public class TestPlayerScript : MonoBehaviour
{
    public static int playerMaxHP=100;
    public static int bullets = 10;
    public static Dictionary<string,(int count,int receiptNumber)> itemDictionary = new Dictionary<string, (int count, int receiptNumber)>();
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            foreach (var item in itemDictionary)
            {
                Debug.Log("Key: " + item.Key);
                Debug.Log("Count: " + item.Value.count);
                Debug.Log("Receipt Number: " + item.Value.receiptNumber);
            }
        }
    }
}
