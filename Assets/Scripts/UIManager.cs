using System;
using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_InputField tmpInputField1;
    [SerializeField] private TMP_InputField tmpInputField2;
    [SerializeField] private Button rollButton;
    public event Action<int, int> RollEvent;
    private void Start()
    {
        tmpInputField1.onValueChanged.AddListener(input => ValidateInput(tmpInputField1, input));
        tmpInputField2.onValueChanged.AddListener(input => ValidateInput(tmpInputField2, input));
        rollButton.onClick.AddListener(RollButtonClicked);
    }

    private void ValidateInput(TMP_InputField inputField, string input)
    {
        string numericInput = string.Empty;

        // Rakamları ayıkla
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                numericInput += c;
            }
        }

        // Eğer giriş bir sayıysa, kontrol et
        if (int.TryParse(numericInput, out int number))
        {
            if (number > 6) // Maksimum değeri aşarsa sınırla
            {
                numericInput = "6";
            }
            else if (number < 1) // Minimum değerden küçükse 1 yap
            {
                numericInput = "1";
            }
        }
        else
        {
            // Geçerli bir sayı değilse varsayılan olarak 1 yap
            numericInput = "1";
        }

        // Temizlenmiş ve sınırlandırılmış değeri geri yaz
        if (numericInput != input)
        {
            inputField.text = numericInput;
        }
    }
    public void RollButtonClicked()
    {
        if (GameManager.Instance.bCanRoll)
        {
            GameManager.Instance.bCanRoll = false;
            RollEvent?.Invoke(int.Parse(tmpInputField1.text),int.Parse(tmpInputField2.text));
        }
    }
    }



