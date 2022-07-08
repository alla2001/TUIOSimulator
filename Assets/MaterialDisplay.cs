using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialDisplay : MonoBehaviour
{
    public GameObject matUI;
    public GameObject matHolder;
    public List<GameObject> materials;
    public static MaterialDisplay instance;
    public CustomizableObjects currentSelected;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void LoadMaterials(StyleSo style, CustomizableObjects selected)
    {
        ClearMaterials();
        int i = 0;
        currentSelected = selected;
        foreach (Material mat in style.materials)
        {
            GameObject temp = Instantiate(matUI, matHolder.transform);
            temp.GetComponent<RectTransform>().localPosition = Vector3.zero;
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0,
                 matHolder.GetComponent<RectTransform>().rect.height / 2 - (temp.GetComponent<RectTransform>().rect.height / 2 +
                temp.GetComponent<RectTransform>().rect.height * i), 0);
            materials.Add(temp);
            temp.GetComponent<customizableMaterial>().material = mat;
            temp.GetComponentInChildren<TextMeshProUGUI>().text = mat.name;
            i++;
        }
    }

    public void SetSelectedMaterial(Material mat)
    {
        currentSelected.GetComponent<MeshRenderer>().material = mat;
    }

    public void ClearMaterials()
    {
        foreach (GameObject gm in materials) Destroy(gm);
        materials.Clear();
    }
}