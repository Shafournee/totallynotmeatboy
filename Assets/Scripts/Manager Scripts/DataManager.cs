using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

    private PlayerTimes playerTimes;

	// Use this for initialization
	void Start () {
        LoadTimes();
        SetTimes();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Check if the new level times are better, if so save them into the new levels
    public void SubmitNewTime(float newTime, Level level)
    {
        if(level == Level.Level1)
        {
            if (newTime < playerTimes.level1)
            {
                playerTimes.level1 = newTime;
                PlayerPrefs.SetFloat("level1", playerTimes.level1);
            }
        }
        else if (level == Level.Level2)
        {
            if (newTime < playerTimes.level2)
            {
                playerTimes.level2 = newTime;
                PlayerPrefs.SetFloat("level2", playerTimes.level2);
            }
        }
        else if(level == Level.Finish)
        {
            if (newTime < playerTimes.fullGameTime)
            {
                playerTimes.fullGameTime = newTime;
                PlayerPrefs.SetFloat("fullGameTime", playerTimes.fullGameTime);
            }
        }
    }

    public float GetFastestPlayerTime()
    {
        return playerTimes.fullGameTime;
    }

    public float GetLevelTime(Level level)
    {
        if (level == Level.Level1)
        {
            return playerTimes.level1;
        }
        else if (level == Level.Level2)
        {
            return playerTimes.level2;
        }
        else
        {
            return 10004324;
        }
    }

    public void LoadTimes()
    {
        playerTimes = new PlayerTimes();

        if(PlayerPrefs.HasKey("fullGameTime"))
        {
            playerTimes.fullGameTime = PlayerPrefs.GetFloat("fullGameTime");
        }
        else if (PlayerPrefs.HasKey("level1"))
        {
            playerTimes.level1 = PlayerPrefs.GetFloat("level1");
        }
        else if (PlayerPrefs.HasKey("level2"))
        {
            playerTimes.level2 = PlayerPrefs.GetFloat("level2");
        }
    }

    private void SetTimes()
    {
        if (playerTimes.fullGameTime == 0)
        {
            PlayerPrefs.SetFloat("fullGameTime", 100000f);
        }
        else if (playerTimes.level1 < 1f)
        {
            PlayerPrefs.SetFloat("level1", 100000f);
        }
        else if (playerTimes.level2 < 1f)
        {
            PlayerPrefs.SetFloat("level2", 100000f);
        }

    }
}
