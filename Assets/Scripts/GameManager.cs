using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int time;
    public int score;

    public Text timeLabel;
    public Text scoreLabel;

    public const int ENEMY_DEATH_SCORE = 100;
    public const int LEVEL_FINAL_SCORE = 1000;

    public Turn turn;
    
    public enum Turn {
        PLAYER,
        ENEMY
    }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        //        mushroomController = GetComponent<QuestionMushroomController>();

        turn = Turn.PLAYER;

        time = 0;
        score = 0;

        timeLabel.text = "Time : " + time + " s";
        scoreLabel.text = "Score : " + score;

        InvokeRepeating("Chrono", 1f, 1f);
    }

    void Chrono() {
        time++;
        timeLabel.text = "Time : " + time + " s";
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreLabel.text = "Score : " + score;
    }

    // Update is called once per frame
    void Update () {
    }

    public void finishTurn(){

    }
}
