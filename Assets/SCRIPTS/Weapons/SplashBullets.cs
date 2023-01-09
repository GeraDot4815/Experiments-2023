using System.Collections.Generic;
using UnityEngine;
public class SplashBullets : Bullet
{
    [SerializeField] private List<Bullet> bullets;
    protected override void Start()//Просто хранит несколько пуль, которым передает свойства, полученные от пушки.
                                   //Сам не двигается, но и не падает
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        foreach (Bullet bullet in bullets)
        {
            bullet.damage = damage;
            bullet.damageType = damageType;
        }
        Invoke("EndTime", lifeTime);
    }
    private void EndTime()
    {
        Destroy(gameObject);
    }
}
