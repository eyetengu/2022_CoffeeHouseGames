using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Game_01_ChangeDue : MonoBehaviour
{
    [SerializeField] GameObject _welcomeImage;
    [SerializeField] private string[] _shopInventory;
    [SerializeField] private float[] _shopPrices;
    [SerializeField] private string[] _greatMsgText;

    [SerializeField] TMP_Text _orderItems;
    [SerializeField] TMP_Text _orderPrices;
    [SerializeField] TMP_Text _orderTotalField;
    [SerializeField] TMP_Text _playerMessageField;
    [SerializeField] TMP_Text _correctInARowField;
    [SerializeField] TMP_Text _amountTenderedField;
    
    [SerializeField] TMP_InputField _userInputField;

    [SerializeField] float _orderTotal;
    
    int _itemPriceID;
    int _correctInARow;
    float _amountTendered;
    float _exactChange;
    int _greatMsgID;
    int _game_01_Attempts;
    int _game_01_Correct;

    //BUILT-IN FUNCTIONS
    void Start()
    {
        _welcomeImage.SetActive(true);
        StartCoroutine(IntroDelaySequence());
        EstablishItemPricing();
        AcceptCustomerOrder();
    }


    //INITIALIZE PRICING
    void EstablishItemPricing()
    {
        _shopPrices = new float[_shopInventory.Length];
        foreach (var price in _shopPrices)
        {
            float randomAmount = Random.Range(3.00f, 6.70f);
            _shopPrices[_itemPriceID] = randomAmount;
            _itemPriceID++;
        }
    }


    //CORE FUNCTIONS
    void AcceptCustomerOrder()
    {
        _orderTotal = 0;
        string orderDetails = "";
        string orderPrices = "";

        int randomNumberOfItems = Random.Range(1, 7);
        for(int i = 0; i < randomNumberOfItems; i++)
        {
            var randomItem = Random.Range(0, _shopInventory.Length);
            orderDetails += _shopInventory[randomItem] + "\n";
            orderPrices += "$ " + _shopPrices[randomItem].ToString("F2") + "\n";
            _orderTotal += _shopPrices[randomItem];

            _orderItems.text = orderDetails;
            _orderPrices.text = orderPrices;
            _orderTotalField.text = "ORDER TOTAL: " + "$ " + _orderTotal.ToString("F2");
        }

        _amountTendered = Random.Range(_orderTotal, _orderTotal + 5);
        _amountTenderedField.text = "The Customer Has Given You $ " + _amountTendered.ToString("F2"); 
        _exactChange = _amountTendered - _orderTotal;

    }

    public void CheckUserInput()
    {
        string inputValue = _userInputField.text;

        if (inputValue == _exactChange.ToString("F2"))
            Correct();
        else
            Incorrect();
        Debug.Log(inputValue + " / " + _exactChange);

        _game_01_Attempts++;

        StartCoroutine(DelayClearPlayerMessage());
    }

    void Correct()
    {
        _correctInARow++;
        if(_correctInARow %5 == 0)
        {
            Audio_Manager.Instance.PlayGreatClip();

            _playerMessageField.text = _greatMsgText[_greatMsgID];

            _greatMsgID++;
            if (_greatMsgID > _greatMsgText.Length - 1)
                _greatMsgID = 0;
        }
        else
        {
            Audio_Manager.Instance.PlayGoodClip();

            _playerMessageField.text = "CORRECT";
        }

        _game_01_Correct++;

        CorrectInARowDisplay();
    }

    void Incorrect()
    {
        Audio_Manager.Instance.PlayBadClip();

        _playerMessageField.text = "INCORRECT";
        _correctInARow = 0;

        CorrectInARowDisplay();
    }

    void CorrectInARowDisplay()
    {
        Stats_Manager.Instance.UpdateGame_01_Stats(_game_01_Correct, _game_01_Attempts);
        _correctInARowField.text = _correctInARow.ToString();
    }


    //COROUTINES
    IEnumerator IntroDelaySequence()
    {
        yield return new WaitForSeconds(3);
        _welcomeImage.SetActive(false);
    }

    IEnumerator DelayClearPlayerMessage()
    {
        yield return new WaitForSeconds(2);

        _playerMessageField.text = "";
        _userInputField.text = "";

        AcceptCustomerOrder();
    }
    
}
