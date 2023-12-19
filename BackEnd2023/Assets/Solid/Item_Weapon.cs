using System;
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
    public float attackDelay;
    public bool isAttackLock;

    public FireCtrl fireCtrl;

    public SpriteRenderer model;
    public Sprite[] gunSprites;


    public void OnEnable()
    {
        weaponInfo = ScriptableManager.instance.getTable(ScriptableManager.WeaponScriptableTag).getPrefab<ScriptableWeaponInfo.PrefabInfo>(weaponKind.ToString());
        fireCtrl.setWeapon(this);

        switch (weaponKind)
        {
            case SeedKind.None:
                break;
            case SeedKind.Revolver:
                model.sprite = gunSprites[0];
                break;
            case SeedKind.Minigun:
                model.sprite = gunSprites[1];
                break;
            case SeedKind.Firebat:
                model.sprite = gunSprites[2];
                break;
            case SeedKind.Electric:
                model.sprite = gunSprites[3];
                break;
            case SeedKind.Water:
                break;
            case SeedKind.Tower:
                break;
            case SeedKind.Pot:
                break;
            default:
                break;
        }
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
    public void reload()
    {
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
        this.isGrab = isGrab;
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
    private void Update()
    {
        if (attackDelay >= 0f)
        {
            attackDelay -= Time.deltaTime;
        }
    }
    public void UseCallAttack(I_Attacker attackCtrl, UseState useState, Action endAmmo)
    {
        switch (useState)
        {
            case UseState.None:
                break;
            case UseState.Start:
                switch (weaponKind)
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
                    case SeedKind.Pot:
                        break;
                    default:
                        break;
                }
                break;
            case UseState.Ing:
          
                break;
            case UseState.End:
                break;
        }
        if (attackDelay <= 0f)
        {
            attackDelay = weaponInfo.AttackSpeed;
            fireCtrl.Fire(attackCtrl);

            //���� ���
            switch (weaponInfo.EnergyType)
            {
                case EnergyTypeEnum.Bullet:
                    --nowAmmo;
                    if (nowAmmo <= 0)
                    {
                        //��� �Ҹ���
                        endAmmo?.Invoke();
                        //this.disable();
                    }
                    break;
                case EnergyTypeEnum.Gauge:
                    if (weaponKind == SeedKind.Water)
                    {//���� ������
                        return;
                    }
                    nowGuage -= Time.deltaTime;
                    if (nowGuage <= 0f)
                    {
                        //��� �Ҹ���
                        endAmmo?.Invoke();
                        //this.disable();
                    }
                    break;
                case EnergyTypeEnum.Null:
                    break;
                default:
                    break;
            }

        }
    }
    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {
        switch (useState)
        {
            case UseState.None:
                break;
            case UseState.Start:
                switch (weaponKind)
                {
                    case SeedKind.None:
                        break;
                    case SeedKind.Revolver:
                        isAttackLock = true;
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
                    case SeedKind.Pot:
                        break;
                    default:
                        break;
                }
                break;
            case UseState.Ing:
                if (isAttackLock)
                {
                    return;
                }
                break;
            case UseState.End:
                isAttackLock = false;
                break;
        }
        if (attackDelay <= 0f)
        {
            attackDelay = weaponInfo.AttackSpeed;
            fireCtrl.Fire(rootCtrl);

            //���� ���
            switch (weaponInfo.EnergyType)
            {
                case EnergyTypeEnum.Bullet:
                    --nowAmmo;
                    if (nowAmmo <= 0)
                    {
                        //��� �Ҹ���
                        rootCtrl.interaction.interactionGrabOff();
                        this.disable();
                    }
                    break;
                case EnergyTypeEnum.Gauge:
                    if (weaponKind == SeedKind.Water)
                    {//���� ������
                        return;
                    }
                    nowGuage -= Time.deltaTime;
                    if (nowGuage <= 0f)
                    {
                        //��� �Ҹ���
                        rootCtrl.interaction.interactionGrabOff();
                        this.disable();
                    }
                    break;
                case EnergyTypeEnum.Null:
                    break;
                default:
                    break;
            }

        }
    }
}
