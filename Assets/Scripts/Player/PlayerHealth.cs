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

    public void ApplyDamage(int damageAmount){
        health -= damageAmount;
        if(health < 0){
            health = 0;
        }
        //display health value
        if(health == 0){
            playerScript.enabled = false;
            anim.Play(Tags.DEAD_ANIMATION);
            //call game over panel
        }
    }
}
