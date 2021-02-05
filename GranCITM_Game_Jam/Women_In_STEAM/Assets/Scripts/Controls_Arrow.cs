using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls_Arrow : MonoBehaviour
{
    public int index = 0;
    public GameObject[] cards;
    public Text index_text;
    public Button left_button;
    public Button right_button;

    // Update is called once per frame
    void Update()
    {
        ShowCards();
        HideArrows();
    }
    void ShowCards()
    {
        cards[index].gameObject.SetActive(true);
    }

    void HideArrows()
    {
        if (index == 0)
        {
            left_button.interactable = false;
        }
        else
        {
            left_button.interactable = true;
        }
        if (index == (cards.Length - 1))
        {
            right_button.interactable = false;
        }
        else
        {
            right_button.interactable = true;
        }
    }
    public void NextCard()
    {
        if (index != (cards.Length - 1))
        {
            cards[index].gameObject.SetActive(false);
            index++;
            cards[index].gameObject.SetActive(true);
        }
    }
    public void PreviousCard()
    {
        if (index > 0)
        {
            cards[index].gameObject.SetActive(false);
            index--;
            cards[index].gameObject.SetActive(true);
        }
    }
}

