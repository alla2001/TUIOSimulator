using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    public List<CustomizableObjects> objects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CustomizableObjects>() != null)
        {
            objects.Add(other.GetComponent<CustomizableObjects>());
            other.GetComponent<CustomizableObjects>().ownerSection = this;
        }
        if (other.CompareTag("Player"))
        {
            MovementController.instance.ownerSection = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MovementController.instance.ownerSection = null;
        }
    }
}