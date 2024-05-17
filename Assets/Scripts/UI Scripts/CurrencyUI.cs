using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    private TextMeshProUGUI currencyText;
    void Awake()
    {
        currencyText = GetComponent<TextMeshProUGUI>();
        CurrencyEventHandler.OnCurrencyChanged += Refresh;
    }

    private void Refresh()
    {
        currencyText.text = GameManager.instance.player.currency.ToString();
    }
}
