﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataMgr
{
    private static DataMgr instance = null;

    /// <summary>
    /// 存放所有物品
    /// </summary>
    private List<Item> itemList = new List<Item>();

    private DataMgr()
    {
        TextAsset ta = Resources.Load("ItemData/ItemData") as TextAsset;
        itemList = JsonConvert.DeserializeObject<List<Item>>(ta.text);
    }

    public static DataMgr GetInstance()
    {
        if (instance == null)
        {
            instance = new DataMgr();
        }
        return instance;
    }

    /// <summary>
    /// 根据ID获取物品信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Item GetItemByID(int _id)
    {
        return itemList.Find(x => x.item_ID == _id);
    }
}
/// <summary>
/// 物品类
/// </summary>
[System.Serializable]
public class Item
{
    public string item_Name = "Item Name";
    public string item_Type = "Item Type";
    [Multiline]
    public string description = "Description Here";
    public int item_ID;
    public string item_Img;//图片名字
    public string item_Effect;//特效的名字
    public string item_Sfx;//音效的名字
    public Equipment_Type equipment_Type;
    public int price;
    public int hp, mp, atk, def, spd, hit;
    public float criPercent, atkSpd, atkRange, moveSpd;
}
/// <summary>
/// 穿戴类型
/// </summary>
public enum Equipment_Type
{
    Null = 0, Head_Gear = 1, Armor = 2, Shoes = 3, Accessory = 4, Left_Hand = 5, Right_Hand = 6, Two_Hand = 7
}
