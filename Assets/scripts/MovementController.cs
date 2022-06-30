using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Pointers;
using TouchScript.InputSources;

public class MovementController : MonoBehaviour
{
    public int rotationId = 6;
    public int PositionId = 6;
    public static MovementController instance;
    public Camera cam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdatePoint(Pointer p)
    {
        if (p.Id == rotationId)
        {
            transform.eulerAngles = new Vector3(0, -((ObjectPointer)p).Angle * Mathf.Rad2Deg, 0);
        }
        else if (p.Id == PositionId)
        {
            transform.position = ZoneLimite.instance.GetPointInZone(p.Position, new Vector2(Screen.width, Screen.height));
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    public void Rotate()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}