using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public static class AudioManager {
    static string FMOD_Event_Path = "event:/Music+Ambient";
    static FMOD.Studio.EventInstance Event_Instance;

    public static void Initialize() {
        Event_Instance = FMODUnity.RuntimeManager.CreateInstance(FMOD_Event_Path);
        Event_Instance.start();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">
    /// 0: calm
    /// 1: violent
    /// </param>
    public static void SetStatus(int value) {
        Event_Instance.setParameterByName("Status", value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">
    /// 0: day
    /// 1: night
    /// </param>
    public static void SetTime(int value) {
        Event_Instance.setParameterByName("Time", value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">
    /// 0: menu
    /// 1: initial level
    /// 10: credits
    /// </param>
    public static void SetProgress(int value) {
        Event_Instance.setParameterByName("Progress", value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">-1..1: Card position</param>
    public static void SetCardPos(float value) {
        Event_Instance.setParameterByName("CardPos", value);
    }
}
