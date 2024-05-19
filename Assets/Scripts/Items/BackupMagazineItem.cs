using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupMagazineItem : BaseItemBehaviour
{
    
    private int _baseValue = 1;
    private int _valuePerStack = 1;
    BackupMagazineItem()
    {
        ItemDescription = "Ammo up!";
    }
    public override void ItemBehaviour()
    {   
        PlayerStatictics.bonusAmmo -= _baseValue * (PreviousStack == 0 ? 0 : 1) + _valuePerStack * (PreviousStack <= 0 ? 0 : PreviousStack-1);
        PlayerStatictics.bonusAmmo += _baseValue + _valuePerStack * (Stack - 1);
    }
}
