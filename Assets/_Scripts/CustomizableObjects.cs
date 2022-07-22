using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using EPOOutline;

public class CustomizableObjects : MonoBehaviour
{
    public StyleSo style;
    [HideInInspector] public CustomButton refrenceUIButton;
    private Outlinable outline;
    public Section ownerSection;
    public MaterialSO selectedMaterial;
    // public Material

    private void Start()
    {
        outline = gameObject.AddComponent<Outlinable>();
        outline.AddAllChildRenderersToRenderingList();
        outline.OutlineParameters.Color = new Color(0, (float)116 / 255, (float)69 / 255, (float)200 / 255);
        outline.OutlineParameters.BlurShift = 1f;
        outline.OutlineParameters.Enabled = false;
        MeshRenderer mr = GetComponent<MeshRenderer>();
        foreach (MaterialSO mat in style.materials)
        {
            if (mat.material == mr.material)
            {
                selectedMaterial = mat;
                break;
            }
        }
    }

    public void LoadMaterials()
    {
        MaterialDisplay.instance.LoadMaterials(style, this);
    }

    public void Hilight(Sprite hilighImage)
    {
        refrenceUIButton.GetComponent<Image>().sprite = hilighImage;
        outline.OutlineParameters.Enabled = true;
    }

    public void UnHilight(Sprite unhilighImage)
    {
        refrenceUIButton.GetComponent<Image>().sprite = unhilighImage;
        outline.OutlineParameters.Enabled = false;
    }

    private void FixedUpdate()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(MovementController.instance.GetComponentInChildren<Camera>());

        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds))
        {
            if (ownerSection != null)
            {
                if (!CustomizationManager.Instance.Contains(this) && ownerSection == MovementController.instance.ownerSection)
                {
                    CustomizationManager.Instance.AddObject(this);
                }
            }
            else
            {
                if (!CustomizationManager.Instance.Contains(this))
                {
                    CustomizationManager.Instance.AddObject(this);
                }
            }
        }
        else
        {
            if (MaterialDisplay.instance.currentSelected == this)
            {
                MaterialDisplay.instance.ClearMaterials();
            }
            if (CustomizationManager.Instance.Contains(this))
                CustomizationManager.Instance.RemoveObject(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}