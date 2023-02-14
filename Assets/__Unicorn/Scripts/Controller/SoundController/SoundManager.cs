using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[Singleton("SoundManager", true)]
public class SoundManager : Singleton<SoundManager>
{
    public enum GameSound
    {
        BGM,
        Footstep,
        Spin,
        Lobby,
        ClockTick,
        RewardClick,

        ClockTickFast,
        ClockTickFaster,
        TimeOver,
        TimeBonus,

        BGM_GAME_TUG,
        Tap_Tug,
        Slap,
        MarbleThrow,
        Marble_Landing,
        Power_Click
    }

    public enum Other
    {
        MineExplode,
        PuddleFall,
        Push,
        PushFall,
        Saw,
        Spike_Down,
        Spike_Up,
        KnifePickedPup,
        Tugging,
        Tugging_Win,
        Player_Fall,
        GlassBreak,
        GlassLanding,
        Jump,
        KnifeAttack,
        PlayerDie,
        HitBed
    }

    public enum BreakableObjectSound
    {
        Metal,
        Wood,
        Rock
    }

    [SerializeField] public SoundData soundData;

    public AudioMixer audioMixer;
    public AudioSource bgMusic;
    public AudioSource fxSound;
    public AudioSource fxSoundFootStep;
    public AudioSource clockTickFast;
    public AudioSource coffinSource;
 
    private float bgVol;
    private bool isPlayFootStep;

    #region UNITY METHOD
    private void Start()
    {
        SettingFxSound(PlayerDataManager.GetSoundSetting());
        SettingMusic(PlayerDataManager.GetMusicSetting());
        isPlayFootStep = false;
    }
    #endregion
    #region PUBLIC METHOD

    public void PlayFxSound(Enum soundEnum)
    {
        switch (soundEnum)
        {
            case LevelResult levelResult:
                {
                    switch (levelResult)
                    {
                        case LevelResult.Win:
                            PlaySoundWinCrewmate();
                            break;
                        case LevelResult.Lose:
                            PlaySoundWinImposter();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                }
            case GameSound gameSound:
                {
                    switch (gameSound)
                    {
                        case GameSound.BGM:
                            PlayBGM(Random.Range(0, soundData.AudioBgs.Length));
                            break;
                        case GameSound.BGM_GAME_TUG:
                            PlayBGM(soundData.AudioBgs[4]);
                            break;
                        case GameSound.Footstep:
                            PlayFootStep();
                            break;
                        case GameSound.Spin:
                            PlaySoundSpin();
                            break;
                        case GameSound.Lobby:
                            PlayBGM(soundData.AudiosLobby[Random.Range(0, soundData.AudiosLobby.Length)]);
                            break;
                        case GameSound.ClockTick:
                            PlayFxSound(soundData.AudioClockTick);
                            break;
                        case GameSound.RewardClick:
                            PlayFxSound(soundData.AudioRewardClick);
                            break;
                        case GameSound.ClockTickFast:
                            PlayClockTick(soundData.AudioClockTickFast);
                            break;
                        case GameSound.ClockTickFaster:
                            PlayClockTick(soundData.AudioClockTickFaster);
                            break;
                        case GameSound.TimeOver:
                            PlayFxSound(soundData.AudioTimeOver);
                            break;
                        case GameSound.Tap_Tug:
                            PlayFxSound(soundData.AudioTapTug);
                            break;
                        case GameSound.Slap:
                            PlayFxSound(soundData.AudioSlap);
                            break;
                        case GameSound.MarbleThrow:
                            PlayFxSound(soundData.AudioMarbleThrow);
                            break;
                        case GameSound.Marble_Landing:
                            PlayFxSound(soundData.AudioMarbleLanding);
                            break;
                        case GameSound.Power_Click:
                            PlayFxSound(soundData.AudioPowerClick);
                            break;
                        case GameSound.TimeBonus:
                            PlayFxSound(soundData.AudioMoreTime);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                }
            case TypeSoundIngame collectibleSound:
                {
                    PlaySoundCollectible(collectibleSound);
                    break;
                }
            default:
                PlayFxSound(soundEnum, fxSound);
                break;
        }
    }

    public void PlayFxSound(Enum soundEnum, AudioSource audioSource)
    {
        switch (soundEnum)
        {
            case BreakableObjectSound breakableObjectSound:
                PlayFxSound(soundData.BreakableObjectSounds[(int)breakableObjectSound], audioSource);
                break;
            case Other other:
                {
                    switch (other)
                    {
                        case Other.MineExplode:
                            PlayFxSound(soundData.AudioMineExplode, audioSource);
                            break;
                        case Other.PuddleFall:
                            PlayFxSound(soundData.AudioPuddleFall, audioSource);
                            break;
                        case Other.Push:
                            PlayFxSound(soundData.Push, audioSource);
                            break;
                        case Other.PushFall:
                            PlayFxSound(soundData.PushFall, audioSource);
                            break;
                        case Other.Saw:
                            PlayFxSound(soundData.SawHit, audioSource);
                            break;
                        case Other.Spike_Down:
                            PlayFxSound(soundData.SpikeDown, audioSource);
                            break;
                        case Other.Spike_Up:
                            PlayFxSound(soundData.SpikeUp, audioSource);
                            break;
                        case Other.KnifePickedPup:
                            PlayFxSound(soundData.KnifePickedUp, audioSource);
                            break;
                        case Other.Tugging_Win:
                            PlayFxSound(soundData.AudioTugWin);
                            break;
                        case Other.Tugging:
                            PlayFxSound(soundData.AudioTugging, audioSource);
                            break;
                        case Other.Player_Fall:
                            PlayFxSound(soundData.AudioPlayerFalls[Random.Range(0, soundData.AudioPlayerFalls.Length)]);
                            break;
                        case Other.GlassBreak:
                            PlayFxSound(soundData.GlassBreak, audioSource);
                            break;
                        case Other.GlassLanding:
                            PlayFxSound(soundData.GlassLanding, audioSource);
                            break;
                        case Other.Jump:
                            PlayFxSound(soundData.Jump, audioSource);
                            break;
                        case Other.KnifeAttack:
                            PlayFxSound(soundData.KnifeAttack, audioSource);
                            break;
                        case Other.PlayerDie:
                            PlayFxSound(soundData.AudioPlayerDie[Random.Range(0, soundData.AudioPlayerDie.Length)], audioSource);
                            break;
                        case Other.HitBed:
                            PlayFxSound(soundData.HitBedSound);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                }
            default:
                throw new InvalidEnumArgumentException($"{soundEnum} is not supported");
        }
    }

    public void StopSound(Enum soundEnum)
    {
        switch (soundEnum)
        {
            case GameSound gameSound:
                {
                    switch (gameSound)
                    {
                        case GameSound.Lobby:
                        case GameSound.BGM:
                            bgMusic.DOFade(0, 1f).OnComplete(action: () => bgMusic.Stop());
                            break;
                        case GameSound.Footstep:
                            StopFootStep();
                            break;
                        case GameSound.Spin:
                            StopFxSound();
                            break;
                        case GameSound.ClockTick:
                            StopFxSound();
                            break;
                        case GameSound.RewardClick:
                            StopFxSound();
                            break;
                        case GameSound.ClockTickFast:
                            clockTickFast.Stop();
                            break;
                        case GameSound.ClockTickFaster:
                            clockTickFast.Stop();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                }
            case LevelResult levelResult:
            case TypeSoundIngame collectibleSound:
                {
                    StopFxSound();
                    break;
                }
            case Other other:
                {
                    switch (other)
                    {
                        case Other.MineExplode:
                            StopFxSound();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                }
            default:
                throw new InvalidEnumArgumentException($"{soundEnum} is not supported");
        }
    }

    public void SettingFxSound(bool isOn)
    {
        var vol = isOn ? 1 : 0;
        fxSound.volume = vol;
        fxSoundFootStep.volume = vol;
        fxSound.mute = !isOn;
        fxSoundFootStep.mute = !isOn;
    }

    public void SettingMusic(bool isOn)
    {
        bgVol = isOn ? 1 : 0;
        bgMusic.volume = bgVol;
        bgMusic.mute = !isOn;
        //ValueBGMusic = vol;
    }

    #endregion
    #region PRIVATE METHOD
    private void PlayFxSound(AudioClip clip, AudioSource audioSource)
    {
        audioSource.PlayOneShot(clip);
    }

    public bool IsOnVibration
    {
        get
        {
            return PlayerPrefs.GetInt("OnVibration", 1) == 1 ? true : false;
        }
    }

    private void PlayBGM(int index)
    {
        var backgroundMusics = soundData.AudioBgs;
        PlayBGM(backgroundMusics[index]);

    }

    private void PlayBGM(AudioClip audioClip)
    {
        bgMusic.loop = true;
        bgMusic.clip = audioClip;
        bgMusic.volume = 0;
        bgMusic.DOKill();
        bgMusic.DOFade(bgVol, 1f);
        bgMusic.Play();
    }

    private void PlayClockTick(AudioClip clip)
    {
        clockTickFast.clip = clip;
        clockTickFast.Play();
    }

    private void PlayFxSound(AudioClip clip)
    {
        fxSound.PlayOneShot(clip);
    }

    private void StopFxSound()
    {
        fxSound.Stop();
    }

    public void PlayCoffinTheme(bool isPlaying, float delay = 0)
    {
        if (isPlaying)
        {
            audioMixer.DOSetFloat("BGMVol", -80, delay / 3 * 2).SetEase(Ease.InSine).SetDelay(delay / 3);
            audioMixer.DOSetFloat("FXVol", -80, delay / 3 * 2).SetEase(Ease.InSine).SetDelay(delay / 3)
                .OnComplete(() => coffinSource.Play());
        }
        else
        {
            audioMixer.SetFloat("BGMVol", 0);
            audioMixer.SetFloat("FXVol", 0);
            coffinSource.Stop();
        }
        
    }
    
    public void PlaySoundButton()
    {
        coffinSource.PlayOneShot(soundData.AudioClickBtn);
    }

    public void PlaySoundSpin()
    {
        PlayFxSound(soundData.AudioSpinWheel);
    }

    public void PlaySoundRevive()
    {
        PlayFxSound(soundData.AudioRevive);
    }

    public void PlaySoundReward()
    {
        PlayFxSound(soundData.AudioReward);
    }

    public void PlaySoundStartCrewmate()
    {
        PlayFxSound(soundData.AudioStartCrewmate);
    }

    public void PlaySoundStartImpostor()
    {
        PlayFxSound(soundData.AudioStartImpostor);
    }

    public void PlaySoundWinCrewmate()
    {
        PlayFxSound(soundData.AudioWin);
    }

    public void PlaySoundWinImposter()
    {
        PlayFxSound(soundData.AudioLose);
    }

    public void PlaySoundCollectible(TypeSoundIngame typeSound)
    {
        PlayFxSound(soundData.ListAudioCollects[(int)typeSound - 1]);
    }

    public void PlayFootStep()
    {
        if (isPlayFootStep)
            return;

        isPlayFootStep = true;
        fxSoundFootStep.Play();

        Analytics.LogFirstLogJoystick();
    }

    public void StopFootStep()
    {
        fxSoundFootStep.Stop();
        isPlayFootStep = false;
    }

    public void PlaySoundOverTime()
    {
        fxSound.PlayOneShot(soundData.AudioOverTime);
    }
    #endregion
}
