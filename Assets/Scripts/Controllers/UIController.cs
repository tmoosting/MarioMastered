using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{ 
    public static UIController Instance;


    public GameObject canvas;
    public GameObject startPanel;
    public GameObject dressupPanel;
    public GameObject powerupPanel;
    public GameObject marioPanel;
    public GameObject blackPanel;
    public GameObject endPanel;
    public TextMeshProUGUI clickToStartText;
    public TextMeshProUGUI textOrginal;
    public TextMeshProUGUI textI;
    public TextMeshProUGUI textL;

    public TextMeshProUGUI textWorldBig1;
    public TextMeshProUGUI textWorldBig2;
    public TextMeshProUGUI textWorldSmall1;
    public TextMeshProUGUI textWorldSmall2;



    public GameObject standingPlatform;
    public GameObject marioFrame;
    public GameObject selectionHolder;
    public GameObject proceedButton;

    bool titleScreenUpdated = false;
    bool buttonPressedI = false;
    bool buttonPressedL = false;

    public Button powerupProceedButton;
    public GameObject levelHolder;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject coin;

    public void LoadLevel()
    {
        levelHolder.SetActive(true); 
    }

    void Awake()
    {
        Instance = this;
        startPanel.SetActive(false);
        dressupPanel.SetActive(false);
        powerupPanel.SetActive(false);
        blackPanel.SetActive(false);
        endPanel.SetActive(false);
        canvas.SetActive(true); 
        standingPlatform.SetActive(true);
        marioFrame.SetActive(false);
        selectionHolder.SetActive(false);
        proceedButton.SetActive(true);
        marioPanel.SetActive(false);
        levelHolder.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("i") && titleScreenUpdated == false)
        {
            SoundControllerScript.PlaySound("mario-woohoo");
            buttonPressedI = true;
            UpdateTitleScreen();
        }
        else if (Input.GetKeyDown("l") && titleScreenUpdated == false)
        {
            buttonPressedL = true;
            UpdateTitleScreen();
        }
        if (titleScreenUpdated == true)
        {
            if (buttonPressedI == true)
                if (Input.GetKeyDown("l"))
                    ClearTitleScreen(); 
            if (buttonPressedL == true)
                if (Input.GetKeyDown("i"))
                    ClearTitleScreen();
        }

        if (Input.GetKeyDown("c"))
        {
            //   LoadEndScreen();
            //  TextController.Instance.CallCustomTextForSeconds("ooiiodssd that s ahshhds  dsajdsaj ", 6f);
          //  SoundControllerScript.StopSound();
        }
    }

    // TITLE SCREEN
    public void ShowTitleScreen()
    {
        startPanel.SetActive(true);
    } 
    public void UpdateTitleScreen()
    {
        textOrginal.gameObject.SetActive(false);
        titleScreenUpdated = true;

        if (buttonPressedI == true)        
            textL.gameObject.SetActive(true);        
        if (buttonPressedL == true)        
            textI.gameObject.SetActive(true); 
    }
    public void ClearTitleScreen()
    {
        startPanel.SetActive(false);
        GameController.Instance.SetGameState(GameController.GameState.DressupScreen);
    }

    // DRESSUP SCREEN
    public void ShowDressUpScreen()
    {
        marioPanel.SetActive(true);
        dressupPanel.SetActive(true);
        MaterializeDressUpScreen();
        DressController.Instance.InitializeDressPanel();
    }
    public void ClickDressupButton()
    {
        if (MarioController.Instance.DressupComplete())        
            ClearDressUpScreen();        
    }
    public void ClearDressUpScreen()
    {
        dressupPanel.SetActive(false);
        MarioController.Instance.SetMarioDress(DressController.Instance.GetChoiceIndexes());
        GameController.Instance.SetGameState(GameController.GameState.PowerupScreen);
       
    }

    // POWERUP SCREEN
    public void ShowPowerUpScreen()
    {
        powerupPanel.SetActive(true);
        DressController.Instance.ReloadPowerupImages();
    }
    public void ClickPowerupButton()
    {
        if (MarioController.Instance.PowerupComplete())
            ClearPowerUpScreen();
    }
    public void ClearPowerUpScreen()
    {
        StartFadeToLevel();
    }

    void StartFadeToLevel()
    {
        powerupProceedButton.gameObject.SetActive(false);
        StartCoroutine(FadeBlackInBeforeLevel(1f, 3.5f));
        blackPanel.SetActive(true); 
    }
    void EndFadeToLevel()
    { 
    
    }

    void MaterializeDressUpScreen()
    {
        marioFrame.SetActive(true);
        StartCoroutine(FadeTo(1.0f, 0.1f));
       
    }
    IEnumerator FadeTo(float aValue, float aTime)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / (aTime / 4 ))
        { 
            yield return null;
        }

        float alpha = marioFrame.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            marioFrame.GetComponent<Image>().color = newColor;
            yield return null;
        }
        selectionHolder.SetActive(true); 
    }

    public void LoadEndScreen()
    {        
        Color transparent = new Color(0, 0, 0, 0);
        blackPanel.GetComponent<Image>().color = transparent;

        blackPanel.SetActive(true);

        StartCoroutine(FadeBlackIn(1f, 3.5f)); 
    
    }
    public void MoveWorldTextToCorner()
    {
        textWorldBig1.gameObject.SetActive(false);
        textWorldBig2.gameObject.SetActive(false);
        textWorldSmall1.gameObject.SetActive(true);
        textWorldSmall2.gameObject.SetActive(true);
    }
    public void SetWorldText()
    {
        textWorldSmall2.text = "1-0";
    }
    IEnumerator FadeBlackInBeforeLevel(float aValue, float aTime)
    {
        float alpha = blackPanel.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / (aTime /4))
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            blackPanel.GetComponent<Image>().color = newColor;
            yield return null;
        }
        powerupPanel.SetActive(false);
        marioPanel.SetActive(false);
        textWorldBig1.gameObject.SetActive(true);
        textWorldBig2.gameObject.SetActive(true);
        GameController.Instance.SetGameState(GameController.GameState.LevelOpen);



        StartCoroutine(FadeBlackOutBeforeLevel(0f, aTime));
    }
    IEnumerator FadeBlackOutBeforeLevel(float aValue, float aTime)
    {
        float alpha = blackPanel.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            blackPanel.GetComponent<Image>().color = newColor;
            yield return null;
        }

        EndFadeToLevel();
    }
    IEnumerator FadeBlackIn(float aValue, float aTime)
    { 
        float alpha = blackPanel.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            blackPanel.GetComponent<Image>().color = newColor;
            yield return null;
        }
        StartCoroutine(PostFadeInDelay());
    }
    IEnumerator PostFadeInDelay()
    {
        for (float t = 0.0f; t < 1f; t += Time.deltaTime * 2)
            yield return null;
        endPanel.SetActive(true);
        StartCoroutine(FadeBlackOut(0f, 6f));
    }
    IEnumerator FadeBlackOut(float aValue, float aTime)
    {
        SoundControllerScript.PlaySound("FINAL sound - end scene beach");
        float alpha = blackPanel.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            blackPanel.GetComponent<Image>().color = newColor;
            yield return null;
        }
        
    }
}
