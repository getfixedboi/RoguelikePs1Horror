using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Fix : MonoBehaviour
{
    public void OnEnable()
    {
        StartCoroutine(FixBug());
    }

    public IEnumerator FixBug()
    {
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(1);
    }
}
