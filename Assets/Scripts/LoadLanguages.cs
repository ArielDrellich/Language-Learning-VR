using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Menu;
using UnityEngine.SceneManagement;


// public class LoadLanguages : MonoBehaviour, IMenu
// {
// 	// public string[] languages = { "Slovak", "French", "Georgian", "German", "Greek", "Kannada", "Polish"};
// 	TMPro.TMP_Text language1Text;
// 	TMPro.TMP_Text language2Text;
// 	TMPro.TMP_Text language3Text;

// 	List <KeyValuePair<string, string>> languages;

// 	int index;

//     // Start is called before the first frame update
//     void Start()
//     {
//     	index = -1;

//     	language1Text = GameObject.Find("Language1Text").GetComponent<TMPro.TMP_Text>();
//     	language2Text = GameObject.Find("Language2Text").GetComponent<TMPro.TMP_Text>();
//     	language3Text = GameObject.Find("Language3Text").GetComponent<TMPro.TMP_Text>();

// 		languages = new List<KeyValuePair<string, string>>();
//         languages.Add(new KeyValuePair<string, string>("Slovak", "sk"));
//         languages.Add(new KeyValuePair<string, string>("French", "fr"));
//         languages.Add(new KeyValuePair<string, string>("Georgian", "ka"));
//         languages.Add(new KeyValuePair<string, string>("German", "de"));
//         languages.Add(new KeyValuePair<string, string>("Greek", "el"));
//         languages.Add(new KeyValuePair<string, string>("Kannada", "kn"));
//         languages.Add(new KeyValuePair<string, string>("Polish", "pl"));

//         // langages_rev = languages.Reverse();
//         DoClick(null);

//     }

//     // Update is called once per frame
//     void Update()
//     {
//     	language1Text = GameObject.Find("Language1Text").GetComponent<TMPro.TMP_Text>();
//     	language2Text = GameObject.Find("Language2Text").GetComponent<TMPro.TMP_Text>();
//     	language3Text = GameObject.Find("Language3Text").GetComponent<TMPro.TMP_Text>();
        
//     }

//     public void DoClick(GameObject clicker) {
//     	if (clicker == null || clicker.name == "up") {
//     		index = (PlayerPrefs.GetInt("languageIndex") + 1) % languages.Count;
//     		language1Text.text = languages[index].Key;
// 	   		language2Text.text = languages[(index + 1) % languages.Count].Key;
// 	  		language3Text.text = languages[(index + 2) % languages.Count].Key;
// 	  		PlayerPrefs.SetInt("languageIndex", index);
//     	} else if (clicker.name == "down") {
// 	    	index = PlayerPrefs.GetInt("languageIndex") - 1;
// 	    	if (index < 0) {
// 	    		index = languages.Count - 1;
// 	    	}
// 	    	language1Text.text = languages[index].Key;
// 		    language2Text.text = languages[(index + 1) % languages.Count].Key;
// 		    language3Text.text = languages[(index + 2) % languages.Count].Key;
// 		    PlayerPrefs.SetInt("languageIndex", index);
// 	    } else if (clicker.tag == "LanguageButton") {
// 	    	foreach (KeyValuePair<string, string> oneLanguage in languages)
// 			{

// 				string language = clicker.gameObject.transform.GetChild(0).name;
// 				TMPro.TMP_Text languageTxt = GameObject.Find(language).GetComponent<TMPro.TMP_Text>();
// 	 		   if (oneLanguage.Key == languageTxt.text)
// 	 		   {
// 	 		   		string choice = oneLanguage.Value;
// 	 		   		PlayerPrefs.SetString("languageChoice", choice);
// 	 		   		Debug.Log(choice);
// 	 		   }
// 			}
// 			SceneManager.LoadScene("Forest");
// 	    }

//     }
// }


public class LoadLanguages : MonoBehaviour, IClickable
{
	// public string[] languages = { "Slovak", "French", "Georgian", "German", "Greek", "Kannada", "Polish"};
	TMPro.TMP_Text language1Text;
	TMPro.TMP_Text language2Text;
	TMPro.TMP_Text language3Text;

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
    	language1Text = GameObject.Find("Language1Text").GetComponent<TMPro.TMP_Text>();
    	language2Text = GameObject.Find("Language2Text").GetComponent<TMPro.TMP_Text>();
    	language3Text = GameObject.Find("Language3Text").GetComponent<TMPro.TMP_Text>();
        
    }

    public void LookedAt(RaycastHit hit) {
		reticle.SetColor(Color.red);
        if (Input.GetButtonDown("Fire1"))
			DoClick(hit.collider.gameObject);
	}

    public void DoClick(GameObject clicker) {
    	if (clicker == null || clicker.name == "up") {
    		index = (PlayerPrefs.GetInt("languageIndex") + 1) % languages.Count;
    		language1Text.text = languages[index].Key;
	   		language2Text.text = languages[(index + 1) % languages.Count].Key;
	  		language3Text.text = languages[(index + 2) % languages.Count].Key;
	  		PlayerPrefs.SetInt("languageIndex", index);
    	} else if (clicker.name == "down") {
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

				string language = clicker.gameObject.transform.GetChild(0).name;
				TMPro.TMP_Text languageTxt = GameObject.Find(language).GetComponent<TMPro.TMP_Text>();
	 		   if (oneLanguage.Key == languageTxt.text)
	 		   {
	 		   		string choice = oneLanguage.Value;
	 		   		PlayerPrefs.SetString("languageChoice", choice);
	 		   		Debug.Log(choice);
	 		   }
			}
			SceneManager.LoadScene("Forest");
	    }

    }
}
