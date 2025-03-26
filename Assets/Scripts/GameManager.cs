using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR;

[System.Serializable]
public class UserData
{
    public string userName;
    public float handCash;
    public float bankBalance;

    public UserData(string name, float beforeHandCash, float beforeBankBalance)
    {
        userName = name;
        handCash = beforeHandCash;
        bankBalance = beforeBankBalance;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UserData userData;
    UIManager uiManager;

    private string saveFile;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        

        saveFile = Path.Combine(Application.persistentDataPath, "userData.json");

        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();

        LoadUserData();
    }
    private void Start()
    {
        if(uiManager != null && userData != null)
        {
            uiManager.AddBank(userData.bankBalance);
            uiManager.AddCash(userData.handCash);
        }
    }
    public void SaveUserData()
    {
        string jsonData = JsonUtility.ToJson(userData, true);
        File.WriteAllText(saveFile, jsonData);
        Debug.Log("데이터 저장 완료: " + saveFile);
    }

    public void LoadUserData()
    {
        if (File.Exists(saveFile))
        {
            string jsonData = File.ReadAllText(saveFile);
            userData = JsonUtility.FromJson<UserData>(jsonData);
            Debug.Log("데이터 로드 완료");
        }
        else
        {
            Debug.Log("저장된 데이터 없음");
            userData = new UserData("김규태", 0, 0);
            SaveUserData();
        }
        if (uiManager != null)
        {
            uiManager.AddBank(userData.bankBalance);
            uiManager.AddCash(userData.handCash);
        }
    }

    public void AddHandCash(float amount)
    { /* 기존 curCash와 curBankBalance 같은 값을 초기화 한후 사용했지만 데이터 저장을 위해
       바로 userData 에 저장을 한다 */
        userData.handCash += amount;
        uiManager.AddCash(userData.handCash);
        SaveUserData();
        Debug.Log("소지금 변경: " + userData.handCash);
    }
    public void Addbank(float amount)
    {
        userData.bankBalance += amount;
        uiManager.AddBank(userData.bankBalance);
        SaveUserData();
        Debug.Log("은행 잔액 변경: " + userData.bankBalance);
    }
}
