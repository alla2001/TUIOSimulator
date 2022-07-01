using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneLimite : MonoBehaviour
{
    public static ZoneLimite instance;
    public Image Player;
    public RawImage Map;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public bool OnMap(Vector2 screenPos)
    {
        Vector3 pos = new Vector3(screenPos.x, screenPos.y, 0) -
            (new Vector3(
                Map.GetComponent<BoxCollider2D>().offset.x,
                Map.GetComponent<BoxCollider2D>().offset.y, 0) +
                Map.transform.position + new Vector3(Screen.width / 2, Screen.height / 4));
        print(screenPos);
        if (pos.x >= -Map.GetComponent<BoxCollider2D>().size.x / 2 &&
            pos.x <= Map.GetComponent<BoxCollider2D>().size.x / 2 &&
            pos.y >= -Map.GetComponent<BoxCollider2D>().size.y / 2
            && pos.y <= Map.GetComponent<BoxCollider2D>().size.y / 2)
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
        Player.rectTransform.localPosition = new Vector3(
            Map.GetComponent<BoxCollider2D>().size.x * (screenPos.x - 0.5f),
            Map.GetComponent<BoxCollider2D>().size.y * (screenPos.y - 0.5f), 0)
            +
            new Vector3(
                Map.GetComponent<BoxCollider2D>().offset.x,
                Map.GetComponent<BoxCollider2D>().offset.y, 0);

        Vector3 post = new Vector3(
            Map.GetComponent<BoxCollider2D>().size.x * (screenPos.x - 0.5f),
            Map.GetComponent<BoxCollider2D>().size.y * (screenPos.y - 0.5f), 0)
            +
            new Vector3(
                Map.GetComponent<BoxCollider2D>().offset.x,
                Map.GetComponent<BoxCollider2D>().offset.y, 0);

        screenPos = new Vector2(post.x / Map.GetComponent<BoxCollider2D>().size.x, post.y / Map.GetComponent<BoxCollider2D>().size.y);

        Vector3 center = transform.TransformPoint(GetComponent<BoxCollider>().center);
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 pos = new Vector3(
            center.x - collider.bounds.size.x / 2 + collider.bounds.size.x * screenPos.x,
           MovementController.instance.transform.position.y,
          center.z - collider.bounds.size.z / 2 + collider.bounds.size.z * screenPos.y);

        return pos;
    }
}