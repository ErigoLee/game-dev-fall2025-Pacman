using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    private Item item;

    private void Start(){
        item = null;
    }
    
    public void OnDrop(PointerEventData eventData){
        
        if(transform.childCount == 0){
            Item _item = eventData.pointerDrag.GetComponent<Item>();
            _item.parentAfterDrag = transform;
        }
        
    }
   

    public bool IsEmpty(){
        if(item == null){
            return true;
        }
        else{
            return true;
        }
    }

    public bool AddItem(GameObject itemObj){
        MonoBehaviour[] allComponents = itemObj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour comp in allComponents)
        {
            if (comp is Item _item)
            {
                item = _item;
                break;  // Use the first match found
            }
        }
        
        if(item != null){
            itemObj.transform.SetParent(this.transform,false);
            Debug.Log("Name" + item.ItemName);
            return true;
        }
        else{
            Destroy(itemObj);
            return false;
        }
       
    }

}
