﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    int TOWER_TIME_TURN = 20;
    int ENEMY_TIME_TURN = 5;
    int TOWER_LIFE = 100;
    int ENEMY_LIFE = 100;
    int TOWER_DAMAGE = 15;
    int ENEMY_DAMAGE = 7;

    public static GameManager instance;
    GameObject tower;
    DefenseController towerController;
    GameObject enemy;
    EnemyController enemyController;

    public Text enemyTurnText;
    public Text enemyTimeText;
    public Text enemyLifeText;
    public Text playerTurnText;
    public Text playerTimeText;
    public Text playerLifeText;
    public Text countDownTurnText;
    public Text countDownTimeText;

    public int changingTurnCountDown;

    public Turn turn;
    int TowerTurnTime;
    int TankTurnTime;
    int towerLifePoints;
    int enemyLifePoints;

    public enum Turn {
        PLAYER,
        ENEMY
    }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        Initialize();
        SetInitialTurn();
    }

    private void Initialize()
    {
        tower = GameObject.FindWithTag("Tower");
        towerController = tower.GetComponent<DefenseController>();
        enemy = GameObject.FindWithTag("Enemy");
        enemyController = enemy.GetComponent<EnemyController>();

        enemyLifePoints = ENEMY_LIFE;
        towerLifePoints = TOWER_LIFE;

        enemyTurnText.color = Color.red;
        playerTurnText.color = Color.green;
        enemyTurnText.text = "Turn: Computer";
        playerTurnText.text = "Turn: Player";

        countDownTurnText.gameObject.SetActive(false);
        countDownTimeText.gameObject.SetActive(false);
    }

    private void SetInitialTurn()
    {
        turn = Turn.PLAYER;
        TowerTurnTime = TOWER_TIME_TURN;
        towerController.StartTurn();
        InvokeRepeating("TowerTurnCountDown", 1f, 1f);
    }

    private void Update()
    {
        if (towerLifePoints > 0 && enemyLifePoints > 0)
        {
            if (turn == Turn.PLAYER)
            {
                if (TowerTurnTime < 0)
                {
                    ChangeTurn();
                }
                else
                {
                    enemyTimeText.text = "Time: " + ENEMY_TIME_TURN + "s";
                    playerTimeText.text = "Time: " + TowerTurnTime + "s";
                }
            }
            else if (turn == Turn.ENEMY)
            {
                if (TankTurnTime < 0)
                {
                    ChangeTurn();
                }
                else
                {
                    enemyTimeText.text = "Time: " + TankTurnTime + "s";
                    playerTimeText.text = "Time: " + TOWER_TIME_TURN + "s";
                }
            }
            enemyLifeText.text = "Life: " + enemyLifePoints;
            playerLifeText.text = "Life: " + towerLifePoints;
        }
        else {
            SceneManager.LoadScene("EndScene");

        }
    }

    public void ChangeTurn()
    {
        CancelInvoke();
        if (turn == Turn.ENEMY)
        {
            enemyTurnText.color = Color.red;
            playerTurnText.color = Color.green;
            countDownTurnText.text = "Player";
            TowerTurnTime = TOWER_TIME_TURN;
            TankTurnTime = ENEMY_TIME_TURN;
        }
        else if (turn == Turn.PLAYER)
        {
            enemyTurnText.color = Color.green;
            playerTurnText.color = Color.red;
            countDownTurnText.text = "Computer";
            TankTurnTime = ENEMY_TIME_TURN;
            TowerTurnTime = TOWER_TIME_TURN;
        }

        StartCoroutine("ChangingTurnCountDown");
    }

    IEnumerator ChangingTurnCountDown() {
        yield return new WaitForSeconds(0.25f);

        countDownTurnText.gameObject.SetActive(true);
        countDownTimeText.gameObject.SetActive(true);

        for (changingTurnCountDown = 3; changingTurnCountDown > 0; changingTurnCountDown--) {
            countDownTimeText.text = changingTurnCountDown.ToString();
            yield return new WaitForSeconds(1f);
        }
        
        turn = (turn == Turn.PLAYER) ? Turn.ENEMY : Turn.PLAYER;

        countDownTurnText.gameObject.SetActive(false);
        countDownTimeText.gameObject.SetActive(false);

        StartNextTurn();
    }

    private void StartNextTurn() {
        if (turn == Turn.PLAYER) {
            towerController.StartTurn();
            //TowerTurnTime = TOWER_TIME_TURN;
            InvokeRepeating("TowerTurnCountDown", 1f, 1f);
        }
        else if (turn == Turn.ENEMY) {
            enemyController.StartTurn();
            //TankTurnTime = ENEMY_TIME_TURN;
            InvokeRepeating("TankTurnCountDown", 1f, 1f);
        }
    }

    void TowerTurnCountDown() 
    {
        TowerTurnTime--;
    }

    void TankTurnCountDown()
    {
        TankTurnTime--;
    }

    public void SubstractLifePoints()
    {
        if (turn == Turn.PLAYER) {
            enemyLifePoints -= TOWER_DAMAGE;
        } else if (turn == Turn.ENEMY) {
            towerLifePoints -= ENEMY_DAMAGE;
        }
    }

    public void CancelCountDowns(){
        CancelInvoke();
    }
}
