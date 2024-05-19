using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseItemBehaviour : Interactable
{
    protected static GameObject player { private set; get; }
    public int Stack { get; protected set; }
    public int PreviousStack { get; protected set; }
    [HideInInspector]
    public string ItemDescription;
    public Sprite ItemSprite;
    public static readonly float ItemDropChance = 0.25f;
    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player");

        Destroy(gameObject,12f);
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
        AllForStupidDB.Curcount+=1;
        
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

    public void Update()
    {
        transform.Rotate(0,0,.3f);
    }
}