using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    public List<Enemy> enemies = new List<Enemy>();
    private readonly string PlayerPerfabsPrefix = "Prefabs/Player/";

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            CreatePlayer();
        }
    }

    public void CreatePlayer()
    {
        string playerName = Enum.GetName(typeof(PLAYER_INDEX), CharacterSelectManager.selectedCharacterIndex);
        GameObject playerPerfab = Resources.Load<GameObject>(PlayerPerfabsPrefix + playerName);
        Player player = Instantiate(playerPerfab, this.transform).GetComponent<Player>();
        Game.Instance.Player = player.GetComponent<Player>();
    }

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