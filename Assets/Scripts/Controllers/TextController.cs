using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextController : MonoBehaviour
{
    public static TextController Instance;

    public enum TextAction { None, SetDitchClothesText, SetLetsGoText, SetExcuseText, SetThoughtText,  }

    Dictionary<TextAction, System.Action> textActionMapping = new Dictionary<TextAction, System.Action>();

    Mario mario;
    public GameObject textPanel;
    public TextMeshProUGUI actualText;

    public Sprite textImagePreSpin;
    public Sprite textImageFirstPlatformTrigger;
    public Sprite textImagePostJumpFail1;
    public Sprite textImagePostJumpFail2; 

    //public string textIntro = "Choose an outfit for Mario";
    public string string1;
    public string string2;
    public string string3;

    bool waitingForInput = false;

    void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
         mario = MarioController.Instance.mario; 
        textActionMapping.Add(TextAction.SetDitchClothesText, SetImageTextPreSpin);
        textActionMapping.Add(TextAction.SetLetsGoText, SetImageTextFirstPlatformTrigger);
        textActionMapping.Add(TextAction.SetExcuseText, SetImageTextFirstJumpFail);
        textActionMapping.Add(TextAction.SetThoughtText, SetImageTextSecondJumpFail); 

        if (GameController.Instance.textFreeMode == true)
        {
            MarioController.Instance.mario.textHolder.transform.localPosition = new Vector3(-200f, -200f, 0);
            MarioController.Instance.mario.customText.gameObject.transform.localPosition = new Vector3(-200f, -200f, 0);
        }
    }
    public void CallText(TextAction action)
    {
        if (textActionMapping.ContainsKey(action))
            textActionMapping[action]();
        else
            Debug.Log("Calling 'text that is no in dictionary" + action.ToString()); 
    }
    public void CallCustomTextForSeconds (string str, float seconds)
    { 
        SoundControllerScript.PlaySound("text");
        mario.SetCustomText(str);
        mario.StartCoroutine(mario.ClearTextAfterSeconds(seconds));
    }
    public void SetTextUntilInput(string str)
    { 
        mario.SetCustomText(str);
        waitingForInput = true;
    }
    private void Update()
    {
        if (Input.anyKey)
            if (waitingForInput == true)
                mario.ClearText();
    }
  
    //public void ShowTextPanel(GameController.GameState state)
    //{
    //    textPanel.SetActive(true);
    //    if (state == GameController.GameState.DressupScreen)
    //        actualText.text = textIntro;
    //}

    public void HideTextPanel()
    {
     //   textPanel.SetActive(false);

    }
    public void ClearText()
    {
        mario.ClearText();
    }


    public void SetImageTextPreSpin()
    {
        SoundControllerScript.PlaySound("text");
        mario.textHolder.SetActive(true);
        mario.textHolder.GetComponent<SpriteRenderer>().sprite = textImagePreSpin;
    }
    public void SetImageTextFirstPlatformTrigger()
    {
        SoundControllerScript.PlaySound("text");
        mario.textHolder.SetActive(true);
        mario.textHolder.GetComponent<SpriteRenderer>().sprite = textImageFirstPlatformTrigger;
        StartCoroutine(mario.ClearTextAfterSeconds(6f));
    }
    public void SetImageTextFirstJumpFail()
    {
        SoundControllerScript.PlaySound("text");
        mario.textHolder.SetActive(true);
        mario.textHolder.GetComponent<SpriteRenderer>().sprite = textImagePostJumpFail1;
        StartCoroutine(mario.ClearTextAfterSeconds(6f));
    }
    public void SetImageTextSecondJumpFail()
    {
        SoundControllerScript.PlaySound("text");
        mario.textHolder.SetActive(true);
        mario.textHolder.GetComponent<SpriteRenderer>().sprite = textImagePostJumpFail2; 
    }
  

}
