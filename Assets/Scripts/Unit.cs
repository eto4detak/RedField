using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, IFrontImage
{
    internal float aggression = 0f;
    internal NavMeshAgent agent;
    internal float attackRadius = 2f;
    protected List<float> allTypeAggression = new List<float>();
    public UnitCommand command;
    protected List<Unit> commandTargets = new List<Unit>();
    internal bool canDamage = true;
    internal bool canRun = true;
    internal float domageVal = 1f;
    protected Faction faction = Faction.Neutral;
    internal float jumpForce = 20.0f;
    internal bool isGrounded = true;
    internal float health = 1f;
    internal float maxHealth = 1f;
    internal float mana = 1f;
    internal float maxMana = 1f;
    internal float maxAggression = 1f;
    protected UnitStatus status = UnitStatus.Norm;
    internal UnitGroup selfGroup;
    protected float slowDown = 1f;
    internal static GameObject prefab;
    private Vector3? newPosition;
    internal float runSpeed = 2f;
    internal Rigidbody rb;

    internal Vector3? NewPosition { get => newPosition; set => newPosition = value; }
    Sprite IFrontImage.FrontImage { get; set; }

    protected virtual void Awake()
    {
      //  command = UnitCommand.Idle;

        SelectObjects.SetAllowed(this);
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
      

    }

    protected virtual void Update()
    {
        CheckErrorPositionY();
        UpdateAction();
       
    }

    void OnMouseDown()
    {
    }

    void OnMouseExit()
    {
        GetComponentInChildren<Renderer>().material.color = Color.white;
    }


    private void UpdateAction()
    {

        //if (command == UnitCommand.Attack)
        //{
        //    if (attackRadius > GetDistanceToTarget())
        //    {
        //      //  Debug.Log(name + " Attack " + commandTargets[0].name);
        //        Damage(commandTargets[0], domageVal);
        //    }
        //    else
        //    {
        //    }

        //    if (status == UnitStatus.Battle)
        //    {

        //    }else if (status == UnitStatus.Run)
        //    {

        //    }
        //    else
        //    {
        //    }
        //}
    }


    private float GetDistanceToTarget()
    {
        if (commandTargets[0] != null)
        {
            return Vector3.Distance(commandTargets[0].transform.position, transform.position);
        }
        return float.PositiveInfinity;
    }


    public void Damage(Unit target,  float val)
    {
        target.health = Mathf.Max(target.health - val, 0);
    }

    public static void SetAttackTarget(List<Unit> units, List<Unit> target)
    {
        foreach (var selectedUnit in units)
        {
            selectedUnit.SetAttackTarget(target);
        }
    }


    public void SetAttackTarget(List<Unit> target)
    {
        commandTargets.Clear();
       // command = UnitCommand.Attack;
        commandTargets.AddRange(target);

        if (commandTargets.Count > 0)
        {
            agent.destination = commandTargets[0].transform.position;
        }
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
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void MoveToPoint(Vector3 point)
    {
        status = UnitStatus.Run;
        Ray ray = Camera.main.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            agent.destination = hit.point;
        }
    }



    //public void MoveGroupToPoint( Vector3 point)
    //{

    //    status = UnitStatus.Run;
    //    Ray ray = Camera.main.ScreenPointToRay(point);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        agent.destination = hit.point;
    //    }
    //}
}

public enum Faction : int
{
    Hero = 0,
    Red,
    Blue,
    Back,
    Green,
    White,
    Brown,
    Orange,
    Neutral,
    Passive,
    Hostile,
    Boss,
}

public enum UnitStatus : int
{
    Norm = 0,
    Run,
    Immobilized,
    Battle,

}
