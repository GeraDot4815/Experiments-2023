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
    private bool useIce;
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
    protected override void GetStrengths()// У Чебупели нет сильных сторон
    {
        return;
    }
    protected override void GetWeakness()// Подключаем уникальные эффекты от локаций
    {
        useIce = false;
        switch (level.biom)
        {
            case ElementTypes.Elements.Fire:
                factSpeed /= elementSpeedCoof;
                break;
            case ElementTypes.Elements.Ice:
                factSpeed *= elementSpeedCoof;
                useIce = true;
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
    public override void GetDamage(float damage, ElementTypes.Elements damageType)//Т.к. доп урона от стихий не получаем, мето тупо игнорит тип урона
    {
        GetDamage(damage);
    }
    public override void Move(Vector2 direction, float speedCoof = 1)
    {
        base.Move(direction, speedCoof);
        if (canMove && rb.velocity.x!=0)
        {
            //Перебрасываем оружие в другую руку при повороте
            weaponPoint.localPosition = new Vector3(Mathf.Abs(weaponPoint.localPosition.x) * Mathf.Sign(rb.velocity.x), weaponPoint.localPosition.y);
            //if (useIce) rb.AddForce(direction * elementIceForce, ForceMode2D.Impulse);
        }
    }
}
