using UnityEngine;
using System;
using System.Web;
using System.Net;

//namespace Translator {
	public class Translator : MonoBehaviour
	{
		public TMPro.TMP_Text wordToTranslate;

	    // Start is called before the first frame update
	    void Start()
	    {
	        //Translate(wordToTranslate.text);
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        
	    }

	    public string Translate(string word,string fromLanguage, string toLanguage)
	    {
	            //Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");
	            //var toLanguage = "ru";//Deutsch
	            //var fromLanguage = "en";//English
	            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
	            var webClient = new System.Net.WebClient
	            {
	                Encoding = System.Text.Encoding.UTF8
	            };
	            var result = webClient.DownloadString(url);
	            try
	            {
	                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
	                //wordToTranslate.text = result;
	                return result;
	            }
	            catch (Exception exc)
	            {
	                Debug.Log("Error: " + exc);
	                return null;
	            }

	    }


	}
