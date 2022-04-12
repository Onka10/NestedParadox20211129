using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    public void TittleBGM()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
    }

    public void StageBGM()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage);
    }

    public void BossBGM()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Boss);
    }

    public void Click0()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Click_Decide);
    }

    public void Click1()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Click_Choice);
    }

    public void Click3()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Click_Cancel);
    }

    public void Guard()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.GuardKun);
    }

    public void Wall()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.SacrificeWall);
    }
    public void Heal()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Heal);
    }

    public void Summon()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Summon);
    }

    public void Offence()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.OffenceUp);
    }

    public void Speed()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.SpeedUp);
    }

    public void LastSword()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.LastSword);
    }
}
