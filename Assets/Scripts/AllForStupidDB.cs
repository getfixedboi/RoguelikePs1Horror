using UnityEngine;
public class AllForStupidDB : MonoBehaviour
{
    #region time db
    private static float _bestTime = 0;
    private float _curTime = 0;
    public static float GetBestTime
    {
        get { return _bestTime; }
    }

    public static void SetBestTime(float bestTime)
    {
        _bestTime = bestTime;
    }
    #endregion

    #region items count db
    private static int _bestCount = 0;
    public static int CurCount = 0;
    public static int GetBestItemsCount
    {
        get { return _bestCount; }
    }

    public static void SetBestCount(int bestCount)
    {
        _bestCount = bestCount;
    }
    #endregion
    
    private void Start()
    {
        _curTime = 0;
        CurCount = 0;
    }

    private void Update()
    {
        _curTime += Time.deltaTime;
        _bestTime = _curTime > _bestTime ? _curTime : _bestTime;

        _bestCount = CurCount > _bestCount ? CurCount : _bestCount;

        if(!Input.GetKeyDown(KeyCode.UpArrow))
        {
            return;
        }
    }
}