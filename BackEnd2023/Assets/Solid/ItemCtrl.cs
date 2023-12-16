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
    public static ItemCtrl newItem(ItemKind itemKind, string tag)
    {
        switch (itemKind)
        {
            case ItemKind.Pot:
                return newItem(ScriptableManager.instance.getTable("Scriptable").getPrefab<Scriptable_Object.PrefabInfo>("Pot").Prefabs.GetComponent<ItemCtrl>());
            case ItemKind.Seed:
                return newItem(ScriptableManager.instance.getTable("PlantScriptable").getPrefab<ScriptablePlantInfo.PrefabInfo>(tag).prefab.GetComponent<ItemCtrl>());
            case ItemKind.Weapon:
                //return newItem(ScriptableManager.instance.getTable("WeaponScriptable").getPrefab<ScriptableWeaponInfo.PrefabInfo>(tag).bulletprefab.GetComponent<ItemCtrl>());//Todo �� ���� ���� ���������� ��ü�ؾ���
                return newItem(ScriptableManager.instance.getTable("Scriptable").getPrefab<Scriptable_Object.PrefabInfo>(tag).Prefabs.GetComponent<ItemCtrl>());//Todo �� ���� ���� ���������� ��ü�ؾ���
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

    //public bool isGrab;//�׷����� �ʿ��Ѱ�?
    [SerializeField]
    public bool isGrabLock;//�׷� ���� �Ұ���
    public virtual bool IsGrabLock => isGrabLock;
    [SerializeField]
    public bool isInterLock;//���ͷ��� ���
    public virtual bool IsInterLock => isInterLock;


    //���콺 Ŭ���� ������ ��� ó��
    public abstract void UseCall(RootCtrl rootCtrl, UseState useState);
    //��� ���� ó��
    public abstract ItemCtrl GrabToggle(RootCtrl rootCtrl, bool isGrab);


    public abstract bool checkUse(ItemCtrl nowItem);




}
