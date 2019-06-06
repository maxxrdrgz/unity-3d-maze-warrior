﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button[] levels;

    public Animator anim;

    public void PlayGame(){
        anim.Play("SlideIn");
    }

    public void Back(){
        anim.Play("SlideOut");
    }

    public void LoadLevel(int level){
        SceneManager.LoadScene("Level"+level);
    }
}
