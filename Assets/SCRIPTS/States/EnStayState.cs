using UnityEngine;
[CreateAssetMenu(fileName = "NewEnStayState", menuName = "Custom/States/Enemies/Stay")]
public class EnStayState : EnemyState
{
    public override void Run()
    {
        owner.SetIdle();
        isFinished = true;
    }   
}
