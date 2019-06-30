using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    [HideInInspector]
    public bool isPlayerAlive;
    public float timerTime;
    public GameObject endPanel;
    public GameObject coinParent;
    public Sprite[] stars;

    private Text coinText, healthText, timerText;
    private int coinScore;
    private int starScore;
    private int starIndex;
    private float totalPossibleScore;
    private float currentScore;
    private int playerhealth;
    private float initTime;


    private void Awake() {
        MakeInstance();
        Time.timeScale = 1f;
        coinText = GameObject.Find("CoinText").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();

        coinText.text = "Coins: " + coinScore;
        initTime = timerTime;
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
        if(timerTime >= 0){
            CountdownTimer();
        }
        if(!isPlayerAlive){
            GameOver();
            enabled = false;
        }
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
        playerhealth = health;
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
        SoundManager.instance.PlayGameOverSound();
        Time.timeScale = 0f;
        endPanel.SetActive(true);
        endPanel.transform.Find("EndGame Text").GetComponent<Text>().text = "Gameover";
        endPanel.transform.Find("Stars").GetComponent<Image>().sprite = stars[0];
    }

    public void CompletedLevel(){
        SoundManager.instance.PlayWinSound();
        Time.timeScale = 0f;
        CalculateStarScore(playerhealth);
        endPanel.transform.Find("EndGame Text").GetComponent<Text>().text = "Complete";
        endPanel.transform.Find("Stars").GetComponent<Image>().sprite = stars[starScore];
        endPanel.SetActive(true);
    }

    private void CalculateStarScore(int health){
        //100 is for playerhealth
        totalPossibleScore = initTime + 100 + coinParent.transform.childCount;
        currentScore = (timerTime + health + coinScore) / totalPossibleScore;

        if(currentScore >= 0.75){
            starScore = 3;
        }else if(currentScore >= 0.5 && currentScore < .75){
            starScore = 2;
        }else if(currentScore >= .25 && currentScore < .5){
            starScore = 1;
        }else if(currentScore < .25){
            starScore = 0;
        }
        GameManager.instance.StoreStarScore(starScore);
    }
    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevelSelect(){
        SceneManager.LoadScene("MainMenu");
    }
}
