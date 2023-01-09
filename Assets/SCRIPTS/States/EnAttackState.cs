using UnityEngine;
[CreateAssetMenu(fileName = "NewEnAttackState", menuName = "Custom/States/Enemies/Attack")]
public class EnAttackState : EnemyState
{
    public override void Run()//Ничего особенного, просто взываем атаку, которая кончается по галочке "IsAttacking"
    {
        owner.Attack();
        isFinished = true;
    }
}
