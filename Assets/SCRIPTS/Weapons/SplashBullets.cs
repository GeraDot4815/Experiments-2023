using System.Collections.Generic;
using UnityEngine;
public class SplashBullets : Bullet
{
    [SerializeField] private List<Bullet> bullets;
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        foreach (Bullet bullet in bullets)
        {
            bullet.damage = damage;
        }
        Invoke("EndTime", lifeTime);
    }
    private void EndTime()
    {
        Destroy(gameObject);
    }
}
