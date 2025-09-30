using System.Linq;
using UnityEngine;

public class MainInventory : MonoBehaviour
{
    [SerializeField] private InventorySlot [] slots;

    private int insertionOrder = 0;

    void OnValidate()
    {
        slots = GetComponentsInChildren<InventorySlot>(true)
                .OrderBy(s => s.transform.GetSiblingIndex())
                .ToArray();
    }

    public void AddItem(GameObject item){
        for (int i=insertionOrder; i<slots.Length; i++){
            if(slots[i].IsEmpty() && slots[i].AddItem(item)==true){
               insertionOrder++;
               break;
            }
        }
    }
}
