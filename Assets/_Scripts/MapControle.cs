using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControle : MonoBehaviour
{
    public bool inZone = false;
    private BoxCollider boxCollider;
    public static MapControle instance;
    public SurfaceObject currentObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            inZone = true;
            currentObject = other.gameObject.GetComponent<SurfaceObject>();
            other.gameObject.GetComponent<SurfaceObject>().InMap();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            inZone = false;
            currentObject = null;
            other.gameObject.GetComponent<SurfaceObject>().OutOffMap();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Controller") && inZone)
        {
            if (other.GetComponent<SurfaceObject>() != null)
            {
                //Vector2 pos = new Vector2(other.GetComponent<SurfaceObject>().tuioObject.X, other.GetComponent<SurfaceObject>().tuioObject.Y);
                //MovementController.instance.MoveTo(pos, other.GetComponent<SurfaceObject>().tuioObject.Angle);
            }
        }
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