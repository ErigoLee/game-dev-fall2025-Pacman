using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StrawberryItem : Item, IBeginDragHandler, IDragHandler, IEndDragHandler
{
   [Header("UI")]
    public Image image;

    [HideInInspector] public Transform _parentAfterDrag;
    public override Transform parentAfterDrag
    {
        get => _parentAfterDrag;
        set => _parentAfterDrag = value;
    }
    public new string ItemName { get; private set; }

     // No constructor—use Awake() for initialization
    private void Awake()
    {
        ItemName = "strawberry";  // Set once here
    }

    //Drag and drop
    public void OnBeginDrag(PointerEventData eventData){
        image.raycastTarget = false;
        _parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        image.raycastTarget = true;
        transform.SetParent(_parentAfterDrag);
    }
}
