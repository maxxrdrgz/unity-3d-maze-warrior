using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    private PlayerScript playerScript;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        playerScript = GetComponent<PlayerScript>();
        anim = GetComponent<Animator>();
    }

    private void Start() {
        GameplayController.instance.DisplayHealth(health);
    }

    public void ApplyDamage(int damageAmount){
        health -= damageAmount;
        if(health < 0){
            health = 0;
        }
        GameplayController.instance.DisplayHealth(health);
        if(health == 0){
            playerScript.enabled = false;
            anim.Play(Tags.DEAD_ANIMATION);
            GameplayController.instance.isPlayerAlive = false;
            GameplayController.instance.GameOver();
        }
    }
}
