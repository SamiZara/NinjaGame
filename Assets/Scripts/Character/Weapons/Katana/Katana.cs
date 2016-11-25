using UnityEngine;
using System.Collections;

public class Katana : WeaponBase
{

    public float dashTimer, speed,damage;
    private float lastUsedTime;
    private Rigidbody2D playerRb;
    private bool isDashing;
    public BoxCollider2D boxCollider;

    void Start()
    {
        playerRb = CharacterController.player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            if (lastUsedTime + dashTimer <= Time.time)
            {
                isDashing = false;
                boxCollider.enabled = false;
				playerRb.velocity = Vector3.zero;
            }
        }
    }

    public override void Shoot()
    {
        base.Shoot();
        if (lastUsedTime + cooldown < Time.time)
        {
            isDashing = true;
            boxCollider.enabled = true;
            lastUsedTime = Time.time;
            var v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            float degree = MathHelper.degreeBetween2Points(v3, transform.position);
            playerRb.velocity = new Vector2(-speed * Mathf.Cos(degree * Mathf.Deg2Rad), -speed * Mathf.Sin(degree * Mathf.Deg2Rad));
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Character enemy = collider.transform.GetComponent<Character>();
        if(enemy != null)
        {
            enemy.MeleeWeaponDamage(damage);
        }
    }
}
