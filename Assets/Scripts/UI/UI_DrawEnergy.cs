using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NestedParadox.Players;
using UniRx;

public class UI_DrawEnergy : MonoBehaviour
{
    private Slider DrawEnegrySlider;
    [SerializeField] PlayerCore _playercore;
    void Start()
    {
        DrawEnegrySlider = GetComponent<Slider>();
        DrawEnegrySlider.maxValue = 1;

        // _playercore.PlayerHP
        // .Subscribe(x=>UpdateDrawEnergyUI(x))
        // .AddTo(this);
    }

    // private void UpdateDrawEnergyUI(int x){
    //     DrawEnegrySlider.value = x;
    // }
}
