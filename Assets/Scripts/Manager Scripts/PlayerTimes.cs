using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level { Level1, Level2, Level3, Finish };

public class PlayerTimes
{
    // Saving each level time as a float is really inefficent, I'm sure, and I should probably figure out how to do
    // ArrayPrefs, but this should be okay for now
    public float fullGameTime;
    public float Level1;
    public float Level2;
    public float Level3;

    public float[] Level = new float[5];
}
