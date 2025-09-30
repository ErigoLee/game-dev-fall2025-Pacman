using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;

    [HideInInspector] public Transform parentAfterDrag;

    [SerializeField] private string item_name;

    private void Awake(){
        if (CompareTag("Apple_Item") && !string.Equals(item_name, "apple")) {
            item_name = "apple";
        }

        if (CompareTag("Orange_Item") && !string.Equals (item_name, "orange")) {
            item_name = "orange";
        }

        if(CompareTag("Banana_Item") && !string.Equals (item_name, "banana")) {
            item_name = "banana";
        }

        if (CompareTag("Watermelon_Item") && !string.Equals(item_name,"watermelon")){
            item_name = "watermelon";
        }

        if (CompareTag("Strawberry_Item") && !string.Equals(item_name,"strawberry")){
            item_name = "strawberry";
        }

        if (CompareTag("Ghost_Item") && !string.Equals(item_name,"ghost")){
            item_name = "ghost";
        }
    }

    public string ItemName () => item_name;
    //Drag and drop
    public void OnBeginDrag(PointerEventData eventData){
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
