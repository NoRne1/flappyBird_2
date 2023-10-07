/*
 Create By Ray : ray@raymix.net @ 极视教育
 */

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