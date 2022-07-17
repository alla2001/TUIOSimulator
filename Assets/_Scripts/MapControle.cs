using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControle : MonoBehaviour
{
    private bool inZone = false;
    private BoxCollider boxCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            inZone = false;
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