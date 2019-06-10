using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button[] levels;
    public Animator anim;
    public Sprite[] stageUnlock;
    public Sprite stageLocked;
    public Text starScoreText;


    private void Start() {
        GameManager.instance.UpdateLockedLevels();
        UpdateLevelStatus();
        UpdateStarScoreText();

    }

    public void PlayGame(){
        anim.Play("SlideIn");
    }

    public void Back(){
        anim.Play("SlideOut");
    }

    void LoadLevel(int level){
        GameManager.instance.currentLevel = level;
        SceneManager.LoadScene("Level"+level);
    }

    public void checkIfLevelIsLocked(int level){
        if(GameManager.instance.levelsUnlocked[level-1]){
            LoadLevel(level);
        }
    }

    void UpdateLevelStatus(){
        for(int levelIndex = 1; levelIndex < GameManager.instance.totalNumOfLevels; levelIndex++){
            if(GameManager.instance.levelsUnlocked[levelIndex]){
                levels[levelIndex].GetComponent<Image>().sprite = stageUnlock[0];
                levels[levelIndex].GetComponentInChildren<Text>().text = "Unlocked";
            }
        }
    }

    void UpdateStarScoreText(){
        starScoreText.text = "Total Stars: " + GameManager.instance.totalStarScore;
    }
}
