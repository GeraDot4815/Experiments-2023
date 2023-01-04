using UnityEngine;
public class Kostya : Enemy
{
    [SerializeField] private EnemyState moveToPlayerState;
    [SerializeField] private EnemyState speedRunToPlayerState;
    [SerializeField] private EnemyState attackState;
    [SerializeField] private EnemyState keepDistanceState;
    [SerializeField] private EnemyState stayState;

    [SerializeField] private float seePlayerDist;
    [SerializeField] private float runDist;
    [SerializeField] private float attackDist;
    [SerializeField] private float attackRadius;

    [SerializeField] private int percentChanceOfPassive;
    [SerializeField] private int percentChanceOfStay;
    private int chance;

    [SerializeField] private Transform attackZone;
    protected override void Start()
    {
        base.Start();
        chance = 0;
        attackZone.localScale = new Vector3(attackRadius, attackRadius, 1);
    }
    protected override void ChangeState()
    {
        if (canMove && !isAttacking)
        {
            RandomChangeState();
        }
    }
    private void RandomChangeState()
    {
        if (currentState == keepDistanceState)
        {
            SetState(stayState);
            return;
        }
        chance = Random.Range(1, 101);
        if (chance >= percentChanceOfPassive)
        {
            float dist = Vector2.Distance(transform.position, player.transform.position);
            if (dist <= attackDist) SetState(attackState);
            else if (dist <= runDist) SetState(speedRunToPlayerState);
            else if (dist <= seePlayerDist) SetState(moveToPlayerState);
            else SetState(stayState);
        }
        else
        {
            chance = Random.Range(1, 101);
            if (chance >= percentChanceOfStay)
            {
                SetState(keepDistanceState);
            }
            else SetState(stayState);
        }
    }
    public override void Attack()
    {
        isAttacking = true;
        canMove = false;
        animator.SetTrigger("Attack");
    }
    public void AttackAnimTrigger()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, attackRadius);
        if (col != null && col.gameObject.layer==StaticVariables.PlayerLayer)
        {
            player.GetDamage(baseDamage);
        }
        isAttacking = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, seePlayerDist);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, runDist);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackDist);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
