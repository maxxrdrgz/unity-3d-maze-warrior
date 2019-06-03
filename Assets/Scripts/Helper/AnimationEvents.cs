using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject player, playButton;

    void DeactivateGameObjects(){
        player.SetActive(false);
        playButton.SetActive(false);
    }

    void ActivateGameObjects(){
        player.SetActive(true);
        playButton.SetActive(true);
    }
}
