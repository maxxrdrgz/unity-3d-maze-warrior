using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public LayerMask playerLayer;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    /** 
        This function will create an overlap sphere collider on the playerlayer,
        to detect collisions with the player. If true, deal damage to the player.
    */
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position, 
            0.1f, 
            playerLayer
        );    

        if(hits.Length > 0){
            if(hits[0].gameObject.tag == Tags.PLAYER_TAG){
                hits[0].gameObject
                    .GetComponent<PlayerHealth>()
                    .ApplyDamage(damageAmount);
                
                
            }
        }
    }
}
