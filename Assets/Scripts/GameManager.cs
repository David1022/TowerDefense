using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Text enemyTurnText;
    public Text enemyTimeText;
    public Text enemyLifeText;
    public Text playerTurnText;
    public Text playerTimeText;
    public Text playerLifeText;
    public Text countDownTurnText;
    public Text countDownTimeText;

    public int countDown;

    public const int ENEMY_DEATH_SCORE = 100;
    public const int LEVEL_FINAL_SCORE = 1000;

    public Turn turn;
    
    public enum Turn {
        PAUSED,
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
        turn = Turn.PLAYER;

        InitializeTexts();
    }

    private void InitializeTexts()
    {
        enemyTurnText.color = Color.red;
        playerTurnText.color = Color.green;

        enemyTurnText.text = "Turn: Computer";
        enemyTimeText.text = "Time: 20s";
        enemyLifeText.text = "Life: 100";
        playerTurnText.text = "Turn: Player";
        playerTimeText.text = "Time: 20s";
        playerLifeText.text = "Life: 100";

        countDownTurnText.gameObject.SetActive(false);
        countDownTimeText.gameObject.SetActive(false);
    }

    private void Update()
    {

    }

    public void ChangeTurn()
    {
        if (turn == Turn.ENEMY)
        {
            enemyTurnText.color = Color.red;
            playerTurnText.color = Color.green;
            countDownTurnText.text = "Player";
        }
        else if (turn == Turn.PLAYER)
        {
            enemyTurnText.color = Color.green;
            playerTurnText.color = Color.red;
            countDownTurnText.text = "Computer";
        }

        turn = Turn.PAUSED;

        Invoke("StartCountDown", 2);
    }

    void StartCountDown() {
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown() {
        countDownTurnText.gameObject.SetActive(true);
        countDownTimeText.gameObject.SetActive(true);

        for (countDown = 3; countDown > 0; countDown--) {
            countDownTimeText.text = countDown.ToString();
            yield return new WaitForSeconds(1f);
        }
        
        turn = (turn == Turn.PLAYER) ? Turn.ENEMY : Turn.PLAYER;

        countDownTurnText.gameObject.SetActive(false);
        countDownTimeText.gameObject.SetActive(false);
    }
}
