using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SimpleTrap: MonoBehaviour{
    
    private Animator anim;
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
    private Dictionary<TrapOptions, string> animMap;

    private float trapDelay;

    [SerializeField] private bool playAnimation = false;

    


    void Start()
    {
        if (playAnimation){anim = GetComponent<Animator>();}
        

        directionMap = new Dictionary<TrapOptions, Vector3>
        {
            { TrapOptions.Upward, Vector3.up },
            { TrapOptions.Forward, Vector3.forward },
            { TrapOptions.Backward, Vector3.back },
            { TrapOptions.Right, Vector3.right},
            { TrapOptions.Left, Vector3.left },
            { TrapOptions.Spin, Vector3.one } 
        };

        animMap = new Dictionary<TrapOptions, string>{
            { TrapOptions.Upward, "SimpleTrapUpward"},
            { TrapOptions.Forward, "SimpleTrapForward"},
            { TrapOptions.Backward, "SimpleTrapBackward" },
            { TrapOptions.Right, "SimpleTrapRight"},
            { TrapOptions.Left, "SimpleTrapLeft" },
            { TrapOptions.Spin, "SimpleTrapSpin" } 
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

    async void triggerTrap(Collider other)
    {
        BallBehavior ballBehavior = other.gameObject.GetComponent<BallBehavior>();

        
        if (animMap.TryGetValue(option, out string animationName))
        {
            Debug.Log("Attempting to play animation"+ animationName);
            if (playAnimation){
                anim.Play(animationName);
            }
        }

        if (directionMap.TryGetValue(option, out Vector3 direction))
        {
            await Task.Delay((int)(trapDelay * 1000));
            ballBehavior.HitBall(direction, strength);
        }
        else
        {
            Debug.LogError("Invalid trap option selected!");
        }

    }

}