using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedSetting : MonoBehaviour
{
    // Start is called before the first frame update
    public static int ConfigTime = 900;
    public static int LoadLevel = 0;
    public static string[] LevelName = new string[] { "Easy", "Medium", "Hard", "Extreme" };

}
