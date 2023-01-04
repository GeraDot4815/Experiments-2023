using UnityEngine;
[CreateAssetMenu(fileName = "NewKeepDistState", menuName = "Custom/States/Enemies/KeepDistance")]
public class EnKeepDistanceState : EnemyState
{
    [SerializeField] private float speedCoof = 1;
    [SerializeField] private float minDist = 0.1f;
    [SerializeField] private float targetDistance = 0.1f;
    public override void Run()
    {
        Vector2 playerPos = Player.Instance.transform.position;
        Vector2 myPos = owner.transform.position;
        Vector2 heading = (playerPos - myPos);
        float dist = heading.magnitude;
        if ((minDist != 0 && Mathf.Abs(dist) - minDist <= 0) || (Mathf.Abs(dist) - targetDistance >= 0))
        {
            isFinished = true;
        }
        else
        {
            Vector2 dir = heading / dist;
            owner.Move(-dir, speedCoof);
        }
    }
}
