using UnityEngine;
using UnityEditor;


public enum SIDE
{

    NONE = 0,
    PLAYER,
    ENEMY,
}

public enum ENEMY_TYPE
{
    NORMAL_ENEMY,
    //摇摆的敌人
    SWING_ENEMY,

    BOSS,


}

public enum GAME_STATUS
{
    READY,
    INGAME,
    OVER
}

public enum PLAYER_INDEX
{
    Player = 0,
    Player2 = 1,
    Player3 = 2,
}