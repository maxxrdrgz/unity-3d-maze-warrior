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
    
    /** 
        Creates a singleton that persists after loading a new scene
    */
    void MakeInstance(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    
    /** 
        Adds up all the stars earned for each level
    */
    void UpdateTotalStarScore(){
        totalStarScore = 0;
        for(int i = 0; i < starsEarnedPerLevel.Length; i++){
            totalStarScore += starsEarnedPerLevel[i];
        }
    }

    /** 
        Initializes gameplay data which includes getting the total number of
        levels, ensuring the first level is unlocked and setting stars to 0.
    */
    void InitializeData(){
        totalNumOfLevels = SceneManager.sceneCountInBuildSettings - 1;
        totalStarScore = 0;
        levelsUnlocked = new bool[totalNumOfLevels];
        levelsUnlocked[0] = true; // first level is always unlocked
        starsEarnedPerLevel = new int[totalNumOfLevels];
        // minus one for main menu scene
    }

    /** 
        Set's the level index to true

        @param {int} the level #
    */
    void UnlockLevel(int level){
        levelsUnlocked[level-1] = true;
    }

    /** 
        Simple formula to determine how many accumulated stars are needed to 
        unlock a level.

        @param {int} the level #
    */
    public int GetStarsNeededForUnlock(int level){
        return level * 2;
    }

    /** 
        Stores the number of stars earned for a given level in an array, updates
        the total star score and updates the locked levels array.

        @param {int} the number of stars earned for the given level
    */
    public void StoreStarScore(int starsEarned){
        if(starsEarnedPerLevel[currentLevel-1] < starsEarned){
            starsEarnedPerLevel[currentLevel-1] = starsEarned;
            UpdateTotalStarScore();
            UpdateLockedLevels();
        }
    }

    /** 
        Updates the status of the level index to true if the level has been
        unlocked.
    */
    public void UpdateLockedLevels(){
        for(int levelIndex = 1; levelIndex < totalNumOfLevels; levelIndex++){
            if(totalStarScore >= GetStarsNeededForUnlock(levelIndex)){
                levelsUnlocked[levelIndex] = true;
            }
        }
    }
}
