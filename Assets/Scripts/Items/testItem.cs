using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testItem : BaseItemBehaviour
{
    private int _baseValue = 10;
    private int _valuePerStack = 5;
    public override void ItemBehaviour()
    {
        if (stack > 1)
        {
            PlayerStatictics.bonusHP -= _baseValue + _valuePerStack * (stack - 2);
            PlayerStatictics.bonusHP += _baseValue + _valuePerStack * (stack - 1);
        }
        else if (stack == 1)
        {
            PlayerStatictics.bonusHP += _baseValue + _valuePerStack * (stack - 1);
        }
    }
}
