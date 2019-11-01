using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mario : MonoBehaviour
{

    public enum PowerupType { Power1, Power2, Power3  }
    public enum HeadType { Head1, Head2, Head3, Head4  }
    public enum BodyType { Body1, Body2, Body3, Body4  }
    public enum HandsType { Hands1, Hands2, Hands3, Hands4  }
    public enum FeetType { Feet1, Feet2, Feet3, Feet4  }
     
    public SpriteRenderer headSprite;
    public SpriteRenderer bodySprite;
    public SpriteRenderer feetSprite;
    public SpriteRenderer powerupSprite;
    public GameObject textHolder;
    public TextMeshProUGUI customText;


    public PowerupType chosenPowerupType;
    public HeadType chosenHeadType;
    public BodyType chosenBodyType;
    public HandsType chosenHandsType;
    public FeetType chosenFeetType;

    public GameObject skeletonSprite;
    public Sprite emptyText;


    public bool hasCoin = false;

    bool onPlatform;
    bool failedStillMidAir;

    [HideInInspector]
    public bool isSpinJumping = false;

    bool hatRemoved = false;
    bool bodyRemoved = false;
    bool feetRemoved = false;

    int flipCounter;
    bool flipped = false;
    public bool failTriggered = false;

    public void EnterPlatform()
    { 
        MarioController.Instance.marioAI.CallTrigger(MarioAI.Trigger.EnterPlatform); 
    }
    public void ExitPlatform()
    {
        MarioController.Instance.marioAI.CallTrigger(MarioAI.Trigger.ExitPlatform); 
    }
    void FailJumpMidAir()
    {
        failedStillMidAir = true;
        MarioController.Instance.marioAI.CallTrigger(MarioAI.Trigger.FailJumpMidAir); 
    }
    void FailJumpMidLand()
    {
        failedStillMidAir = false;
        MarioController.Instance.marioAI.CallTrigger(MarioAI.Trigger.FallJumpLand);
    }
    void GrabCoin()
    {
        hasCoin = true;
        UIController.Instance.coin.SetActive(false);
        SoundControllerScript.PlaySound("coin");
    }
    private void Update()
    {
        if (gameObject.transform.localPosition.x > 0.92f && gameObject.transform.localPosition.x < 2f && gameObject.transform.localPosition.y > 1.92f)
            if (onPlatform == false)
                EnterPlatform();
        if (gameObject.transform.localPosition.x < 0.92f || gameObject.transform.localPosition.x > 2f || gameObject.transform.localPosition.y < 1.92f)
            if (onPlatform == true)
                ExitPlatform();

        if (gameObject.transform.localPosition.x > 1.32f && gameObject.transform.localPosition.x < 1.6f && gameObject.transform.localPosition.y > 1.9f)
            if (hasCoin == false)
                GrabCoin();

        if (failedStillMidAir == true)
            if (MarioController.Instance.IsMarioOnGround() == true)
                FailJumpMidLand();

        if (gameObject.transform.localPosition.x > 2.97f && gameObject.transform.localPosition.y > 2.7f)
            if (failedStillMidAir == false)
                FailJumpMidAir();

        // fix for fall through floor after failed jump
        if ( gameObject.transform.localPosition.y < 0.8f) 
            gameObject.transform.localPosition  = new Vector3(gameObject.transform.localPosition.x, 1f, 0); 

        // fix for stutter on ground level
        if (gameObject.transform.localPosition.y < 1.023633f) 
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, 1.021633f, 0); 
    }

  
    // ---------------- LAZY MODE TEXTS
    public void ClearText()
    {
        customText.gameObject.SetActive(false);
        textHolder.SetActive(false);
    }
    public void SetCustomText(string str)
    {
        customText.gameObject.SetActive(true);
        textHolder.SetActive(true);
        customText.text = str;
        textHolder.GetComponent<SpriteRenderer>().sprite = emptyText;
    }
 
    public IEnumerator ClearTextAfterSeconds(float seconds)
    { 
        for (float t = 0.0f; t < seconds; t += Time.deltaTime * 2)
            yield return null;
        ClearText();
    }
    // --------------------------- WALK OFF

    //public void MarioWalksOff()
    //{
    //    StartCoroutine(WalkOff());
    //}
    //void MarioHasWalkedOff()
    //{
    //    GameController.Instance.SetGameState(GameController.GameState.EndScreen);
    //}
    //IEnumerator WalkOff()
    //{
    //    float runDistance = 6f;
    //    float runSpeed =  0.7f;
    //    Mario mario = MarioController.Instance.mario;
    //    Transform marioTransform = mario.gameObject.transform;

    //    for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
    //    {
    //        mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
    //        marioTransform.position += Vector3.right * runSpeed * Time.deltaTime;
    //        yield return null;
    //    }
    //    MarioHasWalkedOff();
    //}

    // --------------------------- FREAKOUTS

    public void MarioFreaksSmall()
    {
        StartCoroutine(SmallLeft());
    }

    public void MarioFreaks()
    {
        RemovePowerup();
        StartCoroutine(VeryFirstLeft());
    }
    void MarioDoneFreaking()
    {
        GameController.Instance.SetGameState(GameController.GameState.DoneFreaking);
    }
    IEnumerator SmallLeft()
    {
        float runDistance = 0.1f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 2.4f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform; 
        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
            marioTransform.position += Vector3.left * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SmallRight());
    }
    IEnumerator SmallRight()
    {
        float runDistance = 0.12f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 2.2f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;
        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
            marioTransform.position += Vector3.right * runSpeed * Time.deltaTime;
            yield return null;
        } 
        StartCoroutine(SmallLeftAgain());
    }
    IEnumerator SmallLeftAgain()
    {
        float runDistance = 0.07f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 2f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;
        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
            marioTransform.position += Vector3.left * runSpeed * Time.deltaTime;
            yield return null;
        }
    }
    // TODO: Combine all below into one adjustable coroutine
    IEnumerator VeryFirstLeft()
    {
        float runDistance = 0.3f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 2f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;

      ClearText();

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
            marioTransform.position += Vector3.left * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FirstRight());
    }

    IEnumerator FirstRight()
    {
        float runDistance = 0.3f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed *1.5f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        { 
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
            marioTransform.position += Vector3.right * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FirstLeft());  
    }
    IEnumerator FirstLeft()
    {
        float runDistance = 0.25f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 1.3f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
            marioTransform.position += Vector3.left * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SecondRight());
    }
    IEnumerator SecondRight()
    {
        float runDistance = 0.25f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 1.1f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
            marioTransform.position += Vector3.right * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SecondLeft());
    }
    IEnumerator SecondLeft()
    {
        float runDistance = 0.25f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed *1.2f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
            marioTransform.position += Vector3.left * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ThirdRight());
    }
    IEnumerator ThirdRight()
    {
        float runDistance = 0.35f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 1.1f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
            marioTransform.position += Vector3.right * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ThirdLeft()); 
    }
    IEnumerator ThirdLeft()
    {
        float runDistance = 0.2f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed *1.1f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
            marioTransform.position += Vector3.left * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FourthRight());
    }
    IEnumerator FourthRight()
    {
        float runDistance = 0.25f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 1f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
            marioTransform.position += Vector3.right * runSpeed * Time.deltaTime;
            yield return null;
        }
        MarioDoneFreaking();
    }
    // --------------------------- SPINJUMP
    bool startedJump = false;
    public void MarioSpinJumps()
    {
        if (startedJump == false)
        {
            startedJump = true;
            isSpinJumping = true;
            StartCoroutine(StartCoroutineAfterSeconds(SpinJump(), MarioController.Instance.PreSpinPauseTime));
        }
     
    }
    void MarioHasJumped()
    {
        StartCoroutine(FinishSpin());
    }
    void MarioHasSpinned()
    {
        isSpinJumping = false;
        UIController.Instance.leftWall.SetActive(true);

        //       GameController.Instance.SetGameState(GameController.GameState.ClothesDropped);
    }
    IEnumerator StartCoroutineAfterSeconds(IEnumerator routine, float seconds)
    {
        for (float t = 0.0f; t < seconds; t += Time.deltaTime * 2)        
            yield return null;        
        StartCoroutine(routine);
    }
    IEnumerator SpinJump()
    {
        SoundControllerScript.PlaySound("changing his clothes");
        float jumpHeight = 1.2f;
        float jumpSpeed = 2f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < jumpHeight; t += Time.deltaTime)
        {
            if (t > jumpHeight / 3 && hatRemoved == false)
                RemoveHat();
            if (t > jumpHeight / 1.5 && bodyRemoved == false)
                RemoveBody();
            flipCounter++;
            if (flipCounter > 3)
            {
                flipCounter = 0;
                flipped = !flipped;
            }
       
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = flipped;
            mario.headSprite.GetComponent<SpriteRenderer>().flipX = flipped;
            mario.bodySprite.GetComponent<SpriteRenderer>().flipX = flipped;
            mario.feetSprite.GetComponent<SpriteRenderer>().flipX = flipped;
            marioTransform.position += Vector3.up * jumpSpeed * Time.deltaTime;
            yield return null;
        }
        MarioHasJumped();
    }
    IEnumerator FinishSpin()
    {
        float jumpHeight = 0.3f;
        float jumpSpeed = 5f;
        Mario mario = MarioController.Instance.mario;
        Transform marioTransform = mario.gameObject.transform;
        for (float t = 0.0f; t < jumpHeight; t += Time.deltaTime)
        {
            if (t > jumpHeight / 2 && feetRemoved == false)
                RemoveFeet();
            flipCounter++;
            if (flipCounter > 3)
            {
                flipCounter = 0;
                flipped = !flipped;
            }
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = flipped;
            marioTransform.position += Vector3.down * jumpSpeed * Time.deltaTime;
            yield return null;

        }
    
        MarioHasSpinned(); 
    }
    IEnumerator DropPowerup()
    { 
        float runDistance = 100f;
        float runSpeed = 10.0f;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            powerupSprite.gameObject.transform.position += Vector3.up * runSpeed * Time.deltaTime;
            yield return null;
        }
        powerupSprite.gameObject.SetActive(false);
    }
    public void RemovePowerup()
    {
        StartCoroutine(DropPowerup());
    }
    void RemoveHat()
    {
        SoundControllerScript.PlaySound("changing his clothes"); 
        hatRemoved = true;
        StartCoroutine(FlyHat());
    }
    void RemoveBody()
    { 
        bodyRemoved = true;
        StartCoroutine(FlyBody());
    }
    void RemoveFeet()
    {
        SoundControllerScript.PlaySound("changing his clothes");
        feetRemoved = true;
        StartCoroutine(FlyFeet());
    }


    IEnumerator FlyHat()
    {
        float runDistance = 46f;
        float runSpeed = 2.1f; 

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        { 
            headSprite.gameObject.transform.position += Vector3.left * runSpeed * Time.deltaTime;
            headSprite.gameObject.transform.position += Vector3.up * runSpeed * Time.deltaTime;
            yield return null;
        }
        headSprite.gameObject.SetActive(false); 
    }
    IEnumerator FlyBody()
    {
        float runDistance =46f;
        float runSpeed = 1.9f;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            bodySprite.gameObject.transform.position += Vector3.right * runSpeed * Time.deltaTime;
            bodySprite.gameObject.transform.position += Vector3.up * runSpeed *1.2f * Time.deltaTime;
            yield return null;
        }
        bodySprite.gameObject.SetActive(false);
    }
    IEnumerator FlyFeet()
    {
        float runDistance = 46f;
        float runSpeed = 2.0f;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            feetSprite.gameObject.transform.position += Vector3.left * runSpeed * Time.deltaTime;
            feetSprite.gameObject.transform.position += Vector3.down * runSpeed * Time.deltaTime / 2;
            yield return null;
        }
        feetSprite.gameObject.SetActive(false);
    }
}
