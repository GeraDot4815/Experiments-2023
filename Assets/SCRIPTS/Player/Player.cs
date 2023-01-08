using System.Collections;
using UnityEngine;
public class Player : Creature
{
    public static Player Instance;
    [field: SerializeField] public Weapon weapon { get; private set; }
    [SerializeField] private Transform weaponPoint;

    private const float elementSpeedCoof = 1.5f;
    private const float elementSpaceDelay = 3f;
    private const float elementSpaceDamage = 1f;
    [SerializeField] private float elementIceForce = 20f;
    private IEnumerator spaceRoutine;
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
    protected override void GetStrengths()
    {
        return;
    }
    protected override void GetWeakness()
    {
        switch (level.biom)
        {
            case ElementTypes.Elements.Fire:
                factSpeed /= elementSpeedCoof;
                break;
            case ElementTypes.Elements.Ice:
                factSpeed *= elementSpeedCoof;
                break;
            case ElementTypes.Elements.Space:
                spaceRoutine = SpaceEffectCoroutine();
                StartCoroutine(spaceRoutine);
                break;
        }
    }
    private IEnumerator SpaceEffectCoroutine()
    {
        while (level.biom.HasFlag(ElementTypes.Elements.Space))
        {
            yield return new WaitForSeconds(elementSpaceDelay);
            GetDamage(elementSpaceDamage);
        }
    }
    public override void GetDamage(float damage, ElementTypes.Elements damageType)
    {
        GetDamage(damage);
    }
    public override void Move(Vector2 direction, float speedCoof = 1)
    {
        base.Move(direction, speedCoof);
        if (canMove && rb.velocity.x!=0)
        {
            weaponPoint.localPosition = new Vector3(Mathf.Abs(weaponPoint.localPosition.x) * Mathf.Sign(rb.velocity.x), weaponPoint.localPosition.y);
            if (level.biom.HasFlag(ElementTypes.Elements.Ice)) rb.AddForce(direction * elementIceForce, ForceMode2D.Impulse);
        }
    }
}
