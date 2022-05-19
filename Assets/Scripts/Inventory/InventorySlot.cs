using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Item holdingObject;
    public ItemType slotType = ItemType.Any;
    public bool tradeInput = false;
    public bool tradeOutput = false;
    public Trade trade;
    Inventory inv;

    public List<GameObject> outputItems;

    public void OnDrop(PointerEventData eventData)
    {

        if (holdingObject == null)
        {
            if (eventData.pointerDrag.GetComponent<Item>() != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;

            }
        }
    }
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        if (tradeInput)
        {
            trade = GetComponentInParent<Trade>();
        }
    }
    public bool Accepts(Item item)
    {
        if (tradeOutput)
        {
            return false;
        }
        if (holdingObject != null && holdingObject != item)
        {

            return false;

        }
        if (slotType == ItemType.Any)
        {
            holdingObject = item;
            return true;

        }
        if (item.itemType == ItemType.Any)
        {
            holdingObject = item;
            return true;

        }
        if (item.itemType != slotType)
        {
            return false;

        }
        else
        {
            holdingObject = item;
            return true;
        }
    }

    public bool Absorbs(Item item)
    {
        if (tradeInput)
        {
            return false;
        }
        if (slotType == ItemType.Any)
        {
            return false;
        }
        else
        {
            if (item.itemType == ItemType.Any)
            {
                return true;

            }
            else if (item.itemType == slotType)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
    }

    public void OutputItem()
    {
        inv.TradeOutput(slotType, this);
    }

}
