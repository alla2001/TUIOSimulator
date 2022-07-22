using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomButton : MonoBehaviour
{
    public CustomizableObjects obj;
    private Vector3 targetpos;
    private bool end;

    private void OnEnable()
    {
        end = false;
    }

    public void EndLife()
    {
        end = true;
        targetpos = GetComponent<RectTransform>().localPosition - new Vector3(400, 0, 0);
    }

    private void Update()
    {
        if (end)
        {
            GetComponent<RectTransform>().localPosition = Vector3.Lerp(GetComponent<RectTransform>().localPosition, targetpos, 3f * Time.deltaTime);
            if (Vector3.Distance(GetComponent<RectTransform>().localPosition, targetpos) < 1f)
            {
                Destroy(gameObject);
            }
        }
    }
}