using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class customizableMaterial : MonoBehaviour
{
    public Material material;
    public RawImage imageRender;
    public TextMeshProUGUI Name;

    public void SetImage(Texture image)
    {
        imageRender.texture = image;
    }

    public void SetText(string text)
    {
        Name.text = text;
    }

    public void SetMaterialInfo(Texture image, string text, Material material)
    {
        SetImage(image);
        SetText(text);
        this.material = material;
    }

    public void SetMaterial()
    {
        MaterialDisplay.instance.SetSelectedMaterial(material);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}