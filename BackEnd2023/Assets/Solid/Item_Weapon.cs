using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeedKind
{
    None = 0,
    Revolver = 1,
    Minigun = 2,
    Firebat = 3,
    Electric = 4,
    Water = 5,
    Tower = 6,
    Pot = 7,
}

public class Item_Weapon : ItemCtrl
{


    public override ItemKind itemKind => ItemKind.Weapon;

    public SeedKind weaponKind;


    public ScriptableWeaponInfo.PrefabInfo weaponInfo;

    public float nowGuage;//���� ������
    public int nowAmmo;//���� ź

    public FireCtrl fireCtrl;

    private void OnEnable()
    {
        weaponInfo = ScriptableManager.instance.getTable(ScriptableManager.WeaponScriptableTag).getPrefab<ScriptableWeaponInfo.PrefabInfo>(weaponKind.ToString());
        fireCtrl.setWeapon(this);

        switch (weaponInfo.EnergyType)
        {
            case EnergyTypeEnum.Bullet:
                nowAmmo = weaponInfo.BulletCount;
                break;
            case EnergyTypeEnum.Gauge:
                nowGuage = weaponInfo.GaugeTime;
                break;
            case EnergyTypeEnum.Null:
                break;
            default:
                break;
        }
    }
    public override bool checkUse(ItemCtrl nowItem)
    {//����� ȭ�п� ����Ұ��? �� ���������� ��ó�� Ÿ���Ĺ��� ������ �ڵ����� ����, �ݱ�� ȸ�� 
        switch (nowItem.itemKind)
        {
            case ItemKind.Pot:
                Item_Pot pot = nowItem as Item_Pot;
                if (pot != null)
                {
                    if (pot.nowSeed != null && pot.isWood && pot.nowSeed.seedKind == SeedKind.Tower)
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
    }


    public override ItemCtrl GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {
        if (isGrab == false)
        {
            switch (weaponKind)
            {
                case SeedKind.None:
                    break;
                case SeedKind.Water:
                    break;
                case SeedKind.Tower:
                    break;
                case SeedKind.Revolver:
                case SeedKind.Minigun:
                case SeedKind.Firebat:
                case SeedKind.Electric:
                    //�����Q���� �ֺ��� Ÿ���ִ��� üũ�ؼ� ����ֱ� 
                    ItemCtrl itemCtrl = InteractionCtrl.getSelectItemCtrl(this.transform, checkUse);
                    if (itemCtrl != null)
                    {
                        Item_Pot pot = itemCtrl as Item_Pot;
                        if (pot != null)
                        {
                            if (pot.nowSeed != null && pot.isWood && pot.nowSeed.seedKind == SeedKind.Tower)
                            {
                                pot.setWeapon(this);
                            }
                        }
                    }
                    break;
            }

        }
        return this;
    }


    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {
        fireCtrl.Fire(rootCtrl);

        //���� ���
        switch (weaponInfo.EnergyType)
        {
            case EnergyTypeEnum.Bullet:
                if (nowAmmo <= 0)
                {
                    //��� �Ҹ���

                }
                break;
            case EnergyTypeEnum.Gauge:
                if (weaponKind == SeedKind.Water)
                {//���� ������
                    return;
                }
                if (nowGuage <= 0f)
                {
                    //��� �Ҹ���
                }
                break;
            case EnergyTypeEnum.Null:
                break;
            default:
                break;
        }

    }
}
