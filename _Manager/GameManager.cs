using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private CinemachineVirtualCamera cinemachine;
    private SaveManager saveManager;

    public MathProblem[] problems;              // list of all problems
    public int curProblem;                      // current problem the player needs to solve
    public int numAnsCorrect;                   // a count of the correct answers picked
    public int numAnsIncorrect;                 // a count of the incorrect answers picked
    public float timePerProblem;                // time allowed to answer each problem
    public float remainingLevelTime = 120;      // time remaining for the current level
    
    public LevelLoaderManager lvlLoaderManager; // level loader and transition object   

    [HideInInspector]
    public GameObject player;                   // player object

    // game data to load and save
    [HideInInspector]
    public GameData gameData;
    [HideInInspector]
    public int k_SelectedColorInt, k_SelectedHairColorInt, k_SelectedTopColorInt, k_SelectedBottomColorInt, k_SelectedHeadInt;
    [HideInInspector]
    public int k_SelectedHairInt, k_SelectedHatInt, k_SelectedFaceInt, k_SelectedMaskInt, k_SelectedTopInt, k_SelectedBottomInt;
    
    [HideInInspector]
    public float starRating;

    // math calculation variables
    public float correctCanisterFill, incorrectCanisterFill;
    public float currentCorrectFill, currentIncorrectFill;
    public float fillCanisterBy = 5f;
    private float maxFill = 100f;
    
    private bool CorrectBerryFound;
    private int CorrectBerryFinderCount = 0;

    // berry collection splash effect
    public CollectorSplashSpawner collectorSplashSpawner;

    public Color[] k_SkinTones = {
        // Real Skin Tones
        new Color(250, 242, 195, 255),
        new Color(250, 221, 192, 255),
        new Color(248, 209, 182, 255),
        new Color(247, 201, 154, 255),
        new Color(194, 142, 108, 255),
        new Color(149, 95, 72, 255),
        // Fun Skin Tones
        new Color(250, 220, 236, 255),
        new Color(221, 158, 158, 255),
        new Color(158, 187, 216, 255),
        new Color(99, 166, 207, 255),
        new Color(77, 208, 180, 255),
        new Color(0, 169, 100, 255)
    };

    public Color[] k_HairColors = {
        // left side hair colors
        new Color(57, 54, 46, 255),
        new Color(105, 73, 65, 255),
        new Color(208, 15, 74, 255),
        new Color(229, 117, 60, 255),
        // right side hair colors
        new Color(238, 214, 139, 255),
        new Color(176, 210, 96, 255),
        new Color(100, 173, 192, 255),
        new Color(131, 67, 107, 255)
    };

    public Color[] k_ClothesColors = {
        // left side clothes colors
        new Color(255, 92, 108, 255),
        new Color(122, 12, 21, 255),
        new Color(0, 190, 128, 255),
        new Color(180, 39, 73, 255),
        new Color(164, 108, 154, 255),
        new Color(72, 175, 191, 255),
        // right side clothes colors
        new Color(246, 236, 169, 255),
        new Color(255, 163, 192, 255),
        new Color(217, 209, 135, 255),
        new Color(240, 208, 105, 255),
        new Color(254, 144, 90, 255),
        new Color(199, 236, 241, 255)
    };

    private static GameManager instance;   // Static instance which allows the script to be accessed
    public static GameManager Instance{
        get{
            if(instance == null){
                instance = GameObject.FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    void Awake(){
        // set up player object
        player = GameObject.FindGameObjectWithTag("Player");
        // set up camera 
        cinemachine = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        if(cinemachine)
            cinemachine.Follow = player.transform;        
        // set up save manager object
        saveManager = GetComponent<SaveManager>();
        //load game data if it exists
        if(saveManager)
            LoadGameData();
    }

    void Start(){

        if(SceneManager.GetActiveScene().name != "TestShopScene"){
            FillCanisterBy();
            // set the initial math problem
            SetMathProblem(/*0*/);
            // reset the canister fill amount at start of level
            ResetCanisterFill();
            // start level countdown timer
            GetComponent<LevelTimer>().StartTimer();
        }
    }

#region Math Berry Logics

    // Win method called when the player answers all the problems
    public void Win(){
        Debug.Log("YOU WIN!");
        GetComponent<LevelTimer>().StopTimer();
        CalcStarRating();
        PauseGame();
    }

    // Lose method called when the remaining time on a level reaches 0
    public void Lose(){
        Debug.Log("Try again...");
        PauseGame();
    }

    // sets the current problem
    void SetMathProblem (/*int problem*/){
        curProblem = Random.Range(0, problems.Length); //problem;
        // remainingLevelTime = timePerProblem;
        // set UI text to show problem and answers
        UIManager.Instance.SetProblemText(problems[curProblem]);
    }

    public bool CheckOtherBerryAnswers(){
        GameObject[] berries = GameObject.FindGameObjectsWithTag("ProblemBerry");

        foreach(GameObject berry in berries){
            if(berry.GetComponent<ProblemBerry>().berryAnsNum == problems[curProblem].correctBerry){
                //Debug.Log("Correct berry answer found!");
                return true;
            }
        }

        return false;
    }

    void ResetBerryAnswers(){
        GameObject[] berries = GameObject.FindGameObjectsWithTag("ProblemBerry");
        CorrectBerryFound = false;

        foreach(GameObject berry in berries){
            berry.GetComponent<ProblemBerry>().SetBerryAnswer();

            CorrectBerryFinderCount++;

            if(berry.GetComponent<ProblemBerry>().berryAnsNum == problems[curProblem].correctBerry){
                //Debug.Log("Correct berry answer found!");
                CorrectBerryFound = true;
            }
            
            if(CorrectBerryFinderCount == berries.Length && !CorrectBerryFound){
                //Debug.Log("Set last berry to correct answer");
                berry.GetComponent<ProblemBerry>().berryAnsNum = problems[curProblem].correctBerry;
                
            }
        }
    }

    // called when the player enters the correct berry
    void CorrectAnswer(){
        //Debug.Log("Correct answer!");
        // is this the last problem?
        if(problems.Length - 1 == numAnsCorrect){
            Win();
        }else{
            numAnsCorrect++;
            IncreaseCorrectCanisterFill();
            SetMathProblem(/*curProblem + 1*/);
            ResetBerryAnswers();
        }
    }
    
    // called when the player enters the incorrect berry
    void IncorrectAnswer (){
        //Debug.Log("Incorrect answer...");
        // how many wrong answers so far
        if(numAnsIncorrect == problems.Length - 1){
            Lose();
        }else{
            numAnsIncorrect++;
            DecreaseCorrectCanisterFill();
            IncreaseIncorrectCanisterFill();
            //ResetBerryAnswers();
            //player.Stun();
        }
    }

    // called when the player enters a berry
    public void OnPlayerPickBerry (float berry){
        // call collector splash spawner object
        collectorSplashSpawner.MakeSplash();

        // did the player pick up the correct berry?
        if (berry == problems[curProblem].correctBerry)
            CorrectAnswer();
        else
            IncorrectAnswer();
    }

    void CalcStarRating(){
        int numCorrectAnswers = problems.Length - numAnsIncorrect;
        Debug.Log("Num Correct Answers: " + numCorrectAnswers);
        int totalNumAnswers = problems.Length;
        Debug.Log("Total Num Answers: " + totalNumAnswers);
        float fraction = (float)numCorrectAnswers / totalNumAnswers;
        Debug.Log("Fraction: " + fraction);
        starRating = fraction * 100f;
        Debug.Log("Star Rating: " + starRating);

        if(starRating >= 33f && starRating < 66f){
            // award player 1 star
            // save star rating
        }else if(starRating >= 66f && starRating < 70f){
            // award player 2 star
            // save star rating
        }else{
            // award player 3 star
            // save star rating
        }
    }
#endregion

#region UI Canister Fill Logics
    void FillCanisterBy(){
        fillCanisterBy = Mathf.RoundToInt(maxFill / problems.Length);
    }

    void ResetCanisterFill(){
        currentCorrectFill = currentIncorrectFill = 0;
        correctCanisterFill = incorrectCanisterFill = 0;

        UIManager.Instance.correctBerryBar.fillAmount = correctCanisterFill;
        UIManager.Instance.incorrectBerryBar.fillAmount = incorrectCanisterFill;
    }

    public void IncreaseCorrectCanisterFill(){
        currentCorrectFill += fillCanisterBy;
        float calc_fill = currentCorrectFill / maxFill;
        SetCorrectCanisterFill(calc_fill);
    }

    public void DecreaseCorrectCanisterFill(){
        if(currentCorrectFill > fillCanisterBy * 2){
            currentCorrectFill -= fillCanisterBy * 2;
        }else{
            currentCorrectFill = 0;
        }

        //Debug.Log("Current Correct Fill: " + currentCorrectFill);

        float calc_fill = currentCorrectFill / maxFill;
        SetCorrectCanisterFill(calc_fill);
    }

    void SetCorrectCanisterFill(float fillAmount){
        UIManager.Instance.correctBerryBar.fillAmount = fillAmount;
    }
    
    public void IncreaseIncorrectCanisterFill(){
        currentIncorrectFill += fillCanisterBy;
        float calc_fill = currentIncorrectFill / maxFill;
        SetIncorrectCanisterFill(calc_fill);
    }

    void SetIncorrectCanisterFill(float fillAmount){
        UIManager.Instance.incorrectBerryBar.fillAmount = fillAmount;
    }
#endregion

#region Save and Load Game Data
    // insert character selection choices into game data object 
    // then save game data to file
    public void SaveGameData(){
        gameData.savedSelectedColorInt = k_SelectedColorInt;
        gameData.savedSelectedHairColorInt = k_SelectedHairColorInt;
        gameData.savedSelectedTopColorInt = k_SelectedTopColorInt;
        gameData.savedSelectedBottomColorInt = k_SelectedBottomColorInt;
        gameData.savedSelectedHeadInt = k_SelectedHeadInt;
        gameData.savedSelectedHairInt = k_SelectedHairInt;
        gameData.savedSelectedHatInt = k_SelectedHatInt;
        gameData.savedSelectedFaceInt = k_SelectedFaceInt;
        gameData.savedSelectedMaskInt = k_SelectedMaskInt;
        gameData.savedSelectedTopInt = k_SelectedTopInt;
        gameData.savedSelectedBottomInt = k_SelectedBottomInt;
        
        saveManager.Save();
    }

    // load game data from file
    // then insert character selection into variables to be used
    public void LoadGameData(){
        saveManager.Load();

        k_SelectedColorInt = gameData.savedSelectedColorInt;
        k_SelectedHairColorInt = gameData.savedSelectedHairColorInt;
        k_SelectedTopColorInt = gameData.savedSelectedTopColorInt;
        k_SelectedBottomColorInt = gameData.savedSelectedBottomColorInt;
        k_SelectedHeadInt = gameData.savedSelectedHeadInt;
        k_SelectedHairInt = gameData.savedSelectedHairInt;
        k_SelectedHatInt = gameData.savedSelectedHatInt;
        k_SelectedFaceInt = gameData.savedSelectedFaceInt;
        k_SelectedMaskInt = gameData.savedSelectedMaskInt;
        k_SelectedTopInt = gameData.savedSelectedTopInt;
        k_SelectedBottomInt = gameData.savedSelectedBottomInt;
    }
#endregion

#region Game Menu Logic
    void PauseGame(){
        Time.timeScale = 0;
    }

    void ResumeGame (){
        Time.timeScale = 1;
    }
#endregion

#region SceneTransition Logic
    public void GoToNamedScene(string sceneName){
        if(lvlLoaderManager){
            StartCoroutine(lvlLoaderManager.LoadNamedLevel(sceneName));
        }else{
            Debug.LogError("Error loading scene: LevelLoaderManager not Found!");
        }
    }
#endregion

#region Character Customisation Getters
    // Getters for character customisation script
    public int GetSelectedColor(){
        return k_SelectedColorInt;
    }
    public int GetSelectedHairColor(){
        return k_SelectedHairColorInt;
    }
    public int GetSelectedTopColor(){
        return k_SelectedTopColorInt;
    }
    public int GetSelectedBottomColor(){
        return k_SelectedBottomColorInt;
    }
    public int GetSelectedHead(){
        return k_SelectedHeadInt;
    }
    public int GetSelectedHair(){
        return k_SelectedHairInt;
    }
    public int GetSelectedHat(){
        return k_SelectedHatInt;
    }
    public int GetSelectedFace(){
        return k_SelectedFaceInt;
    }
    public int GetSelectedMask(){
        return k_SelectedMaskInt;
    }
    public int GetSelectedTop(){
        return k_SelectedTopInt;
    }
    public int GetSelectedBottom(){
        return k_SelectedBottomInt;
    }
#endregion

#region Character Customisation Setters
    // setters for character customisation script
    public void SetSelectedColor(int colourChoice){
        k_SelectedColorInt = colourChoice;
    }
    public void SetSelectedHairColor(int hairColourChoice){
        k_SelectedHairColorInt = hairColourChoice;
    }
    public void SetSelectedTopColor(int topColorChoice){
        k_SelectedTopColorInt = topColorChoice;
    }
    public void SetSelectedBottomColor(int bottomColorChoice){
        k_SelectedBottomColorInt = bottomColorChoice;
    }
    public void SetSelectedHead(int headChoice){
        k_SelectedHeadInt = headChoice;
    }
    public void SetSelectedHair(int hairChoice){
        k_SelectedHairInt = hairChoice;
    }
    public void SetSelectedHat(int hatChoice){
        k_SelectedHatInt = hatChoice;
    }
    public void SetSelectedFace(int faceChoice){
        k_SelectedFaceInt = faceChoice;
    }
    public void SetSelectedMask(int maskChoice){
        k_SelectedMaskInt = maskChoice;
    }
    public void SetSelectedTop(int topChoice){
        k_SelectedTopInt = topChoice;
    }
    public void SetSelectedBottom(int bottomChoice){
        k_SelectedBottomInt = bottomChoice;
    }
#endregion
}
