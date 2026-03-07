using DarkMagic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    float speed = 20;
    float step;
    float lifetime = 30;
    MeshRenderer[] meshes;
    bool exploded = false;

    void Start()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
    }

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

    async void HandleLifetime()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            await Explode();
        }
    }

    async Awaitable Explode()
    {
        exploded = true;
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].enabled = false;
        }

        var aoe = transform.Find("AOESphere")?.gameObject;
        if (aoe)
        {
            aoe.SetActive(true);
            await Awaitable.WaitForSecondsAsync(.6f);
            Destroy(aoe);
        }
    }

    async void OnTriggerEnter(Collider other)
    {
        if (exploded)
            return;

        var tryEnemy = other.gameObject?.GetComponent<Enemy>();

        if (tryEnemy)
        {
            var enemy = tryEnemy;
            await Explode();
        }
    }
}
