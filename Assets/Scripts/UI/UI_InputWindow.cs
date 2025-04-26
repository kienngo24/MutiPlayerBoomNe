using System;
using UnityEngine;
using TMPro;
using System.Linq;


public class UI_InputWindow : MonoBehaviour {

    private static UI_InputWindow instance;
    private Button_UI okBtn;
    private Button_UI cancelBtn;
    private TextMeshProUGUI titleText;
    private TMP_InputField inputField;


    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);        
        instance = this;

        okBtn = GetComponentsInChildren<Button_UI>().FirstOrDefault(btn => btn.gameObject.name == "okBtn");
        cancelBtn = GetComponentsInChildren<Button_UI>().FirstOrDefault(btn => btn.gameObject.name == "cancelBtn");
        titleText =  GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(tmg => tmg.gameObject.name == "titleText"); 
        inputField = GetComponentsInChildren<TMP_InputField>().FirstOrDefault(input => input.gameObject.name == "inputField"); 
        if(okBtn != null && cancelBtn != null)
            SetButtonBehaviour();
        Hide();
    }
    private void SetButtonBehaviour()
    {
        okBtn.SetHoverBehaviourType();
        cancelBtn.SetHoverBehaviourType();
    }
    private void Start() {
        
    }
    private void Show(string titleString, string inputString, string validCharacters, int characterLimit, Action onCancel, Action<string> onOk) {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

        titleText.text = titleString;

        inputField.characterLimit = characterLimit;
        inputField.onValidateInput = (string text, int charIndex, char addedChar) => {
            return ValidateChar(validCharacters, addedChar);
        };

        inputField.text = inputString;
        inputField.Select();

        okBtn.ClickFunc = () => {
            Hide();
            onOk(inputField.text);
        };

        cancelBtn.ClickFunc = () => {
            Hide();
            onCancel();
        };
    }
    public static void Show_Static(string titleString, string inputString, string validCharacters, int characterLimit, Action onCancel, Action<string> onOk) {
        instance.Show(titleString, inputString, validCharacters, characterLimit, onCancel, onOk);
    }
    public static void Show(string titleString, int defaultInt, Action onCancel, Action<int> onOk) 
    {
        instance.Show(titleString, defaultInt.ToString(),"0123456789-", 20, onCancel,
        (string inputText) =>
        {
            if (int.TryParse(inputText, out int _i)) {
                    onOk(_i);
                } else {
                    onOk(defaultInt);
                }
        });
    }
    private char ValidateChar(string validCharacters, char addedChar) {
        if (validCharacters.IndexOf(addedChar) != -1) {
            // Valid
            return addedChar;
        } else {
            // Invalid
            return '\0';
        }
    }
    

    
    public void Hide() => gameObject.SetActive(false);


}
