using DarkMagic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    // This is currently only for player projectiles.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

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
