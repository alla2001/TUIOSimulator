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
    public customizableMaterial currentSelectedMaterial;
    public float margin;
    public float Ypos;
    public ScrollRect slider;

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
            float posx = (temp.GetComponent<RectTransform>().rect.width / 2 +
                (margin * (i + 1)) +
                temp.GetComponent<RectTransform>().rect.width * i);
            float posy = -matHolder.GetComponent<RectTransform>().rect.height / 2;
            temp.GetComponent<RectTransform>().localPosition = new Vector3(-temp.GetComponent<RectTransform>().rect.width - 10, posy + Ypos
              , 0);
            temp.GetComponentInChildren<customizableMaterial>().targetPos = new Vector3(posx, posy + Ypos
              , 0);
            materials.Add(temp);

            temp.GetComponentInChildren<customizableMaterial>().SetMaterialInfo(mat.Image, mat.name, mat);
            if (mat == currentSelected.selectedMaterial)
            {
                currentSelectedMaterial = temp.GetComponentInChildren<customizableMaterial>();
                currentSelectedMaterial.Hilight();
            }
            i++;
        }
        slider.horizontalNormalizedPosition = -10;
    }

    public void SetSelectedMaterial(MaterialSO mat, customizableMaterial matUi)
    {
        if (currentSelectedMaterial != null)
        {
            currentSelectedMaterial.UnHilight();
            currentSelected.selectedMaterial = mat;
        }

        currentSelectedMaterial = matUi;
        currentSelectedMaterial.Hilight();
        currentSelected.GetComponent<MeshRenderer>().material = mat.material;
    }

    public void ClearMaterials()
    {
        foreach (GameObject gm in materials) Destroy(gm);
        materials.Clear();
    }
}