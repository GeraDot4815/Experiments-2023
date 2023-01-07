using System;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Creature : MonoBehaviour
{
    [SerializeField] protected int maxHP;
    protected int factHP;

    [SerializeField] protected float movingSpeed=0;

    [SerializeField] protected ElementTypes.Elements weakness;
    [SerializeField] protected ElementTypes.Elements strengths;

    protected Rigidbody2D rb;
    protected BoxCollider2D collider2D;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    public bool canMove;

    public event OnChangeHP onChangeHP;
    public delegate void OnChangeHP(int factHP, int maxHP);
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        collider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected virtual void Start()
    {
        canMove = true;
        factHP = maxHP;
        onChangeHP?.Invoke(factHP, maxHP);
    }
    protected virtual void Update()
    {
        if (factHP <= 0) OnDeath();
        if(!canMove)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("IsMove", false);
        }
    }
    public virtual void Move(Vector2 direction, float speedCoof=1)
    {
        if (canMove)
        {
            rb.velocity = direction * movingSpeed * speedCoof;

            if (rb.velocity != Vector2.zero) animator.SetBool("IsMove", true);
            else animator.SetBool("IsMove", false);

            if (rb.velocity.x > 0) spriteRenderer.flipX = false;
            else if (rb.velocity.x < 0) spriteRenderer.flipX = true;
        }
    }
    public void StopMoving()
    {
        if(rb.velocity!=Vector2.zero) Move(Vector2.zero);
    }
    /// <summary>
    /// Останавливает персонажа и включает анимацию покоя
    /// </summary>
    public void SetIdle()
    {
        StopMoving();
        canMove = false;
        animator.SetTrigger("Idle");
    }
    public void ContinueMoveAnimTrigger()
    {
        canMove = true;
    }
    #region HP Change
    public virtual void GetDamage(int damage)
    {
        Debug.Log(gameObject.name+" ДО "+factHP.ToString());
        factHP -= damage;
        onChangeHP?.Invoke(factHP, maxHP);
        Debug.Log(gameObject.name + " ПОСЛЕ " + factHP.ToString());
    }
    public virtual void PlusHP(int heal)
    {
        Debug.Log(gameObject.name + " ДО " + factHP.ToString());
        factHP += heal;
        onChangeHP?.Invoke(factHP, maxHP);
        Debug.Log(gameObject.name + " ПОСЛЕ " + factHP.ToString());
    }
    #endregion
    public virtual void OnDeath()
    {
        Debug.Log("Я сдох");
    }
}
