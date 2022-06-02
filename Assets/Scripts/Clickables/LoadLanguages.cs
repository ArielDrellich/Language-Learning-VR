using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLanguages : ScrollSelector, IClickable
{
	TMPro.TMP_Text language1Text;
	TMPro.TMP_Text language2Text;
	TMPro.TMP_Text language3Text;
    public GameObject language1Button;
    public GameObject language2Button;
    public GameObject language3Button;
    bool clicked = false;

    public class LanguageInfo {
        public string language;
        public string code;
        public int    isRTL;

        public LanguageInfo(string language, string code, int isRTL = 0)
        {
            this.language = language;
            this.code = code;
            this.isRTL = isRTL;    
        }
    }

    List <LanguageInfo> languages;

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

        languages = new List<LanguageInfo>();
        languages.Add(new LanguageInfo("Arabic", "ar", 1)); 
        languages.Add(new LanguageInfo("Catalan", "ca")); 
        languages.Add(new LanguageInfo("Czech", "cs"));  
        languages.Add(new LanguageInfo("Danish", "da")); 
        // languages.Add(new KeyValuePair<string, string>("Dutch", "nl"));  the sound is not good

        languages.Add(new LanguageInfo("English", "en")); 
        languages.Add(new LanguageInfo("French", "fr")); 
        languages.Add(new LanguageInfo("German", "de")); 
        languages.Add(new LanguageInfo("Greek", "el"));
        languages.Add(new LanguageInfo("Hebrew", "he", 1));
        languages.Add(new LanguageInfo("Italian", "it")); 
        languages.Add(new LanguageInfo("Latvian", "lv")); 
        languages.Add(new LanguageInfo("Norwegian", "no")); 
        languages.Add(new LanguageInfo("Polish", "pl"));
        languages.Add(new LanguageInfo("Portuguese", "pt"));
        languages.Add(new LanguageInfo("Russian", "ru")); 
        languages.Add(new LanguageInfo("Slovak", "sk")); 
        languages.Add(new LanguageInfo("Spanish", "es")); 
        languages.Add(new LanguageInfo("Swedish", "sv"));
        languages.Add(new LanguageInfo("Turkish", "tr")); 
        languages.Add(new LanguageInfo("Vietnamese", "vi")); 

        PlayerPrefs.SetInt("languageIndex", 0);

        language1Text.text = languages[0].language;
        language2Text.text = languages[1].language;
        language3Text.text = languages[2].language;

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
    	if (clicker.name == "down") {
            PlayerPrefs.SetString("languageChoice","");
            index = ((PlayerPrefs.GetInt("languageIndex")) + 1) % languages.Count;
    		language1Text.text = languages[index].language;
	   		language2Text.text = languages[(index + 1) % languages.Count].language;
	  		language3Text.text = languages[(index + 2) % languages.Count].language;
	  		PlayerPrefs.SetInt("languageIndex", index);
    	} 
        else if (clicker == null || clicker.name == "up")
        {
            PlayerPrefs.SetString("languageChoice", "");
            index = PlayerPrefs.GetInt("languageIndex") - 1;
	    	if (index < 0) {
	    		index = languages.Count - 1;
	    	}
	    	language1Text.text = languages[index].language;
		    language2Text.text = languages[(index + 1) % languages.Count].language;
		    language3Text.text = languages[(index + 2) % languages.Count].language;
		    PlayerPrefs.SetInt("languageIndex", index);
	    } else if (clicker.tag == "LanguageButton")
        {
	    	foreach (LanguageInfo oneLanguage in languages)
			{
			    TMPro.TMP_Text languageTxt = clicker.gameObject.transform.parent.GetComponentInChildren<TMPro.TMP_Text>(); // LanguageXtext

	 		    if (oneLanguage.language == languageTxt.text)
	 		    {
                    clicker.GetComponent<Renderer>().material.color = Color.grey;

                    string choice = oneLanguage.code;
	 		   		PlayerPrefs.SetString("languageChoice", choice);

                    PlayerPrefs.SetInt("isRTL", oneLanguage.isRTL);
                }
			}
	    }

    }


}
