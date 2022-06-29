using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Pointers;
using TouchScript.InputSources;

public class MovementController : MonoBehaviour
{
    public int id = 6;
    public static MovementController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpadteRotation(Pointer p)
    {
        transform.eulerAngles = new Vector3(0, ((ObjectPointer)p).Angle, 0);
        transform.position = p.Position;
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