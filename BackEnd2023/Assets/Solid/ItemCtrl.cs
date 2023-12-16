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

    ///������ �±�
    ///��� �Լ� (��Ʈ ��Ʈ�� �����ּ���
    ///��� ����

    public virtual ItemKind itemKind => ItemKind.None;

    //public bool isGrab;//�׷����� �ʿ��Ѱ�?
    public bool isGrabLock;//�׷� ���� �Ұ���
    public bool isInterLock;//���ͷ��� ���


    //���콺 Ŭ���� ������ ��� ó��
    public abstract void UseCall(RootCtrl rootCtrl, UseState useState);
    //��� ���� ó��
    public abstract void GrabToggle(RootCtrl rootCtrl, bool isGrab);
    public abstract bool checkUse(ItemCtrl nowItem);


    public virtual void disable()
    {
        //Ǯ�� ȸ�� 
    }
}
