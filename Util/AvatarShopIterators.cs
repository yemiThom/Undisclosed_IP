using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarShopIterators : MonoBehaviour
{
    public bool isBackButton;
    public int choiceIterator;

    public Button backButton;
    public Button nextButton;

    public Sprite readySprite;

    public GameObject CamFlasher;
    public GameObject[] skinChoiceObjects;
    public GameObject[] hairChoiceObjects;
    public GameObject[] clothesChoiceObjects;
    public GameObject[] extrasChoiceObjects;


    // Start is called before the first frame update
    void Start()
    {
        choiceIterator = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(choiceIterator){
            case 0:
                // disable back button
                DisableBackButtton();

                // if hair choice objects are active, disable
                HideResetPenguins(1);
                HideResetPenguins(3);

                // show skin choice objects
                ShowPenguins(0);
                break;
            case 1:
                // if back button not active, enable
                if(!backButton.interactable)
                    EnableBackButton();
                
                // if skin/clothes choice objects are active, disable
                HideResetPenguins(0);
                HideResetPenguins(2);

                // show hair choice objects
                ShowPenguins(1);
                break;
            case 2:
                // if hair/extras choice objects are active, disable
                HideResetPenguins(1);
                HideResetPenguins(3);

                // show clothes choice objects
                ShowPenguins(2);
                break;
            case 3:
                // if clothes choice objects are active, disable
                HideResetPenguins(2);

                // show extras choice objects
                ShowPenguins(3);
                break;
        }
    }

    public void IterateBack(){
        isBackButton = true;

        //GameManager.Instance.SaveGameData();
    }

    public void IterateNext(){
        isBackButton = false;

        //GameManager.Instance.SaveGameData();
    }

    public void IterateChoices(){
        if(isBackButton){
            if(choiceIterator > 0){
                choiceIterator -= 1;
                DoCamFlash();
            }/*else{
                choiceIterator = 3;
            }*/
        }else{

            if(choiceIterator == 2){
                ChangeNextButton();
            }

            if(choiceIterator < 3){
                choiceIterator += 1;
                DoCamFlash();
            }else{
                //choiceIterator = 0;
                GameManager.Instance.SaveGameData();
                GameManager.Instance.GoToNamedScene("TestGameScene");
            }
        }

        //DoCamFlash();
    }

    void DoCamFlash(){
        CamFlasher.GetComponentInChildren<CameraFlashEffect>().doCameraFlash = true;
    }

    void DisableBackButtton(){
        if(backButton){
            backButton.interactable = false;
        }else{
            Debug.LogError("No Button attached!");
        }
    }

    void EnableBackButton(){
        if(backButton){
            backButton.interactable = true;
        }else{
            Debug.LogError("No Button attached!");
        }
    }

    void ChangeNextButton(){
        nextButton.image.sprite = readySprite;
    }

    void ShowPenguins(int objNum){
        switch (objNum){
            case 0:
                // activate main skin gameobject[0]
                skinChoiceObjects[0].gameObject.SetActive(true);
                break;
            case 1:
                // activate main hair gameobject[1]
                hairChoiceObjects[0].gameObject.SetActive(true);
                break;
            case 2:
                // activate main clothes gameobject[2]
                clothesChoiceObjects[0].gameObject.SetActive(true);
                break;
            case 3:
                // activate main extras gameobject[3]
                extrasChoiceObjects[0].gameObject.SetActive(true);
                break;
        }
    }

    void HideResetPenguins(int objNum){
        switch (objNum){
            case 0:
                // reset timer script
                skinChoiceObjects[1].GetComponent<Timer>().m_RunOnce = true;
                skinChoiceObjects[2].GetComponent<Timer>().m_RunOnce = true;

                // deactivate button objects
                skinChoiceObjects[3].gameObject.SetActive(false);
                skinChoiceObjects[4].gameObject.SetActive(false);

                // activate main skin gameobject[0]
                skinChoiceObjects[0].gameObject.SetActive(false);
                break;
            case 1:
                // reset timer script
                hairChoiceObjects[1].GetComponent<Timer>().m_RunOnce = true;

                // deactivate button objects
                hairChoiceObjects[2].gameObject.SetActive(false);

                // activate main hair gameobject[1]
                hairChoiceObjects[0].gameObject.SetActive(false);
                break;
            case 2:
                // reset timer script
                clothesChoiceObjects[1].GetComponent<Timer>().m_RunOnce = true;

                // deactivate button objects
                clothesChoiceObjects[2].gameObject.SetActive(false);

                // activate main clothes gameobject[2]
                clothesChoiceObjects[0].gameObject.SetActive(false);
                break;
            case 3:
                // reset timer script
                extrasChoiceObjects[1].GetComponent<Timer>().m_RunOnce = true;

                // deactivate button objects
                extrasChoiceObjects[2].gameObject.SetActive(false);

                // reset canvas group alpha to 0
                extrasChoiceObjects[3].GetComponent<CanvasGroup>().alpha = 0;

                // activate main extras gameobject[3]
                extrasChoiceObjects[0].gameObject.SetActive(false);
                break;
        }
    }
}
