using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseItemBehaviour : Interactable
{
    protected readonly static GameObject _player;
    public int stack { get; protected set; }
    static BaseItemBehaviour()
    {
        _player = GameObject.FindWithTag("Player");
    }
    public override sealed void OnFocus()
    {
        InteractText = "[E] - pick up";
    }
    public override sealed void OnLoseFocus()
    {
        InteractText = "";
    }
    public override sealed void OnInteract()
    {
        OnGet(GetType());
        OnLoseFocus();
    }

    public void OnGet(System.Type itemType)
    {
        if (!_player.GetComponent(itemType))
        {
            _player.AddComponent(itemType);
            _player.GetComponent<PlayerStatictics>()?.PlayerItems.Add(this);
        }
        var item = (BaseItemBehaviour)_player.GetComponent(itemType);
        item.stack += 1;
        ItemBehaviour();//update itemEffectBehaviour, that dependens on its stack count

        IsLastInteracted = true;
    }

    public void OnLoss()
    {
        stack--;
        ItemBehaviour();//update itemEffectBehaviour, that dependens on its stack count

        if(stack<=0)
        {
            _player.GetComponent<PlayerStatictics>()?.PlayerItems.Remove(this);
            Destroy(this);
        }
    }
    public abstract void ItemBehaviour();
}