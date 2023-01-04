using UnityEngine;
[CreateAssetMenu(fileName = "NewEnAttackState", menuName = "Custom/States/Enemies/Attack")]
public class EnAttackState : EnemyState
{
    public override void Run()
    {
        owner.Attack();
        isFinished = true;
    }
}
