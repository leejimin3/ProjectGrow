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
    //public static ItemCtrl newItem(ItemKind itemKind)
    //{
    //    switch (itemKind)
    //    {
    //        case ItemKind.Pot:
    //            return newItem(ScriptableManager.instance.getTable(""))
    //        case ItemKind.Seed:
    //            return
    //        case ItemKind.Weapon:
    //            return
    //    }
    //}
    //private static ItemCtrl newItem(ItemCtrl prefab)
    //{

    //}
    public static Dictionary<ItemKind, ObjectPooling<ItemCtrl>> poolDic = new Dictionary<ItemKind, ObjectPooling<ItemCtrl>>();

    protected Action<I_Pool> disableAction;
    public void SetPoolEvent(Action<I_Pool> poolevent)
    {
        disableAction += poolevent;
    }
    public virtual void disable()
    {
        //Ǯ�� ȸ�� 
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
