using DarkMagic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    // This is currently only for player projectiles.

    void OnTriggerEnter(Collider other)
    {
        var tryEnemy = other.gameObject?.GetComponent<Enemy>();

        if (tryEnemy)
        {
            var enemy = tryEnemy;
            enemy.Die();
        }
    }
}
