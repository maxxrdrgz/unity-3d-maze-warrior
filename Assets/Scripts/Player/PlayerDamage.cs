using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int damageAmount = 15;
    public LayerMask enemyLayer;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    /** 
        This function will create an overlap sphere collider on the enemylayer,
        to detect collisions with the enemy. If true, deal damage to the enemy.
    */
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position, 
            0.6f, 
            enemyLayer
        );    

        if(hits.Length > 0){
            if(hits[0].gameObject.tag == Tags.ENEMY_TAG){
                hits[0].gameObject
                    .GetComponent<EnemyHealth>()
                    .ApplyDamage(damageAmount);
                SoundManager.instance.PlayHitSound();
            }
        }
    }
}
