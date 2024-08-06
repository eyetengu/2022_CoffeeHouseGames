using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_02_Comparisons : MonoBehaviour
{
    int _valueLeft;
    int _valueRight;

    [SerializeField] TMP_Text _leftValueDisplay;
    [SerializeField] TMP_Text _rightValueDisplay;
    [SerializeField] TMP_Text _comparisonValueDisplay;
    [SerializeField] TMP_Text _playerMessage;
    [SerializeField] TMP_Text _stringOfCorrect_text;

    [SerializeField] string[] _comparisonValues = { "<", "=", ">" };
    [SerializeField] string[] _successStrings = { "Way To Go", "You're On Fire", "You're An Ace" };
    int _comparisonID;
    int _numberOfCorrectInARow;
    int _successValue;
    int _numberCorrect;
    int _numberAttempts;


    //BUILT-IN FUNCTIONS
    void Start()
    {
        ChooseNewValues();

        _comparisonValueDisplay.text = _comparisonValues[_comparisonID];
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
            //ChooseNewValues();

        if (Input.GetKeyDown(KeyCode.W))
            _comparisonID++;
        if (Input.GetKeyDown(KeyCode.S)) 
            _comparisonID--;
        if (_comparisonID < 0) _comparisonID = 2;
        else if (_comparisonID > 2) _comparisonID = 0;

        _comparisonValueDisplay.text = _comparisonValues[_comparisonID];

        if (Input.GetKeyDown(KeyCode.Space))
            CompareAll();

    }

    //CORE LOGIC
    void ChooseNewValues()
    {
        _valueLeft = Random.Range(1, 101);
        _valueRight = Random.Range(1, 101);

        _leftValueDisplay.text = _valueLeft.ToString();
        _rightValueDisplay.text = _valueRight.ToString();
    }

    public void CompareAll()
    {
        if(_valueLeft < _valueRight)
        {
            if (_comparisonID == 0)
                Correct();
            else
                Incorrect();
        }
        else if(_valueLeft == _valueRight)
        {
            if(_comparisonID == 1)
                Correct();
            else
                Incorrect();
        }
        else if (_valueLeft > _valueRight)
        {
            if (_comparisonID == 2)
                Correct();
            else
                Incorrect();
        }
        _numberAttempts++;
    }

    void Correct() 
    {
        _numberCorrect++;
        _numberOfCorrectInARow++;
        _stringOfCorrect_text.text = _numberOfCorrectInARow.ToString();

        if (_numberOfCorrectInARow % 5 == 0)
        {
            Audio_Manager.Instance.PlayGreatClip();

            _playerMessage.text = _successStrings[_successValue];
            _successValue++;
            if (_successValue > _successStrings.Length - 1)
                _successValue = 0;
        }
        else
        {
            _playerMessage.text = "CORRECT";
            Audio_Manager.Instance.PlayGoodClip();
        }

        StartCoroutine(DelayClearPlayerMessage());
    }

    void Incorrect()
    {
        Audio_Manager.Instance.PlayBadClip();

        _numberOfCorrectInARow = 0;
        _stringOfCorrect_text.text = _numberOfCorrectInARow.ToString();

        _playerMessage.text = "INCORRECT";

        StartCoroutine(DelayClearPlayerMessage());
    }

    //COROUTINES
    IEnumerator DelayClearPlayerMessage()
    {
        Stats_Manager.Instance.UpdateGame_02_Stats(_numberCorrect, _numberAttempts);

        yield return new WaitForSeconds(2);
        _playerMessage.text = "";
        ChooseNewValues();
    }
}
