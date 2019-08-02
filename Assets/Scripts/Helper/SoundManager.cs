using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField]
    private AudioSource coin_audio_source;
    [SerializeField]
    private AudioSource bg_audio_source;
    [SerializeField]
    private AudioSource player_audio_source;
    [SerializeField]
    private AudioSource fight_fx_audio_source;
    [SerializeField]
    private AudioClip coinSound;
    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip landingSound;
    [SerializeField]
    private AudioClip winSound;
    [SerializeField]
    private AudioClip gameoverSound;
    [SerializeField]
    private AudioClip playerHitSound;
    [SerializeField]
    private AudioClip enemyDeadSound;

    private void Awake() {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /** 
        Creates a singleton that only exists within the current scene
    */
    void MakeInstance(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public void PlayCoinSound(){
        coin_audio_source.clip = coinSound;
        coin_audio_source.Play();
    }

    public void StopBgMusic(){
        bg_audio_source.Stop();
    }

    public void PlayGameOverSound(){
        StopBgMusic();
        bg_audio_source.clip = gameoverSound;
        bg_audio_source.loop = false;
        bg_audio_source.Play();
    }

    public void PlayWinSound(){
        StopBgMusic();
        bg_audio_source.clip = winSound;
        bg_audio_source.loop = false;
        bg_audio_source.Play();
    }

    public void PlayJumpSound(){
        player_audio_source.clip = jumpSound;
        player_audio_source.Play();
    }
    
    public void PlayLandingSound(){
        player_audio_source.clip = landingSound;
        player_audio_source.Play();
    }

    public void PlayHitSound(){
        fight_fx_audio_source.clip = playerHitSound;
        fight_fx_audio_source.Play();
    }

    public void PlayEnemyDeadSound(){
        fight_fx_audio_source.clip = enemyDeadSound;
        fight_fx_audio_source.Play();
    }
}
