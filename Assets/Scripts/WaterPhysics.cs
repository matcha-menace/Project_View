using UnityEngine;
using System.Linq;
public class WaterPhysics: MonoBehaviour
{
    [SerializeField] private float bouyancy = 29f;

    // add different tags that can float
    [SerializeField] private string[] floatTypes = new string[] { "Player",};

    [SerializeField] private float depthModifier = 1;

    private Collider m_collider;
    private Vector3 colliderMin;

    private Vector3 colliderMax;

    private Vector3 WaterPressure;

    private float depth;

    void Start()
    {
        m_collider = GetComponent<Collider>();
        colliderMin = m_collider.bounds.min;
        colliderMax = m_collider.bounds.max;

    }

    void OnTriggerStay(Collider other)
    { 
        if (floatTypes.Contains(other.tag))
        {
            ApplyBouyancy(other);
        }
    }

    void ApplyBouyancy(Collider other)
    {
        Debug.Log("Applying Bouynancy To Game Object: " + other.gameObject);
        Debug.Log("Min and max bounds: " + colliderMin.y + ", " + colliderMax.y);
        Debug.Log("Object Location: " + other.transform.position.y);

        depth = colliderMax.y - other.transform.position.y;
        other.GetComponent<Rigidbody>().AddForce(CalculateWaterForce(depth));
    }

    Vector3 CalculateWaterForce(float depth)
    {
        return (Vector3.up * bouyancy * depth * depthModifier);
    }
}