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

    void DeactivateEnemy(){
        gameObject.SetActive(false);
    }
}
