using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    Text scoreText;
    GameLogic gameLogic;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        gameLogic = FindObjectOfType<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameLogic.GetScore().ToString();
    }
}
