using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public enum GameState { WelcomeScreen, DressupScreen, PowerupScreen, LevelOpen, WalkedIn, ClothesDropped,  JumpFailed, MarioFreaks, DoneFreaking, BadControls, JumpedAround, EndScreen}
     

    GameState currentGameState;
    
    [HideInInspector]
    public bool allowMovement = false;
     

    Mario mario;
    MarioAI marioAI;

    public bool textFreeMode;

    void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        marioAI = MarioController.Instance.marioAI;
        OpenGame();
        TextController.Instance.HideTextPanel();
        mario = MarioController.Instance.mario;
    }

   void OpenGame()
    { 
            SetGameState(GameState.WelcomeScreen); 
    } 


    public void SetGameState (GameState state)
    {
        if (state == GameState.WelcomeScreen)
        {
            
            UIController.Instance.ShowTitleScreen();
        }
        if (state == GameState.DressupScreen)
        {
       //     TextController.Instance.ShowTextPanel(state);
            UIController.Instance.ShowDressUpScreen();
        }
        if (state == GameState.PowerupScreen)
        {
            UIController.Instance.ShowPowerUpScreen();
        }
        if (state == GameState.LevelOpen)
        {
            SoundControllerScript.PlaySound("background sound");
            MarioController.Instance.LoadMario();
            UIController.Instance.LoadLevel();
            DressController.Instance.LoadDressToCharacter();
            
            // Calls sequence start on finish:
            StartCoroutine(MoveMarioRight(5f, 1.7f, 0.7f)); 
        } 
        //if (state == GameState.WalkedIn)
        //{
        //    StartCoroutine(PostWalkInDelay());
        //}
        //if (state == GameState.ClothesDropped)
        //{
        //    SoundControllerScript.PlaySound("changing his clothes");
        //    allowMovement = true;
        //}
        //if (state == GameState.JumpFailed)
        //{
        //    allowMovement = false;
        //    StartCoroutine(PostFailDelay());
        //}
        //if (state == GameState.MarioFreaks)
        //{
        //    MarioController.Instance.mario.MarioFreaks();
        //}
        //if (state == GameState.DoneFreaking)
        //{
        //    StartCoroutine(PostFreakDelay());

        //}
        //if (state == GameState.BadControls)
        //{
       //  MarioController.Instance.FlipControls();
        //    allowMovement = true;
        //} 
        if (state == GameState.EndScreen)
        {
            SoundControllerScript.StopSound();
            UIController.Instance.LoadEndScreen();
        }
        currentGameState = state;
    }

    IEnumerator MoveMarioRight(float delay, float distance, float speed)
    {

        for (float t = 0.0f; t < delay; t += Time.deltaTime)
        { 
            yield return null;
        }
        for (float t = 0.0f; t < distance; t += Time.deltaTime)
        {
            mario.gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
            yield return null;
        }
        MarioController.Instance.StartMarioAI();
    }

    public GameState GetGameState()
    {
        return currentGameState;
    }

    //IEnumerator PostWalkInDelay()
    //{
    //    Debug.Log("PostWalkInTextDelay");

    //    for (float t = 0.0f; t < 0.1f; t += Time.deltaTime  )
    //        yield return null;

    //    TextController.Instance.CallText(TextController.TextAction.SetDitchClothesText);
    //    StartCoroutine(PostWalkInTextDelay());
    //}
    //IEnumerator PostWalkInTextDelay()
    //{
      
    //    for (float t = 0.0f; t < 0.2f; t += Time.deltaTime  )
    //        yield return null;
    //    mario.ClearText();
    //    Debug.Log("PostWalkInTextDelay");
    //    MarioController.Instance.mario.MarioSpinsAndJumps();
    //}
   

    //IEnumerator PostFailDelay()
    //{
    //    for (float t = 0.0f; t < 5f; t += Time.deltaTime * 2)
    //        yield return null;
    //    while (MarioController.Instance.mario.gameObject.transform.localPosition.y > 2f)
    //        yield return null;
    //    SetGameState(GameState.MarioFreaks);
    //}
    //IEnumerator PostFreakDelay()
    //{
    //    for (float t = 0.0f; t < 0.5f; t += Time.deltaTime * 2)
    //        yield return null;
    //    SetGameState(GameState.BadControls);
    //}
    //IEnumerator PostJumpFailDelay()
    //{
    //    for (float t = 0.0f; t < 8f; t += Time.deltaTime * 2)
    //        yield return null;
    //    SetGameState(GameState.JumpedAround);
    //}
 
}
