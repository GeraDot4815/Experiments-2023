using UnityEngine;
public class Gun : Weapon
{
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected Transform bulletPoint;
    [Range(-360, 360)]
    [SerializeField] private int rotationOffset=0;//Опциональная дичь
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        //вращаем пушку за курсором
        Vector3 difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }
    protected override void OnAttack()//Создаем пулю и отправляем ее в обрый путь
    {   
        Bullet nBullet = Instantiate(bullet, bulletPoint.position, transform.rotation);
        nBullet.transform.parent = null;
        nBullet.damage = factDamage;
        nBullet.damageType = damageType;
    }
}