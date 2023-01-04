using UnityEngine;
[CreateAssetMenu(fileName = "NewEnMoveToPlayerState", menuName = "Custom/States/Enemies/MoveToPlayer")]
public class EnMoveToPlayerState : EnemyState
{
    [SerializeField] private float speedCoof=1;
    [SerializeField] private float startDist=0;
    [SerializeField] private float targetDistance=0.1f;
    public override void Run()
    {
        Vector2 playerPos = Player.Instance.transform.position;
        Vector2 myPos = owner.transform.position;
        Vector2 heading = (playerPos - myPos);
        float dist = heading.magnitude;
        if((startDist!=0 && Mathf.Abs(dist)-startDist>=0)||(Mathf.Abs(dist) - targetDistance <= 0))
        {
            isFinished = true;
        }
        else
        {
            Vector2 dir = heading / dist;
            owner.Move(dir, speedCoof);
        }
    }
}
