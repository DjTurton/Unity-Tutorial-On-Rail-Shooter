using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] GameObject deathFX;

    //trigger on colision with other
    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }

    //process of killing the player
    private void StartDeathSequence()
    {
        //send to player to disable player controls
        SendMessage("OnPlayerDeath");
    }

    //reload the level
    private void ReloadScene() // string referenced
    {
        SceneManager.LoadScene(1);
    }
}
