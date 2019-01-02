using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : TTUIPage
{
    //主界面的各种按钮
    private Button buttonStatus, buttonEquip, buttonBag, buttonSkill, buttonTishi;
    //private List<int> tempShopList = new List<int>();

    public MainPanel():base(UIType.Normal,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/MainPanel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //找到各个按钮
        buttonStatus = transform.Find("ButtonStatus").GetComponent<Button>();
        buttonEquip = transform.Find("ButtonEquip").GetComponent<Button>();
        buttonBag = transform.Find("ButtonBag").GetComponent<Button>();
        buttonSkill = transform.Find("ButtonSkill").GetComponent<Button>();
        buttonTishi = transform.Find("ButtonTishi").GetComponent<Button>();

        buttonTishi.gameObject.SetActive(false);//默认隐藏提示按钮
        ShopItemlist.OnNPCTrigger += ShowTishi;//提示按钮在人物接近商店NPC时出现,远离时隐藏
        //buttonTishi.onClick.AddListener(() => TTUIPage.ShowPage<ShopPanel>(tempShopList));
        buttonBag.onClick.AddListener(() => TTUIPage.ShowPage<BagPanel>());
    }


    /// <summary>
    /// 是否显示提示按钮
    /// </summary>
    /// <param name="isshow">按钮是否显示</param>
    /// <param name="_itemList">NPC传来的物品列表</param>
    public void ShowTishi(bool isshow,List<int> _itemList)
    {
        //tempShopList = _itemList;
        buttonTishi.gameObject.SetActive(isshow);//默认隐藏提示按钮
        if (isshow)
        {
            buttonTishi.onClick.AddListener(() => TTUIPage.ShowPage<ShopPanel>(_itemList));
        }

        if (!isshow)
        {
            if (TTUIPage.allPages.ContainsKey("ShopPanel"))
            {
                TTUIPage.ClosePage<ShopPanel>();
            }        
            buttonTishi.onClick.RemoveAllListeners();
        }
    }
}
