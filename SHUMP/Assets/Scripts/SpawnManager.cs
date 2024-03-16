using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;


public class SpawnManager : Singleton<SpawnManager>
{
    protected SpawnManager() { }
    
    public SpriteRenderer playerPrefab;

    [SerializeField]
    SpriteRenderer yellowSeedPrefab;

    [SerializeField]
    SpriteRenderer purpleBirdPrefab;

    [SerializeField]
    SpriteRenderer redBirdPrefab;

    [SerializeField]
    SpriteRenderer blueSeedPrefab;

    [SerializeField]
    SpriteRenderer poopPrefab;

    public List<SpriteRenderer> spawnedEnemy;

    public List<SpriteRenderer> spawnedBlueSeedsAndPoop;

    public List<SpriteRenderer> spawnedYellowSeeds;


    [SerializeField]
    float secondsBetweenSpawn;

    [SerializeField]
    float elapsedTime = 0.0f;

    [SerializeField]
    float halfElapsedTime = 1.5f;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float screenH = Camera.main.orthographicSize * 2f;
        float screenW = screenH * Camera.main.aspect;

        elapsedTime += Time.deltaTime;
        halfElapsedTime += Time.deltaTime;

        if (halfElapsedTime > secondsBetweenSpawn)
        {
            SpawnPurpleBird();
            halfElapsedTime = 1.0f;
        }

        if (elapsedTime > secondsBetweenSpawn)
        {
            SpawnRedBird();
            elapsedTime = 0.0f;
        }

        if (Keyboard.current.jKey.wasPressedThisFrame == true)
        {
            ShootSeed();
        }
    }


    // Spawn Purple Bird
    public void SpawnPurpleBird()
    {
        SpriteRenderer newEnemy = Instantiate(purpleBirdPrefab);
        SpriteRenderer newSeed = Instantiate(blueSeedPrefab);

        float screenH = Camera.main.orthographicSize * 2f;
        float screenW = screenH * Camera.main.aspect;

        float x = screenW - 8;
        float y = UnityEngine.Random.Range(-screenH + 6.5f, screenH - 10.0f);

        newEnemy.transform.position = new Vector3(x, y, 0);
        newSeed.transform.position = new Vector3(x-1.5f, y, 0);

        spawnedEnemy.Add(newEnemy);
        spawnedBlueSeedsAndPoop.Add(newSeed);
    }


    // Spawn Red Bird
    public void SpawnRedBird()
    {
        SpriteRenderer newEnemy = Instantiate(redBirdPrefab);
        SpriteRenderer newPoop = Instantiate(poopPrefab);

        float screenH = Camera.main.orthographicSize * 2f;
        float screenW = screenH * Camera.main.aspect;

        float x = screenW - 8;
        float y = UnityEngine.Random.Range(-screenH + 10.0f, screenH - 5.5f);

        newEnemy.transform.position = new Vector3(x, y, 0);
        newPoop.transform.position = new Vector3(x - 0.5f, y - 1.0f, 0);

        spawnedEnemy.Add(newEnemy);
        spawnedBlueSeedsAndPoop.Add(newPoop);
    }


    // spawn player's seeds
    void ShootSeed()
    {
        SpriteRenderer newSeed = Instantiate(yellowSeedPrefab);
        newSeed.transform.position = new Vector3(playerPrefab.transform.position.x + 1.5f, playerPrefab.transform.position.y, 0);

        spawnedYellowSeeds.Add(newSeed);
    }
}
