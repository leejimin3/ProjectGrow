using UnityEngine;

public enum Faction
{
    None = 0,
    Player = 1,
    Pot = 2,
    Enemy = 10,
}
public class WeaponCtrl : MonoBehaviour, InitiallizeInterface
{


    RootCtrl rootCtrl;
    public Transform targetTran;


    //���� ȣ��

    //���콺 ���� ó��

    //��ų ��� ó��
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
    }

    private Transform AimPoint()
    {
        return targetTran;
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
