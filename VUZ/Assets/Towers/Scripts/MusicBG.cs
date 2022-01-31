using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBG : MonoBehaviour
{
    private static MusicBG musicInstance;
    void Awake()
    {
        if (musicInstance == null)
        {
            musicInstance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
