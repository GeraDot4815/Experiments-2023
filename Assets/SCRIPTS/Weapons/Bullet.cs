using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [HideInInspector] public float damage;//Передается от оружия
    [HideInInspector] public ElementTypes.Elements damageType;//Передается от оружия
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
    protected virtual void OnHit(Creature target)//Если попал во что-то живое, передаем ему урон, иначе тупо "пиф-паф"
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
        for(int i=0; i<StaticVariables.DamagingLayers.Length; i++)//Чекаем, есть ли клиент в ченом списке смертных
        {
            if (layer == StaticVariables.DamagingLayers[i])
            {
                Creature creature = collision.gameObject.GetComponent<Creature>();
                OnHit(creature);
            }
        }
    }
}
