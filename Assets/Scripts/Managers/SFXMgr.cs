using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXMgr : MonoBehaviour
{

    private static SFXMgr instance;
    [SerializeField] AudioClip bamboo_swing;
    [SerializeField] AudioClip splash;
    [SerializeField] AudioClip ingame_Himguage;
    [SerializeField] AudioClip ingame_sign;
    [SerializeField] AudioClip fishComing;
    [SerializeField] AudioClip open_bag;
    [SerializeField] AudioClip eating_food;
    [SerializeField] AudioClip reel_up;
    [SerializeField] AudioClip reel_down;
    [SerializeField] AudioClip minigame_fishBump;
    [SerializeField] AudioClip minigame_fishWhip;
    [SerializeField] AudioClip button;
    [SerializeField] AudioClip coin;
    [SerializeField] AudioClip walk;
    [SerializeField] AudioClip human_FallDown;
    [SerializeField] AudioClip prologue_typing;
    [SerializeField] AudioClip prologue_car;
    [SerializeField] AudioClip prologue_radio;
    AudioSource[] sound; // 0 : 기본 BGM / 1: 버튼, 물고기 부딫힘 / 2 : 지속 효과음

    public static SFXMgr Instance {
        get 
        {
            return instance;
        }
        set
        {
            Instance = value;
        }
    }

    private void Awake() {
        if (instance != null) {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;

        sound = GetComponents<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float v)
    {
        for(int i = 0; i < sound.Length; i++)
        {
            sound[i].volume = v * 0.8f;
        }
    }

    public void PlaySFX()
    {
        sound[2].Play();
    }

    public void StopSFX()
    {
        sound[2].Stop();
    }

    public void Swing()
    {
        sound[0].PlayOneShot(bamboo_swing, sound[0].volume);
    }

    public void Splash()
    {
        sound[0].PlayOneShot(splash, sound[0].volume);
    }

    public void SetPowerguageSound()
    {
        sound[2].clip = ingame_Himguage;
    }

    public void BiteSign()
    {
        sound[0].PlayOneShot(ingame_sign, sound[0].volume);
    }

    public void MoveToMinigame()
    {
        sound[0].PlayOneShot(fishComing, sound[0].volume);
    }

    public void OpenBag()
    {
        sound[0].PlayOneShot(open_bag, sound[0].volume);
    }

    public void EatFood()
    {
        sound[0].PlayOneShot(eating_food, sound[0].volume);
    }

    public void SetReelUpSound()
    {
        sound[2].clip = reel_up;
    }

    public void SetReelDownSound()
    {
        sound[2].clip = reel_down;
    }

    public void FishBump()
    {
        sound[1].PlayOneShot(minigame_fishBump, sound[0].volume);
    }

    public void FishPass()
    {
        StartCoroutine(WaitAndPlay(minigame_fishWhip, 0.6f));
    }

    public void Button()
    {
        sound[1].PlayOneShot(button);
    }

    public void Coin()
    {
        sound[0].PlayOneShot(coin);
    }

    public void Walk()
    {
        sound[0].PlayOneShot(walk);
    }

    public void FallDown()
    {
        StartCoroutine(WaitAndPlay(human_FallDown, 0.5f));
    }

    public void SetTypingSound()
    {
        sound[2].clip = prologue_typing;
    }

    public void StartCar()
    {
        StartCoroutine(PlayAndFadeOut(prologue_car, 2f));
    }

    public void Radio()
    {
        sound[0].PlayOneShot(prologue_radio, sound[0].volume);
    }

    IEnumerator WaitAndPlay(AudioClip soundToPlay, float second)
    {
        yield return new WaitForSeconds(second);
        sound[0].PlayOneShot(soundToPlay, sound[0].volume);
    }

    IEnumerator PlayAndFadeOut(AudioClip soundToFade, float delaySecond = 0f)
    {
        float f_time = 0f;
        float currVolume = sound[2].volume;
        sound[2].clip = soundToFade;
        PlaySFX();
        yield return new WaitForSeconds(delaySecond);
        while(sound[2].volume > 0)
        {
            f_time += UnityEngine.Time.deltaTime;
            sound[2].volume = Mathf.Lerp(currVolume, 0, f_time);
            yield return null;
        }
        StopSFX();
        sound[2].volume = currVolume;
    }

}
