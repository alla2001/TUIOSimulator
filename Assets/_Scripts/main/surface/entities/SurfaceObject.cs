using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using System.Collections.Generic;
using TUIOSimulator.Entities;
using TouchScript.Gestures;
using UnityEngine.UI;

public class SurfaceObject : MonoBehaviour, ISurfaceEntity
{
    public enum ObjcetState
    {
        movement, fov, lookup
    }

    public Surface surface { get; protected set; }
    public GameObject Dot;
    public GameObject UI;
    public GameObject fovUI;
    public GameObject LookyUi;
    public Image degrees;
    public GameObject OnMapUi;

    public ITUIOEntity tuioEntity
    { get { return tuioObject; } }

    public TUIOObject tuioObject { get; protected set; }

    public int id
    { get { return _id; } }

    [SerializeField]
    [Range(0, 7)]
    private int _id;

    public List<Sprite> sprites;
    public List<Color> colors;
    public float waitHighlight = 3f;
    [HideInInspector] public float prevAngle;

    //public SpriteRenderer spriteRenderer { get; protected set; }
    public Collider2D collider2d { get; protected set; }

    private PressGesture pressGesture;
    public ObjcetState state = ObjcetState.movement;
    private Vector3 prevDir;
    private bool inMap;

    protected void Awake()
    {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
        pressGesture = GetComponent<PressGesture>();
    }

    protected void OnEnable()
    {
        Init(_id);
        pressGesture.Pressed += OnPressed;
    }

    public void ChangeState(int newState)
    {
        state = (ObjcetState)newState;
        switch (state)
        {
            case ObjcetState.movement:
                OnMapUi.SetActive(true);
                fovUI.SetActive(false);
                LookyUi.SetActive(false);
                break;

            case ObjcetState.fov:

                OnMapUi.SetActive(false);
                fovUI.SetActive(true);
                LookyUi.SetActive(false);
                break;

            default:
                OnMapUi.SetActive(false);
                fovUI.SetActive(false);
                LookyUi.SetActive(true);
                break;
        }
    }

    protected void Update()
    {
        if (!rotating)
        {
            degrees.transform.rotation = UI.transform.rotation;
        }
        if (inMap)
        {
            if (state == ObjcetState.fov)
            {
                float value = Vector3.Angle(transform.up, prevDir);

                MovementController.instance.ChangeFov(value * 0.02f * AngleDir(prevDir, UI.transform.up, Vector3.forward));
                prevDir = UI.transform.up;
            }
            if (state == ObjcetState.lookup)
            {
                float value = Vector3.Angle(transform.up, prevDir);

                MovementController.instance.RotateY(value * 0.01f * AngleDir(prevDir, UI.transform.up, Vector3.forward));
                prevDir = UI.transform.up;
            }
        }

        if (surface != null)
            UpdateObject();
    }

    private void LateUpdate()
    {
        print(" late update :" + GetComponent<RectTransform>().localPosition);
    }

    private float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0)
        {
            return 1;
        }
        else if (dir < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    protected void OnDisable()
    {
        pressGesture.Pressed -= OnPressed;
        RemoveFromSurface();
    }

    public void InMap()
    {
        inMap = true;
        OnMapUi.SetActive(true);
    }

    public void OutOffMap()
    {
        ChangeState(0);
        inMap = false;
        OnMapUi.SetActive(false);
        fovUI.SetActive(false);
    }

    //
    // meta
    //
    public void EnableDot()
    {
        Dot.SetActive(true);
    }

    private bool rotating;

    public IEnumerator WaitHighlight()
    {
        EnableDot();
        degrees.gameObject.SetActive(true);
        rotating = true;
        yield return new WaitForSeconds(waitHighlight);
        rotating = false;
        degrees.gameObject.SetActive(false);
        degrees.fillAmount = 0;
        DisableDot();
    }

    public void DisableDot()
    {
        Dot.SetActive(false);
    }

    public void Init(int id)
    {
        _id = id;
        name = "object-" + _id.ToString();

        //    if (_id < sprites.Count) spriteRenderer.sprite = sprites[_id];
        //    if (_id < colors.Count) spriteRenderer.color = colors[_id];
    }

    //
    // surface
    //

    public void AddToSurface()
    {
        if (surface != null)
            RemoveFromSurface();

        surface = GetComponentInParent<Surface>();

        Vector2 normalisedPosition = surface.GetNormalisedPosition(transform);
        float angleRads = (transform.localEulerAngles.z < 0f) ? ((transform.localEulerAngles.z % 360f + 360f) * Mathf.Deg2Rad) : ((transform.localEulerAngles.z % 360f) * Mathf.Deg2Rad);

        tuioObject = new TUIOObject(surface.NextSessionId(), _id, normalisedPosition.x, normalisedPosition.y, -angleRads, 0f, 0f, 0f, 0f, 0f);
        surface.Add(this);
        surface.ShowSurfaceObjectOnTop(this);
    }

    public void UpdateObject()
    {
        Vector2 tuioPosition = surface.GetNormalisedPosition(transform);
        float angleRads = (transform.localEulerAngles.z < 0f) ? ((transform.localEulerAngles.z % 360f + 360f) * Mathf.Deg2Rad) : ((transform.localEulerAngles.z % 360f) * Mathf.Deg2Rad);

        tuioObject.Update(tuioPosition.x, tuioPosition.y, -angleRads, 0f, 0f, 0f, 0f, 0f);
    }

    private float i;

    public void OnChangedAngel(float angle)
    {
        if (!rotating)
        {
            i = 0;
            degrees.transform.eulerAngles = new Vector3(0f, 0f, angle);
            prevAngle = angle;
        }

        if (angle > 0 && prevAngle < 0)
        {
            prevAngle = angle;
            degrees.fillClockwise = false;
            degrees.fillAmount = 0;
            degrees.transform.eulerAngles = new Vector3(0f, 0f, angle);
        }
        if (angle < 0 && prevAngle > 0)
        {
            prevAngle = angle;
            degrees.fillClockwise = false;
            degrees.fillAmount = 0;
            degrees.transform.eulerAngles = new Vector3(0f, 0f, angle);
        }
        i += (Mathf.Abs(prevAngle - angle) / 360);

        degrees.fillAmount += (Mathf.Abs(prevAngle - angle) / 360);
        EnableDot();
        StopAllCoroutines();
        StartCoroutine(WaitHighlight());
        UI.transform.eulerAngles = new Vector3(0f, 0f, angle);

        prevAngle = angle;
    }

    public void RemoveFromSurface()
    {
        if (surface == null) return;

        surface.Remove(this);
        surface = null;
        tuioObject = null;
    }

    private void OnPressed(object sender, System.EventArgs e)
    {
        if (surface != null)
            surface.ShowSurfaceObjectOnTop(this);
    }

    //
    // triggers
    //

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Surface"))
            AddToSurface();
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Surface"))
            RemoveFromSurface();
    }
}