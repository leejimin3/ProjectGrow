using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pot : ItemCtrl
{

    public override ItemKind itemKind => ItemKind.Pot;

    public override bool IsGrabLock => base.IsGrabLock && (nowSeed.seedKind == SeedKind.Tower && weapon != null) == false;

    public Item_Seed nowSeed;
    public float waterValue;
    public bool isWood;//������ �Ƶ�


    public Item_Weapon weapon;//Ÿ���ϰ�� ����ִ� ����
    public int weaponUseCount;//���� ���Ƚ��


    public override bool checkUse(ItemCtrl nowItem)
    {
        //ȭ�п� �Է� �����°� ����, �����ϰ�� Ÿ��ȭ����
        switch (nowItem.itemKind)
        {
            case ItemKind.None:
                break;
            case ItemKind.Slot:
                //�翡 ȭ�� ��ġ
                PotSlot slot = nowItem as PotSlot;
                if (slot != null && slot.nowItemCtrl == null)
                {
                    return true;
                }
                break;
            case ItemKind.Seed: //�̺κ��� seed�� �Űܾ���
                Item_Seed seed = nowItem as Item_Seed;
                if (seed != null && nowSeed == null)
                {
                    //ȭ�п� ���ҽ� ����
                    switch (nowSeed.seedKind)
                    {
                        case SeedKind.None:
                        case SeedKind.Revolver:
                        case SeedKind.Minigun:
                        case SeedKind.Firebat:
                        case SeedKind.Electric:
                            return true;
                    }
                }
                break;
        }
        return false;
    }


    public void useAction(ItemCtrl nowItem)
    {
        //ȭ�п� �Է� �����°� ����, �����ϰ�� Ÿ��ȭ����
        switch (nowItem.itemKind)
        {
            case ItemKind.None:
                break;
            case ItemKind.Slot:
                //�翡 ȭ�� ��ġ
                PotSlot slot = nowItem as PotSlot;
                if (slot != null && slot.nowItemCtrl == null)
                {
                    FieldCtrl.Instance.setSlot(this, slot);
                    return;
                }
                break;
            case ItemKind.Seed:
                break;
        }

    }
    public void setWeapon(Item_Weapon weapon)
    {
        if (nowSeed.seedKind == SeedKind.Tower)
        {
            this.weapon = weapon;
            switch (weapon.weaponKind)
            {
                case SeedKind.None:
                    break;
                case SeedKind.Revolver:
                    break;
                case SeedKind.Minigun:
                    break;
                case SeedKind.Firebat:
                    break;
                case SeedKind.Electric:
                    break;
                case SeedKind.Water:
                    break;
                case SeedKind.Tower:
                    break;
            }
        }
    }
    public void setSeed(Item_Seed seed)
    {
        if (nowSeed == null)
        {
            isWood = false;
            nowSeed = seed;
            nowSeed.transform.SetParent(this.transform);
            nowSeed.transform.localPosition = Vector3.zero;
            nowSeed.gameObject.SetActive(false);//�ϴ� ������
                                                //ȭ�п� ���ҽ� ����
            switch (nowSeed.seedKind)
            {
                case SeedKind.None:
                    break;
                case SeedKind.Revolver:
                    break;
                case SeedKind.Minigun:
                    break;
                case SeedKind.Firebat:
                    break;
                case SeedKind.Electric:
                    break;
            }
        }
    }
    public override ItemCtrl GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {
        if (nowSeed.seedKind == SeedKind.Tower)
        {
            ItemCtrl temp = weapon;
            weapon = null;
            return temp;
        }
        else
        {
            return this;
        }
    }

    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {

        //�ֺ��� Slot�� üũ�ؼ� ���� ����� ���Կ� ��ġ����

        switch (useState)
        {
            case UseState.None:
                break;
            case UseState.Start:

                if (nowSeed != null)
                {
                    if (isWood)
                    {
                        //���� ����
                        //���� ȸ��
                        nowSeed.disable();
                        nowSeed = null;
                    }
                }
                else
                {//���� ����, ��ġ �ȵ�
                    ItemCtrl hitCtrl = InteractionCtrl.getSelectItemCtrl(rootCtrl.transform, checkUse);
                    if (hitCtrl != null)
                    {
                        //Todo �������� ������ ������
                        //rootCtrl.weaponCtrl.ItemRemove();
                        useAction(hitCtrl);
                        rootCtrl.interaction.interactionGrabOff();
                    }
                }
                break;
            case UseState.Ing:
                break;
            case UseState.End:
                break;
        }
    }

    public void Update()
    {
        if (nowSeed != null)
        {
            if (waterValue > 0f && isWood == false)
            {
                waterValue -= Time.deltaTime;
                nowSeed.addWeight(Time.deltaTime, this);
                //������ ǥ��
                //nowSeed.nowWeight / nowSeed.maxWeight;//���� ������
                //(nowSeed.nowWeight+waterValue) / nowSeed.maxWeight;//���� �����
            }
        }
    }

    public void woodOn()
    {
        isWood = true;
        //������ ���� ���� ���� �̹��� ����
        switch (nowSeed.seedKind)
        {
            case SeedKind.None:
                break;
            case SeedKind.Revolver:
                break;
            case SeedKind.Minigun:
                break;
            case SeedKind.Firebat:
                break;
            case SeedKind.Electric:
                break;
            case SeedKind.Tower:
                //Todo PlantInfo�� �����ؾ���
                weaponUseCount = ScriptableManager.instance.getTable("PlantScriptable").getPrefab<ScriptablePlantInfo.PrefabInfo>(nowSeed.seedKind.ToString()).Reusecount;
                break;
        }

    }



}
