using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;

    //���� ȣ��

    //���콺 ���� ó��

    //��ų ��� ó��
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
    }

    private Vector2 AimPoint()
    {
        return Input.mousePosition;
    }

    public void AttackCommand()
    {
        switch (rootCtrl.inputCtrl.attackState)
        {
            case BtnState.None:
                break;
            case BtnState.Down:
                rootCtrl.interaction.InteractionEnter();
                break;
            case BtnState.Stay:
                rootCtrl.interaction.InteractionStay();
                break;
            case BtnState.Up:
                rootCtrl.interaction.InteractionExit();
                break;
        }
    }
    private void Update()
    {
        AttackCommand();
    }

}
