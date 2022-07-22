using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneLimite : MonoBehaviour
{
    public static ZoneLimite instance;

    public RawImage Map;
    private Vector3 normlizedPosOnMap;
    [SerializeField] private BoxCollider2D boxCollider;
    public Camera topDownCamera;
    public List<Location> locations = new List<Location>();

    [System.Serializable]
    public class Location
    {
        public string name;
        public Transform pos;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void LoadLocation(string name)
    {
        foreach (Location loc in locations)
        {
            if (loc.name == name)
            {
                MovementController.instance.transform.position = loc.pos.position;
                break;
            }
        }
    }

    private void Start()
    {
    }

    private Vector2 posOnMap;

    public bool OnMap(Vector2 screenPos)
    {
        BoxCollider2D collider = boxCollider;
        Vector2 mapPos = Map.rectTransform.localPosition + new Vector3(Screen.width / 2, Screen.height / 2, 0);

        posOnMap = screenPos - mapPos;
        //print(posOnMap);
        normlizedPosOnMap = new Vector2((posOnMap.x / Map.rectTransform.rect.width), (posOnMap.y / Map.rectTransform.rect.height)) + new Vector2(0.5f, 0.5f);

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
    }

    private Ray r;

    public Vector3 GetPointInZone(Vector2 screenPos)
    {
        //print(normlizedPosOnMap * new Vector2(topDownCamera.pixelHeight, topDownCamera.scaledPixelWidth));
        r = topDownCamera.ScreenPointToRay(normlizedPosOnMap * new Vector2(topDownCamera.pixelWidth, topDownCamera.pixelHeight));
        RaycastHit hit;
        if (Physics.Raycast(r, out hit))
        {
            return hit.point;
        }
        Vector3 center = transform.TransformPoint(GetComponent<BoxCollider>().center);
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 pos = new Vector3(
            center.x - collider.bounds.size.x / 2 + collider.bounds.size.x * normlizedPosOnMap.x,
           MovementController.instance.transform.position.y,
          center.z - collider.bounds.size.z / 2 + collider.bounds.size.z * normlizedPosOnMap.y);

        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(r.origin, r.direction * 100f);
    }

    private void Update()
    {
        Vector2 mapPos = Map.rectTransform.localPosition + new Vector3(Screen.width / 2, Screen.height / 2, 0);
        //print(mapPos);
    }
}