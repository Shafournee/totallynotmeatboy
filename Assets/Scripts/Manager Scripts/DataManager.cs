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
            if (newTime < playerTimes.Level1)
            {
                playerTimes.Level1 = newTime;
                PlayerPrefs.SetFloat("Level1", playerTimes.Level1);
            }
        }
        else if (level == Level.Level2)
        {
            if (newTime < playerTimes.Level2)
            {
                playerTimes.Level2 = newTime;
                PlayerPrefs.SetFloat("Level2", playerTimes.Level2);
            }
        }
        else if (level == Level.Level3)
        {
            if (newTime < playerTimes.Level3)
            {
                playerTimes.Level3 = newTime;
                PlayerPrefs.SetFloat("Level3", playerTimes.Level3);
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
            return playerTimes.Level1;
        }
        if (level == Level.Level2)
        {
            return playerTimes.Level2;
        }
        if (level == Level.Level3)
        {
            return playerTimes.Level3;
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
        if (PlayerPrefs.HasKey("Level1"))
        {
            playerTimes.Level1 = PlayerPrefs.GetFloat("Level1");
        }
        if (PlayerPrefs.HasKey("Level2"))
        {
            playerTimes.Level2 = PlayerPrefs.GetFloat("Level2");
        }
        if (PlayerPrefs.HasKey("Level3"))
        {
            playerTimes.Level3 = PlayerPrefs.GetFloat("Level3");
        }
    }

    private void SetTimes()
    {
        if (playerTimes.fullGameTime == 0f)
        {
            PlayerPrefs.SetFloat("fullGameTime", 100000f);
        }
        if (playerTimes.Level1 == 0f)
        {
            PlayerPrefs.SetFloat("Level1", 100000f);
        }
        else if (playerTimes.Level2 == 0f)
        {
            PlayerPrefs.SetFloat("Level2", 100000f);
        }
        else if (playerTimes.Level3 == 0f)
        {
            PlayerPrefs.SetFloat("Level3", 100000f);
        }

    }


}
