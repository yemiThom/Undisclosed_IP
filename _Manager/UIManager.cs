using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI problemText;                 // text that displays the maths problem
    public TextMeshProUGUI[] answersTexts;              // array of the berry answers texts
    public TextMeshProUGUI timeText;                    // text that displays the remaining level time
    public TextMeshProUGUI endText;                     // text displayed a the end of the game (win or game over)
    
    public Image correctBerryBar, incorrectBerryBar;    // images to indicate the berry collector tank fill level

    private static UIManager instance;   // Static instance which allows the script to be accessed
    public static UIManager Instance{
        get{
            if(instance == null){
                instance = GameObject.FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }



    // sets the ship UI to display the new problem
    public void SetProblemText (MathProblem problem)
    {
        string operatorText = "";
    
        // convert the problem operator from an enum to an actual text symbol
        switch(problem.operation)
        {
            case MathsOperation.Addition: operatorText = " + "; break;
            case MathsOperation.Subtraction: operatorText = " - "; break;
            case MathsOperation.Multiplication: operatorText = " x "; break;
            case MathsOperation.Division: operatorText = " ÷ "; break;
        }
    
        // set the problem text to display the problem
        problemText.text = problem.firstNumber + operatorText + problem.secondNumber;
    }
}
