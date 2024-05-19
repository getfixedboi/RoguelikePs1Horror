using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllForStupidDB : MonoBehaviour
{
    #region time db
    private static float _bestTime = 0;
    private float _curtime = 0;
    public static float GetBestTime
    {
        get
        {
            return _bestTime;
        }
    }
    #endregion
    #region items count db
    private static int _bestCount = 0;
    public static int Curcount = 0;
    public static int GetBestItemsCount
    {
        get
        {
            return _bestCount;
        }
    }
    #endregion

    private void Update()
    {
        _curtime += Time.deltaTime;
        _bestTime = _curtime > _bestTime ? _curtime : _bestTime;

        _bestCount = Curcount > _bestCount ? Curcount : _bestCount;
    }
}
