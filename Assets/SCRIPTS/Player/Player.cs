using UnityEngine;
public class Player : Creature
{
    public static Player Instance;
    [field: SerializeField] public Weapon weapon { get; private set; }
    [SerializeField] private Transform weaponPoint;
    private void Awake()
    {
        base.Awake();
        Instance = this;
    }
    protected override void Start()
    {
        base.Start();
        try
        {
            weapon = GetComponentInChildren<Weapon>();
        }
        catch
        {
            throw new System.Exception("No Weapon");
        }
    }
    public override void Move(Vector2 direction, float speedCoof = 1)
    {
        base.Move(direction, speedCoof);
        if (canMove && rb.velocity.x!=0)
        {
            weaponPoint.localPosition = new Vector3(Mathf.Abs(weaponPoint.localPosition.x) * Mathf.Sign(rb.velocity.x), weaponPoint.localPosition.y);
        }
    }
}
