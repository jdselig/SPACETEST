using DarkMagic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Player player;
    protected float speed;
    float step;
    float stoppingDistance = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (speed == 0)
        {
            speed = Random.Range(1, 5);
        }
    }

    public virtual void Die()
    {
        // play an effect
        V.Broadcast<EnemyDied>();
        Destroy(gameObject);
    }

    void HandleMove()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < stoppingDistance)
        {
            return; // stop if too close
        }

        step = speed * Time.deltaTime * GameManager.level;

        // Move the object towards the target position
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.transform.position,
            step
        );
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
}
