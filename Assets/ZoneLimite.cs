using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneLimite : MonoBehaviour
{
    public static ZoneLimite instance;
    public Image Player;
    public RawImage Map;
    private Vector3 normlizedPosOnMap;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private Vector2 posOnMap;

    public bool OnMap(Vector2 screenPos)
    {
        BoxCollider2D collider = Map.GetComponent<BoxCollider2D>();
        Vector2 mapPos = Map.rectTransform.localPosition + new Vector3(Screen.width / 2, Screen.height / 2, 0);

        posOnMap = screenPos - mapPos;

        Vector2 posOnCollider = (posOnMap - (collider.offset)) + collider.size / 2;
        print(posOnCollider);
        normlizedPosOnMap = new Vector2(posOnCollider.x / collider.size.x, posOnCollider.y / collider.size.y);

        if (normlizedPosOnMap.x >= 0 &&
            normlizedPosOnMap.x <= 1 &&
           normlizedPosOnMap.y >= 0 &&
           normlizedPosOnMap.y <= 1)
        {
            return true;
        }
        else return false;
    }

    public void RotatePlayerIcon(float z)
    {
        Player.rectTransform.eulerAngles = new Vector3(0, 0, -z);
    }

    public Vector3 GetPointInZone(Vector2 screenPos)
    {
        Player.rectTransform.localPosition = posOnMap;

        Vector3 center = transform.TransformPoint(GetComponent<BoxCollider>().center);
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 pos = new Vector3(
            center.x - collider.bounds.size.x / 2 + collider.bounds.size.x * normlizedPosOnMap.x,
           MovementController.instance.transform.position.y,
          center.z - collider.bounds.size.z / 2 + collider.bounds.size.z * normlizedPosOnMap.y);

        return pos;
    }

    private void Update()
    {
        Vector2 mapPos = Map.rectTransform.localPosition + new Vector3(Screen.width / 2, Screen.height / 2, 0);
        //print(mapPos);
    }
}