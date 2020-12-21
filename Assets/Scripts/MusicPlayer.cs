using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private void Awake()
    {
        int musicPlayersCount = FindObjectsOfType<MusicPlayer>().Length;
        if (musicPlayersCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);        
        }
    }
}
