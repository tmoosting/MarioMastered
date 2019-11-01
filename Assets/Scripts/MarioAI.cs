using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAI : MonoBehaviour
{

    public enum Trigger { EnterPlatform, ExitPlatform, FailJumpMidAir, FallJumpLand, JumpEverShorter, WalkOffscreen }
    public enum MarioAction { None,LittleHop, LittleFreakout,  WalkBackOffPlatform,FreakOut, InvertControls, LimitJumps, WalkIntoSunshine } 

    Mario mario;

    // Triggers
    bool onPlatform;
    int platformEnterCount = 0; 
    int platformExitCount = 0; 
    int jumpFailCount = 0;
    int jumpShortCount = 0;

    bool waitTrigger = false;
    bool countFrames = false;

    Dictionary<MarioAction, System.Action> marioActionMapping = new Dictionary<MarioAction, System.Action>();
    int frame = 0;
    int sequenceCount = 0;

    void Update()
    {
     
            frame++;
       
    }

    void Start()
    {
        mario = MarioController.Instance.mario; 
        marioActionMapping.Add(MarioAction.LittleHop, MarioLittleHop);
        marioActionMapping.Add(MarioAction.LittleFreakout, MarioSmallFreakOut);
        marioActionMapping.Add(MarioAction.WalkBackOffPlatform, MarioWalkBackOffPlatform);
        marioActionMapping.Add(MarioAction.FreakOut, MarioFreakOut);
        marioActionMapping.Add(MarioAction.InvertControls, MarioInverted);
        marioActionMapping.Add(MarioAction.LimitJumps, MarioJumpHandicapped);
        marioActionMapping.Add(MarioAction.WalkIntoSunshine, MarioWalkSunshine);
       
    }

    public void CallTrigger(Trigger trigger)
    {
        if (trigger == Trigger.EnterPlatform)
            TriggerEnterPlatform();
        if (trigger == Trigger.ExitPlatform)
            TriggerExitPlatform();
        if (trigger == Trigger.FailJumpMidAir)
            TriggerMidAirFail();
        if (trigger == Trigger.FallJumpLand)
            TriggerFailJumpLand();
        if (trigger == Trigger.JumpEverShorter)
            TriggerJumpShort();  
        //if (trigger == Trigger.WalkOffscreen)
        //    TriggerJumpShort();
    }
    public void CallAction(MarioAction action)
    {
        if (marioActionMapping.ContainsKey(action))
            marioActionMapping[action]();
        else
            Debug.Log("Calling action that is not in dictionary" + action.ToString());
    }



    public void StartSequence()
    {
        UIController.Instance.MoveWorldTextToCorner();
        StartCoroutine(Sequence());    
    }
    IEnumerator Sequence()
    { 
        LockMario();

        ResetFrameCount();
        yield return new WaitUntil(() => frame >= 50);

        TextController.Instance.CallText(TextController.TextAction.SetDitchClothesText);

        if (GameController.Instance.textFreeMode == true)
        { 
            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 40);

            CallAction(MarioAction.LittleHop);
 
        }

        ResetFrameCount();
        yield return new WaitUntil(() => frame >= 100);

        TextController.Instance.ClearText();

        mario.MarioSpinJumps();

        ResetFrameCount();
        yield return new WaitUntil(() => frame >= 100);

        UnlockMario();

        yield return new WaitUntil(() => platformEnterCount == 2);

        ResetFrameCount();
        yield return new WaitUntil(() => frame >= 60);

        TextController.Instance.CallText(TextController.TextAction.SetLetsGoText);

        yield return new WaitUntil(() => jumpFailCount == 2);

 

        TextController.Instance.CallText(TextController.TextAction.SetExcuseText);

        if (GameController.Instance.textFreeMode == true)
        {
            LockMario();

            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 40);

            CallAction(MarioAction.LittleHop);

            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 40);

            CallAction(MarioAction.LittleFreakout);

            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 20);

            UnlockMario();
        }

        yield return new WaitUntil(() => jumpFailCount == 4);

        LockMario();

        TextController.Instance.CallText(TextController.TextAction.SetThoughtText);

        ResetFrameCount();
        yield return new WaitUntil(() => frame >= 120);

        TextController.Instance.ClearText();

        if (GameController.Instance.textFreeMode == true)
        {
            LockMario();

            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 40);

            CallAction(MarioAction.LittleHop);

            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 40);

            CallAction(MarioAction.LittleHop);

            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 60); 
        }

        CallAction(MarioAction.FreakOut);

        UnlockMario();

        ResetPlatformCount();
        yield return new WaitUntil(() => platformEnterCount == 1);

        LockMario();

        ResetFrameCount();
        yield return new WaitUntil(() => frame >= 200);

        CallAction(MarioAction.WalkBackOffPlatform);

        ResetFrameCount();
        yield return new WaitUntil(() => frame >= 50);

        UnlockMario();

        CallAction(MarioAction.InvertControls);

        ResetFrameCount();
        yield return new WaitUntil(() => frame >= 350);

        CallAction(MarioAction.LimitJumps);

        yield return new WaitUntil(() => (jumpShortCount >= 11 && mario.gameObject.transform.localPosition.x < 2.2f));

        LockMario();

        ResetFrameCount();
        yield return new WaitUntil(() => frame >= 250);

        CallAction(MarioAction.WalkIntoSunshine);

        yield return new WaitUntil(() => mario.gameObject.transform.localPosition.x > 4.5f);

        ResetFrameCount();
        yield return new WaitUntil(() => frame >= 150);

        GameController.Instance.SetGameState(GameController.GameState.EndScreen);


    }
    void ResetFrameCount()
    {
        frame = 0;
    } 
    void ResetPlatformCount()
    {
        platformEnterCount = 0;
    }
   

    //public void PauseMarioForSeconds(float seconds)
    //{
    //    if (seconds == 0)
    //        seconds = 99999999f;
    //    //TODO
    //}

    public void LockMario()
    {
     //   Debug.Log("lock");
        GameController.Instance.allowMovement = false;
    }  
    public void UnlockMario()
    {
     //   Debug.Log("UNLOCK");
        GameController.Instance.allowMovement = true;

    }

   


    void TriggerEnterPlatform()
    {
        onPlatform = true;
        platformEnterCount++;

        //if (platformEnterCount == 1)
        //    TextController.Instance.SetImageTextFirstPlatformTrigger();
        //if (platformEnterCount == 1)
        //{
        //    // walk off platform action
        //} 
    }
    void TriggerExitPlatform()
    {
        onPlatform = false;
        platformExitCount++;
    }
    void TriggerMidAirFail()
    {

    }
    void TriggerFailJumpLand()
    { 
        jumpFailCount++; 
    }
    void TriggerJumpShort()
    {
        jumpShortCount++; 
    }


    // -------- ACTIONS



    void MarioLittleHop()
    {
        StartCoroutine(LittleHop());

    }
    void MarioSmallFreakOut()
    {
        mario.MarioFreaksSmall();

    }
    void MarioFreakOut()
    {
        mario.MarioFreaks();
    }
    void MarioWalkBackOffPlatform()
    { 
      StartCoroutine(  MoveMarioLeft(1.6f, 0.6f));
    }
       void MarioInverted()
    {
        MarioController.Instance.FlipControls();
    }
    void MarioJumpHandicapped()
    {
        MarioController.Instance.LimitJumps();
    }  
    void MarioWalkSunshine()
    {
        TextController.Instance.CallCustomTextForSeconds("ciaorivederci", 12f);
        UIController.Instance.rightWall.SetActive(false);
        StartCoroutine(MoveMarioRight(10f, 0.7f));
    }

    // ----------- COROUTINES
 
    IEnumerator MoveMarioRight(float distance, float speed)
    {
        for (float t = 0.0f; t < distance; t += Time.deltaTime)
        {
            mario.gameObject.transform.position += Vector3.right * speed * Time.deltaTime; 
            yield return null;  
        } 
    }
    IEnumerator MoveMarioLeft( float distance, float speed)
    {
        for (float t = 0.0f; t < distance; t += Time.deltaTime)
        {
            mario.gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
            yield return null;
        }
    }

    IEnumerator LittleHop ()
    {
        for (float t = 0.0f; t < 0.1f; t += Time.deltaTime)
        {
            mario.gameObject.transform.position += Vector3.up * 2f * Time.deltaTime; 
            yield return null;
        }
    }

}
