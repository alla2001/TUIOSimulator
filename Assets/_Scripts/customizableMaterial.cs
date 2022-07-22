using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class customizableMaterial : MonoBehaviour
{
    public MaterialSO material;
    public RawImage imageRender;
    public TextMeshProUGUI Name;
    public Image hilight;
    public Vector3 targetPos;

    public void SetImage(Texture image)
    {
        imageRender.texture = image;
    }

    public void SetText(string text)
    {
        Name.text = text;
    }

    public void SetMaterialInfo(Texture image, string text, MaterialSO material)
    {
        SetImage(image);
        SetText(text);
        this.material = material;
    }

    public void SetMaterial()
    {
        MaterialDisplay.instance.SetSelectedMaterial(material, this);
    }

    public void Hilight()
    {
        hilight.gameObject.SetActive(true);
        Name.color = Color.white;
    }

    public void UnHilight()
    {
        hilight.gameObject.SetActive(false);
        Name.color = Color.black;
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.parent.GetComponent<RectTransform>().localPosition = Vector3.Lerp(transform.parent.GetComponent<RectTransform>().localPosition, targetPos, 3f * Time.deltaTime);
    }
}