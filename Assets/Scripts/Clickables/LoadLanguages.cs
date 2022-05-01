using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLanguages : ScrollSelector, IClickable
{
	// public string[] languages = { "Slovak", "French", "Georgian", "German", "Greek", "Kannada", "Polish"};
	TMPro.TMP_Text language1Text;
	TMPro.TMP_Text language2Text;
	TMPro.TMP_Text language3Text;
    public GameObject language1Button;
    public GameObject language2Button;
    public GameObject language3Button;
    bool clicked = false;

    List <KeyValuePair<string, string>> languages;

	int index;
	ReticleManager reticle;

    // Start is called before the first frame update
    void Start()
    {
		reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
    	index = -1;

    	language1Text = GameObject.Find("Language1Text").GetComponent<TMPro.TMP_Text>();
    	language2Text = GameObject.Find("Language2Text").GetComponent<TMPro.TMP_Text>();
    	language3Text = GameObject.Find("Language3Text").GetComponent<TMPro.TMP_Text>();

        language1Button = GameObject.Find("Language1Button");
        language2Button = GameObject.Find("Language2Button");
        language3Button = GameObject.Find("Language3Button");

        resetColors();

        languages = new List<KeyValuePair<string, string>>();
        languages.Add(new KeyValuePair<string, string>("Catalan", "ca")); 
        languages.Add(new KeyValuePair<string, string>("Czech", "cs"));  
        languages.Add(new KeyValuePair<string, string>("Danish", "da")); 
        // languages.Add(new KeyValuePair<string, string>("Dutch", "nl"));  the sound is not good

        languages.Add(new KeyValuePair<string, string>("English", "en")); 
        languages.Add(new KeyValuePair<string, string>("French", "fr")); 
        languages.Add(new KeyValuePair<string, string>("German", "de")); 
        languages.Add(new KeyValuePair<string, string>("Greek", "el"));
        languages.Add(new KeyValuePair<string, string>("Italian", "it")); 
        languages.Add(new KeyValuePair<string, string>("Latvian", "lv")); 
        languages.Add(new KeyValuePair<string, string>("Norwegian", "no")); 
        languages.Add(new KeyValuePair<string, string>("Polish", "pl"));
        languages.Add(new KeyValuePair<string, string>("Portuguese", "pt"));
        languages.Add(new KeyValuePair<string, string>("Russian", "ru")); 
        languages.Add(new KeyValuePair<string, string>("Slovak", "sk")); 
        languages.Add(new KeyValuePair<string, string>("Spanish", "es")); 
        languages.Add(new KeyValuePair<string, string>("Swedish", "sv"));
        languages.Add(new KeyValuePair<string, string>("Turkish", "tr")); 
        languages.Add(new KeyValuePair<string, string>("Vietnamese", "vi")); 
        
        // langages_rev = languages.Reverse();
        PlayerPrefs.SetInt("languageIndex", 0);
        //DoClick(null);

    }

    // Update is called once per frame
    void Update()
    {
    	language1Text = GameObject.Find("Language1Text").GetComponent<TMPro.TMP_Text>();
    	language2Text = GameObject.Find("Language2Text").GetComponent<TMPro.TMP_Text>();
    	language3Text = GameObject.Find("Language3Text").GetComponent<TMPro.TMP_Text>();
    }

    public void LookedAt(RaycastHit hit) {
		reticle.SetColor(Color.red);
        ToggleClick(hit);

    }

    public void ToggleClick(RaycastHit hit)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            resetColors();
            if (!this.clicked)
            {
                this.clicked = true;
                DoClick(hit.collider.gameObject);
            }

            else
            {
                DoClick(hit.collider.gameObject);
                clicked = false;
            }
        }
    }

   public override void resetColors()
    {
        language1Button.GetComponent<Renderer>().material.color = Color.white;
        language2Button.GetComponent<Renderer>().material.color = Color.white;
        language3Button.GetComponent<Renderer>().material.color = Color.white;
    }

    public override void DoClick(GameObject clicker) {
    	if (clicker.name == "up") {
            // Debug.Log(PlayerPrefs.GetInt("languageIndex"));

    		index = (PlayerPrefs.GetInt("languageIndex")) % languages.Count;
    		language1Text.text = languages[index].Key;
	   		language2Text.text = languages[(index + 1) % languages.Count].Key;
	  		language3Text.text = languages[(index + 2) % languages.Count].Key;
	  		PlayerPrefs.SetInt("languageIndex", index + 1);

    	} else if (clicker == null || clicker.name == "down") {
            index = PlayerPrefs.GetInt("languageIndex") - 1;
	    	if (index < 0) {
	    		index = languages.Count - 1;
	    	}
	    	language1Text.text = languages[index].Key;
		    language2Text.text = languages[(index + 1) % languages.Count].Key;
		    language3Text.text = languages[(index + 2) % languages.Count].Key;
		    PlayerPrefs.SetInt("languageIndex", index);
	    } else if (clicker.tag == "LanguageButton") {
	    	foreach (KeyValuePair<string, string> oneLanguage in languages)
			{
			    string language = clicker.gameObject.transform.GetChild(0).name; // LanguageXtext
                TMPro.TMP_Text languageTxt = GameObject.Find(language).GetComponent<TMPro.TMP_Text>();
	 		    if (oneLanguage.Key == languageTxt.text)
	 		    {
                    clicker.GetComponent<Renderer>().material.color = Color.grey;

                    string choice = oneLanguage.Value;
	 		   		PlayerPrefs.SetString("languageChoice", choice);
                }
			}
	    }

    }


}
