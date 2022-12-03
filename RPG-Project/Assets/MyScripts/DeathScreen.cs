using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathScreen : MonoBehaviour
{
    public Animator anim;
    private GameObject saveObj;
    
    // Update is called once per frame
    void Update()
    {
        if (SaveScript.playerHealth <= 0)
        {
            anim.SetTrigger("death");
            StartCoroutine(WaitToReload());
        }
    }

    IEnumerator WaitToReload()
    {
        yield return new WaitForSeconds(1.5f);
        SaveScript.playerHealth = 1.0f;
        SaveScript.instance = 0;
        saveObj = GameObject.Find("SaveObject");
        Destroy(saveObj);
        SceneManager.LoadScene(0);
    }
}
