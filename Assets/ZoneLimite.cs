using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneLimite : MonoBehaviour
{
    public static ZoneLimite instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public Vector3 GetPointInZone(Vector2 screenPos, Vector2 screenSize)
    {
        Vector2 normalizedValues = new Vector2(screenPos.x / screenSize.x, screenPos.y / screenSize.y);

        Vector3 post = new Vector3(GetComponent<BoxCollider>().bounds.size.x * normalizedValues.x,
           MovementController.instance.transform.position.y,
            GetComponent<BoxCollider>().bounds.size.z * normalizedValues.y);

        return post;
    }
}