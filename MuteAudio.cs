using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour
{
    public static bool buttonmute = false; 
    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            buttonmute = true;
            AudioListener.volume = 0;
        }
        else
        {
            buttonmute = false;
            AudioListener.volume = 1;
        }
    }
}