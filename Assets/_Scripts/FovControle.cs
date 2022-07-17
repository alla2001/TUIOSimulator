using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovControle : ControleArea

{
    public Camera mainCamera;

    public override void ChangeValue(float value)
    {
        mainCamera.fieldOfView += value * 0.1f;
        value = value * 0.1f;
        base.SetTextValue(mainCamera.fieldOfView);
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