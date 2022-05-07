using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColLet : MonoBehaviour
{
    TMPro.TMP_Text gameName;
    float startTime;

    void Start()
    {
        gameName = GameObject.Find("LanguageLearningVR").GetComponent<TMPro.TMP_Text>();
        gameName.text = RandomMenuColor();
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
        string coloredgameName = "";
        foreach (char c in gameName)
        {
            coloredgameName += "<color=#" + ColorToHex(RandomColor()) + ">" + c + "</color>";

        }
        return coloredgameName;
    }
    void ChangeText()
    {
        if (Time.fixedTime - startTime > 2)
        {
            gameName.text = RandomMenuColor();
            startTime = Time.fixedTime;
        }
    }
}
