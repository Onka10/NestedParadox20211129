using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmAudioSource;
    //[SerializeField] AudioSource introAudioSource;
    [SerializeField] AudioSource seAudioSource;

    [SerializeField] List<BGMSoundData> bgmSoundDatas;
    [SerializeField] List<SESoundData> seSoundDatas;

    public float masterVolume = 1;
    public float bgmMasterVolume = 1;
    public float seMasterVolume = 1;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //BGMの再生
    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        bgmAudioSource.Play();
        
        if(bgm==BGMSoundData.BGM.Title) bgmAudioSource.loop = false;
        else bgmAudioSource.loop = true;
    }

    /*
    public void PlayIntroBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        introAudioSource.clip = data.audioClip;
        introAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        introAudioSource.Play();
    }*/

    //SEの再生
    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = seSoundDatas.Find(data => data.se == se);
        seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        seAudioSource.PlayOneShot(data.audioClip);
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        Title,
        Stage,
        Boss,
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        Click_Decide,//決定クリック
        Click_Choice,//選択
        Click_Cancel,//キャンセル
        GuardKun,//ガードくん
        SacrificeWall,//サクリファイスウォール
        SpeedUp,//スピードアップ
        Heal,//回復
        OffenceUp,//攻撃力アップ
        LastSword,//最期の剣
        Summon,//召喚SE

    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}
