using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent (typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int baseDamage;
    [SerializeField] protected ElementTypes.Elements damageType;
    [SerializeField] protected float delay;
    protected float timeBtwAttack;
    protected Animator animator;
    protected Player player;
    protected Rigidbody2D playerRb;
    protected SpriteRenderer spriteRenderer;
    protected virtual void Start()
    {
        player = Player.Instance;
        playerRb = player.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        timeBtwAttack = 0;
    }
    protected virtual void Update()
    {
        if (timeBtwAttack >= 0) timeBtwAttack -= Time.deltaTime;
    }
    public virtual void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            animator.SetTrigger("Attack");
            OnAttack();
            timeBtwAttack = delay;
        }
    }
    protected abstract void OnAttack();
}
