using UnityEngine;
public abstract class Enemy : Creature
{
    [SerializeField] protected EnemyState startState;
    [SerializeField] protected float baseDamage;
    [SerializeField] protected ElementTypes.Elements damageType;
    protected EnemyState currentState;
    protected Player player;
    protected bool isAttacking;
    protected const float elementSpeedCoof = 1.25f;
    protected override void Awake()
    {
        base.Awake();
        isAttacking = false;
        SetState(startState);
    }
    protected override void Start()
    {
        base.Start();
        player = Player.Instance;
    }
    protected override void Update()
    {
        base.Update();
        UpdateStates();
    }
    protected override void GetStrengths()
    {
        factSpeed *= elementSpeedCoof;
    }
    protected override void GetWeakness()
    {
        factSpeed /= elementSpeedCoof;
    }
    public virtual void Attack()
    {
        isAttacking = true;
        canMove = false;
        animator.SetTrigger("Attack");
    }
    #region смена состояний
    protected virtual void UpdateStates()
    {
        if (!currentState.isFinished) currentState.Run();
        else ChangeState();
    }
    protected virtual void ChangeState()//default if start state. Can be changed
    {
        SetState(startState);
    }
    protected void SetState(EnemyState newState)
    {
        currentState = Instantiate(newState);
        currentState.Init(this);
    }
    #endregion
}
