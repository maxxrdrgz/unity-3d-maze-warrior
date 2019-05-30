using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField]
    private AudioSource audiosource;
    [SerializeField]
    private AudioClip coinSound;

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

    void MakeInstance(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public void PlayCoinSound(){
        audiosource.clip = coinSound;
        audiosource.Play();
    }

}
