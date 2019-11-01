using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public static MarioController Instance;

    public Mario mario;
    public MarioAI marioAI;
    public float PreSpinPauseTime = 0f;
    public float FreakoutBaseSpeed = 2f;
    int failCount = 0;

    void Awake()
    {
        Instance = this;
    }

    public void LoadMario()
    {
        DressController.Instance.LoadDressToCharacter();
    }
    public void StartMarioAI()
    {
        marioAI.StartSequence();
    }

     

    public void SetMarioDress(List<int> indexList)
    {  
        mario.chosenHeadType = (Mario.HeadType)indexList[0]-1;
        mario.chosenBodyType = (Mario.BodyType)indexList[1]-1;
        mario.chosenHandsType = (Mario.HandsType)indexList[2]-1;
        mario.chosenFeetType = (Mario.FeetType)indexList[3]-1;  
        mario.chosenPowerupType = (Mario.PowerupType)indexList[4]-1;  
    }
   
    public void FlipControls()
    {
        mario.gameObject.GetComponent<MarioCharacter>().controlsInverted = true;
    }  
    public void LimitJumps()
    {
        mario.gameObject.GetComponent<MarioCharacter>().jumpsLimited = true;
    }
    public bool DressupComplete()
    {
        return true;
    }
    public bool PowerupComplete()
    {
        return true;
    }


   


    public bool IsMarioOnGround()
    {
        if (MarioController.Instance.mario.gameObject.GetComponent<MarioCharacter>().IsGrounded() == true)
            return true;
        else
            return false;
    }
    
    public void MarioHasFailedJump()
    {
      //  Debug.Log("failed pre");
        StartCoroutine(FailDelay());
        failCount++;
        if (failCount == 1)
            StartCoroutine(Text3OnLand());
        if (failCount > 1)
        {
            StartCoroutine(Text4OnLand());
            StartCoroutine(WaitToHitGround());
        }
    }
    IEnumerator Text3OnLand()
    {
        while (mario.GetComponent<MarioCharacter>().IsGrounded() == false)
            yield return null;
        TextController.Instance.SetImageTextFirstJumpFail();
    }
    IEnumerator Text4OnLand()
    {
        while (mario.GetComponent<MarioCharacter>().IsGrounded() == false)
            yield return null;
        TextController.Instance.SetImageTextSecondJumpFail();
    }
    IEnumerator FailDelay()
    {
        for (float t = 0.0f; t < 2f; t += Time.deltaTime * 2)
        {
            yield return null;       
        }        
        mario.failTriggered = false;
       // Debug.Log("failed post");
    }
    IEnumerator WaitToHitGround()
    {
        while (mario.gameObject.GetComponent<MarioCharacter>().IsGrounded() == false)
            yield return null;

        GameController.Instance.SetGameState(GameController.GameState.JumpFailed);

    }
}
