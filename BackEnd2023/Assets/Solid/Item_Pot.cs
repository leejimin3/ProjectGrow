using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pot : ItemCtrl, I_Faction, I_Attacker
{
    public Transform myTransform => this.transform;
    public bool IsTarget => hpCtrl.nowHp > 0f;

    public override ItemKind itemKind => ItemKind.Pot;

    public override bool IsGrabLock => isGrabLock && (nowSeed != null && nowSeed.seedKind == SeedKind.Tower && weapon != null) == false;
    public override bool IsInterLock => isInterLock;

    public Item_Seed nowSeed;
    public float waterValue;
    public bool isWood;//������ �Ƶ�
    public PotHpCtrl hpCtrl;

    public Animator ani;

    public GameObject toworObj;
    public Sprite[] gunIcons;
    public SpriteRenderer[] gunIconSprites;
    public Sprite[] singIcons;
    public SpriteRenderer sing;


    public Item_Weapon weapon;//Ÿ���ϰ�� ����ִ� ����
    public int weaponUseCount;//���� ���Ƚ��

    public Transform weaponPivot;
    public RootCtrl targetRoot;


    public Transform TargetTran => targetRoot.transform;
    public Faction Faction => Faction.None;

    public void DeadEvent(I_Faction i_Faction)
    {
        targetRoot = null;
    }
    public override void disable()
    {

        Invoke("potDisable", 2f);
    }
    public void potDisable()
    {

        FieldCtrl.Instance.removePot(this);
        base.disable();
    }
    public void Update()
    {
        SeedUpdate();

        if (reloadTemp > 0f && waterValue <= 0f)
        {
            reloadTemp -= Time.deltaTime;
            ani.SetFloat("LeefFill", ((1f - (reloadTemp / reloadTime))));
            return;
        }
        if (weapon != null && nowSeed.seedKind == SeedKind.Tower)
        {
            if (targetRoot == null)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, 10f, LayerManager.Instance.HitZone);
                if (hits != null)
                {
                    for (int i = 0; i < hits.Length; i++)
                    {
                        I_HitZone hitZone = hits[i].GetComponent<I_HitZone>();
                        if (hitZone != null && hitZone.RootCtrl != null && hitZone.Faction == Faction.Enemy)
                        {
                            targetRoot = hitZone.RootCtrl;
                            targetRoot.SetDisableOneEvent(targetOff);
                        }
                    }
                }
            }
            else if (Mathf.Max(5f, weapon.weaponInfo.AttackDistance * 0.5f) > Vector2.Distance(this.transform.position, TargetTran.position))
            {
                weapon.UseCallAttack(this, UseState.Start, endAmmo);
            }
        }
    }
    public void targetOff(I_Pool targetPool)
    {
        if (targetPool == (I_Pool)targetRoot)
        {
            targetRoot = null;
        }
    }
    public void SeedUpdate()
    {
        if (nowSeed != null)
        {
            if (waterValue > 0f && isWood == false)
            {
                waterValue -= Time.deltaTime;
                nowSeed.addWeight(Time.deltaTime, this);
                ani.SetFloat("LeefFill", nowSeed.weightFill);
                //������ ǥ��
                //nowSeed.nowWeight / nowSeed.maxWeight;//���� ������
                //(nowSeed.nowWeight+waterValue) / nowSeed.maxWeight;//���� �����
            }
            else if (waterValue > 0f && nowSeed.seedKind == SeedKind.Tower && reloadTemp > 0f)
            {
                waterValue -= Time.deltaTime;
                reloadTemp -= Time.deltaTime * 2f;
                ani.SetFloat("LeefFill", ((1f - (reloadTemp / reloadTime))));
            }
        }
    }

    public void Awake()
    {
        ani = GetComponentInChildren<Animator>(true);
        hpCtrl = GetComponentInChildren<PotHpCtrl>(true);
        toworObj.SetActive(false);
        sing.gameObject.SetActive(false);
        for (int i = 0; i < gunIconSprites.Length; i++)
        {
            gunIconSprites[i].transform.parent.gameObject.SetActive(false);
        }
    }
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
                if (slot != null && slot.nowPot == null)
                {
                    return true;
                }
                break;
            case ItemKind.Seed: //�̺κ��� seed�� �Űܾ���
                Item_Seed seed = nowItem as Item_Seed;
                if (seed != null && nowSeed == null)
                {
                    //ȭ�п� ���ҽ� ����
                    switch (seed.seedKind)
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
                if (slot != null && slot.nowPot == null)
                {
                    //FieldCtrl.Instance.setSlot(this, slot);
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
            weapon.transform.SetParent(weaponPivot);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localScale = Vector3.one;
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
            sing.gameObject.SetActive(false);

            toworObj.SetActive(false);

            for (int i = 0; i < gunIconSprites.Length; i++)
            {
                gunIconSprites[i].transform.parent.gameObject.SetActive(false);
            }                               //ȭ�п� ���ҽ� ����
            switch (nowSeed.seedKind)
            {
                case SeedKind.None:
                    break;
                case SeedKind.Revolver:
                    sing.gameObject.SetActive(true);
                    sing.sprite = singIcons[0];
                    ani.SetInteger("LeefType", 1);
                    for (int i = 0; i < gunIconSprites.Length; i++)
                    {
                        gunIconSprites[i].sprite = gunIcons[0];
                    }
                    break;
                case SeedKind.Minigun:
                    sing.gameObject.SetActive(true);
                    sing.sprite = singIcons[1];
                    ani.SetInteger("LeefType", 2);
                    for (int i = 0; i < gunIconSprites.Length; i++)
                    {
                        gunIconSprites[i].sprite = gunIcons[1];
                    }
                    break;
                case SeedKind.Firebat:
                    sing.gameObject.SetActive(true);
                    sing.sprite = singIcons[2];
                    ani.SetInteger("LeefType", 3);
                    for (int i = 0; i < gunIconSprites.Length; i++)
                    {
                        gunIconSprites[i].sprite = gunIcons[2];
                    }
                    break;
                case SeedKind.Electric:
                    sing.gameObject.SetActive(true);
                    sing.sprite = singIcons[3];
                    ani.SetInteger("LeefType", 4);
                    for (int i = 0; i < gunIconSprites.Length; i++)
                    {
                        gunIconSprites[i].sprite = gunIcons[3];
                    }
                    break;
                case SeedKind.Water:
                    break;
                case SeedKind.Tower:
                    sing.gameObject.SetActive(true);
                    sing.sprite = singIcons[4];
                    ani.SetInteger("LeefType", 5);
                    toworObj.SetActive(true);

                    break;
                case SeedKind.Pot:
                    break;
            }
        }
    }
    public override ItemCtrl GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {
        this.isGrab = isGrab;
        if (nowSeed != null && nowSeed.seedKind == SeedKind.Tower)
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
    private float reloadTime = 15f;
    public float reloadTemp;
    public void endAmmo()
    {
        reloadTemp = reloadTime;
        weapon.reload();
        ani.SetFloat("LeefFill", 0f);

        --weaponUseCount;

        if (weaponUseCount <= 0)
        {
            weapon.disable();
            weapon = null;
            //hpCtrl.nowHp = Mathf.Min(hpCtrl.nowHp, 40f);
            //hpCtrl.maxHp = 40f;
            //nowSeed.disable();
            //nowSeed = null;
            //toworObj.gameObject.SetActive(false);
            //sing.gameObject.SetActive(false);
            //isWood = false;
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
                    switch (nowSeed.seedKind)
                    {
                        case SeedKind.None:
                        case SeedKind.Tower:
                        case SeedKind.Pot:
                            //���� ȸ�� �ȵǴ� ���
                            return;
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
                    }
                    if (isWood && nowSeed.seedKind != SeedKind.Tower)
                    {
                        //���� ����
                        //���� ȸ��
                        sing.gameObject.SetActive(false);

                        ani.SetInteger("LeefType", 0);
                        ani.SetFloat("LeefFill", 0f);
                        for (int i = 0; i < gunIconSprites.Length; i++)
                        {
                            gunIconSprites[i].transform.parent.gameObject.SetActive(false);
                        }
                        void seedCraft(PlantNameEnum plant, SeedKind seedKind)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                Item_Seed seedItem = ItemCtrl.newItem(ItemKind.Seed, plant.ToString()) as Item_Seed;
                                seedItem.seedKind = seedKind;
                                seedItem.transform.position = gunIconSprites[i].transform.position;
                                seedItem.potOut();
                            }
                        }
                        switch (nowSeed.seedKind)
                        {
                            case SeedKind.None:
                                break;
                            case SeedKind.Revolver:
                                seedCraft(PlantNameEnum.Rovolver, SeedKind.Revolver);
                                break;
                            case SeedKind.Minigun:
                                seedCraft(PlantNameEnum.Minigun, SeedKind.Minigun);
                                break;
                            case SeedKind.Firebat:
                                seedCraft(PlantNameEnum.flame_thrower, SeedKind.Firebat);
                                break;
                            case SeedKind.Electric:
                                seedCraft(PlantNameEnum.Lighting, SeedKind.Electric);
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
                        weapon = null;
                        nowSeed.disable();
                        isWood = false;
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


    public void woodOn()
    {
        isWood = true;
        //������ ���� ���� ���� �̹��� ����
        switch (nowSeed.seedKind)
        {
            case SeedKind.None:
                break;
            case SeedKind.Revolver:
            case SeedKind.Minigun:
            case SeedKind.Firebat:
            case SeedKind.Electric:
                for (int i = 0; i < gunIconSprites.Length; i++)
                {
                    gunIconSprites[i].transform.parent.gameObject.SetActive(true);
                }
                break;

            case SeedKind.Tower:
                //Todo PlantInfo�� �����ؾ���
                for (int i = 0; i < gunIconSprites.Length; i++)
                {
                    gunIconSprites[i].transform.parent.gameObject.SetActive(false);
                }
                hpCtrl.nowHp = 100f;
                hpCtrl.maxHp = 100f;
                weaponUseCount = 3;// ScriptableManager.instance.getTable(ScriptableManager.PlantScriptableTag).getPrefab<ScriptablePlantInfo.PrefabInfo>(nowSeed.seedKind.ToString()).Reusecount;
                break;
        }

    }



}
