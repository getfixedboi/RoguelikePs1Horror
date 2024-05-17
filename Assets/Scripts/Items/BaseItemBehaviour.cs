using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseItemBehaviour : Interactable
{
    protected static GameObject player { private set; get; }
    public int Stack { get; protected set; }
    public int PreviousStack { get; protected set; }
    public string itemDescription;
    public Sprite ItemSprite;
    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player");
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
        OnGet(this.GetType());
        OnLoseFocus();
        Destroy(gameObject);
    }

    public void OnGet(System.Type itemType)
    {
        if (!player.GetComponent(itemType))
        {
            player.AddComponent(itemType);
            player.GetComponent<PlayerStatictics>().PlayerItems.Add(this);
        }
        var item = (BaseItemBehaviour)player.GetComponent(itemType);
        item.Stack++;
        item.PreviousStack = item.Stack-1;
        item.ItemBehaviour();//update itemEffectBehaviour, that dependens on its stack count
        player.GetComponent<PlayerStatictics>().UpdateItemsOutput();

        IsLastInteracted = true;
    }

    public void OnLoss()
    {
        Stack--;
        PreviousStack = Stack-1;
        ItemBehaviour();//update itemEffectBehaviour, that dependens on its stack count
        player.GetComponent<PlayerStatictics>().UpdateItemsOutput();
        
        if (Stack <= 0)
        {
            player.GetComponent<PlayerStatictics>()?.PlayerItems.Remove(this);
            player.GetComponent<PlayerStatictics>().UpdateItemsOutput();
            Destroy(this);
        }
    }
    public abstract void ItemBehaviour();
}