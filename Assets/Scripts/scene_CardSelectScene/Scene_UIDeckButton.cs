using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_UIDeckButton : MonoBehaviour
{

    public Scene_CardSelect scs;

    public Button btn;
    public Image image;
    public Text desc;

    public Scene_UICardButton targetCard;

    public void OnButtonDown_ReturnToCards()
    {
        if (scs == null) scs = FindObjectOfType<Scene_CardSelect>();

        if (targetCard == null) return;


        scs.RemoveFromDeck(targetCard.ID);


        targetCard.ui_button.interactable = true;
        btn.interactable = false;

        // image.sprite = null;
        desc.text = "Empty";

        targetCard = null;
    }

}
