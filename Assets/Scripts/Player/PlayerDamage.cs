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

    // Update is called once per frame
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
            }
        }
    }
}
