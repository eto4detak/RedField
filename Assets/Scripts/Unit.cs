using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    internal static GameObject prefab;

    private Vector3? newPosition;

    protected float slowDown = 1f;

    internal float runSpeed = 2f;
    internal float health = 1f;
    internal bool canDamage = true;
    internal bool canRun = true;
    internal Rigidbody rb;
    internal float jumpForce = 20.0f;
    internal bool isGrounded = true;

    internal NavMeshAgent agent;
   // public LayerMask groundMask;
   // public Transform target;
    //public float groundRadius;
    //public Collider[] ground;

    internal Vector3? NewPosition { get => newPosition; set => newPosition = value; }


    protected virtual void Awake()
    {
        SelectObjects.SetAllowed(gameObject);
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected void Start()
    {
      

    }

    protected virtual void Update()
    {
        CheckErrorPositionY();
    }


    private void CheckErrorPositionY()
    {
        if (transform.position.y < WorldManager.minPositionY)
        {
            transform.position = new Vector3(transform.position.x, WorldManager.startPositionY,transform.position.z);
        }
    }


    public virtual void RunToPoint()
    {
        if ( NewPosition == null || !canRun) return;
        if ( NewPosition == transform.position)
        {
            NewPosition = null;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, (Vector3)NewPosition, runSpeed * Time.deltaTime);
    }

    public virtual void ReceiveDamage()
    {
        Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    protected virtual void AimCounterpoise()
    {
        transform.position = Vector3.MoveTowards(transform.position, (Vector3)NewPosition, runSpeed * Time.deltaTime);
    }

    public virtual void Jump()
    {
        Debug.Log("test123");
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

}

class UnitCommand
{

}