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

    private Text coinText, healthText, timerText;
    private int coinScore;

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
    }
}
