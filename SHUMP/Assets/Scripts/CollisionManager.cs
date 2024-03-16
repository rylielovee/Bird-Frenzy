using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    SpawnManager spawnManager;

    public int playerHealth = 100;

    public int playerScore = 0;

    public int enemyHealth = 100;

    public Text GameOverLabel;

    // Start is called before the first frame update
    void Start()
    {
        GameOverLabel.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            GameOverLabel.text = "GAME OVER";
            //Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }

        if (enemyHealth <= 0)
        {
            GameOverLabel.text = "YOU WIN!";
            //Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }


        // this checks yellow seeds vs enemies
        if (spawnManager.spawnedEnemy != null)
        {
            if (spawnManager.spawnedYellowSeeds != null)
            {
                for (int i = spawnManager.spawnedEnemy.Count - 1; i >= 0; i--)
                {
                    for (int k = spawnManager.spawnedYellowSeeds.Count - 1; k >= 0; k--)
                    {
                        if (CircleCollisionCheck(spawnManager.spawnedYellowSeeds[k], spawnManager.spawnedEnemy[i]) == true)
                        {
                            playerScore += 25;

                            enemyHealth -= 10;

                            Destroy(spawnManager.spawnedYellowSeeds[k].gameObject);
                            spawnManager.spawnedYellowSeeds.Remove(spawnManager.spawnedYellowSeeds[k]);

                            Destroy(spawnManager.spawnedEnemy[i].gameObject);
                            spawnManager.spawnedEnemy.Remove(spawnManager.spawnedEnemy[i]);
                        }
                    }
                }
            }
        }


        // this checks yellow seeds vs blue seeds / poop
        if (spawnManager.spawnedBlueSeedsAndPoop != null)
        {
            if (spawnManager.spawnedYellowSeeds != null)
            {
                for (int i = spawnManager.spawnedBlueSeedsAndPoop.Count - 1; i >= 0; i--)
                {
                    for (int k = spawnManager.spawnedYellowSeeds.Count - 1; k >= 0; k--)
                    {
                        if (CircleCollisionCheck(spawnManager.spawnedYellowSeeds[k], spawnManager.spawnedBlueSeedsAndPoop[i]) == true)
                        {
                            playerScore += 10;

                            enemyHealth -= 5;

                            Destroy(spawnManager.spawnedYellowSeeds[k].gameObject);
                            spawnManager.spawnedYellowSeeds.Remove(spawnManager.spawnedYellowSeeds[k]);

                            Destroy(spawnManager.spawnedBlueSeedsAndPoop[i].gameObject);
                            spawnManager.spawnedBlueSeedsAndPoop.Remove(spawnManager.spawnedBlueSeedsAndPoop[i]);
                        }
                    }
                }
            } 
        }


        // this checks player vs enemies
        if (spawnManager.spawnedEnemy != null)
        {
            for (int i = spawnManager.spawnedEnemy.Count - 1; i >= 0; i--)
            {
                if (CircleCollisionCheck(spawnManager.playerPrefab, spawnManager.spawnedEnemy[i]) == true)
                {
                    playerHealth -= 20;

                    Destroy(spawnManager.spawnedEnemy[i].gameObject);
                    spawnManager.spawnedEnemy.Remove(spawnManager.spawnedEnemy[i]);
                }
            }

            for (int i = spawnManager.spawnedEnemy.Count - 1; i >= 0; i--)
            {
                if (EnemyOutOfBounds(spawnManager.spawnedEnemy[i]) == true)
                {
                    Destroy(spawnManager.spawnedEnemy[i].gameObject);
                    spawnManager.spawnedEnemy.Remove(spawnManager.spawnedEnemy[i]);
                }
            }
        }

        // this checks player vs blue seeds / poop
        if (spawnManager.spawnedBlueSeedsAndPoop != null)
        {
            for (int i = spawnManager.spawnedBlueSeedsAndPoop.Count - 1; i >= 0; i--)
            {
                if (CircleCollisionCheck(spawnManager.playerPrefab, spawnManager.spawnedBlueSeedsAndPoop[i]) == true)
                {
                    playerHealth -= 10;

                    Destroy(spawnManager.spawnedBlueSeedsAndPoop[i].gameObject);
                    spawnManager.spawnedBlueSeedsAndPoop.Remove(spawnManager.spawnedBlueSeedsAndPoop[i]);
                }
            }

            for (int i = spawnManager.spawnedBlueSeedsAndPoop.Count - 1; i >= 0; i--)
            {
                if (EnemyOutOfBounds(spawnManager.spawnedBlueSeedsAndPoop[i]) == true)
                {
                    Destroy(spawnManager.spawnedBlueSeedsAndPoop[i].gameObject);
                    spawnManager.spawnedBlueSeedsAndPoop.Remove(spawnManager.spawnedBlueSeedsAndPoop[i]);
                }
            }
        }


        if (spawnManager.spawnedYellowSeeds != null)
        {
            for (int i = spawnManager.spawnedYellowSeeds.Count - 1; i >= 0; i--)
            {
                if (YellowSeedOutOfBounds(spawnManager.spawnedYellowSeeds[i]) == true)
                {
                    Destroy(spawnManager.spawnedYellowSeeds[i].gameObject);
                    spawnManager.spawnedYellowSeeds.Remove(spawnManager.spawnedYellowSeeds[i]);
                }
            }
        }
    }


    // collision check for everything
    public bool CircleCollisionCheck(SpriteRenderer spriteA, SpriteRenderer spriteB)
    {
        float combinedRadius = spriteA.bounds.extents.y + spriteB.bounds.extents.y;

        float xNum = (spriteA.transform.position.x) - (spriteB.transform.position.x);
        xNum = Mathf.Pow(xNum, 2);

        float yNum = (spriteA.transform.position.y) - (spriteB.transform.position.y);
        yNum = Mathf.Pow(yNum, 2);

        if (Mathf.Sqrt(xNum + yNum) <= combinedRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // checks if enemy birds and enemy seeds/poop are out of bounds
    public bool EnemyOutOfBounds(SpriteRenderer sprite)
    {
        float screenH = Camera.main.orthographicSize * 2f;
        float screenW = screenH * Camera.main.aspect;

        if ((sprite.transform.position.x < -screenW))
        {
            return true;
        }
        else if ((sprite.transform.position.x < -screenH))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // checks if player seeds are out of bounds
    public bool YellowSeedOutOfBounds(SpriteRenderer sprite)
    {
        float screenH = Camera.main.orthographicSize * 2f;
        float screenW = screenH * Camera.main.aspect;

        if ((sprite.transform.position.x > screenW))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
