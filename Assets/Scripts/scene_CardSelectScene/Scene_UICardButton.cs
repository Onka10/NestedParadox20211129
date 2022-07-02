using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_UICardButton : MonoBehaviour
{

    public int ID;
    public bool onDeck = false;

    Scene_CardSelect scs;

    public void OnButtonDown()
    {
        if (scs == null) scs = FindObjectOfType<Scene_CardSelect>();

        if (onDeck)
        {
            scs.cardCount--;
            transform.SetParent(scs.parent_cards);
            onDeck = false;
        }
        else
        {

            if (scs.cardCount < 10)
            {
                scs.cardCount++;
                transform.SetParent(scs.parent_deck);
                onDeck = true;
            }
        }

        scs.Refresh();

    }
}