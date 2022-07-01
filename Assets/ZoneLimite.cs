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

    public bool OnMap(Vector2 screenPos)
    {
        Vector2 mapPos = Map.rectTransform.position + new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 posOnMap = screenPos - mapPos;

        normlizedPosOnMap = new Vector2(posOnMap.x / Map.rectTransform.rect.width, posOnMap.y / Map.rectTransform.rect.height);

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
        Vector2 temp = new Vector2(Mathf.Clamp(normlizedPosOnMap.x, 0, 1), Mathf.Clamp(normlizedPosOnMap.y, 0, 1));
        Player.rectTransform.localPosition = new Vector3(
            temp.x * Map.rectTransform.rect.width,
            temp.y * Map.rectTransform.rect.height, 0);

        Vector3 center = transform.TransformPoint(GetComponent<BoxCollider>().center);
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 pos = new Vector3(
            center.x - collider.bounds.size.x / 2 + collider.bounds.size.x * temp.x,
           MovementController.instance.transform.position.y,
          center.z - collider.bounds.size.z / 2 + collider.bounds.size.z * temp.y);

        return pos;
    }
}