using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemKind
{
    None = 0,
    Slot = 1,
    Pot = 2,
    Seed = 3,//����
    Weapon = 5,//����
}
public abstract class ItemCtrl : MonoBehaviour, I_Pool
{
    public static PlantNameEnum ChangePlant(SeedKind seedKind)
    {
        switch (seedKind)
        {
            case SeedKind.Revolver:
                return PlantNameEnum.Rovolver;
            case SeedKind.Minigun:
                return PlantNameEnum.Minigun;
            case SeedKind.Firebat:
                return PlantNameEnum.flame_thrower;
            case SeedKind.Electric:
                return PlantNameEnum.Lighting;
            case SeedKind.None:
            case SeedKind.Water:
            case SeedKind.Tower:
            case SeedKind.Pot:
            default:
                return PlantNameEnum.Pot;
        }
    }
    public static SeedKind ChangeSeed(PlantNameEnum seedKind)
    {
        switch (seedKind)
        {
            case PlantNameEnum.Rovolver:
                return SeedKind.Revolver;
            case PlantNameEnum.Minigun:
                return SeedKind.Minigun;
            case PlantNameEnum.flame_thrower:
                return SeedKind.Firebat;
            case PlantNameEnum.Lighting:
                return SeedKind.Electric;
            case PlantNameEnum.Dionaea:
                return SeedKind.Tower;
            case PlantNameEnum.Pot:
                return SeedKind.Pot;
        }
        return SeedKind.None;
    }

    public static ItemCtrl newItem(ItemKind itemKind, string tag)
    {
        switch (itemKind)
        {
            case ItemKind.Pot:
                return newItem(ScriptableManager.instance.getTable(ScriptableManager.ScriptableTag).getPrefab<Scriptable_Object.PrefabInfo>("Pot").Prefabs.GetComponent<ItemCtrl>());
            case ItemKind.Seed:
                return newItem(ScriptableManager.instance.getTable(ScriptableManager.PlantScriptableTag).getPrefab<ScriptablePlantInfo.PrefabInfo>(tag).prefab.GetComponent<ItemCtrl>());
            case ItemKind.Weapon:
                return newItem(ScriptableManager.instance.getTable(ScriptableManager.WeaponScriptableTag).getPrefab<ScriptableWeaponInfo.PrefabInfo>(tag).weaponPrefab.GetComponent<ItemCtrl>());
                //return newItem(ScriptableManager.instance.getTable(ScriptableManager.ScriptableTag).getPrefab<Scriptable_Object.PrefabInfo>(tag).Prefabs.GetComponent<ItemCtrl>());//Todo �� ���� ���� ���������� ��ü�ؾ���
        }
        return null;
    }
    public static ItemCtrl newItem(ItemCtrl prefab)
    {
        if (poolDic.ContainsKey(prefab.itemKind) == false)
        {
            poolDic[prefab.itemKind] = new ObjectPooling<ItemCtrl>();
            poolDic[prefab.itemKind].Initialize(prefab, GameManager.poolParent, 10);
        }
        return poolDic[prefab.itemKind].GetObject(prefab);
    }
    public static Dictionary<ItemKind, ObjectPooling<ItemCtrl>> poolDic = new Dictionary<ItemKind, ObjectPooling<ItemCtrl>>();


    public virtual void potOut()
    {
        isGrabLock = true;
        StartCoroutine(cor_Move());
    }
    public IEnumerator cor_Move()
    {
        float time = 0.2f;
        Vector3 dic = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0).normalized;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            this.transform.Translate(dic * Time.deltaTime, Space.World);
            yield return null;
        }
        isGrabLock = false;

    }

    protected Action<I_Pool> disableAction;
    public void SetPoolEvent(Action<I_Pool> poolevent)
    {
        disableAction += poolevent;
    }
    public virtual void disable()
    {
        //Ǯ�� ȸ�� 
        this.transform.SetParent(GameManager.poolParent);
        this.gameObject.SetActive(false);
        disableAction?.Invoke(this);
    }
    ///������ �±�
    ///��� �Լ� (��Ʈ ��Ʈ�� �����ּ���
    ///��� ����

    public virtual ItemKind itemKind => ItemKind.None;

    public bool isGrab;
    [SerializeField]
    public bool isGrabLock;//�׷� ���� �Ұ���
    public virtual bool IsGrabLock => isGrabLock;
    [SerializeField]
    public bool isInterLock;//���ͷ��� ���
    public virtual bool IsInterLock => isInterLock || isGrab == false;


    //���콺 Ŭ���� ������ ��� ó��
    public abstract void UseCall(RootCtrl rootCtrl, UseState useState);
    //��� ���� ó��
    public abstract ItemCtrl GrabToggle(RootCtrl rootCtrl, bool isGrab);


    public abstract bool checkUse(ItemCtrl nowItem);




}
