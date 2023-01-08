using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public ElementTypes.Elements damageType;
    [SerializeField] protected float speed=1;
    [SerializeField] protected int lifeTime=10;
    protected Rigidbody2D rb;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        Move();
        Invoke("OnHit", lifeTime);
    }
    protected virtual void Move()
    {
        rb.velocity = transform.right*speed;
    }
    protected virtual void OnHit(Creature target)
    {
        if(target!=null) target.GetDamage(damage, damageType);
        Destroy(gameObject);
    }
    protected virtual void OnHit()
    {
        Destroy(gameObject);
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;
        for(int i=0; i<StaticVariables.DamagingLayers.Length; i++)
        {
            if (layer == StaticVariables.DamagingLayers[i])
            {
                Creature creature = collision.gameObject.GetComponent<Creature>();
                OnHit(creature);
            }
        }
    }
}
