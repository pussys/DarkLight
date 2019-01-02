using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;

public class UserModel
{
    /*  {"UserList":[{"Hp":80,"MaxHp":120,"Attack":35,"Speed":25}]}  */
    public int Hp;
    public int MaxHp;
    public int Attack;
    public int Speed;
}
//public  class UserModelList
//{
//    public List<UserModel> UserList = new List<UserModel>();
//}

[System.Serializable]
public class GoodsModel //商品信息
{
    public int Id;
    //public string Name;
    //public string Nature;//图片种类(图片名)
    //public string Function;//描述
    //public int Value;//值
    public int Num;//数量
}
//public class GoodsModelList
//{
//    public List<GoodsModel> GoodsList;
//}

public class Save
{
    /// <summary>
    /// 背包里的物品
    /// </summary>
    private static List<GoodsModel> goodList;
    /// <summary>
    /// 用户列表
    /// </summary>
    private static List<UserModel> UserList;

    public static List<GoodsModel> GoodList
    {
        get;set;
    }

    /// <summary>
    /// 购买物品
    /// </summary>
    /// <param name="_item"></param>
    public static void BuyItem(Item _item)
    {
        if (goodList == null)
        {
            goodList = new List<GoodsModel>();
        }
        GoodsModel gm = goodList.Find(x => x.Id == _item.item_ID);
        if (gm != null)
        {
            gm.Num += 1;
        }
        else
        {
            goodList.Add(new GoodsModel() { Id = _item.item_ID, Num = 1 });
        }
        SaveGoods();
    }

    private static void SaveGoods()
    {
        string path = Application.dataPath + @"/Resources/Setting/GoodsList.json";

        using (StreamWriter sw = new StreamWriter(path))
        {
            string json = JsonConvert.SerializeObject(goodList);
            sw.Write(json);
        }
        AssetDatabase.Refresh();
    }
}

