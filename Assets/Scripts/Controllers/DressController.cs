using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressController : MonoBehaviour
{
    public static DressController Instance;


    public GameObject arrowLeft1;
    public GameObject arrowLeft2;
    public GameObject arrowLeft3;
    public GameObject arrowLeft4;
    public GameObject arrowRight1;
    public GameObject arrowRight2;
    public GameObject arrowRight3;
    public GameObject arrowRight4;
    public Image imageBox1;
    public Image imageBox2;
    public Image imageBox3;
    public Image imageBox4;
    public Image imageBox5;
    public Image imageFrame1; 
    public Image imageFrame2; 
    public Image imageFrame3; 
    public Image imageFrame4;
    public Image imageFrame5;
    public GameObject characterHead;
    public GameObject characterBody;
    public GameObject characterHands;
    public GameObject characterFeet;
    public GameObject characterPowerup;

    public Dictionary<int, Sprite> powerupSelection = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> dressSelection1 = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> dressSelection2 = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> dressSelection3 = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> dressSelection4 = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> powerupFrame = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> dressFrame1 = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> dressFrame2 = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> dressFrame3 = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> dressFrame4 = new Dictionary<int, Sprite>();
    public Dictionary<Mario.PowerupType, Sprite> powerupCharacter = new Dictionary<Mario.PowerupType, Sprite>();
    public Dictionary<Mario.HeadType, Sprite> dressCharacter1 = new Dictionary<Mario.HeadType, Sprite>();
    public Dictionary<Mario.BodyType, Sprite> dressCharacter2 = new Dictionary<Mario.BodyType, Sprite>();
    public Dictionary<Mario.HandsType, Sprite> dressCharacter3 = new Dictionary<Mario.HandsType, Sprite>();
    public Dictionary<Mario.FeetType, Sprite> dressCharacter4 = new Dictionary<Mario.FeetType, Sprite>();

    int index1 = 2;
    int index2 = 1;
    int index3 = 1;
    int index4 = 1;
    int index5 = 1;

    void Awake()
    {
        Instance = this;
    }

    public void InitializeDressPanel()
    {
        powerupSelection.Add(1, SpriteController.Instance.powerupBox);
        powerupSelection.Add(2, SpriteController.Instance.powerupBox2);
        powerupSelection.Add(3, SpriteController.Instance.powerupBox3);
        dressSelection1.Add(1, SpriteController.Instance.dressHead1);
        dressSelection1.Add(2, SpriteController.Instance.dressHead2);
        dressSelection1.Add(3, SpriteController.Instance.dressHead3);
        dressSelection1.Add(4, SpriteController.Instance.dressHead4);
        powerupFrame.Add(1, SpriteController.Instance.powerupFrame);
        powerupFrame.Add(2, SpriteController.Instance.powerupFrame2);
        powerupFrame.Add(3, SpriteController.Instance.powerupFrame3);
        dressFrame1.Add(1, SpriteController.Instance.dressFrameHead1);
        dressFrame1.Add(2, SpriteController.Instance.dressFrameHead2);
        dressFrame1.Add(3, SpriteController.Instance.dressFrameHead3);
        dressFrame1.Add(4, SpriteController.Instance.dressFrameHead4);

        dressSelection2.Add(1, SpriteController.Instance.dressBody1);
        dressSelection2.Add(2, SpriteController.Instance.dressBody2);
        dressSelection2.Add(3, SpriteController.Instance.dressBody3);
        dressSelection2.Add(4, SpriteController.Instance.dressBody4);
        dressFrame2.Add(1, SpriteController.Instance.dressFrameBody1);
        dressFrame2.Add(2, SpriteController.Instance.dressFrameBody2);
        dressFrame2.Add(3, SpriteController.Instance.dressFrameBody3);
        dressFrame2.Add(4, SpriteController.Instance.dressFrameBody4);

        dressSelection3.Add(1, SpriteController.Instance.dressHand1);
        dressSelection3.Add(2, SpriteController.Instance.dressHand2);
        dressSelection3.Add(3, SpriteController.Instance.dressHand3);
        dressSelection3.Add(4, SpriteController.Instance.dressHand4);
        dressFrame3.Add(1, SpriteController.Instance.dressFrameHand1);
        dressFrame3.Add(2, SpriteController.Instance.dressFrameHand2);
        dressFrame3.Add(3, SpriteController.Instance.dressFrameHand3);
        dressFrame3.Add(4, SpriteController.Instance.dressFrameHand4);

        dressSelection4.Add(1, SpriteController.Instance.dressFeet1);
        dressSelection4.Add(2, SpriteController.Instance.dressFeet2);
        dressSelection4.Add(3, SpriteController.Instance.dressFeet3);
        dressSelection4.Add(4, SpriteController.Instance.dressFeet4);
        dressFrame4.Add(1, SpriteController.Instance.dressFrameFeet1);
        dressFrame4.Add(2, SpriteController.Instance.dressFrameFeet2);
        dressFrame4.Add(3, SpriteController.Instance.dressFrameFeet3);
        dressFrame4.Add(4, SpriteController.Instance.dressFrameFeet4);  

        powerupCharacter.Add(Mario.PowerupType.Power1, SpriteController.Instance.powerupMario);
        powerupCharacter.Add(Mario.PowerupType.Power2, SpriteController.Instance.powerupMario2);
        powerupCharacter.Add(Mario.PowerupType.Power3, SpriteController.Instance.powerupMario3);
        dressCharacter1.Add(Mario.HeadType.Head1, SpriteController.Instance.dressCharacterHead1);
        dressCharacter1.Add(Mario.HeadType.Head2, SpriteController.Instance.dressCharacterHead2);
        dressCharacter1.Add(Mario.HeadType.Head3, SpriteController.Instance.dressCharacterHead3);
        dressCharacter1.Add(Mario.HeadType.Head4, SpriteController.Instance.dressCharacterHead4); 
        dressCharacter2.Add(Mario.BodyType.Body1, SpriteController.Instance.dressCharacterBody1);
        dressCharacter2.Add(Mario.BodyType.Body2, SpriteController.Instance.dressCharacterBody2);
        dressCharacter2.Add(Mario.BodyType.Body3, SpriteController.Instance.dressCharacterBody3);
        dressCharacter2.Add(Mario.BodyType.Body4, SpriteController.Instance.dressCharacterBody4); 
        dressCharacter3.Add(Mario.HandsType.Hands1, SpriteController.Instance.dressCharacterHands1);
        dressCharacter3.Add(Mario.HandsType.Hands2, SpriteController.Instance.dressCharacterHands2);
        dressCharacter3.Add(Mario.HandsType.Hands3, SpriteController.Instance.dressCharacterHands3);
        dressCharacter3.Add(Mario.HandsType.Hands4, SpriteController.Instance.dressCharacterHands4); 
        dressCharacter4.Add(Mario.FeetType.Feet1, SpriteController.Instance.dressCharacterFeet1);
        dressCharacter4.Add(Mario.FeetType.Feet2, SpriteController.Instance.dressCharacterFeet2);
        dressCharacter4.Add(Mario.FeetType.Feet3, SpriteController.Instance.dressCharacterFeet3);
        dressCharacter4.Add(Mario.FeetType.Feet4, SpriteController.Instance.dressCharacterFeet4); 
         

        imageFrame1.gameObject.SetActive(false);
        imageFrame2.gameObject.SetActive(false);
        imageFrame3.gameObject.SetActive(false);
        imageFrame4.gameObject.SetActive(false);
        imageFrame5.gameObject.SetActive(false);

        ReloadImages();
    }
    public void ReloadPowerupImages()
    {
        imageBox5.GetComponent<Image>().sprite = powerupSelection[index5];
        imageFrame5.GetComponent<Image>().sprite = powerupFrame[index5];
        if (index5 > 0)
            imageFrame5.gameObject.SetActive(true);
    }

    public void ReloadImages()
    {
        imageBox1.GetComponent<Image>().sprite = dressSelection1[index1];
        imageBox2.GetComponent<Image>().sprite = dressSelection2[index2];
        imageBox3.GetComponent<Image>().sprite = dressSelection3[index3];
        imageBox4.GetComponent<Image>().sprite = dressSelection4[index4];

        imageFrame1.GetComponent<Image>().sprite = dressFrame1[index1];
        imageFrame2.GetComponent<Image>().sprite = dressFrame2[index2];
        imageFrame3.GetComponent<Image>().sprite = dressFrame3[index3];
        imageFrame4.GetComponent<Image>().sprite = dressFrame4[index4];

        if (index1 > 0)
            imageFrame1.gameObject.SetActive(true);
        if (index2 > 0)
            imageFrame2.gameObject.SetActive(true);
        //if (index3 > 0)
        //    imageFrame3.gameObject.SetActive(true);
        if (index4 > 0)
            imageFrame4.gameObject.SetActive(true);
        if (index1 <= 0)
            imageFrame1.gameObject.SetActive(false);
        if (index2 <= 0)
            imageFrame2.gameObject.SetActive(false);
        //if (index3 <= 0)
        //    imageFrame3.gameObject.SetActive(false);
        if (index4 <= 0)
            imageFrame4.gameObject.SetActive(false);

    }

    public void ArrowLeft1()
    {
        index1--;
        if (index1 < 2)
            index1 = 2;
        //if (index1 == 1)
        //    arrowLeft1.SetActive(false);
        //if (index1 == 3)
        //    arrowRight1.SetActive(true);
        ReloadImages();
    }
    public void ArrowLeftPowerup()
    {
        index5--;
        if (index5 < 1)
            index5 = 1;
        ReloadPowerupImages();
    }
    public void ArrowLeft2()
    {
        index2--;
        if (index2 < 1)
            index2 = 1;
        //if (index2 == 1)
        //    arrowLeft2.SetActive(false);
        //if (index2 == 3)
        //    arrowRight2.SetActive(true);
        ReloadImages();
    }
    public void ArrowLeft3()
    {
        index3--;
        if (index3 < 1)
            index3 = 1;
        //if (index3 == 1)
        //    arrowLeft3.SetActive(false);
        //if (index3 == 3)
        //    arrowRight3.SetActive(true);
        ReloadImages();
    }
    public void ArrowLeft4()
    {
        index4--;
        if (index4 < 1)
            index4 = 1;
        //if (index4 == 1)
        //    arrowLeft4.SetActive(false);
        //if (index4 == 3)
        //    arrowRight4.SetActive(true);
        ReloadImages();
    }
    public void ArrowRight1()
    { 
        index1++;
        if (index1 > 4)
            index1 = 4;
        //if (index1 == 2)
        //    arrowLeft1.SetActive(true);
        //if (index1 == 4)
        //    arrowRight1.SetActive(false);
        ReloadImages();
        UIController.Instance.proceedButton.SetActive(true);
    }
    public void ArrowRightPowerup()
    {
        index5++;
        if (index5 > 3)
            index5 = 3; 
        ReloadPowerupImages();
    //    UIController.Instance.proceedButton.SetActive(true);
    }
    public void ArrowRight2()
    {
        index2++;
        if (index2 > 4)
            index2 = 4;
        //if (index2 == 2)
        //    arrowLeft2.SetActive(true);
        //if (index2 == 4)
        //    arrowRight2.SetActive(false);
        ReloadImages();
        UIController.Instance.proceedButton.SetActive(true);
    }
    public void ArrowRight3()
    {
        index3++;
        if (index3 > 4)
            index3 = 4;
        //if (index3 == 2)
        //    arrowLeft3.SetActive(true);
        //if (index3 == 4)
        //    arrowRight3.SetActive(false);
        ReloadImages();
        UIController.Instance.proceedButton.SetActive(true);
    }
    public void ArrowRight4()
    {
        index4++;
        if (index4 > 4)
            index4 = 4;
        //if (index4 == 2)
        //    arrowLeft4.SetActive(true);
        //if (index4 == 4)
        //    arrowRight4.SetActive(false);
        ReloadImages();
        UIController.Instance.proceedButton.SetActive(true);
    }
   
    public List<int> GetChoiceIndexes()
    {
        List<int> returnList = new List<int>();
        returnList.Add(index1);
        returnList.Add(index2);
        returnList.Add(index3);
        returnList.Add(index4);
        returnList.Add(index5);
        return returnList;
    }


    public void LoadDressToCharacter()
    {
        characterHead.GetComponent<SpriteRenderer>().sprite = dressCharacter1[MarioController.Instance.mario.chosenHeadType];
        characterBody.GetComponent<SpriteRenderer>().sprite = dressCharacter2[MarioController.Instance.mario.chosenBodyType];
        characterHands .GetComponent<SpriteRenderer>().sprite = dressCharacter3[MarioController.Instance.mario.chosenHandsType];
        characterFeet.GetComponent<SpriteRenderer>().sprite = dressCharacter4[MarioController.Instance.mario.chosenFeetType];
        characterPowerup.GetComponent<SpriteRenderer>().sprite = powerupCharacter[MarioController.Instance.mario.chosenPowerupType];
    }
}
