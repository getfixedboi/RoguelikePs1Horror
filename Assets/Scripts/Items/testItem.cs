using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testItem : BaseItemBehaviour
{
    private int _baseValue = 10;
    private int _valuePerStack = 5;
    protected override void Awake()
    {
        base.Awake();
        itemDescription = "item 1";
    }
    public override void ItemBehaviour()
    {
        //UnityEngine.Debug.Log($"{PreviousStack}/{Stack}");
        //UnityEngine.Debug.Log($"{_baseValue * (PreviousStack <= 0 ? 0 : 1) + _valuePerStack * (PreviousStack <= 0 ? 0 : PreviousStack-1)}/{_baseValue + _valuePerStack * (Stack - 1)}");
        PlayerStatictics.bonusHP -= _baseValue * (PreviousStack == 0 ? 0 : 1) + _valuePerStack * (PreviousStack <= 0 ? 0 : PreviousStack-1);
        PlayerStatictics.bonusHP += _baseValue + _valuePerStack * (Stack - 1);
    }
}
