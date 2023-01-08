using UnityEngine;
public class Mage : Enemy
{
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected float shotDelay;
    protected float factDelay;
    private float timeBtwAttack;
    [SerializeField] protected Transform bulletPoint;

    [SerializeField] protected EnemyState runOutState;
    [SerializeField] protected EnemyState keepDistanceState;
    [SerializeField] protected EnemyState stayState;
    [SerializeField] protected EnemyState attackState;
    [SerializeField] protected EnemyState moveToPlayerState;

    [SerializeField] protected int chanceOfRun;
    [SerializeField] protected int chanceOfKeep;
    [SerializeField] protected int chanceOfStay;

    [SerializeField] protected float minDist;
    [SerializeField] protected float middleDist;
    [SerializeField] protected float maxDist;
    private const float elementDelaycoof = 1.5f;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        factDelay = shotDelay;
    }
    protected override void Update()
    {
        base.Update();
        if (timeBtwAttack > 0) timeBtwAttack -= Time.deltaTime;
    }
    protected override void GetStrengths()
    {
        base.GetStrengths();
        shotDelay /= elementDelaycoof;
    }
    protected override void GetWeakness()
    {
        base.GetWeakness();
        shotDelay *= elementDelaycoof;
    }
    protected override void ChangeState()
    {
        if (canMove && !isAttacking)
        {
            float dist = Vector2.Distance(transform.position, player.transform.position);
            if (dist <= minDist) RandomChangeState(chanceOfRun, runOutState);
            else if (dist <= middleDist) RandomChangeState(chanceOfKeep, keepDistanceState);
            else if (dist <= maxDist) RandomChangeState(chanceOfStay, stayState);
            else if (dist > maxDist) SetState(moveToPlayerState);
        }
    }
    private void RandomChangeState(int chanceOfAgressive, EnemyState passiveState)
    {
        int chance = Random.Range(1, 101);
        if (chance > chanceOfAgressive) SetState(passiveState);
        else SetState(attackState);
    }
    public override void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            base.Attack();
        }
    }
    public void AttackAnimTrigger()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 myPos = transform.position;
        float angle = Mathf.Atan2(playerPos.y - myPos.y, playerPos.x - myPos.x) * Mathf.Rad2Deg;
        
        Bullet nBullet = Instantiate(bullet, bulletPoint.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle));
        nBullet.transform.parent = null;
        nBullet.damage = baseDamage;
        nBullet.damageType = damageType;

        timeBtwAttack = factDelay;
        isAttacking = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDist);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, middleDist);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, maxDist);
    }
}
