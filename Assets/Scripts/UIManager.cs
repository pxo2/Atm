using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI cash;
    public TextMeshProUGUI Bank;

    void Start()
    {
        cash.gameObject.SetActive(true);
        Bank.gameObject.SetActive(true);
    }

    public void AddCash(float amount)
    {
        cash.text = amount.ToString();
    }

    public void AddBank(float amount)
    {
        Bank.text = amount.ToString();
    }
}
