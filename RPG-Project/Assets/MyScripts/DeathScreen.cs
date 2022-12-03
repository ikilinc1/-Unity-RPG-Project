using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathScreen : MonoBehaviour
{
    public Animator anim;
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
        SceneManager.LoadScene(0);
    }
}
