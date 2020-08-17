using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemBerry : MonoBehaviour
{
    public float berryAnsNum;
    private int lastValue;

    void Start(){
        SetBerryAnswer();   // set the berries answer variable
    }

    public void SetBerryAnswer()
    {
        if(GameManager.Instance.CheckOtherBerryAnswers()){
            // get a random answer number from the list of answers 
            // in the current math problem, including wrong answers
            berryAnsNum = GameManager.Instance.problems[GameManager.Instance.curProblem].answers[Random.Range(0, GameManager.Instance.problems[GameManager.Instance.curProblem].answers.Length)];
        }else{
            berryAnsNum = GameManager.Instance.problems[GameManager.Instance.curProblem].correctBerry;
        }
        //berryAnsNum = GameManager.Instance.problems[GameManager.Instance.curProblem].answers[UniqueRandomFloat(0, GameManager.Instance.problems[GameManager.Instance.curProblem].answers.Length)];
        // set the answers texts to display the correct and incorrect answers
        ChangeAnswerText(berryAnsNum.ToString());
    }

    int UniqueRandomFloat(int min, int max){
        int val = Random.Range(min, max);
        while(lastValue == val)
        {
            val = Random.Range(min, max);
        }
        lastValue = val;
        return val;
    }

    void ChangeAnswerText(string ansTxt){
        transform.parent.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ansTxt;
    }

    void OnTriggerEnter2D(Collider2D col){
        // if this object collides with the player object
        // call OnPlayerPickBerry() to decide if the answer is correct or not
        // then reset the berry spawn object 
        // before destroying this gameobject
        if(col.CompareTag("Player")){
            GameManager.Instance.OnPlayerPickBerry(berryAnsNum);

            ChangeAnswerText("");

            if(transform.parent != null)
                transform.parent.GetComponent<BerrySpawner>().berryIsPicked = true;

            Destroy(this.gameObject);
        }
    }
}
