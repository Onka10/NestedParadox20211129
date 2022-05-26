using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayUpdateNew : Singleton<BGMPlayUpdateNew>
{
    public AudioSource audioSource; 
    int timeSamples_Now = 0;    
    int LOOPSTART = 1886769;       //ループ開始箇所のサンプル数
    int LOOPLENGTH = 4009847;      //ループ区間に含まれるサンプル数

    public void PlayStart()
    {
      audioSource.Play(0);
      Debug.Log(audioSource.timeSamples);
    }

    void Update(){

      if ( !audioSource.isPlaying ){    //chrome系のブラウザではポーズ時にBGM停止状態でtimeSamplesが進んでしまう事が有る事象への対策
        audioSource.timeSamples = 0;
      }

    //   Debug.Log(audioSource.timeSamples);

      if( audioSource.timeSamples > LOOPSTART + LOOPLENGTH ){   //
        Debug.Log("判定");
        audioSource.timeSamples = LOOPSTART + ((audioSource.timeSamples - LOOPSTART - 1 ) % LOOPLENGTH );
      }

    }
}
