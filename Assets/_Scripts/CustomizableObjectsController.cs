using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizableObjectsController : ControleArea

{
    public CustomizationManager customizationManager;
    private float tempValue;

    public override void ChangeValue(float value)
    {
        tempValue += value;

        if (Mathf.Abs(tempValue) > 20)
        {
            customizationManager.changeSelected((tempValue < 0) ? true : false);
            tempValue = 0;
        }

        base.ChangeValue(value);
    }
}