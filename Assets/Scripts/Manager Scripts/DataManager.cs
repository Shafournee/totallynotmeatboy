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
        else if (level == Level.Level4)
        {
            if (newTime < playerTimes.Level4)
            {
                playerTimes.Level4 = newTime;
                PlayerPrefs.SetFloat("Level4", playerTimes.Level4);
            }
        }
        else if (level == Level.Level5)
        {
            if (newTime < playerTimes.Level5)
            {
                playerTimes.Level5 = newTime;
                PlayerPrefs.SetFloat("Level5", playerTimes.Level5);
            }
        }
        else if (level == Level.Level6)
        {
            if (newTime < playerTimes.Level6)
            {
                playerTimes.Level6 = newTime;
                PlayerPrefs.SetFloat("Level6", playerTimes.Level6);
            }
        }
        else if (level == Level.Level7)
        {
            if (newTime < playerTimes.Level7)
            {
                playerTimes.Level7 = newTime;
                PlayerPrefs.SetFloat("Level7", playerTimes.Level7);
            }
        }
        else if (level == Level.Level8)
        {
            if (newTime < playerTimes.Level8)
            {
                playerTimes.Level8 = newTime;
                PlayerPrefs.SetFloat("Level8", playerTimes.Level8);
            }
        }
        else if (level == Level.Level9)
        {
            if (newTime < playerTimes.Level9)
            {
                playerTimes.Level9 = newTime;
                PlayerPrefs.SetFloat("Level9", playerTimes.Level9);
            }
        }
        else if (level == Level.Level10)
        {
            if (newTime < playerTimes.Level10)
            {
                playerTimes.Level10 = newTime;
                PlayerPrefs.SetFloat("Level10", playerTimes.Level10);
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
        if (level == Level.Level4)
        {
            return playerTimes.Level4;
        }
        if (level == Level.Level5)
        {
            return playerTimes.Level5;
        }
        if (level == Level.Level6)
        {
            return playerTimes.Level6;
        }
        if (level == Level.Level7)
        {
            return playerTimes.Level7;
        }
        if (level == Level.Level8)
        {
            return playerTimes.Level8;
        }
        if (level == Level.Level9)
        {
            return playerTimes.Level9;
        }
        if (level == Level.Level10)
        {
            return playerTimes.Level10;
        }
        else
        {
            return 100000f;
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
        if (PlayerPrefs.HasKey("Level4"))
        {
            playerTimes.Level4 = PlayerPrefs.GetFloat("Level4");
        }
        if (PlayerPrefs.HasKey("Level5"))
        {
            playerTimes.Level5 = PlayerPrefs.GetFloat("Level5");
        }
        if (PlayerPrefs.HasKey("Level6"))
        {
            playerTimes.Level6 = PlayerPrefs.GetFloat("Level6");
        }
        if (PlayerPrefs.HasKey("Level7"))
        {
            playerTimes.Level7 = PlayerPrefs.GetFloat("Level7");
        }
        if (PlayerPrefs.HasKey("Level8"))
        {
            playerTimes.Level8 = PlayerPrefs.GetFloat("Level8");
        }
        if (PlayerPrefs.HasKey("Level9"))
        {
            playerTimes.Level9 = PlayerPrefs.GetFloat("Level9");
        }
        if (PlayerPrefs.HasKey("Level10"))
        {
            playerTimes.Level10 = PlayerPrefs.GetFloat("Level10");
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
        else if (playerTimes.Level4 == 0f)
        {
            PlayerPrefs.SetFloat("Level4", 100000f);
        }
        else if (playerTimes.Level5 == 0f)
        {
            PlayerPrefs.SetFloat("Level5", 100000f);
        }
        else if (playerTimes.Level6 == 0f)
        {
            PlayerPrefs.SetFloat("Level6", 100000f);
        }
        else if (playerTimes.Level7 == 0f)
        {
            PlayerPrefs.SetFloat("Level7", 100000f);
        }
        else if (playerTimes.Level8 == 0f)
        {
            PlayerPrefs.SetFloat("Level8", 100000f);
        }
        else if (playerTimes.Level9 == 0f)
        {
            PlayerPrefs.SetFloat("Level9", 100000f);
        }
        else if (playerTimes.Level10 == 0f)
        {
            PlayerPrefs.SetFloat("Level10", 100000f);
        }

    }


}
