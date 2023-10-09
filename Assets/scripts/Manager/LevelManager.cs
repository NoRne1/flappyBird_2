using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{

    public List<Level> levels;
    public int currentLevelId = 1;
    private Level level;

    public void nextLevel()
    {
        DestroyLevel();
        this.LoadLevel(currentLevelId + 1);
    }

    public void LoadLevel(int levelID)
    {
        DestroyLevel();
        currentLevelId = levelID;
        this.level = Instantiate<Level>(levels[levelID - 1], this.transform);
    }

    public void DestroyLevel() {
        if (level != null)
        {
            Destroy(level.gameObject);
            level = null;
        }
    }
}
