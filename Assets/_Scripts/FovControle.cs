using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovControle : ControleArea

{
    public Camera mainCamera;

    public override void ChangeValue(float value)
    {
        mainCamera.fieldOfView += value * 1f;
        value = value * 0.1f;
        base.SetTextValue(mainCamera.fieldOfView);
    }
}