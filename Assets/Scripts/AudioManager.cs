using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public static class AudioManager {
    static string FMOD_Event_Path = "event:/Music+Ambient";
    static string FMOD_Event_Path_Click = "event:/Click1";
    
    static FMOD.Studio.EventInstance Event_Instance;

    public static void Initialize() {
        if (!Event_Instance.isValid()) {
            Event_Instance = FMODUnity.RuntimeManager.CreateInstance(FMOD_Event_Path);
            Event_Instance.start();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">
    /// 0: calm
    /// 1: violent
    /// </param>
    public static void SetStatus(int value) {
        //Debug.Log("Status "+value);
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
        //Debug.Log("Time "+value);
        Event_Instance.setParameterByName("Time", value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">
    /// 0: menu
    /// 1..4: music intensity
    /// 7: good ending
    /// 8: neutral ending
    /// 9: evil ending
    /// 10: credits
    /// </param>
    public static void SetProgress(int value) {
        //Debug.Log("Progress "+value);
        Event_Instance.setParameterByName("Progress", value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">-1..1: Card position</param>
    public static void SetCardPos(float value) {
        //Debug.Log("CardPost "+value);
        Event_Instance.setParameterByName("CardPos", value);
    }

    public static void ClickSound() {
        FMODUnity.RuntimeManager.CreateInstance(FMOD_Event_Path_Click).start();
    }
}
