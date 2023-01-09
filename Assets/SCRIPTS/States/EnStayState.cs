using UnityEngine;
[CreateAssetMenu(fileName = "NewEnStayState", menuName = "Custom/States/Enemies/Stay")]
public class EnStayState : EnemyState
{
    public override void Run()//Ничего особенного, включаем анимацию покоя, которую надо стопнуть триггером
    {
        owner.SetIdle();
        isFinished = true;
    }   
}
