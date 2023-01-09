using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent (typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float baseDamage;
    protected float factDamage;
    //Оружие может получить усиление урона и скорости атаки или, наоборот, дебафф
    [SerializeField] protected ElementTypes.Elements damageType;
    [SerializeField] protected ElementTypes.Elements weakness;
    private const float elementDamageCoof=1.5f;
    private const float elementDelayCoof = 1.5f;

    protected Level level;


    [SerializeField] protected float delay;
    protected float factDelay;
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

        factDamage = baseDamage;
        factDelay = delay;

        level = Level.Instance;
        GetBiomEffect(level.biom);

        timeBtwAttack = 0;
    }
    protected virtual void Update()
    {
        if (timeBtwAttack >= 0) timeBtwAttack -= Time.deltaTime;
    }
    #region получаем эффекты от среды
    private void GetBiomEffect(ElementTypes.Elements element)
    {
        if (weakness.HasFlag(element)) GetWeakness();
        if (damageType.HasFlag(element)) GetStrengths();
    }
    protected virtual void GetWeakness()
    {
        factDamage /= elementDamageCoof;
        factDelay *= elementDelayCoof;
    }
    protected virtual void GetStrengths()
    {
        factDamage *= elementDamageCoof;
        factDelay /= elementDelayCoof;
    }
    #endregion
    public virtual void Attack()//Подобная структура общая почти для всех
    {
        if (timeBtwAttack <= 0)
        {
            animator.SetTrigger("Attack");
            OnAttack();
            timeBtwAttack = factDelay;
        }
    }
    protected abstract void OnAttack();//А это уже меняем по необходимости
}
