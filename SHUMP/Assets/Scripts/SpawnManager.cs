using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnManager : Singleton<SpawnManager>
{
    public enum EnemyTypes
    {
        PurpleBird,
        RedBird
    }

    protected SpawnManager() { }

    [SerializeField]
    SpriteRenderer playerPrefab;

    [SerializeField]
    SpriteRenderer enemyPrefab;

    [SerializeField]
    SpriteRenderer seedPrefab;

    [SerializeField]
    List<Sprite> enemySprites;


    public List<SpriteRenderer> spawnedEnemy;
    public List<SpriteRenderer> spawnedSeeds;

    [SerializeField]
    float secondsBetweenSpawn;

    [SerializeField]
    float elapsedTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn)
        {
            SpawnEnemy();
            elapsedTime = 0.0f;
        }

        for (int i = 0; i < spawnedEnemy.Count; i++)
        {
            if (spawnedEnemy[i] != null)
            {
                CircleCollisionCheck(playerPrefab, spawnedEnemy[i]);
            }
            if (spawnedSeeds[i] != null)
            {
                CircleCollisionCheck(playerPrefab, spawnedSeeds[i]);
            }
        }
    }


    public void SpawnEnemy()
    {
        SpriteRenderer newEnemy = Instantiate(enemyPrefab);
        SpriteRenderer newSeed = Instantiate(seedPrefab);

        // change sprite
        newEnemy.sprite = enemySprites[(int)PickRandomEnemy()];  // need to cast as a int because it is technically stored as an int

        float screenH = Camera.main.orthographicSize * 2f;
        float screenW = screenH * Camera.main.aspect;

        float x = screenW - 8;
        float y = Random.Range(-screenH + 5.5f, screenH - 5.5f);

        newEnemy.transform.position = new Vector3(x, y, 0);
        newSeed.transform.position = new Vector3(x-1.5f, y, 0);

        spawnedEnemy.Add(newEnemy);

        spawnedSeeds.Add(newSeed);
    }


    EnemyTypes PickRandomEnemy()
    {
        float randValue = Random.Range(0f, 1f);

        if (randValue < .5f)
        {
            return EnemyTypes.PurpleBird;
        }
        else
        {
            return EnemyTypes.RedBird;
        }

    }


    void CircleCollisionCheck(SpriteRenderer spriteA, SpriteRenderer spriteB)
    {
        float combinedRadius = spriteA.bounds.extents.y + spriteB.bounds.extents.y;

        float xNum = (spriteA.transform.position.x) - (spriteB.transform.position.x);
        xNum = Mathf.Pow(xNum, 2);

        float yNum = (spriteA.transform.position.y) - (spriteB.transform.position.y);
        yNum = Mathf.Pow(yNum, 2);

        if (Mathf.Sqrt(xNum + yNum) <= combinedRadius)
        {
            Destroy(spriteB);
        }
    }

}
