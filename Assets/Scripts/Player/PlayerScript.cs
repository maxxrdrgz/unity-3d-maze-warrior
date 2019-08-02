using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject damagePoint;

    private Rigidbody rbody;
    private Animator anim;
    private bool isPlayerMoving;
    private float playerSpeed = 0.5f;
    private float rotationSpeed = 4f;
    private float jumpForce = 3f;
    private bool canJump;
    private float moveHorizontal, moveVertical;
    private float rotY = 0f;

    private void Awake() {
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rotY = transform.localRotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        Attack();
        IsOnGround();
        Jump();
    }

    private void FixedUpdate() {
        MoveAndRotate();
    }

    /** 
        Depending on the input from the user, move horizontal will make the
        player move forward while move veritcal will rotate the player.
    */
    void PlayerMoveKeyboard(){
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            moveHorizontal = -1;
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) ||
           Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) ){
            moveHorizontal = 0;
        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            moveHorizontal = 1;
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            moveVertical = 1;
        }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)){
            moveVertical = 0;
        }
    }

    /** 
        Changes the player's rigid body to actually move the player
    */
    void MoveAndRotate(){
        if(moveVertical != 0){
            rbody.MovePosition(
                transform.position + transform.forward * 
                (moveVertical * playerSpeed)
            );
        }
        rotY += moveHorizontal * rotationSpeed;
        rbody.rotation = Quaternion.Euler(0f, rotY, 0f);
    }

    /** 
        Animates the player's movement based on it's current state.
    */
    void AnimatePlayer(){
        if(moveVertical != 0){
            if(!isPlayerMoving){
                if(!anim.GetCurrentAnimatorStateInfo(0)
                        .IsName(Tags.RUN_ANIMATION)){
                    isPlayerMoving = true;
                    anim.SetTrigger(Tags.RUN_TRIGGER);
                }
            }
        }else{
            if(isPlayerMoving){
                if(anim.GetCurrentAnimatorStateInfo(0)
                        .IsName(Tags.RUN_ANIMATION)){
                    isPlayerMoving = false;
                    anim.SetTrigger(Tags.STOP_TRIGGER);
                }
            }
        }
    }

    /** 
        When the user presses the k key, the players attack animation is played
        if the player is not running or already attacking.
    */
    void Attack(){
        if(Input.GetKeyDown(KeyCode.K)){
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.RUN_ANIMATION) ||
               !anim.GetCurrentAnimatorStateInfo(0).IsName(Tags.ATTACK_ANIMATION)){
                
                anim.SetTrigger(Tags.ATTACK_TRIGGER);
            }
        }
    }

    /** 
        Checks if player is on the ground. If so, the player can jump.
    */
    void IsOnGround(){
        if(!canJump){
            canJump = Physics.Raycast(
                groundCheck.position, 
                Vector3.down, 
                0.1f, 
                groundLayer
            );
        }
    }

    /** 
        When the user presses the space bar, if the player is grounded, this 
        function will play the jump animation and the jump sound.
    */
    void Jump(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(canJump){
                canJump = false;
                anim.SetTrigger(Tags.JUMP_TRIGGER);
                SoundManager.instance.PlayJumpSound();
            }
        }
    }

    /** 
        Activates gameobject that can be found on the player's sword.
    */
    void ActivateDamagePoint(){
        damagePoint.SetActive(true);
    }

    /** 
        Deactivates gameobject that can be found on the player's sword.
    */
    void DeactivateDamagePoint(){
        damagePoint.SetActive(false);
    }
    
    /** 
        Detects if the player has collided with either the treasure, a coin or
        if the player has collided with a doors entry point and triggers the 
        door open animation.

        @param {Collider} A Collider involved in this collision.
    */
    private void OnTriggerEnter(Collider other) {
        if(other.tag == Tags.COIN_TAG){
            other.gameObject.SetActive(false);
            GameplayController.instance.CoinCollected();
            SoundManager.instance.PlayCoinSound();
        }

        if(other.tag == Tags.DOOR_TAG){
            other.gameObject.GetComponent<Animator>().Play("DoorOpen");
        }

        if(other.tag == Tags.TREASURE_TAG){
            GameplayController.instance.CompletedLevel();
        }
    }

    /** 
        Detects if the player has walked out far enough away from a door. If so
        the door's close animation will play.

        @param {Collider} The other Collider involved in this collision.
    */
    private void OnTriggerExit(Collider other) {
        if(other.tag == Tags.DOOR_TAG){
            other.gameObject.GetComponent<Animator>().Play("DoorClose");
        }
    }
}
