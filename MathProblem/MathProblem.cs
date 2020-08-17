﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MathProblem {
    public float firstNumber;           // first number in the problem
    public float secondNumber;          // second number in the problem
    public MathsOperation operation;    // operator between the two numbers
    public float[] answers;             // array of all possible answers including the correct one
 
    //[Range(0, 3)]
    public float correctBerry;             // correct berry answer
}

//Enum to determine how two numbers will be calculated together
public enum MathsOperation{
    Addition,
    Subtraction,
    Multiplication,
    Division
}