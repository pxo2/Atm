using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupBank : MonoBehaviour
{
    public GameManager gameManager;
    PopupBank popupBank;

    [SerializeField] private GameObject mainScreen; // 메인
    [SerializeField] private GameObject DepositMenu; // 입금메뉴
    [SerializeField] private GameObject WithdrawalMenu; // 출금메뉴
    [SerializeField] private Button depositButton; // 메인버튼
    [SerializeField] private Button withdrawalButton; // 메인버튼
    [SerializeField] private Button backButton_d; // 이전 버튼
    [SerializeField] private Button backButton_w; // 이전 버튼
    [SerializeField] private TMP_InputField depositField;
    [SerializeField] private TMP_InputField withdrawalField;
    [SerializeField] private Button customDeposit; 
    [SerializeField] private Button customWithdrawal; 


    [SerializeField] public TextMeshProUGUI cashText;
    [SerializeField] public TextMeshProUGUI balanceText;

    private void Awake()
    {
        popupBank = GameObject.FindWithTag("GameManager").GetComponent<PopupBank>();
    }
    public void Start()
    {
        mainScreen.gameObject.SetActive(true);
        DepositMenu.gameObject.SetActive(false);
        WithdrawalMenu.gameObject.SetActive(false);

        depositButton.onClick.AddListener(DepositScreen);
        withdrawalButton.onClick.AddListener(WithdrawalScreen);
        backButton_d.onClick.AddListener(TitleScreen);
        backButton_w.onClick.AddListener(TitleScreen);

        //customDeposit.onClick.AddListener(CustomInputDeposit);
        //customWithdrawal.onClick.AddListener(CustomInputWithdrawal);

        RefreshUI();
    }

    public void DepositScreen()
    {
        depositButton.gameObject.SetActive(false);
        withdrawalButton.gameObject.SetActive(false);
        DepositMenu.SetActive(true);
    }

    public void WithdrawalScreen()
    {
        depositButton.gameObject.SetActive(false);
        withdrawalButton.gameObject.SetActive(false);
        WithdrawalMenu.SetActive(true);
    }

    public void TitleScreen()
    {
        WithdrawalMenu.SetActive(false);
        DepositMenu.SetActive(false);
        depositButton.gameObject.SetActive(true);
        withdrawalButton.gameObject.SetActive(true);
    }
    
    public void CustomInputDeposit()
    {
        if (float.TryParse(depositField.text, out float amount))
        {
            DepositMoney(amount);
        }
    }

    public void CustomInputWithdrawal()
    {
        if (float.TryParse(withdrawalField.text, out float amount))
        {
            WithdrawalMoney(amount);
        }
    }

    public void DepositMoney(float amount)
    {
        if (gameManager.userData.handCash >= amount) 
        {
            gameManager.userData.handCash -= amount;
            gameManager.userData.bankBalance += amount;
            gameManager.SaveUserData();
            RefreshUI();
        }
        else
        {
            Debug.Log("현금이 부족합니다");
        }

    }
    public void WithdrawalMoney(float amount)
    {
        if (gameManager.userData.bankBalance >= amount)
        {
            gameManager.userData.bankBalance -= amount;
            gameManager.userData.handCash += amount;
            gameManager.SaveUserData();
            RefreshUI();
        }
        else
        {
            Debug.Log("보유 금액이 부족합니다");
        }
    }


    public void RefreshUI()
    {
        cashText.text = $"{gameManager.userData.handCash:N0}";
        balanceText.text = $"{gameManager.userData.bankBalance:N0}";
    }

}