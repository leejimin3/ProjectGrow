using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemCtrl : MonoBehaviour
{
    ///������ �±�
    ///��� �Լ� (��Ʈ ��Ʈ�� �����ּ���
    ///��� ����
    ///


    //���콺 Ŭ���� ������ ��� ó��
    public abstract void UseCall(I_RootCtrl rootCtrl);
    //��� ���� ó��
    public abstract void GrabToggle(I_RootCtrl rootCtrl, bool isGrab);

}
