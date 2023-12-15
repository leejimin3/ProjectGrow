using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemKind
{
    None = 0,
    Slot = 1,
    Pot = 2,
    Seed = 3,//����

}
public abstract class ItemCtrl : MonoBehaviour
{
    public readonly static LayerMask ItemInterObj = 1 + (1 << LayerMask.NameToLayer("ItemInterObj"));

    ///������ �±�
    ///��� �Լ� (��Ʈ ��Ʈ�� �����ּ���
    ///��� ����

    public virtual ItemKind itemKind => ItemKind.None;

    //public bool isGrab;//�׷�����
    public bool isGrabLock;//�׷� ���� �Ұ���
    public bool isUseLock;


    //���콺 Ŭ���� ������ ��� ó��
    public abstract void UseCall(I_RootCtrl rootCtrl, UseState useState);
    //��� ���� ó��
    public abstract void GrabToggle(I_RootCtrl rootCtrl, bool isGrab);
    public abstract bool checkUse(ItemCtrl nowItem);


    public virtual void disable()
    {
        //Ǯ�� ȸ�� 
    }
}
