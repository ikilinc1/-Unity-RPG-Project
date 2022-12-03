using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject continueButton;
    public GameObject loadingScreen;
    public GameObject saveObject;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Application.persistentDataPath + "/save.dat" != null)
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }

        Cursor.visible = true;
    }

    public void ContinueGame()
    {
        loadingScreen.SetActive(true);
        saveObject.SetActive(true);
        SaveScript.continueData = true;
        StartCoroutine(WaitToLoad());
    }
    
    public void NewGame()
    {
        SaveScript.playerHealth = 1.0f;
        SaveScript.newGame = true;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}
