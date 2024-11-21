using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton

    public static SoundManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    #endregion


    [SerializeField] private List<Sound> bgmSounds;
    private Dictionary<string, AudioClip> bgmPool;


    [SerializeField] private List<Sound> sfxSounds;
    private Dictionary<string, AudioClip> sfxPool;

    private void Start()
    {

        bgmPool = new();
        foreach(var sound in bgmSounds)
        {
            //add to bgmPool
        }

        sfxPool = new();
        foreach (var sound in sfxSounds)
        {
            //add to sfxPool
        }

    }

    private IEnumerator DoOnElapse(float soundDuration, Action OnElapse)
    {
        yield return new WaitForSeconds(soundDuration);
        OnElapse?.Invoke();
    }

    public static void PlaySFX(string name, Action OnElapse = null)
    {

    }

    public static void PlayBGM(string name, Action OnElapse = null)
    {

    }

}
