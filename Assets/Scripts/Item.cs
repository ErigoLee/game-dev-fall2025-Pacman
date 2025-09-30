using UnityEngine;

public abstract class Item :  MonoBehaviour
{
    public string ItemName { get; }
    public virtual Transform parentAfterDrag { get; set; }
}
