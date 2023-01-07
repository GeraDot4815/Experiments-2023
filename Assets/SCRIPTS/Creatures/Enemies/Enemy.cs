using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Enemy : Creature
{
    [SerializeField] protected EnemyState startState;
    [SerializeField] protected int baseDamage;
    protected EnemyState currentState;
    protected Player player;
    protected bool isAttacking;
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
        if (!canMove)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    public abstract void Attack();
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
