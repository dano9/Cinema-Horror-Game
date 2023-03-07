using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractabilityUI : MonoBehaviour
{
    public float spacing = 60;
    public int itemsPerRow = 3;
    public RectTransform itemSlotHolder;
    public List<Image> itemSlots;
    public GameObject itemSlotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateItemSlots(Item[] allItems)
    {
        List<Item> items = new List<Item>();
        foreach (Item item in allItems)
        {
            if (item.equipped) {items.Add(item);}
        }
        bool reInstantiate = false;
        if (items.Count != itemSlots.Count)
        {
            foreach (Image img in itemSlots)
            {
                Destroy(img.gameObject);
            }
            itemSlots.Clear();
            reInstantiate = true;
        }
        int rows = (int)Mathf.Ceil((float)items.Count / itemsPerRow);
        itemSlotHolder.sizeDelta = new Vector2(Mathf.Clamp(items.Count, 1, itemsPerRow), rows) * (spacing + 5);
        itemSlotHolder.localPosition = new Vector2(0, (float)(rows - 1) * 0.5f) * (spacing + 5);
        for (int i = 0; i < items.Count; i++)
        {
            if (reInstantiate)
            {
                itemSlots.Add(Instantiate(itemSlotPrefab,itemSlotHolder).GetComponent<Image>());
                itemSlots[i].sprite = items[i].icon;
                int index = items[i].index;
                int id = i;
                itemSlots[i].GetComponent<Interactable>().interactActions[0].AddListener(delegate { itemSlots[id].GetComponent<Interactable>().SetPlayerGrabbed(index);});
            }
            int columns = Mathf.Clamp(items.Count, 1, itemsPerRow);
            itemSlots[i].transform.localPosition = new Vector2(((i % itemsPerRow) - ((columns - 1f) * 0.5f)) * spacing, (Mathf.Floor(i / itemsPerRow) - ((rows - 1) * 0.5f)) * spacing);
        }
    }
}
