using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
/// <summary>
/// 解析数据
/// </summary>
public class Analysis : MonoBehaviour {

	void Awake () {
        // 用户数据解析
        UserAnalysis();
        // 物品数据解析
        GoodsAnalysis();
    }
    /// <summary>
    /// 用户数据解析
    /// </summary>
	void UserAnalysis()
    {
        TextAsset userTA = Resources.Load("Setting/UserJson") as TextAsset;
        if (!userTA)
        {
            return;
        }
        //Save.UserList = JsonConvert.DeserializeObject<List<UserModel>>(userTA.text);
        //print(userTA.text);
    }

    /// <summary>
    /// 物品数据解析
    /// </summary>
    void GoodsAnalysis()
    {
        TextAsset goodsTA = Resources.Load("Setting/GoodsList") as TextAsset;
        if (!goodsTA)
        {
            Debug.Log("goodList文件不存在！");
            return;
        }
        Save.GoodList = JsonConvert.DeserializeObject<List<GoodsModel>>(goodsTA.text);
        print(goodsTA.text);
    }

}
