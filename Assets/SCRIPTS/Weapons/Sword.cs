using UnityEngine;
public class Sword : Weapon
{
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        //Зеркалим меч при повороте. Можно попробовать как-то не в Апдейте, но пох, не такая тяжелая операция
        if (playerRb.velocity.x > 0) spriteRenderer.flipX = false;
        else if (playerRb.velocity.x < 0) spriteRenderer.flipX = true;
    }
    protected override void OnAttack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, attackRadius);//Чекаем полученные коллайдеры в радиусе
        foreach (Collider2D col in cols)
        {
            int layer = col.gameObject.layer;
            for (int i=0; i<StaticVariables.DamagingLayers.Length; i++)
            {
                if (layer == StaticVariables.DamagingLayers[i])//Ищем клиента в черном списке
                {
                    Creature creature = col.GetComponent<Creature>();
                    if(creature!=null) creature.GetDamage(factDamage, damageType);//Стукаем его по башке
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
