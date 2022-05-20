using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class SetLeaderboard : MonoBehaviour, IClickable
{
    // Start is called before the first frame update

    ReticleManager reticle;

    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        
        int score = FindObjectOfType<LevelManager>().score;

        Social.ReportScore(score, "CgkIoZqCn5wdEAIQAw", (successLb) => { // publish to the table the score
                                                                         // handle success or failure
            if (successLb)
            {
                //Debug.Log("success:)");
            }
            else
            {
                //Debug.Log("fail:(");
            }
        });
    }

    public void LookedAt(RaycastHit hit)
    {
        reticle.SetColor(Color.red);
        if (Input.GetButtonDown("Fire1"))
        {
            Social.ShowLeaderboardUI();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
