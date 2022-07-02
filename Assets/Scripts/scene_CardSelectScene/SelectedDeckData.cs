using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDeckData : MonoBehaviour
{

    
    public static SelectedDeckData instance;
    public int[] deckData;

    private void Awake()
    {
        instance = this;
    }

}
