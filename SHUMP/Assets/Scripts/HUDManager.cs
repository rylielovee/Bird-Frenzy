using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //use this for all the UI stuff

public class HudManager : MonoBehaviour
{
    [SerializeField]
    CollisionManager collisionManager;

    [SerializeField]
    Text scoreLabel;

    [SerializeField]
    Slider healthSlider;

    [SerializeField]
    Slider enemySlider;

    const string k_SCORE_STR = "Score: {0}";


    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = 100;
        enemySlider.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = collisionManager.playerHealth;

        enemySlider.value = collisionManager.enemyHealth;

        scoreLabel.text = string.Format(k_SCORE_STR, collisionManager.playerScore);
    }
}
