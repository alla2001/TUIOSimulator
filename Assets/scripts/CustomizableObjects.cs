using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnDestroy()
    {
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