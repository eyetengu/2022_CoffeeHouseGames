using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_03_Percentages_Decimals_Fractions : MonoBehaviour
{
    [SerializeField] private int[] _percentages;
    [SerializeField] private float[] _decimal;
    [SerializeField] private int[] _fraction;

    [SerializeField] private TMP_Text _percentageField;
    [SerializeField] private TMP_Text _decimalField;
    [SerializeField] private TMP_Text _fractionField;
    [SerializeField] private TMP_Text _playerMessage;


    int _percentID;
    int _decimalID;
    int _fractionID;
    int _numberCorrect;
    int _numberAttempt;


    void Start()
    {
        DisplayRandomPercentValue();
        DisplayRandomDecimalValue();
        DisplayRandomFractionValue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CompareAllValues();
        }
    }

    void DisplayAllCorrectValues()
    {
        int generalID = Random.Range(0, 13);
        Debug.Log("ID: " + generalID);
        _percentageField.text = _percentages[generalID].ToString() + "%";
        _decimalField.text = _decimal[generalID].ToString();
        _fractionField.text = "1/" +_fraction[generalID].ToString();
    }

    void DisplayRandomPercentValue()
    {
        _percentID = Random.Range(0, 13);
        _percentageField.text = _percentages[_percentID].ToString() + "%";
    }

    void DisplayRandomDecimalValue() 
    {
        _decimalID = Random.Range(0, 13);
        _decimalField.text = _decimal[_decimalID].ToString();
    }

    void DisplayRandomFractionValue()
    {
        _fractionID = Random.Range(0, 13);
        _fractionField.text = _fraction[_fractionID].ToString();
    }

    public void NextPercentage(int value)
    {
        _percentID += value;

        if (_percentID > _percentages.Length - 1)
            _percentID = 0;

        if (_percentID < 0)
            _percentID = _percentages.Length - 1;

        _percentageField.text = _percentages[_percentID].ToString() + "%";
    }

    public void NextDecimal(int value)
    {
        _decimalID += value;

        if(_decimalID > _decimal.Length-1)
            _decimalID = 0;

        if (_decimalID < 0)
            _decimalID = _decimal.Length - 1;

        _decimalField.text = _decimal[_decimalID].ToString();

    }

    public void NextFraction(int value)
    {
        _fractionID += value;

        if(_fractionID > _fraction.Length-1)
            _fractionID = 0;

        if (_fractionID < 0)
            _fractionID = _fraction.Length - 1;

        _fractionField.text = "1/" + _fraction[_fractionID].ToString();

    }

    public void CompareAllValues()
    {
        if (_percentID == _decimalID && _decimalID == _fractionID)
            Correct();
        else
            Incorrect();

        _numberAttempt++;
    }

    void Correct()
    {
        _numberCorrect++;
        UI_Manager.Instance.UpdatePlayerMessage("CORRECT");

//        _playerMessage.text = "CORRECT";

        StartCoroutine(DelayClearPlayerMessage());
    }

    void Incorrect()
    {
        UI_Manager.Instance.UpdatePlayerMessage("INCORRECT");

//        _playerMessage.text = "INCORRECT";

        StartCoroutine(DelayClearPlayerMessage());
    }

    IEnumerator DelayClearPlayerMessage()
    {
        Stats_Manager.Instance.UpdateGame_03_Stats(_numberCorrect, _numberAttempt);

        yield return new WaitForSeconds(2);

        UI_Manager.Instance.UpdatePlayerMessage("");

 //       _playerMessage.text = "";
        DisplayRandomPercentValue();
    }
}
