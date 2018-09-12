using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level { Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Level10, Finish };

public class PlayerTimes
{
    // Saving each level time as a float is really inefficent, I'm sure, and I should probably figure out how to do
    // ArrayPrefs, but this should be okay for now
    public float fullGameTime;
    public float Level1;
    public float Level2;
    public float Level3;
    public float Level4;
    public float Level5;
    public float Level6;
    public float Level7;
    public float Level8;
    public float Level9;
    public float Level10;

    // Save the player prefs for audio levels
    public int MusicLevels;
    public int EffectLevels;
}
