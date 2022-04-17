using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class TranslateText : MonoBehaviour
{
    private Translator     translator;
    private string         chosenLanguage;
    private TMPro.TMP_Text text;
    private string[]       endPunctuation = { ".", "!", "?", "\n" };

    // Start is called before the first frame update
    void Start()
    {
        translator = gameObject.AddComponent<Translator>();

        chosenLanguage = PlayerPrefs.GetString("languageChoice");
        
        // for debugging
        if (chosenLanguage == "")
            chosenLanguage = "en";
        
        text = this.GetComponent<TMPro.TMP_Text>();

        /* Translate Api seems to only translate one sentence at a time, and fails
            on new line characters. I split the string and translate each substring
            individually to avoid this.*/
        string[] splitTextByNewLine = text.text.Split(new string[] { "\r\n", "\r", "\n" },
                                                                  StringSplitOptions.None); 
                                                                  
        text.text = "";
        foreach (string newLineSubstring in splitTextByNewLine)
        {
            string[] splitTextBySentence = newLineSubstring.Split(new string[] {". "}, StringSplitOptions.None);

            foreach (string sentence in splitTextBySentence)
            {
                string translatedText = translator.Translate(sentence, "en", chosenLanguage);

                if (translatedText != "l,null,")
                {
                    text.text += translatedText;
                    if (!endPunctuation.Any(x => text.text.EndsWith(x)))
                        text.text += ". ";
                }
                else 
                {
                    Debug.Log("Tranlated text is null.");
                }
            }


            text.text += "\n";
        }

    }
}
