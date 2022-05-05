using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Item holdingObject;
    public ItemType slotType = ItemType.Any;
    
    public void OnDrop(PointerEventData eventData)
    {
        
        if(holdingObject == null)
        {
            if(eventData.pointerDrag.GetComponent<Item>() != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                
            }
        }
    }
    void Start() 
    {
        
    }
    public bool Accepts(Item item)
    {
        if(holdingObject != null && holdingObject != item)
        {
            
            return false;
            
        }
        if(slotType == ItemType.Any)
        {
            holdingObject = item;
            return true;
            
        }
        if(item.itemType == ItemType.Any)
        {
            holdingObject = item;
            return true;
            
        }
        if(item.itemType != slotType)
        {
            return false;
            
        }
        else
        {
            holdingObject = item;
            return true;
            
        }
        return true;         
    }

    public bool Absorbs(Item item)
    {
        if(slotType == ItemType.Any)
        {
            return false;
        }
        else
        {
            if(item.itemType == ItemType.Any)
            {
                return true;
                
            }
            else if(item.itemType == slotType)
            {
                return true;
                
            }
            else
            {
                return false;
            }
        }
    }
    
}
