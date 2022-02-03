#region Access
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using XavHelpTo.Change;
using XavHelpTo.Get;
using XavHelpTo.Set;
using XavHelpTo;
#endregion
/// <summary>
/// Manejador de audio
/// Dependencias: <seealso cref="DataSystem"/>
/// </summary>
public class AudioSystem : MonoBehaviour
{
    #region Variable
    [Tooltip("Key usado en el mixer de Music")]
    private const string MUSIC_KEY = "MusicVolume";
    [Tooltip("Key usado en el mixer de Sound")]
    private const string SOUND_KEY = "SoundVolume";
    private const float TIMER_FADE = 5;
    private const float MAX_dB = 55f;//dato curioso: Según la OMS, el nivel de ruido que el oído humano puede tolerar sin alterar su salud es de 55 decibeles. Y dependiendo del tiempo de exposición, ruidos mayores a los 60 decibeles pueden provocarnos malestares físicos.
    private static AudioSystem _;
    private Vector2 dBValues;
    private AudioSource src_sound;
    private AudioSource src_generalSound;

    [Header("AudioSystem")]
    public AudioMixer mixer;
    [Space]
    [Header("General Music")]
    [Space]
    public AudioClip[] clip_generalMusic;
    [Header("AudioSystem General Sounds")]
    [Space]
    public AudioClip[] clip_generalSounds;
    //[Space]
    //[SerializeField, Readonly] public GameObject pref_parent_specified_player;
    #endregion
    #region Event
    private void Awake()
    {
        this.Singleton(ref _, true);
        this.Component(out src_sound);
        transform.GetChild(0).Component(out src_generalSound, true);
        _.Normalize(_.dBValues.x);
        _.Normalize(_.dBValues.y);
    }
    
    #endregion
    #region Method
    /// <summary>
    /// Normalize the value
    /// </summary>
    private float Normalize(float value) => (value.PercentOf(MAX_dB) / 100) + 1;
    /// <summary>
    /// Based on the max dB adjust the Volume with the saved percentage
    /// Using <see cref="SavedData"/>
    /// </summary>
    private void SetAdjustedB(ref float dB, float percent, string key)
    {
        mixer.GetFloat(key, out dB);
        //percent.Print("blue");
        dB = (-1 + percent).QtyOf(MAX_dB) * 100;
        mixer.SetFloat(key, dB);
    }
    private IEnumerator FadePlay(float timer, bool fadeIn = true, AudioClip clip = default)
    {

        int volumeToReach = fadeIn.ToInt();
        float lastVolume = _.src_sound.volume;
        float val = Time.deltaTime / timer;
        float magnitude = lastVolume.UnitInTime(volumeToReach);

        if (!fadeIn) magnitude--;

        while (!_.src_sound.volume.Equals(volumeToReach))
        {
            _.src_sound.volume = (_.src_sound.volume + magnitude).Min(0).Max(1);
            yield return new WaitForSeconds(val);
        }

        //si hay clip...
        if (clip)
        {
            src_sound.clip = clip;
            src_sound.Play();
            // $"Sonando {clip.name}".Print("lime");
            StartCoroutine(FadePlay(timer));
        }
    }

    /// <summary>
    /// Adjust the Music based in a 0-1 value
    /// </summary>
    public static void SetMusic(float percent) => _.SetAdjustedB(ref _.dBValues.x, percent, MUSIC_KEY);
    /// <summary>
    /// Adjust the Sound based in a 0-1 value
    /// </summary>
    public static void SetSound(float percent) => _.SetAdjustedB(ref _.dBValues.y, percent, SOUND_KEY);
    /// <summary>
    /// Plays the music, if exist another playing then trys to go down and set the new one
    /// </summary>
    public static void PlayMusic(GeneralMusic g) => PlayMusic(g.ToInt());
    public static void PlayMusic(int index) => PlayMusic(_.clip_generalMusic[index]);
    public static void PlayMusic(AudioClip clip)
    {
        if (_.src_sound.clip && clip.name.Equals(_.src_sound.clip.name))
        {
            #if UNITY_EDITOR
            "Se esta intentando una cancion que ya esta puesta".Print("yellow");
            #endif
            return;//🛡
        }

        _.StartCoroutine(_.FadePlay(TIMER_FADE, false, clip));
    }
    /// <summary>
    /// Playes one of the most common sounds in game
    /// </summary>
    public static void PlaySound(GeneralSounds g, float pitch = 1, float pitchModify = .2f) => PlaySound(g.ToInt(), pitch, pitchModify);
    public static void PlaySound(int index, float pitch = 1, float pitchModify = .2f) => PlaySound(_.clip_generalSounds[index], pitch, pitchModify);
    public static void PlaySound(AudioClip clip,float pitch=1, float pitchModify= .2f)
    {
        _.src_generalSound.pitch = pitch + (pitchModify.MinusMax());
        _.src_generalSound.PlayOneShot(clip);
    }
    /// <summary>
    /// Get the reference of the specified Sound
    /// </summary>
    public static AudioClip GetSound(GeneralSounds g) => _.clip_generalSounds[g.ToInt()];
    #endregion
}
/// <summary>
/// General music, reference of all the music in game
/// </summary>
public enum GeneralMusic{

    MENU_1=0,
    GAME_1=1,
}
/// <summary>
/// General sounds, Reference of all the sounds in game
/// </summary>
public enum GeneralSounds
{
}
