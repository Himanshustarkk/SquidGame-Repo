using System;
using DG.Tweening.Core.Easing;
using System.Runtime.InteropServices;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Sounds[] sounds;
    public bool isMuted = false;
    private float timer;
    private bool still_pitch;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        isMuted = PlayerPrefs.GetInt("SoundMuted", 0) == 1;

        foreach (Sounds s in sounds)
        {
            s.audio = gameObject.AddComponent<AudioSource>();
            s.audio.clip = s.clip;
            s.audio.playOnAwake = false;
            s.audio.volume = isMuted ? 0 : s.volume;
            s.audio.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sounds snd = Array.Find(sounds, s => s.name == name);
        if (snd == null || isMuted)
            return;

        if (still_pitch && name == "collect_cube")
        {
            timer = 0f;
            snd.audio.pitch += .01f;
        }
        else if (!still_pitch && name == "collect_cube")
        {
            snd.audio.pitch = 1f;
            still_pitch = true;
        }
        snd.audio.Play();
    }

    public void Stop(string name)
    {
        Sounds snd = Array.Find(sounds, s => s.name == name);
        if (snd == null)
            return;
        snd.audio.Stop();
    }

    private void Update()
    {
        if (timer <= 1f && still_pitch)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            still_pitch = false;
        }
    }

    public void sPlay(string name, float pitch)
    {
        Sounds snd = Array.Find(sounds, s => s.name == name);
        if (snd == null || isMuted)
            return;

        snd.audio.pitch = pitch;
        snd.audio.Play();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        Debug.Log("Printing Mute Value" + isMuted);
        PlayerPrefs.SetInt("SoundMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        foreach (Sounds s in sounds)
        {
            if (s.audio != null)
                s.audio.volume = isMuted ? 0 : s.volume;
        }
    }

    // For Sounds
    [DllImport("__Internal")]
    private static extern void GoToURLInSameTab(string url);
    public void ExitGame()
    {
        
            string url = "https://" + FetchHostname();
        Debug.Log("Exit Button");
            GoToURLInSameTab(url);
      

    }

    [DllImport("__Internal")]
    private static extern IntPtr GetHostname();

    public static string FetchHostname()
    {
        try
        {
            IntPtr ptr = GetHostname();
            return Marshal.PtrToStringUTF8(ptr);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to fetch hostname: {e.Message}");
            return string.Empty;
        }
    }

}
