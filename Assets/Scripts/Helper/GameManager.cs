using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [HideInInspector]
    public int totalStarScore;
    public bool[] levelsUnlocked;
    public int[] starsEarnedPerLevel;
    
    [HideInInspector]
    public int currentLevel;
    [HideInInspector]
    public int totalNumOfLevels;

    private void Awake() {
        MakeInstance();
        InitializeData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeInstance(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    void UpdateTotalStarScore(){
        totalStarScore = 0;
        for(int i = 0; i < starsEarnedPerLevel.Length; i++){
            totalStarScore += starsEarnedPerLevel[i];
        }
    }

    void InitializeData(){
        totalNumOfLevels = SceneManager.sceneCountInBuildSettings - 1;
        totalStarScore = 0;
        levelsUnlocked = new bool[totalNumOfLevels];
        levelsUnlocked[0] = true; // first level is always unlocked
        starsEarnedPerLevel = new int[totalNumOfLevels];
        // minus one for main menu scene
    }

    void UnlockLevel(int level){
        levelsUnlocked[level-1] = true;
    }

    public int GetStarsNeededForUnlock(int level){
        return level * 2;
    }

    public void StoreStarScore(int starsEarned){
        if(starsEarnedPerLevel[currentLevel-1] < starsEarned){
            starsEarnedPerLevel[currentLevel-1] = starsEarned;
            UpdateTotalStarScore();
            UpdateLockedLevels();
        }
    }

    public void UpdateLockedLevels(){
        for(int levelIndex = 1; levelIndex < totalNumOfLevels; levelIndex++){
            if(totalStarScore >= GetStarsNeededForUnlock(levelIndex)){
                levelsUnlocked[levelIndex] = true;
            }
        }
    }
}
