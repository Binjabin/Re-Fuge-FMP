using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
public enum ItemType
{
    Energy,
    Food,
    Water,
    Any
}
public class Item : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Vector2 localPoint;

    private Canvas canvas;
    private RectTransform rect;
    private RectTransform parentRect;
    private CanvasGroup canvasGroup;
    private Vector3 startPos;
    public GameObject prefab;
    public ItemType itemType;
    Inventory inv;
    public InventorySlot currentSlot;
    public float value = 10f;
    public InventorySlot startSlot = null;


    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdatePosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        InventorySlot slot = null;
        foreach (var result in results)
        {
            slot = result.gameObject.GetComponent<InventorySlot>();
            if (slot != null)
            {
                break;

            }
        }

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (slot != null)
        {
            if (slot.Accepts(this))
            {
                currentSlot = slot;
                if(slot.tradeInput)
                {
                    slot.trade.CheckTrade();
                }
                if (slot.Absorbs(this))
                {
                    StartCoroutine(Absorb());
                }
                return;
            }
        }


        transform.position = startPos;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        startPos = transform.position;
        if (currentSlot != null)
        {
            currentSlot.holdingObject = null;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        parentRect = transform.parent.GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        inv = FindObjectOfType<Inventory>();
        if (startSlot == null)
        {
            InventorySlot[] standardSlotsArray = canvas.GetComponentsInChildren<InventorySlot>();
            List<InventorySlot> slots = standardSlotsArray.ToList();
            List<InventorySlot> standardSlots = new List<InventorySlot>();
            foreach (InventorySlot slot in slots)
            {
                if (slot.slotType == ItemType.Any)
                {
                    standardSlots.Add(slot);
                }
            }
            foreach (InventorySlot slot in standardSlots)
            {
                if (slot.holdingObject == null)
                {
                    GetComponent<RectTransform>().position = slot.GetComponent<RectTransform>().position;
                    currentSlot = slot;
                    slot.holdingObject = this;
                    return;
                }
            }
        }
        else
        {
            
            GetComponent<RectTransform>().position = startSlot.gameObject.GetComponent<RectTransform>().position;
            currentSlot = startSlot;
            startSlot.holdingObject = this;
        }

    }




    void UpdatePosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint;
    }

    public IEnumerator Absorb()
    {
        float elapsedTime = 0f;
        float waitTime = 1f;
        while (elapsedTime < waitTime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        currentSlot.holdingObject = null;
        inv.TopUp(currentSlot.slotType, value);
        inv.currentItems.Remove(gameObject);
        Destroy(gameObject);

    }
    public void Trade()
    {
        StartCoroutine(AbsorbForTrade());
    }
    public IEnumerator AbsorbForTrade()
    {
        float elapsedTime = 0f;
        float waitTime = 1f;
        while (elapsedTime < waitTime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        currentSlot.holdingObject = null;
        inv.currentItems.Remove(gameObject);
        Destroy(gameObject);

    }
}
