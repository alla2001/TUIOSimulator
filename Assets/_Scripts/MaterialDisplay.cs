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
    public float margin;
    public float Ypos;

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
        foreach (MaterialSO mat in style.materials)
        {
            GameObject temp = Instantiate(matUI, matHolder.transform);
            temp.GetComponent<RectTransform>().localPosition = Vector3.zero;
            float posx = -matHolder.GetComponent<RectTransform>().rect.width / 2 + (temp.GetComponent<RectTransform>().rect.width / 2 +
                (margin * (i + 1)) +
                temp.GetComponent<RectTransform>().rect.width * i);

            temp.GetComponent<RectTransform>().localPosition = new Vector3(posx, Ypos
              , 0);
            materials.Add(temp);

            temp.GetComponent<customizableMaterial>().SetMaterialInfo(mat.Image, mat.name, mat.material);

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