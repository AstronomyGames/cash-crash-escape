using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    private int level;
    private int coins;
    public int Level { get { if (level < 1) level = 1; return level; } set { level = value; } }
    public int Coins { get; set; }
}
