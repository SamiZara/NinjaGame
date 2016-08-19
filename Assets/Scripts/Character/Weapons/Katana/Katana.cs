using UnityEngine;
using System.Collections;

public class Katana : MonoBehaviour
{

    public float cooldown, damage, speed, dashTimer;
    private float lastUsedTime;
    private Rigidbody2D playerRb;
    private bool isDashing;
    private Vector2 speedBeforeDash;
    public Character character;

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
                playerRb.velocity = speedBeforeDash;
            }
        }
    }

    public void Dash()
    {
        if (lastUsedTime + cooldown < Time.time)
        {
            isDashing = true;
            lastUsedTime = Time.time;
            speedBeforeDash = playerRb.velocity;
            var v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            float degree = MathHelper.degreeBetween2Points(v3, transform.position);
            playerRb.velocity = new Vector2(-speed * Mathf.Cos(degree * Mathf.Deg2Rad), -speed * Mathf.Sin(degree * Mathf.Deg2Rad));
        }
    }
}
