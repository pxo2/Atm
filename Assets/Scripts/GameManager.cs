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
        else
        {
            Destroy(gameObject);
            return;
        }

        saveFile = Path.Combine(Application.persistentDataPath, "userData.json");

        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();

        LoadUserData();
    }
    private void Start()
    {
        if(uiManager != null)
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
            Debug.Log("저장된 데이터 없음, 기본값 설정");
            userData = new UserData("김규태", 85000, 200000);
            SaveUserData();
        }
        if (uiManager != null)
        {
            uiManager.AddBank(userData.bankBalance);
            uiManager.AddCash(userData.handCash);
        }
    }

    public void AddHandCash(float amount)
    {
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
