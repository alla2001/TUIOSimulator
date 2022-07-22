using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Pointers;
using TouchScript.InputSources;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementController : MonoBehaviour
{
    public int objectMovementId = 1;
    public bool useNavMesh = true;
    public static MovementController instance;
    public Camera cam;
    private NavMeshAgent NavMeshAgent;
    public Section ownerSection;
    public GameObject CameraHolder;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeFov(float amount)
    {
        GetComponentInChildren<Camera>().fieldOfView = Mathf.Clamp(GetComponentInChildren<Camera>().fieldOfView + amount, 40, 95);
    }

    public void RotateY(float amount)
    {
        if (CameraHolder.transform.eulerAngles.x + amount > 320 || CameraHolder.transform.eulerAngles.x + amount < 40)
        {
            CameraHolder.transform.eulerAngles = new Vector3(CameraHolder.transform.eulerAngles.x + amount,
          CameraHolder.transform.eulerAngles.y, CameraHolder.transform.eulerAngles.z);
        }
        else
        if (Mathf.Abs(CameraHolder.transform.eulerAngles.x + amount - 320) < Mathf.Abs(CameraHolder.transform.eulerAngles.x + amount - 40))
        {
            CameraHolder.transform.eulerAngles = new Vector3(320,
          CameraHolder.transform.eulerAngles.y, CameraHolder.transform.eulerAngles.z);
        }
        else
        {
            CameraHolder.transform.eulerAngles = new Vector3(40,
             CameraHolder.transform.eulerAngles.y, CameraHolder.transform.eulerAngles.z);
        }
    }

    public void UpdatePoint(Pointer p)
    {
        if (ZoneLimite.instance.OnMap(p.Position) && MapControle.instance.inZone && MapControle.instance.currentObject.state == SurfaceObject.ObjcetState.movement)
        {
            ZoneLimite.instance.RotatePlayerIcon(((ObjectPointer)p).Angle * Mathf.Rad2Deg);
            transform.eulerAngles = new Vector3(0, ((ObjectPointer)p).Angle * Mathf.Rad2Deg, 0);
            if (useNavMesh)
                NavMeshAgent.SetDestination(ZoneLimite.instance.GetPointInZone(cam.ScreenToViewportPoint(p.Position)));
            else
                transform.position = ZoneLimite.instance.GetPointInZone(cam.ScreenToViewportPoint(p.Position));
        }
    }

    public void MoveTo(Vector2 mapPos, float angle)
    {
        if (ZoneLimite.instance.OnMap(mapPos))
        {
            ZoneLimite.instance.RotatePlayerIcon(angle * Mathf.Rad2Deg);
            transform.eulerAngles = new Vector3(0, angle * Mathf.Rad2Deg, 0);
            if (useNavMesh)
                NavMeshAgent.SetDestination(ZoneLimite.instance.GetPointInZone(mapPos));
            else
                transform.position = ZoneLimite.instance.GetPointInZone(mapPos);
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