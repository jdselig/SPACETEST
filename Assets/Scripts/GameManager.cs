using DarkMagic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    GameObject playerPrefab;
    GameObject enemyPrefab;
    GameObject bossPrefab;
    int score;
    public static int level = 1;
    int enemyCount;
    U.IDisplayHandle lowerHUD;
    int randomMin;
    int randomMax;
    public static bool isPaused = false;

    async void Start()
    {
        Pause();
        var result = await U.PopDialogue(
            "<align=center><size=110><color=#ffbb00><b>SPACETEST</b></color></size> <pbr/> Arrows/WASD to move, Spacebar to shoot, X for a bomb. <pbr/> Good luck!",
            textColor: Color.cyan,
            textSize: 40,
            panelColor: new Color(0, 0, 0, 0),
            borderSize: 0,
            placement: U.Placements.MiddleCenter
        );
        Pause(false);

        GeneratePlayer();
        GenerateEnemies();
        ListenForEvents();

        U.Display(
            () =>
                "SCORE: "
                + score
                + "\n<color=Colors.gold>LEVEL: "
                + level
                + "</color>"
                + "\n<color=Colors.green>HP: "
                + Player.hp
                + "</color>"
                + "\n<color=Colors.magenta>BOMBS: "
                + Player.bombStock
                + "</color>"
        );
    }

    // Pause(false) to unpause
    public static void Pause(bool pause = true)
    {
        if (pause)
        {
            Time.timeScale = 0f;
            GameManager.isPaused = true;
            // AudioListener.pause = true;
        }

        if (!pause)
        {
            Time.timeScale = 1f;
            GameManager.isPaused = false;
            // AudioListener.pause = false;
        }
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void ListenForEvents()
    {
        this.On<PlayerDamaged>(
            V.A<int>(dmg =>
            {
                Debug.Log("oof");
                score--; // health
            })
        );

        this.On<EnemyDied>(() =>
        {
            // play sound
            score++;
            enemyCount--;
            if (enemyCount <= 0)
            {
                GenerateBoss();
                level++;
            }
        });

        this.On<BossDied>(() =>
        {
            // play different sound
            score += 10000;
            GenerateEnemies();
            enemyCount--;
            V.Broadcast<RestockBombs>(level);
        });

        // this.On<PlayerDamaged, int>(dmg => Debug.Log(dmg));
    }

    void GeneratePlayer()
    {
        if (!playerPrefab)
            playerPrefab = Resources.Load<GameObject>("Player");
        var position = new Vector3(0, 0, -45);
        var player = Instantiate(playerPrefab, position, Quaternion.identity);
        player.name = "Player";
    }

    void GenerateBoss()
    {
        if (!bossPrefab)
            bossPrefab = Resources.Load<GameObject>("Boss");
        var position = new Vector3(0, 0, 50);
        var enemy = Instantiate(bossPrefab, position, Quaternion.identity);
        enemy.name = "BOSS" + (level - 1);
        enemyCount++;
    }

    void GenerateEnemies()
    {
        randomMin = Mathf.Clamp(5 * GameManager.level, 5, 80);
        randomMax = Mathf.Clamp(10 * GameManager.level, 10, 100);

        // find the enemy prefab
        if (!enemyPrefab)
            enemyPrefab = Resources.Load<GameObject>("Enemy");

        int random = Random.Range(randomMin, randomMax);
        for (int i = 0; i < random; i++)
        {
            var position = new Vector3(Random.Range(-90, 90), 0, 50);
            var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.name = "ENEMY" + i;
            enemyCount++;
        }

        int random2 = Random.Range(randomMin, randomMax);
        for (int i = 0; i < random2; i++)
        {
            var position = new Vector3(Random.Range(-90, 90), 0, 40);
            var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.name = "ENEMY" + i;
            enemyCount++;
        }

        int random3 = Random.Range(randomMin, randomMax);
        for (int i = 0; i < random3; i++)
        {
            var position = new Vector3(Random.Range(-90, 90), 0, 30);
            var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.name = "ENEMY" + i;
            enemyCount++;
        }
    }

    // Update is called once per frame
    void Update() { }
}
