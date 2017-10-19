using UnityEngine;
using System.Collections;

public class ZController : MonoBehaviour
{

    private Rigidbody m_rigidBody;
    private Collider m_collider;
    //private GameObject[] m_childs;

    public Material DisabledMaterial;
    public Material EnabledMaterial;


    public float InitialScale = 1.0f;
    public float FinalScale = 3.0f;
    public float CounterGravityFloatFactor = 1.1f;
    public float ScaleRate = 0.01f;
    //public float ShrinkRate = 1.0f;
    public float MaxInitialHorizontalImpulse = 0.1f;


    private Vector3 m_scale;
    private Renderer[] m_renderers;
    private bool m_isSolid;

    private void SetSolid(bool solid){
        foreach (Renderer r in m_renderers)
            r.material = (solid) ? EnabledMaterial : DisabledMaterial;

        m_collider.enabled = solid;
        m_isSolid = solid;
    }

    void Awake ()
    {
        //m_childs = GetComponentsInChildren<GameObject> ();

        m_rigidBody = GetComponent<Rigidbody> ();
        m_collider = GetComponent<Collider> ();

        m_renderers = GetComponentsInChildren<Renderer> ();

    }

    void Start ()
    {
        transform.localScale = Vector3.one * InitialScale;

        float factor = Random.Range (-MaxInitialHorizontalImpulse, +MaxInitialHorizontalImpulse);

        SetSolid (false);

        m_rigidBody.AddForce (
            Vector3.left *
            factor, 
            ForceMode.Impulse);
    }

    void Update ()
    {
        if (m_isSolid)
            return;

        m_scale += Vector3.one * ScaleRate * Time.deltaTime;

        if (m_scale.x >= FinalScale)
            SetSolid (true);

        transform.localScale = m_scale ;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        m_rigidBody.AddForce (
            -Physics.gravity * CounterGravityFloatFactor * 50f * Time.fixedDeltaTime, 
            ForceMode.Acceleration);
    }
}
