using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats_Manager : MonoBehaviour
{
    private static Stats_Manager _instance;
    public static Stats_Manager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Stats_Manager Not Found");

            return _instance;
        }
    }

    [SerializeField] private TMP_Text _game_01_Display;    
    [SerializeField] private TMP_Text _game_02_Display;    
    [SerializeField] private TMP_Text _game_03_Display;

    int _correct_01;
    int _attempt_01;

    int _correct_02;
    int _attempt_02;

    int _correct_03;
    int _attempt_03;

    //BUILT-IN FUNCTIONS
    private void Awake()
    {
        _instance = this;
    }


    //CORE FUNCTIONS
    public void UpdateGame_01_Stats(int correct, int attempts)
    {
        _correct_01 += correct;
        _attempt_01 += attempts;

        _game_01_Display.text = "GAME 01: " + _correct_01 + " / " + _attempt_01;
    }

    public void UpdateGame_02_Stats(int correct, int attempts)
    {
        _correct_02 += correct;
        _attempt_02 += attempts;

        _game_02_Display.text = "GAME 02: " + _correct_02 + " / " + _attempt_02;
    }

    public void UpdateGame_03_Stats(int correct, int attempts)
    {
        _correct_03 += correct;
        _attempt_03 += attempts;

        _game_03_Display.text = "GAME 03: " + _correct_03 + " / " + _attempt_03;
    }

}
