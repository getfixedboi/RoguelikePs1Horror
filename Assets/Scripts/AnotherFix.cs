using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnotherFix : MonoBehaviour
{
    public static bool isFirstEnter=true;
    void Start()
    {
        if(isFirstEnter)
        {
            UnityEngine.Debug.Log("reloadScene");
            isFirstEnter=false;
            SceneManager.LoadScene(1);
        }
    }
}
