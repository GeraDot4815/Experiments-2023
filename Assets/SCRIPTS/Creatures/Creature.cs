using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected float maxHP;
    protected float factHP;

    [SerializeField] protected float movingSpeed=0;
    protected float factSpeed;

    protected Level level;
    [SerializeField] protected ElementTypes.Elements weakness;
    [SerializeField] protected ElementTypes.Elements strengths;
    protected const float damageTypeCoof=1.5f;

    protected Rigidbody2D rb;
    protected BoxCollider2D collider2D;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    public bool canMove;

    public event OnChangeHP onChangeHP;
    public delegate void OnChangeHP(float factHP, float maxHP);
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
        factSpeed = movingSpeed;

        level = Level.Instance;
        GetBiomEffect(level.biom);

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
    #region ����� � �������
    private void GetBiomEffect(ElementTypes.Elements element)//�������� � ���� ��������������� �������,
                                                             //���, ����� ������ (����) �������� ���������� ��������������� � �����
    {
        if (weakness.HasFlag(element)) GetWeakness();
        if (strengths.HasFlag(element)) GetStrengths();
    }
    protected abstract void GetWeakness();
    protected abstract void GetStrengths();
    #endregion
    #region ��������
    public virtual void Move(Vector2 direction, float speedCoof=1)//���������� � ���������� �� ����� �� ������ � ����� � ������ � ����� �������
                                                                  //���� ���� ����������, ����� �������������
    {
        if (canMove)
        {
            rb.velocity = direction * factSpeed * speedCoof;

            if (rb.velocity != Vector2.zero) animator.SetBool("IsMove", true);
            else animator.SetBool("IsMove", false);
            //������������ �������, ������� ��� ��������� ���� ��������� ����������� ��������������
            if (rb.velocity.x > 0) spriteRenderer.flipX = false;
            else if (rb.velocity.x < 0) spriteRenderer.flipX = true;
        }
    }
    /// <summary>
    ///������ ������������� ��������
    /// </summary>
    public void StopMoving()
    {
        if(rb.velocity!=Vector2.zero) Move(Vector2.zero);
    }
    #endregion
    #region �������� ��������
    /// <summary>
    /// ������������� ��������� � �������� �������� �����
    /// </summary>
    public void SetIdle()
    {
        StopMoving();
        canMove = false;
        animator.SetTrigger("Idle");
    }
    /// <summary>
    /// ��������� ��� ����������� ��������
    /// </summary>
    public void ContinueMoveAnimTrigger()
    {
        canMove = true;
    }
    #endregion
    #region HP Change
    /// <summary>
    /// ���������� ��� ��������, ������������� � ���� �����
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="damageType"></param>
    public virtual void GetDamage(float damage, ElementTypes.Elements damageType)
    {
        float ndamage = damage;
        if (weakness.HasFlag(damageType)) ndamage *= damageTypeCoof;
        if (strengths.HasFlag(damageType)) ndamage /= damageTypeCoof;
        GetDamage(ndamage);
    }
    public virtual void GetDamage(float damage)
    {
        Debug.Log(gameObject.name+" �� "+factHP.ToString());
        factHP -= damage;
        onChangeHP?.Invoke(factHP, maxHP);
        Debug.Log(gameObject.name + " ����� " + factHP.ToString());
    }
    public virtual void PlusHP(float heal)
    {
        Debug.Log(gameObject.name + " �� " + factHP.ToString());
        factHP += heal;
        onChangeHP?.Invoke(factHP, maxHP);
        Debug.Log(gameObject.name + " ����� " + factHP.ToString());
    }
    #endregion
    public virtual void OnDeath()
    {
        Debug.Log("� ����");
    }
}
