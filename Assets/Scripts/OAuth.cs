using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class OAuth : MonoBehaviour
{
    //[SerializeField]
    public TMPro.TMP_Text welcomeText;
    [SerializeField]
    public TMPro.TMP_Text Leaderboard;
    // HowTo:
    // 1. Install Java JDK - needed for keytool
    // https://www.oracle.com/java/technologies/downloads/#jdk17-windows

    // 2. Configuring Google Play Console
    // https://developers.google.com/games/services/console/enabling

    // 3. Install Play games plugin
    // https://github.com/playgameservices/play-games-plugin-for-unity
    // Add to Unity https://github.com/playgameservices/play-games-plugin-for-unity#plugin-installation

    // 4. Create a Firebase app
    // https://console.firebase.google.com/u/1/
    // From the dropdown menu in "Enter your project name" choose the app that was created in (2). This will link the firebase
    // app to the Google Play Console app. Remember to copy the google-serivces.json file to the Assets folder.

    // 5. Follow instructions in (3) https://github.com/playgameservices/play-games-plugin-for-unity#configure-your-game
    // Specifically, in Unity, open the setup dialog Window > Google Play Games > Setup > Android Setup.
    // In the Resources Definition, paste:
    // <?xml version="1.0" encoding="utf-8"?>
    // <resources>
    //     <string name="app_id">your-app-id</string> // app id in our game:1004013653281
    //     <string name="package_name">your-pacakge-name</string> in our game:com.DefaultCompany.LanguageLearningGame
    // </resources>
    //
    // Under Web App Client ID (Optional), paste the Web client (auto created by Google Service) that was created by the link
    // to the firebase application.

    // Troubleshooting
    // 1. If Android dependencies fail to reload, go to Assets > External Dependency Manager > Android Resolver > Delete Resolved Libraries
    // and then Assets > External Dependency Manager > Android Resolver > Force Resolve.

    // *. Debugging
    // https://www.andrewbirck.com/2020-12-08-vs-unity-android-debugging/


    //private static string kTag = "OAuth";

    void UpdateLb()
    {

    }
    void Start()
    {
    	welcomeText = GameObject.Find("Welcome").GetComponent<TMPro.TMP_Text>();
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        //Debug.unityLogger.Log(kTag, "Authenticating...");

        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (success) =>
        {
            //Debug.Log(success);
            if (success == SignInStatus.Success)
            {
                PlayGamesLocalUser localUser = (PlayGamesLocalUser)Social.localUser;
                welcomeText.text = "Welcome " + localUser.userName;
            }
            else
            {
                welcomeText.text = "Authentication failed";
            }
        });

        Application.quitting += PlayGamesPlatform.Instance.SignOut;
    }

    void Update()
    {
    }
}
