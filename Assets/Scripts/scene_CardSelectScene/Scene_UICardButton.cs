using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_UICardButton : MonoBehaviour
{

    public enum UI_Card_Rarelity { Normal=10, Rare = 50 }

    public int ID;

    public UI_Card_Rarelity rarelity;
    public Button ui_button;
    public Text ui_description;

    Scene_CardSelect scs;

    private void Start()
    {
        Refresh();
    }

    void Refresh()
    {
        ui_description.text = "ID : " + ID;
    }

    public void OnButtonDown()
    {
        if (scs == null) scs = FindObjectOfType<Scene_CardSelect>();

        if (scs.AddToDeck(this))
        {
            // success to adding deck.
            ui_button.interactable = false;
        }

    }
}