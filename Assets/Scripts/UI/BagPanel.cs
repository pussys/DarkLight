using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TinyTeam.UI;
using System;

public class BagPanel : TTUIPage
{
    //信息弹出框
    //装东西
    //容量限制
    //一个格子里的物品数量
    //使用物品

    public Transform Grid;//背包空格
    private List<GameObject> gridList = new List<GameObject>();//背包格子
    private GameObject itemPrefab;//物品模板
    private Button buttonClose;

    //信息显示
    private Text infoName, infoDes;
    private Transform infoParent;
    private Button buttonUse, buttonCancel;

    public BagPanel():base(UIType.Normal,UIMode.HideOther,UICollider.None)
    {
        uiPath = "UIPrefab/BagPanel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //初始化物品预制
        itemPrefab = Resources.Load("UIPrefab/BagItem") as GameObject;
        //初始化格子的父物体
        Grid = Tools.FindInChildren<GridLayoutGroup>(go).transform;
        buttonClose = transform.Find("ButtonClose").GetComponent<Button>();
        buttonClose.onClick.AddListener(Hide);

        BagItem.OnItemSelceted += ShowSelectedItemInfo;

        //初始化信息框
        infoParent = transform.Find("ItemInfo");
        infoName = infoParent.Find("TextName").GetComponent<Text>();
        infoDes = infoParent.Find("TextDes").GetComponent<Text>();
        buttonUse = infoParent.Find("ButtonUse").GetComponent<Button>();
        buttonCancel = infoParent.Find("ButtonCancel").GetComponent<Button>();
        //点击取消时，关闭信息框，取消物品选中效果
        buttonCancel.onClick.AddListener(() => {
            infoParent.gameObject.SetActive(false);
        });

        //初始所有格子
        for (int i = 0; i < Grid.childCount; i++)
        {
            gridList.Add(Grid.GetChild(i).gameObject);
        }
        
    }

    /// <summary>
    /// 显示物品信息
    /// </summary>
    /// <param name="gm"></param>
    private void ShowSelectedItemInfo(GoodsModel gm)
    {
        infoParent.gameObject.SetActive(true);
        Item tempItem = DataMgr.GetInstance().GetItemByID(gm.Id);
        infoName.text = tempItem.item_Name;
        infoDes.text = tempItem.description;

        //修改弹出框的位置
        Vector3 worldPos;
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(
            TTUIRoot.Instance.root as RectTransform,
            Input.mousePosition,
            TTUIRoot.Instance.uiCamera,
            out worldPos))
        {
            infoParent.position = worldPos;
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        ShowBag();
    }

    /// <summary>
    /// 显示背包数据
    /// </summary>
    public void ShowBag()
    {
        //清除背包
        ClearBag();

        //遍历物品信息
        int j = 0;
        foreach (GoodsModel item in Save.GoodList)
        {
            if (item.Num != 0)//物品数量不等于零时
            {
                //创建物品
                GameObject go = GameObject.Instantiate(itemPrefab);
                go.transform.SetParent(Grid.GetChild(j));
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;

                //显示物体的图片及数量
                Sprite tempSprite = Resources.Load<Sprite>("Icon/"+item.Id);
                go.GetComponent<Image>().sprite = tempSprite;
                //设置数量文字
                go.transform.GetChild(0).GetComponent<Text>().text = item.Num + "";
                go.GetComponent<BagItem>().Init(item, tempSprite);
                j++;
            }
        }
    }

    /// <summary>
    /// 清除背包数据
    /// </summary>
    public void ClearBag()
    {
        //删除之前创建物品的预设物
        for (int i = 0; i < gridList.Count; i++)
        {
            if (gridList[i].transform.childCount != 0)
            {
                Transform t = gridList[i].transform.GetChild(0);
                t.parent = null;
                GameObject.Destroy(t.gameObject);
            }
        }
    }

}
