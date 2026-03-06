using DarkMagic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    GameObject bulletPrefab;
    GameObject bombPrefab;
    float coolDown = 0;
    float bulletCoolSpeed = 2;
    int bulletCount = 0;
    int bombCount = 0;
    int bombStock = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        if (speed == 0)
        {
            speed = 30;
        }

        U.Display(() => "BOMBS: " + bombStock, U.Placements.TopRight);

        this.On<RestockBombs>(async amount =>
        {
            bombStock += (int)amount;
            await U.PopBanner("Got " + amount + " bombs!", size: U.Sizes.Inline);
        });
    }

    // Update is called once per frame
    void Update()
    {
        float hMove = I.GetAxis("horizontal");
        float vMove = I.GetAxis("vertical");

        if (hMove != 0)
        {
            transform.AddPosX(hMove * speed * Time.deltaTime);
        }

        if (vMove != 0)
        {
            // Boundaries
            if (vMove < 0 && transform.position.z <= -45)
                return;

            if (vMove > 0 && transform.position.z >= -17.5f)
                return;

            transform.AddPosZ(vMove * speed * Time.deltaTime);
        }

        if (I.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (I.GetKeyDown(KeyCode.X))
        {
            Bomb();
        }

        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime * bulletCoolSpeed;
        }
        else
        {
            coolDown = 0;
        }
    }

    void Shoot()
    {
        if (!bulletPrefab)
            bulletPrefab = Resources.Load<GameObject>("PlayerBullet");

        if (coolDown > 0)
            return;

        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.name = "BULLET" + bulletCount;

        coolDown = 1;
        bulletCount++;
    }

    void Bomb()
    {
        if (!bombPrefab)
            bombPrefab = Resources.Load<GameObject>("PlayerBomb");

        if (bombStock <= 0)
        {
            return;
        }

        if (coolDown > 0)
            return;

        var bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        bomb.name = "BOMB" + bombCount;

        coolDown = 1;
        bombCount++;
        bombStock--;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}

// public class _5
// {

// }
