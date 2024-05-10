using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;

public abstract class Interactable : MonoBehaviour
{
    public bool IsLastInteracted { get; protected set; }
    public string InteractText{get;protected set;}
    protected virtual void Awake()
    {
        IsLastInteracted = false;
    }
    public abstract void OnFocus();
    public abstract void OnLoseFocus();
    public abstract void OnInteract();
}
