using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraTestItems : BaseItemBehaviour
{
    private int _baseValue = 2;
    private int _valuePerStack = 1;
    protected override void Awake()
    {
        base.Awake();
        itemDescription = "item 2";
    }
    public override void ItemBehaviour()
    {
        //UnityEngine.Debug.Log($"{PreviousStack}/{Stack}");
        //UnityEngine.Debug.Log($"{_baseValue * (PreviousStack <= 0 ? 0 : 1) + _valuePerStack * (PreviousStack <= 0 ? 0 : PreviousStack-1)}/{_baseValue + _valuePerStack * (Stack - 1)}");
        PlayerStatictics.bonusDamage -= _baseValue * (PreviousStack == 0 ? 0 : 1) + _valuePerStack * (PreviousStack <= 0 ? 0 : PreviousStack-1);
        PlayerStatictics.bonusDamage += _baseValue + _valuePerStack * (Stack - 1);
    }
}
