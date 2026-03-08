using DarkMagic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    GameObject bulletPrefab;
    GameObject bombPrefab;
    float coolDown = 0;
    float bulletCoolSpeed = 2;
    int bulletCount = 0;
    int bombCount = 0;
    public static int bombStock = 3;
    public static int hp = 5; // refactor to stat

    void Start()
    {
        if (speed == 0)
        {
            speed = 30;
        }

        this.On<RestockBombs>(async amount =>
        {
            bombStock += (int)amount;
            await U.PopBanner("Got " + amount + " bombs!", size: U.Sizes.Inline);
        });
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;

        if (hp <= 0)
        {
            Die();
        }
    }

    async void Die()
    {
        GameManager.Pause();
        var result = await U.PopChoice(
            "Well, you died.\vGive up, or try again?",
            "Try again",
            "Give up"
        );
        if (result.Value == "Try again")
        {
            GameManager.Pause(false);
            // await SceneManager.UnloadSceneAsync("DontDestroyOnLoad");
            // Destroy(GameObject.Find("U_Root"));
            SceneManager.LoadScene("Loader");
        }
        else
        {
            GameManager.Quit();
        }
    }

    void Update()
    {
        if (I.GetKeyDown(KeyCode.P))
        {
            bool arg = !GameManager.isPaused;
            GameManager.Pause(arg);
        }

        if (GameManager.isPaused)
        {
            return;
        }

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

        // Debug
        if (I.GetKeyDown(KeyCode.K))
        {
            TakeDamage(5);
        }

        // // Debug
        // if (I.GetKeyDown(KeyCode.P))
        // {
        //     bool arg = !GameManager.isPaused;
        //     GameManager.Pause(arg);
        // }

        //Debug
        if (I.GetKeyDown(KeyCode.B))
        {
            bombStock += 5;
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
