using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomizationManager : MonoBehaviour
{
    private static List<CustomizableObjects> Objects = new List<CustomizableObjects>();

    public static CustomizationManager Instance;
    public GameObject button;
    public GameObject Holder;

    public CustomizableObjects currentlySelected;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void AddObject(CustomizableObjects custmizable)
    {
        Objects.Add(custmizable);

        UpdateUI();
    }

    public void UpdateUI()
    {
        int i = 0;
        foreach (CustomizableObjects cut in Objects)
        {
            Destroy(cut.refrenceUIButton);
        }
        foreach (CustomizableObjects ob in Objects)
        {
            GameObject temp = Instantiate(button, Holder.transform);

            temp.GetComponent<RectTransform>().localPosition = Vector3.zero;
            if (i == 0)
            {
                if (currentlySelected == null)
                {
                    currentlySelected = ob;
                }
                else
                {
                    i++;
                }
            }

            float ypos =
               0;
            float xpos =
               0;
            if (i % 2 == 0)
            {
                ypos =
               temp.GetComponent<RectTransform>().rect.height * (i / 2);
                xpos = 10 * (i / 2);
            }
            else
            {
                ypos =
                temp.GetComponent<RectTransform>().rect.height * -((i / 2) + 1);
                xpos = 10 * ((i / 2) + 1);
            }
            if (currentlySelected == ob)
            {
                ypos =
              0;
            }
            temp.GetComponent<RectTransform>().localPosition = new Vector3(xpos, ypos, 0);

            temp.GetComponent<CustomButton>().obj = ob;
            temp.GetComponent<Button>().onClick.AddListener(ob.LoadMaterials);
            temp.GetComponentInChildren<TextMeshProUGUI>().text = ob.name;
            ob.refrenceUIButton = temp;

            i++;
        }
    }

    public bool Contrains(CustomizableObjects custmizable)
    {
        foreach (CustomizableObjects cut in Objects)
        {
            if (cut == custmizable)
                return true;
        }
        return false;
    }

    public void RemoveObject(CustomizableObjects custmizable)
    {
        foreach (CustomizableObjects cut in Objects)
        {
            if (cut == custmizable)
            {
                if (currentlySelected != null && currentlySelected.refrenceUIButton == cut)
                {
                    currentlySelected = null;
                }
                Destroy(cut.refrenceUIButton);
                Objects.Remove(cut);
                break;
            }
        }
        UpdateUI();
    }
}