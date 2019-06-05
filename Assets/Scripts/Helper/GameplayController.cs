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
    private int startScore;
    private int starIndex;

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
        endPanel.transform.Find("Stars").GetComponent<Image>().sprite = stars[1];
        endPanel.SetActive(true);
    }

    private void CalculateStarScore(){
        
    }
}
