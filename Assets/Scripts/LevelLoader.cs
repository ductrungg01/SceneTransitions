using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transtitionTime = 1.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        int nextSceneId = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneId == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneId = 0;
        }

        StartCoroutine(LoadLevel(nextSceneId));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transtitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
