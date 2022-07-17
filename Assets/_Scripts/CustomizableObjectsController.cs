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
        print(value);
        if (Mathf.Abs(tempValue) > 50)
        {
            print("value Changed");
            customizationManager.changeSelected((tempValue > 0) ? true : false);
            tempValue = 0;
        }

        base.ChangeValue(value);
    }
}