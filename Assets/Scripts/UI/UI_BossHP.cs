using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UI_BossHP : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderBackground;
    
    // Start is called before the first frame update
    void Start()
    {        
    }

    public void Activate(EnemyBase omnipotence)
    {
        slider.gameObject.SetActive(true);
        sliderBackground.enabled = true;        
        slider.maxValue = omnipotence.Hp.Value;
        omnipotence.Hp.Subscribe(x => slider.value = x).AddTo(this);
        
    }
}
