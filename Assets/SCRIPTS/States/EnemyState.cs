using UnityEngine;
public abstract class EnemyState : ScriptableObject
{
    public bool isFinished { get; protected set; }
    [HideInInspector] public Enemy owner;

    public virtual void Init(Enemy enemy) { owner = enemy; }
    public abstract void Run();
}
