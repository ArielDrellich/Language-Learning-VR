using UnityEngine;
using System;
using System.Web;
using System.Net;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

// namespace Translator {
	public class Translator : MonoBehaviour
	{
		public static TMPro.TMP_Text wordToTranslate;
		AudioSource audioSource;
        AudioClip myClip;
        private string txtToSpeechURL;

	    // Start is called before the first frame update
	    void Start()
	    {
	    	// Attach a new AudioSource component to the current translator
	        audioSource = gameObject.AddComponent<AudioSource>();
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        
	    }

	    public string Translate(string word, string fromLanguage, string toLanguage)
	    {
            //Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            //var toLanguage = "ru";//Deutsch
            //var fromLanguage = "en";//English
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            Debug.Log(url);
            var webClient = new System.Net.WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch (Exception exc)
            {
                Debug.Log("Error: " + exc);
                return null;
            }

	    }

	    public void TextToSpeech(string query, string translatedLang, string encodingFormat)
	    {
	    	// src = https://gist.github.com/alotaiba/1728771
	    	string encodedWord = HttpUtility.UrlEncode(query);
	    	txtToSpeechURL = "http://translate.google.com/translate_tts"
	    		+ "?ie=" + encodingFormat
	    		+ "&q=" + encodedWord
	    		+ "&tl=" + translatedLang
	    		+ "&client=tw-ob";
        }

        public void GetAudio() {
            StartCoroutine("GetAudioClip");
        }
   
        IEnumerator GetAudioClip()
        {
            using (UnityWebRequest www  = UnityWebRequestMultimedia.GetAudioClip(txtToSpeechURL, AudioType.MPEG))
            {
            	yield return www.SendWebRequest();

            	if (www.isNetworkError || www.responseCode != 200) {
            		Debug.Log(www.error);
            	} else {
            		myClip = DownloadHandlerAudioClip.GetContent(www);
            		audioSource.clip = myClip;
            		audioSource.Play();
            	}
            }
        }
}
