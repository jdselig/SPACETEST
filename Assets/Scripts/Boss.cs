using DarkMagic;
using UnityEngine;

public class Boss : Enemy
{
    U.IDisplayHandle bossUI;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        speed = 2 + GameManager.level;
        bossUI = U.Display(
            () => "BOSS " + (GameManager.level - 1),
            U.Placements.TopRight,
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
}
