using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CustomizableObjects : MonoBehaviour
{
    public StyleSo style;
    public GameObject refrenceUIButton;

    private void Start()
    {
    }

    public void LoadMaterials()
    {
        MaterialDisplay.instance.LoadMaterials(style, this);
    }

    public void Hilight(Sprite hilighImage)
    {
        print("Hilight");
        refrenceUIButton.GetComponent<Image>().sprite = hilighImage;
    }

    public void UnHilight(Sprite unhilighImage)
    {
        print("UnHilight");
        refrenceUIButton.GetComponent<Image>().sprite = unhilighImage;
    }

    private void FixedUpdate()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(MovementController.instance.GetComponent<Camera>());

        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds))
        {
            if (!CustomizationManager.Instance.Contrains(this))
                CustomizationManager.Instance.AddObject(this);
        }
        else
        {
            if (MaterialDisplay.instance.currentSelected == this)
            {
                MaterialDisplay.instance.ClearMaterials();
            }
            if (CustomizationManager.Instance.Contrains(this))
                CustomizationManager.Instance.RemoveObject(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}