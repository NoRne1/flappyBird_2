using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropRule : MonoBehaviour {
    public ItemDropRuleStruct parameters;

    public ItemDropRule(ItemDropRuleStruct parameters)
    {
        this.parameters = parameters;
    }

    public void Execute(Vector3 pos)
    {
        int geneNum = 0;
        for (int i = 0; i < parameters.dropNum; i++)
        {
            if (Random.Range(0f, 100f) < parameters.dropRatio)
            {
                Item rule = Instantiate<Item>(parameters.item);
                rule.transform.position = pos + new Vector3(parameters.itemSpacing * geneNum, 0);
                geneNum += 1;
            }
        }
    }
}

[System.Serializable]
public struct ItemDropRuleStruct
{
    public Item item;
    public float itemSpacing;
    public int dropNum;
    public float dropRatio;
}
