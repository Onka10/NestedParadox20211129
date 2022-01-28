using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject[] currentCards = new GameObject[3];
    [SerializeField] Image[] currentCardImages = new Image[3];
    [SerializeField] Sprite[] AllCards;
    [SerializeField] Image DrawEnergyFill;
    [SerializeField] Text leftDeckNumText;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject[] monsterPrefabs;
    [SerializeField] SwordConroller swordController;
    [SerializeField] CharacterMove characterMove;
    [SerializeField] GameObject panel;
    [SerializeField] Image[] cards;
    public int drawEnergy;
    public int cardNum;
    public List<int> deckNums = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    // Start is called before the first frame update
    void Start()
    {
        
        drawEnergy = 0;
        cardNum = 3;
        deckNums = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int randomNum;
        for(int i=0; i<3; i++)
        {
            randomNum = Random.Range(0, 10);
            if(!deckNums.Contains(randomNum))
            {
                randomNum = deckNums[deckNums.Count-1];
            }
            currentCardImages[i].sprite = AllCards[randomNum];
            deckNums.Remove(randomNum);
        }
    }

    public void Pose()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        for(int i=0; i<3; i++)
        {
            cards[i].sprite = currentCardImages[i].sprite;
        }
    }

    public void Back()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseDrawEnergy(int value)
    {
        if(drawEnergy >= 100)
        {
            return;
        }

        drawEnergy += value;
        DrawEnergyFill.fillAmount = drawEnergy / 100.0f;
    }

    IEnumerator LastSword()
    {
        float time = 0;
        GameObject[] monstersArray1 = GameObject.FindGameObjectsWithTag("Monster");
        GameObject[] monsterArray2 = GameObject.FindGameObjectsWithTag("GardKun");
        List<GameObject> monsters = new List<GameObject>();
        monsters.AddRange(monstersArray1);
        monsters.AddRange(monsterArray2);
        swordController.attackValue = monsters.Count;
        while (time<10)
        {
            time += Time.deltaTime;
            yield return null;
        }
        swordController.attackValue = 1;
        yield break;
    }

    public void Draw()
    {
        if (cardNum >= 3 || drawEnergy < 100)
        {
            return;
        }

        if(deckNums.Count <= 0)
        {
            deckNums = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            leftDeckNumText.text = "10";
        }

        drawEnergy = 0;
        DrawEnergyFill.fillAmount = 0;
        int randomNum = Random.Range(0, deckNums.Count);
        foreach(Image cardImage in currentCardImages)
        {
            if(cardImage.sprite == null)
            {
                cardImage.sprite = AllCards[deckNums[randomNum]];
                cardImage.color = new Color(1, 1, 1, 1);
                deckNums.RemoveAt(randomNum);
                break; 
            }
        }
        leftDeckNumText.text = $"{deckNums.Count}";
    }

    public void DrawDevil()
    {
        drawEnergy = 0;
        DrawEnergyFill.fillAmount = 0;
        eventSystem.currentSelectedGameObject.GetComponent<Image>().sprite = AllCards[4];
        deckNums.Remove(4);
        leftDeckNumText.text = $"{deckNums.Count}";
    }


    public void ActivateCard()
    {
        string cardName = eventSystem.currentSelectedGameObject.GetComponent<Image>().sprite.name;
        if(cardName == AllCards[0].name)
        {
            float randomNum = Random.Range(-8.0f, -4.0f);
            GameObject gardKun_clone = Instantiate(monsterPrefabs[0]);
            gardKun_clone.transform.position = new Vector3(randomNum, 1.67f, 0);
            gardKun_clone.transform.GetComponent<GardKun>().basePoint = new Vector3(randomNum, 1.67f, 0);
        }
        else if (cardName == AllCards[1].name)
        {
            float randomNum = Random.Range(-8.0f, -4.0f);
            GameObject gardKun_clone = Instantiate(monsterPrefabs[0]);
            gardKun_clone.transform.position = new Vector3(randomNum, 1.67f, 0);
            gardKun_clone.transform.GetComponent<GardKun>().basePoint = new Vector3(randomNum, 1.67f, 0);
        }
        else if (cardName == AllCards[2].name)
        {
            Instantiate(monsterPrefabs[1]);
        }
        else if (cardName == AllCards[3].name)
        {
            Instantiate(monsterPrefabs[1]);
        }
        else if (cardName == AllCards[4].name)
        {
            GameObject[] monstersArray1 = GameObject.FindGameObjectsWithTag("Monster");
            GameObject[] monsterArray2 = GameObject.FindGameObjectsWithTag("GardKun");
            List<GameObject> monsters = new List<GameObject>();
            monsters.AddRange(monstersArray1);
            monsters.AddRange(monsterArray2);
            if(monsters.Count <= 0)
            {
                return;
            }
            Instantiate(monsterPrefabs[2]);
        }
        else if (cardName == AllCards[5].name)
        {
            GameObject gardKun = GameObject.FindGameObjectWithTag("GardKun");
            if(gardKun == null)
            {
                return;
            }
            for(int i=0; i<4; i++)
            {
                float randomNum = Random.Range(4.0f, 8.0f);
                GameObject gardKun_clone = Instantiate(monsterPrefabs[0]);
                gardKun_clone.transform.position = new Vector3(-1*randomNum, 1.67f, 0);
                gardKun_clone.transform.GetComponent<GardKun>().basePoint = new Vector3(-1*randomNum, 1.67f, 0);
            }
        }
        else if (cardName == AllCards[6].name)
        {
            GameObject gardKun = GameObject.FindGameObjectWithTag("GardKun");
            if (gardKun == null)
            {
                return;
            }
            
            for (int i = 0; i < 4; i++)
            {
                float randomNum = Random.Range(4.0f,8.0f);
                GameObject gardKun_clone = Instantiate(monsterPrefabs[0]);
                gardKun_clone.transform.position = new Vector3(-1*randomNum, 1.67f, 0);
                gardKun_clone.transform.GetComponent<GardKun>().basePoint = new Vector3(-1*randomNum, 1.67f, 0);
            }
        }
        else if (cardName == AllCards[7].name)
        {
            StartCoroutine("LastSword");
        }
        else if (cardName == AllCards[8].name)
        {
            characterMove.IsCasual = true;
        }
        else if (cardName == AllCards[9].name)
        {
            if(!deckNums.Contains(4))
            {
                Debug.Log("ダストデビルがありません");
                eventSystem.currentSelectedGameObject.GetComponent<Image>().sprite = null;
                eventSystem.currentSelectedGameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                cardNum -= 1;
                return;
            }
            if (drawEnergy < 100)
            {
                return;
            }
            DrawDevil();
            return;

        }
        eventSystem.currentSelectedGameObject.GetComponent<Image>().sprite = null;
        eventSystem.currentSelectedGameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        cardNum -= 1;
    }

    
}

