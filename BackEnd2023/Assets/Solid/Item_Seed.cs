using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item_Seed : ItemCtrl
{
    public override ItemKind itemKind => ItemKind.Seed;

    public float nowWeight;
    public float maxWeight;

    public WeaponKind WeaponKind;

    public override bool checkUse(ItemCtrl nowItem)
    {
        //ȭ�п� �Է� �����°� ����, �����ϰ�� Ÿ��ȭ����

        return false;
    }

    public override void GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {

    }

    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {

        //�ֺ��� Slot�� üũ�ؼ� ���� ����� ���Կ� ��ġ����
        Collider2D[] potList = Physics2D.OverlapCircleAll(rootCtrl.transform.position, 1f, LayerManager.Instance.ItemInterObj);

        if (potList != null)
        {
            float minDis = 0f;
            ItemCtrl hitCtrl = null;
            for (int i = 0; i < potList.Length; i++)
            {
                ItemCtrl item = potList[i].GetComponent<ItemCtrl>();
                if (item != null)
                {
                    if (checkUse(item))
                    {
                        float dis = Vector2.Distance(rootCtrl.transform.position, item.transform.position);
                        if (dis < minDis || hitCtrl == null)
                        {
                            hitCtrl = item;
                            minDis = dis;
                        }
                    }
                }
            }
            if (hitCtrl != null)
            {
                //Todo �������� ������ ������
                //rootCtrl.weaponCtrl.ItemRemove();
                //this.disable();//ȭ�� ��ġ�Ҷ� ȸ���� ���ʿ� �ֳ�?
            }
        }

    }
    public void addWeight(float weight, Item_Pot pot)
    {
        nowWeight += weight;
        if (nowWeight >= maxWeight)
        {
            pot.woodOn();
        }
    }


}
