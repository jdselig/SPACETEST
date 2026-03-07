using DarkMagic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed = 130;
    float step;
    float lifetime = 3;

    void Start() { }

    void HandleMove()
    {
        step = speed * Time.deltaTime;

        // Move the object towards the target position
        transform.AddPosZ(step);
    }

    void Update()
    {
        HandleMove();
        HandleLifetime();
    }

    void HandleLifetime()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var tryEnemy = other.gameObject?.GetComponent<Enemy>();

        if (tryEnemy)
        {
            var enemy = tryEnemy;
            enemy.Die();
            Destroy(gameObject);
        }
    }
}
