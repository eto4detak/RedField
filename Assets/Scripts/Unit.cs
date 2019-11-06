using UnityEngine;

public class Unit : MonoBehaviour
{
    internal static GameObject prefab;

    internal float runSpeed = 2f;
    internal float health = 1f;
    private Vector3? newPosition;
    internal bool canDamage = true;
    internal bool canRun = true;

    internal Vector3? NewPosition { get => newPosition; set => newPosition = value; }

    void Start()
    {
    }

    void Update()
    {
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


}

class UnitCommand
{

}