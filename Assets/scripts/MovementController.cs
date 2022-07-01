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

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdatePoint(Pointer p)
    {
        if (/*p.Id == objectMovementId &&*/ ZoneLimite.instance.OnMap(p.Position))
        {
            ZoneLimite.instance.RotatePlayerIcon(((ObjectPointer)p).Angle * Mathf.Rad2Deg);
            transform.eulerAngles = new Vector3(0, ((ObjectPointer)p).Angle * Mathf.Rad2Deg, 0);
            if (useNavMesh)
                NavMeshAgent.SetDestination(ZoneLimite.instance.GetPointInZone(cam.ScreenToViewportPoint(p.Position)));
            else
                transform.position = ZoneLimite.instance.GetPointInZone(cam.ScreenToViewportPoint(p.Position));
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