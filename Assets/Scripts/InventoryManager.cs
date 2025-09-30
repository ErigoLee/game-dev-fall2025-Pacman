using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private GameObject appleItem;
    [SerializeField] private GameObject bananaItem;
    [SerializeField] private GameObject oranageItem;
    [SerializeField] private GameObject strawberryItem;
    [SerializeField] private GameObject waterMelonItem;
    [SerializeField] private GameObject ghostItem;
    [SerializeField] private MainInventory mainInventory;


    

    private void OnEnable(){
        GameManager.AttackEventStart += AddGhosts;
        GameManager.collectItems += AddItems;
    }

    private void OnDisable(){
        GameManager.AttackEventStart -= AddGhosts;
        GameManager.collectItems -= AddItems;
    }

    private void AddItems(object c, string item_name){
        Debug.Log("InventoryManager: "+item_name);
        if(string.Equals(item_name, "apple", System.StringComparison.OrdinalIgnoreCase))
        {
            if(appleItem != null){ 
                mainInventory.AddItem(Instantiate(appleItem,Vector3.zero, Quaternion.identity));
            }
            
        }

        if(string.Equals(item_name,"banana", System.StringComparison.OrdinalIgnoreCase))
        {
            if(bananaItem != null){
                Debug.Log("bananaItem");
                mainInventory.AddItem(Instantiate(bananaItem,Vector3.zero, Quaternion.identity));
            }
           
        }

        if(string.Equals(item_name, "orange", System.StringComparison.OrdinalIgnoreCase))
        {
            if(oranageItem != null){
                mainInventory.AddItem(Instantiate(oranageItem,Vector3.zero, Quaternion.identity));
            }
        }

        if(string.Equals(item_name,"strawberry", System.StringComparison.OrdinalIgnoreCase))
        {
            if(strawberryItem != null){
                mainInventory.AddItem(Instantiate(strawberryItem,Vector3.zero, Quaternion.identity));
            }
        }

        if(string.Equals(item_name, "watermelon",System.StringComparison.OrdinalIgnoreCase))
        {
            if(waterMelonItem != null){
                mainInventory.AddItem(Instantiate(waterMelonItem,Vector3.zero, Quaternion.identity));
            }
        }
    }

    private void AddGhosts(object c){
        if(ghostItem != null){
            Debug.Log("GhostItem");
            mainInventory.AddItem(Instantiate(ghostItem,Vector3.zero, Quaternion.identity));
        }
    }

}
