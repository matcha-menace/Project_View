using UnityEngine;
using System.Collections.Generic;

public class SimpleTrap: MonoBehaviour{
    
    public enum TrapOptions
    {
        Upward,
        Forward,
        Backward,
        Right,
        Left,
        Spin
    }

    private Dictionary<TrapOptions, Vector3> directionMap;

    void Start()
    {
        directionMap = new Dictionary<TrapOptions, Vector3>
        {
            { TrapOptions.Upward, Vector3.up },
            { TrapOptions.Forward, Vector3.forward },
            { TrapOptions.Backward, Vector3.back },
            { TrapOptions.Right, Vector3.right},
            { TrapOptions.Left, Vector3.left },
            { TrapOptions.Spin, Vector3.one } 
        };
    }

    private bool isMoving = false;

    [SerializeField] private float strength = 1f;
    [SerializeField] private TrapOptions option = TrapOptions.Forward;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerTrap(other);
        } 

    }

    void triggerTrap(Collider other)
    {
        BallBehavior ballBehavior = other.gameObject.GetComponent<BallBehavior>();

        if (directionMap.TryGetValue(option, out Vector3 direction))
        {
            ballBehavior.HitBall(direction, strength);
        }
        else
        {
            Debug.LogError("Invalid trap option selected!");
        }
    }

}