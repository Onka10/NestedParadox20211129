using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NestedParadox.Players;
using UniRx;

public class UI_PlayerHP : MonoBehaviour
{
    private Slider HPSlider;

    // Start is called before the first frame update
    public void Init()
    {
        HPSlider = GetComponent<Slider>();
        HPSlider.maxValue = (float)PlayerCore.I.Hp.Value;

        PlayerCore.I.Hp
        .Subscribe(x=>UPdatePlayerHPUI(x))
        .AddTo(this);
    }

    private void UPdatePlayerHPUI(int x){
        HPSlider.value = (float)x;
    }

}
