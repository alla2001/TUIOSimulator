using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SphereCollider))]
public class ControleArea : MonoBehaviour
{
    private SphereCollider sphereCollider;
    private Vector3 prevDir;
    private bool inZone = false;
    public TextMeshProUGUI text;
    public SurfaceObject currentObject;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = (GetComponent<RectTransform>().rect.height / 2) * 0.75f;
        sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            currentObject = other.GetComponent<SurfaceObject>();
            inZone = true;
            prevDir = other.transform.up;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            StopAllCoroutines();
            currentObject.DisableDot();
            currentObject = null;
            inZone = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Controller") && inZone)
        {
            float value = Vector3.Angle(other.transform.GetChild(1).transform.up, prevDir);

            ChangeValue(value * AngleDir(prevDir, other.transform.GetChild(1).transform.up, Vector3.forward));
            prevDir = other.transform.GetChild(1).transform.up;
        }
    }

    private float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0)
        {
            return 1;
        }
        else if (dir < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public void SetTextValue(float value)
    {
        if (text != null) text.text = value.ToString();
    }

    public virtual void ChangeValue(float value)
    {
        if (text != null) text.text = value.ToString();
    }
}