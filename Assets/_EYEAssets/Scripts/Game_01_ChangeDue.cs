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
            Debug.Log(_orderTotal + " BEFORE");
            _orderTotal += Mathf.Round(_shopPrices[randomItem] * 100f) / 100f;
            Debug.Log(_orderTotal + " AFTER");

            _orderItems.text = orderDetails;
            _orderPrices.text = orderPrices;
            _orderTotalField.text = "ORDER TOTAL: " + "$ " + _orderTotal.ToString("F2");
        }

        _amountTendered = Random.Range(_orderTotal, _orderTotal + 5);
        _amountTenderedField.text = "The Customer Has Given You $ " + _amountTendered.ToString("F2"); 
        _exactChange = _amountTendered - _orderTotal;

        Debug.Log(_amountTendered.ToString("F2") + " / " + _orderTotal.ToString("F2"));
    }

    public void CheckUserInput()
    {
        string inputValue = _userInputField.text;
        
        string exactChange = _exactChange.ToString("F2");
        Debug.Log("Exact Change: " + exactChange);
        
        _game_01_Attempts++;

        if (inputValue == exactChange)
            Correct();
        else
            Incorrect();
        Debug.Log(inputValue + " / " + exactChange);


        StartCoroutine(DelayClearPlayerMessage());
    }

    void Correct()
    {
        _correctInARow++;
        if(_correctInARow %5 == 0)
        {
            Audio_Manager.Instance.PlayGreatClip();

            UI_Manager.Instance.UpdatePlayerMessage(_greatMsgText[_greatMsgID]);
//            _playerMessageField.text = _greatMsgText[_greatMsgID];

            _greatMsgID++;
            if (_greatMsgID > _greatMsgText.Length - 1)
                _greatMsgID = 0;
        }
        else
        {
            Audio_Manager.Instance.PlayGoodClip();
            UI_Manager.Instance.UpdatePlayerMessage("CORRECT");

 //           _playerMessageField.text = "CORRECT";
        }

        _game_01_Correct++;

        CorrectInARowDisplay();
    }

    void Incorrect()
    {
        Audio_Manager.Instance.PlayBadClip();
        UI_Manager.Instance.UpdatePlayerMessage("INCORRECT");

//        _playerMessageField.text = "INCORRECT";
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
        UI_Manager.Instance.UpdatePlayerMessage("");

//        _playerMessageField.text = "";
        _userInputField.text = "";

        AcceptCustomerOrder();
    }
    
}
