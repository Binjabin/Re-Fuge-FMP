using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Trade : MonoBehaviour
{
    List<InventorySlot> slots;
    List<InventorySlot> inSlot = new List<InventorySlot>();
    List<InventorySlot> outSlot = new List<InventorySlot>();

    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<InventorySlot>().ToList();
        foreach (InventorySlot slot in slots)
        {
            if(slot.tradeInput)
            {
                inSlot.Add(slot);
            }
            else if(slot.tradeOutput)
            {
                outSlot.Add(slot);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckTrade()
    {
        foreach (InventorySlot slot in inSlot)
        {
            if (slot.holdingObject == null)
            {
                return;
            }
        }
        //Trade Complete
        foreach (InventorySlot slot in inSlot)
        {
            slot.holdingObject.GetComponent<Item>().Trade();
        }

        foreach (InventorySlot slot in outSlot)
        {
            slot.OutputItem();
        }
    }
}
