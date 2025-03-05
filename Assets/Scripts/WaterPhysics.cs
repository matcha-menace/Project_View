using UnityEngine;
using System.Linq;
public class WaterPhysics: MonoBehaviour
{
    // base bouyancy
    [SerializeField] private float bouyancy = 29f;

    // add different tags that can float
    [SerializeField] private string[] floatTypes = new string[] { "Player",};

    // a modifier on how water densiity effect bouyancy
    [SerializeField] private float submergedVolumeModifier = 0.1f;


    // these two controls the drag applied from water to object
    [SerializeField] private float waterDrag = 1f;

    [SerializeField] private float waterAngularDrag = 0.5f;


    private Collider m_collider;

    private Vector3 colliderMin;

    private Vector3 colliderMax;

    private Vector3 WaterPressure;

    private float waterBodyVolume;
    private float waterBodyHeight;

    private float submergedVolume;

    private float depth;

    private Rigidbody targetRigidBody;

    void Start()
    {
        // get collider bounds
        m_collider = GetComponent<Collider>();
        colliderMin = m_collider.bounds.min;
        colliderMax = m_collider.bounds.max;
        waterBodyVolume = m_collider.bounds.size.x * m_collider.bounds.size.y * m_collider.bounds.size.z;
        waterBodyHeight = colliderMax.y - colliderMin.y;

    }

    void OnTriggerStay(Collider other)
    { 
        if (floatTypes.Contains(other.tag))
        {
            // apply bouyancy while object is submerged
            ApplyBouyancy(other);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // add drag and angular drag to submerged object
        targetRigidBody = other.GetComponent<Rigidbody>();

        targetRigidBody.drag += waterDrag;
        targetRigidBody.angularDrag += waterAngularDrag;
    }

    void OnTriggerExit(Collider other)
    {
        // get rid of drag when object leaves water body
        targetRigidBody.drag -= waterDrag;
        targetRigidBody.angularDrag -= waterAngularDrag;
    }

    void ApplyBouyancy(Collider other)
    {
        Debug.Log("Applying Bouynancy To Game Object: " + other.gameObject);
        Debug.Log("Min and max bounds: " + colliderMin.y + ", " + colliderMax.y);
        Debug.Log("Object Location: " + other.transform.position.y);
        Debug.Log("water volume: " + waterBodyVolume);

    
        submergedVolume = ((waterBodyHeight - other.transform.position.y) / waterBodyHeight) * waterBodyVolume;
        Debug.Log("Submerged water volume: " + submergedVolume);

        depth = colliderMax.y - other.transform.position.y;
        targetRigidBody.AddForce(CalculateWaterForce(submergedVolume));
    }

    Vector3 CalculateWaterForce(float submergedVolume)
    {
        return (Vector3.up * bouyancy * submergedVolume * submergedVolumeModifier);
    }
}