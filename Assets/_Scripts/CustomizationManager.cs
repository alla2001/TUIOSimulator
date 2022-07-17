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
    public Sprite hilighImage; public Sprite unhilighImage;
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

    private void UpdateX()
    {
        int i = 0;
        foreach (CustomizableObjects ob in Objects)
        {
            float xpos =
               0;

            xpos = ob.refrenceUIButton.GetComponent<RectTransform>().rect.width / 2 - 20 * (i / 2) + 40;

            xpos = ob.refrenceUIButton.GetComponent<RectTransform>().rect.width / 2 - 20 * ((i / 2) + 1) + 40;
            xpos = ob.refrenceUIButton.GetComponent<RectTransform>().rect.width / 2;
            ob.refrenceUIButton.GetComponent<RectTransform>().localPosition = new Vector3(xpos, ob.refrenceUIButton.GetComponent<RectTransform>().localPosition.y, 0);

            i++;
        }
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

            float ypos =
               0;

            ypos = -Holder.GetComponent<RectTransform>().rect.height / 2 +
              temp.GetComponent<RectTransform>().rect.height * i;

            ob.refrenceUIButton = temp;

            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, ypos, 0);

            temp.GetComponent<CustomButton>().obj = ob;

            temp.GetComponentInChildren<TextMeshProUGUI>().text = ob.name;
            if (currentlySelected == null)
            {
                currentlySelected = ob;
                currentlySelected.LoadMaterials();
                currentlySelected.Hilight(hilighImage);
            }
            else if (currentlySelected == ob)
            {
                currentlySelected.LoadMaterials();
                currentlySelected.Hilight(hilighImage);
            }

            i++;
        }
        UpdateX();
    }

    public void changeSelected(bool Up)
    {
        int index = Objects.FindIndex(a => a == currentlySelected);
        if (Up)
        {
            if (index + 1 < Objects.Count)
            {
                currentlySelected.UnHilight(unhilighImage);
                currentlySelected = Objects[index + 1];
                currentlySelected.LoadMaterials();
                currentlySelected.Hilight(hilighImage);
            }
        }
        else
        {
            if (index - 1 >= 0)
            {
                currentlySelected.UnHilight(unhilighImage);
                currentlySelected = Objects[index - 1];
                currentlySelected.LoadMaterials();
                currentlySelected.Hilight(hilighImage);
            }
        }

        //if (Up)
        //{
        //    if (index % 2 == 0)
        //    {
        //        if (index + 2 < Objects.Count)
        //        {
        //            currentlySelected.UnHilight(unhilighImage);
        //            currentlySelected = Objects[index + 2];
        //            currentlySelected.LoadMaterials();
        //            currentlySelected.Hilight(hilighImage);
        //        }
        //    }
        //    else if (index + 1 < Objects.Count)
        //    {
        //        currentlySelected.UnHilight(unhilighImage);
        //        currentlySelected = Objects[index + 1];
        //        currentlySelected.LoadMaterials();
        //        currentlySelected.Hilight(hilighImage);
        //    }
        //}
        //else
        //{
        //    if (index % 2 == 0)
        //    {
        //        if (index - 1 >= 0)
        //        {
        //            currentlySelected.UnHilight(unhilighImage);
        //            currentlySelected = Objects[index - 1];
        //            currentlySelected.LoadMaterials();
        //            currentlySelected.Hilight(hilighImage);
        //        }
        //    }
        //    else if (index - 2 >= 0)
        //    {
        //        currentlySelected.UnHilight(unhilighImage);
        //        currentlySelected = Objects[index - 1];
        //        currentlySelected.LoadMaterials();
        //        currentlySelected.Hilight(hilighImage);
        //    }
        //}
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
                if (currentlySelected != null && currentlySelected == cut)
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