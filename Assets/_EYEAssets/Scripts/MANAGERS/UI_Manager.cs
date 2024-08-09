using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [Header("MAIN SCREENS")]
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _persistentTitleScreen;

    [Header("GAME SCENES")]
    [SerializeField] private GameObject[] _gamePanels;

    [Header("MESSAGE FIELDS")]
    [SerializeField] TMP_Text _playerMessage;



    //SINGLETON
    private static UI_Manager _instance;
    public static UI_Manager Instance
    {
        get 
        {
            if (_instance == null)
                Debug.Log("UI_Manager Not Found");
            
            return _instance;
        }
    }

    //BUILT-IN FUNCTIONS
    void OnEnable()
    {
        foreach (var panel in _gamePanels)
            panel.SetActive(false);

        _titleScreen.SetActive(true);
        _mainMenu.SetActive(false);
        _persistentTitleScreen.SetActive(true);
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        StartCoroutine(DelayHideTitleScreen());
    }


    //CORE FUNCTIONS
    public void UpdatePlayerMessage(string message)
    {
        _playerMessage.text = message;
        StartCoroutine(RefreshAndResetPlayerMessage());
    }

    //COROUTINES
    IEnumerator DelayHideTitleScreen()
    {
        yield return new WaitForSeconds(3);
        _titleScreen.SetActive(false);
        _mainMenu.SetActive(true);
    }

    IEnumerator RefreshAndResetPlayerMessage()
    {
        yield return new WaitForSeconds(3);
        
        _playerMessage.text = "";
    }
}
