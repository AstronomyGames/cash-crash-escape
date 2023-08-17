using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int maxLevel = 1;
    [SerializeField] private int restartFrom = 1;
    [SerializeField] private GameObject testLevel;
    [SerializeField] private bool loadLevels;


    protected override void Awake()
    {
        base.Awake();
        if (loadLevels)
            SetUpLevel();
    }



    private void SetUpLevel()
    {
        currentLevel = HCStandards.DataManager.GetData().Level;
        int calculatedLevel;

        if (currentLevel > maxLevel)
        {
            calculatedLevel = ((currentLevel - maxLevel) % (maxLevel - restartFrom)) + restartFrom + 1;
        }
        else
        {
            calculatedLevel = currentLevel;
        }

        GameObject go;
        if (testLevel != null)
            go = Instantiate(testLevel);
        else
            go = Instantiate(Resources.Load("Levels/Level " + calculatedLevel, typeof(GameObject))) as GameObject;

        if (go == null)
        {
            Debug.Log("Could Not Load Level => Level prefab is missing");
            return;
        }
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
    }


}


