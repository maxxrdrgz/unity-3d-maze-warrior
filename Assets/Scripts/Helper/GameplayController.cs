using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    [HideInInspector]
    public bool isPlayerAlive;
    public float timerTime = 99f;
    public GameObject endPanel;
    public GameObject completedPanel;
    public Sprite[] stars;

    private Text coinText, healthText, timerText;
    private int coinScore;
    private int starScore;
    private int starIndex;
    private float totalPossibleScore;
    private float currentScore;

    private void Awake() {
        MakeInstance();

        coinText = GameObject.Find("CoinText").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();

        coinText.text = "Coins: " + coinScore;
    }
    // Start is called before the first frame update
    void Start()
    {
        isPlayerAlive = true;
        endPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CountdownTimer();

    }

    void MakeInstance(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public void CoinCollected(){
        coinScore++;
        coinText.text = "Coins: " +coinScore;
    }

    public void DisplayHealth(int health){
        healthText.text = "Health: " + health;
    }

    void CountdownTimer(){
        timerTime -= Time.deltaTime;
        timerText.text = "Time: " + timerTime.ToString("F0");

        if(timerTime <= 0){
            GameOver();
        }
    }

    public void GameOver(){
        Time.timeScale = 0f;
        endPanel.SetActive(true);

    }

    public void CompletedLevel(){
        Time.timeScale = 0f;
        endPanel.transform.Find("EndGame Text").GetComponent<Text>().text = "Complete";
        endPanel.transform.Find("Stars").GetComponent<Image>().sprite = stars[starScore];
        endPanel.SetActive(true);
    }

    private void CalculateStarScore(int health){
        //100 is for playerhealth
        totalPossibleScore = timerTime + 100;
        currentScore = (timerTime + health + coinScore) / totalPossibleScore;

        if(totalPossibleScore >= 0.75){
            starScore = 3;
        }else if(totalPossibleScore >= 0.5 && totalPossibleScore < .75){
            starScore = 2;
        }else if(totalPossibleScore >= .25 && totalPossibleScore < .5){
            starScore = 1;
        }else{
            starScore = 0;
        }
        GameManager.instance.StoreStarScore(starScore);
    }
}
