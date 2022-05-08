using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColLet : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text text;
    TMPro.TMP_Text gameNameText;
    TMPro.TMP_Text winText;
    float startTime;

    void Start()
    {
        try
        { gameNameText = GameObject.Find("LanguageLearningVR").GetComponent<TMPro.TMP_Text>(); }
        catch { }
        if (gameNameText != null)
        {
            gameNameText.text = RandomMenuColor();
        }
        try
        { winText = GameObject.Find("WinText").GetComponent<TMPro.TMP_Text>();}
        catch { }
        if (winText != null)
        {
            winText.text = RandomMenuColor();
        }
        startTime = Time.fixedTime;


    }
    void Update()
    {
        ChangeText();
    }
    string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    Color32 RandomColor()
    {
        return new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
    }

    string RandomMenuColor()
    {
        const string gameName = "Language Learning VR";
        const string win = "YOU WIN!";
        string coloredstring = "";
        if (gameNameText != null)
        {
            foreach (char c in gameName)
            {
                coloredstring += "<color=#" + ColorToHex(RandomColor()) + ">" + c + "</color>";

            }
        }
        if (winText != null)
        {
            foreach (char c in win)
            {
                coloredstring += "<color=#" + ColorToHex(RandomColor()) + ">" + c + "</color>";

            }
        }
        return coloredstring;
    }
    void ChangeText()
    {
        if (Time.fixedTime - startTime > 2)
        {
            if (gameNameText != null)
            {
                gameNameText.text = RandomMenuColor();
            }
            if (winText != null)
            {
                winText.text = RandomMenuColor();
            }

            startTime = Time.fixedTime;
        }
    }
}
