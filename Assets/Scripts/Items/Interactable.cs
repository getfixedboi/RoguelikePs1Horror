using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
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
