using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Creature : MonoBehaviour
{
    [SerializeField] private float maxHP;
    private float factHP;

    private Rigidbody2D rb;

    public event OnChangeHP onChangeHP;
    public delegate void OnChangeHP(float factHP, float maxHP);
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
    private void Start()
    {
        factHP = maxHP;
        onChangeHP?.Invoke(factHP, maxHP);
    }
    private void Update()
    {
        if (factHP <= 0) OnDeath();
    }
    #region HP Change
    public virtual void GetDamage(float damage)
    {
        Debug.Log(gameObject.name+" дн "+factHP.ToString());
        factHP -= damage;
        onChangeHP?.Invoke(factHP, maxHP);
        Debug.Log(gameObject.name + " оняке " + factHP.ToString());
    }
    public virtual void PlusHP(float heal)
    {
        Debug.Log(gameObject.name + " дн " + factHP.ToString());
        factHP += heal;
        onChangeHP?.Invoke(factHP, maxHP);
        Debug.Log(gameObject.name + " оняке " + factHP.ToString());
    }
    #endregion
    public virtual void OnDeath()
    {
        Debug.Log("ъ ЯДНУ");
    }
}
