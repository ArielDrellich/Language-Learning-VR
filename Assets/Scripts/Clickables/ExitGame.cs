using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    private int click = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        click++;
        StartCoroutine(ClickTime());

        if (click > 1)
            if ( SceneManager.GetActiveScene().name != "Tutorial")
                Application.Quit();
            else
                FindObjectOfType<LevelManager>().MainMenu();

        }
    }
     IEnumerator ClickTime()
     {
        //  1 second max in between clicking back
        yield return new WaitForSeconds(1.0f);
        click = 0;
     }
}
