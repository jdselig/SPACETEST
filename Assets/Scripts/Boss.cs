using DarkMagic;
using UnityEngine;

public class Boss : Enemy
{
    // Player player;
    // float speed;
    // float step;
    // float stoppingDistance = 0.5f;
    U.IDisplayHandle bossUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Boss!");
        player = GameObject.Find("Player").GetComponent<Player>();
        speed = 2 + GameManager.level;
        bossUI = U.Display(
            () => "BOSS " + (GameManager.level - 1),
            U.Placements.TopCenter,
            textColor: Color.red
        );
    }

    public override void Die()
    {
        // play an effect
        bossUI.Dispose();
        V.Broadcast<BossDied>();
        gameObject.SetActive(false);
    }

    // void HandleMove()
    // {
    //     if (Vector3.Distance(transform.position, player.transform.position) < stoppingDistance)
    //     {
    //         return; // stop if too close
    //     }

    //     step = speed * Time.deltaTime * GameManager.level;

    //     // Move the object towards the target position
    //     transform.position = Vector3.MoveTowards(
    //         transform.position,
    //         player.transform.position,
    //         step
    //     );
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     HandleMove();
    // }

    // void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log(other.gameObject.name);
    // }
}
