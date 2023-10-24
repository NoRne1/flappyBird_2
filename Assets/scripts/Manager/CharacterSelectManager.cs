using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    public List<CharacterData> availableCharacters = new List<CharacterData>(); // 存储可供选择的角色数据的列表
    public Transform characterSpawnPoint_left; // 用于生成角色的位置

    public Transform characterSpawnPoint_center;

    public Transform characterSpawnPoint_right;
    public static int selectedCharacterIndex = 0; // 选择的角色索引
    private PlayerDisplay currentCharacter; // 当前已生成的角色对象

    private float selectTimer = 0;
    public float selectDuration = 1f;

    private void Start()
    {
        SelectCharacterWithoutAni(selectedCharacterIndex); // 初始化时选择默认角色
    }

    private void Update()
    {
        selectTimer += Time.deltaTime;
    }

    public void SelectNextCharacter()
    {
        if (selectTimer > selectDuration)
        {
            selectedCharacterIndex = (selectedCharacterIndex + 1) % availableCharacters.Count;
            SelectCharacter(selectedCharacterIndex, false);
            selectTimer = 0;
        }
        
    }

    public void SelectPreviousCharacter()
    {
        if (selectTimer > selectDuration)
        {
            selectedCharacterIndex = (selectedCharacterIndex - 1 + availableCharacters.Count) % availableCharacters.Count;
            SelectCharacter(selectedCharacterIndex, true);
            selectTimer = 0;
        }
    }

    private void SelectCharacter(int characterIndex, bool leftOrRight)
    {
        if (availableCharacters.Count > 0)
        {
            // 销毁当前已生成的角色
            if (currentCharacter != null)
            {
                currentCharacter.FadeOut(leftOrRight);
            }

            // 生成新的角色
            CharacterData selectedCharacterData = availableCharacters[characterIndex];
            Transform characterSpawnPoint = leftOrRight ? characterSpawnPoint_left : characterSpawnPoint_right;
            currentCharacter = Instantiate(selectedCharacterData.player, characterSpawnPoint.position, characterSpawnPoint.rotation);
            currentCharacter.FadeIn(leftOrRight);
            // 执行其他操作，比如设置玩家控制、动画等
        }
        else
        {
            Debug.LogError("Select Character out of index!");
        }
    }
    private void SelectCharacterWithoutAni(int characterIndex)
    {
        if (availableCharacters.Count > characterIndex + 1)
        {
            // 销毁当前已生成的角色
            if (currentCharacter != null)
            {
                currentCharacter.DestroySelf();
            }

            // 生成新的角色
            CharacterData selectedCharacterData = availableCharacters[characterIndex];
            currentCharacter = Instantiate(selectedCharacterData.player, characterSpawnPoint_center.position, characterSpawnPoint_center.rotation);
            // 执行其他操作，比如设置玩家控制、动画等
        }
        else
        {
            Debug.LogError("Select Character out of index!");
        }
    }

    public void ConfirmCharacter()
    {
        SceneManager.LoadScene("Main");
    }
}

[Serializable]
public class CharacterData
{
    public PLAYER_INDEX player_index; // 角色序号
    public PlayerDisplay player; // 角色的预制体
}

