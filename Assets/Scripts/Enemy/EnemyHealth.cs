using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;

    private EnemyScript enemyScript;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        enemyScript = GetComponent<EnemyScript>();
        anim = GetComponent<Animator>();
    }

    /** 
        This function will apply damage to the enemy. Once health is equal to
        0, the enemy will have it's collider disabled, this script will be
        disabled, the dead animation will be played and then invoke the
        deactivate enemy function.

        @param {int} amount of damage to apply to enemy health
    */
    public void ApplyDamage(int damageAmount){
        health -= damageAmount;
        if(health < 0){
            health = 0;
        }
        if(health == 0){
            gameObject.GetComponent<Collider>().enabled = false;
            enemyScript.enabled = false;
            anim.SetTrigger(Tags.DEAD_ANIMATION);
            Invoke("DeactivateEnemy", 3f);
        }
    }

    /** 
        This function deactivates the game object this script is attached to.
    */
    void DeactivateEnemy(){
        gameObject.SetActive(false);
    }
}
