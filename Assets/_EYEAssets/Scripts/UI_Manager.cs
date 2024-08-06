using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject[] _gamePanels;


    void OnEnable()
    {
        foreach (var panel in _gamePanels)
            panel.SetActive(false);

        _titleScreen.SetActive(true);
        _mainMenu.SetActive(false);
    }

    private void Start()
    {        
        StartCoroutine(DelayHideTitleScreen());
    }


    //COROUTINES
    IEnumerator DelayHideTitleScreen()
    {
        yield return new WaitForSeconds(3);
        _titleScreen.SetActive(false);
        _mainMenu.SetActive(true);
    }
}
