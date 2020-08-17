using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderManager : MonoBehaviour
{
    public Animator transitionAmim;
    public float transitionTime = 1f;

    public IEnumerator LoadIndexLevel(int levelIndex){
        transitionAmim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public IEnumerator LoadNamedLevel(string levelName){
        transitionAmim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }
}
