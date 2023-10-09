using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    public List<Enemy> enemies = new List<Enemy>();

    public void Clear()
    {
        this.enemies.Clear();
    }

    public Enemy CreateEnemy(GameObject templates)
    {
        if (templates == null)
            return null;

        GameObject obj = Instantiate(templates, this.transform);
        Enemy p = obj.GetComponent<Enemy>();
        this.enemies.Add(p);
        return p;
    }
}