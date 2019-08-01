using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject damagePoint;

    private GameObject player;
    private Rigidbody rbody;
    private Animator anim;
    private float enemy_speed = 10f;
    //distance between enemy and player
    private float enemy_watch_threshold = 70f;
    private float enemy_attack_threshold = 6f;


    private void Awake() {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    /** 
        This function will stop all enemies from moving if the player is dead.
    */
    private void FixedUpdate() {
        if(GameplayController.instance.isPlayerAlive){
            EnemyAI();
        }else{
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.RUN_ANIMATION) ||
               anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.ATTACK_ANIMATION)){
                
                anim.SetTrigger(Tags.STOP_TRIGGER);
            }
        }
    }

    /** 
        This function controls all of the enemy AI. This function first checks,
        if the player is within a certain distance of the enemy, if so, the
        enemy will face towards the players direction and start moving towards
        the player. Once the player is within the attack distance, the enemy 
        will stop running, and start the attacking phase. Lastly, if the player
        is outside of the enemy watch threshold, the enemy will stop moving and
        stand still.
    */
    void EnemyAI(){
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        Vector3 velocity = direction * enemy_speed;

        if(distance > enemy_attack_threshold && 
           distance < enemy_watch_threshold){
            
            rbody.velocity = new Vector3(
                velocity.x, 
                rbody.velocity.y, 
                velocity.z
            );
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.ATTACK_ANIMATION)){
                anim.SetTrigger(Tags.STOP_TRIGGER);
            }

            anim.SetTrigger(Tags.RUN_ANIMATION);
            transform.LookAt(new Vector3(
                player.transform.position.x, 
                player.transform.position.y, 
                player.transform.position.z
            ));
        } else if(distance < enemy_attack_threshold){
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.RUN_ANIMATION)){
                anim.SetTrigger(Tags.STOP_TRIGGER);
            }
            anim.SetTrigger(Tags.ATTACK_ANIMATION);
            transform.LookAt(new Vector3(
                player.transform.position.x, 
                player.transform.position.y, 
                player.transform.position.z
            ));
        } else{
            rbody.velocity = new Vector3(0f, 0f, 0f);
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.RUN_ANIMATION) ||
               anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.ATTACK_ANIMATION)){

                anim.SetTrigger(Tags.STOP_TRIGGER);
            }
        }
    }
    
    /** 
        Activates gameobject that can be found on the enemies sword.
    */
    void ActivateDamagePoint(){
        damagePoint.SetActive(true);
    }

    /** 
        Deactivates gameobject that can be found on the enemies sword.
    */
    void DeactivateDamagePoint(){
        damagePoint.SetActive(false);
    }
}
