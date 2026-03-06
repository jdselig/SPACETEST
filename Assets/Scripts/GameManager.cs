using DarkMagic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject enemyPrefab;
    GameObject bossPrefab;
    int score;
    public static int level = 1;
    int enemyCount;
    U.IDisplayHandle lowerHUD;
    int randomMin;
    int randomMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        var result = await U.PopDialogue(
            "SPACETEST - Arrows to move, spacebar to shoot, X for a bomb. \n Good luck!",
            textColor: Color.cyan,
            textSize: 40
        );
        // var result = await U.PopChoice(
        //     "SPACETEST - Arrows to move, spacebar to shoot, X for a bomb. Got it?",
        //     "Yes",
        //     "No"
        // );

        Debug.Log(result.Value);
        ListenForEvents();
        GenerateEnemies();
        U.Display(() => "SCORE: " + score);

        // lowerHUD = U.Display(
        //     () => "LEVEL " + GameManager.level,
        //     U.Placements.BottomRight,
        //     textColor: Color.skyBlue
        // );
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
