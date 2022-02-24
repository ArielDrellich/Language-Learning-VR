using System.Collections;
using UnityEngine;

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
                Application.Quit();
         }
     }
     IEnumerator ClickTime()
     {
        //  1 second max in between clicking back
        yield return new WaitForSeconds(1.0f);
        click = 0;
     }
}
