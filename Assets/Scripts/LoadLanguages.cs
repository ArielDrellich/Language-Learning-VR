﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu;


public class LoadLanguages : MonoBehaviour, IMenu
{
	// public string[] languages = { "Slovak", "French", "Georgian", "German", "Greek", "Kannada", "Polish"};
	TMPro.TMP_Text language1Text;
	TMPro.TMP_Text language2Text;
	TMPro.TMP_Text language3Text;

	List <KeyValuePair<string, string>> languages;
	List <KeyValuePair<string, string>> languages_rev;

	int index;

    // Start is called before the first frame update
    void Start()
    {
    	index = -1;

    	language1Text = GameObject.Find("Language1Text").GetComponent<TMPro.TMP_Text>();
    	language2Text = GameObject.Find("Language2Text").GetComponent<TMPro.TMP_Text>();
    	language3Text = GameObject.Find("Language3Text").GetComponent<TMPro.TMP_Text>();

		languages = new List<KeyValuePair<string, string>>();
        languages.Add(new KeyValuePair<string, string>("Slovak", "sk"));
        languages.Add(new KeyValuePair<string, string>("French", "fr"));
        languages.Add(new KeyValuePair<string, string>("Georgian", "ka"));
        languages.Add(new KeyValuePair<string, string>("German", "de"));
        languages.Add(new KeyValuePair<string, string>("Greek", "el"));
        languages.Add(new KeyValuePair<string, string>("Kannada", "kn"));
        languages.Add(new KeyValuePair<string, string>("Polish", "pl"));

        // langages_rev = languages.Reverse();
        DoClick(null);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoClick(GameObject clicker) {
    	if (clicker == null || clicker.name == "up") {
    		index = (PlayerPrefs.GetInt("languageIndex") + 1) % languages.Count;
    	} else if (clicker.name == "down") {
	    	index = PlayerPrefs.GetInt("languageIndex") - 1;
	    	if (index < 0) {
	    		index = languages.Count - 1;
	    	}
	    }
	    
	    language1Text.text = languages[index].Key;
	    language2Text.text = languages[(index + 1) % languages.Count].Key;
	    language3Text.text = languages[(index + 2) % languages.Count].Key;
	    PlayerPrefs.SetInt("languageIndex", index);
    }
}
