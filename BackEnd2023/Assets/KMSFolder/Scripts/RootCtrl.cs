using System;
using UnityEngine;

public struct Status
{
    public float speed;
    public Status(float speed)
    {
        this.speed= speed;
    }
}

public class RootCtrl : MonoBehaviour,I_Attacker
{
    public Action lifeAction;
    public Action deadAction;
    public Action<I_Attacker> aggroAction;
    
    
    
    //??? ������Ƽ ���.
    public Transform transform => this.transform;
    public Status status;
    
    public Faction faction;

    public InputCtrl inputCtrl;
    public MoveCtrl moveCtrl;
    public StateCtrl stateCtrl;
    public HpCtrl hpCtrl;
    public WeaponCtrl WeaponCtrl;
    public AnimationCtrl AnimationCtrl;
    //public InteracionCtrl InteracionCtrl;

    public I_Interaction interaction;

    private void Awake()
    {
        status = new Status(3f);
        inputCtrl = gameObject.GetComponent<InputCtrl>();
        inputCtrl.initiallize();

        moveCtrl = gameObject.GetComponent<MoveCtrl>();
        moveCtrl.initiallize();

        stateCtrl = gameObject.GetComponent<StateCtrl>();
        stateCtrl.initiallize();

        hpCtrl = gameObject.GetComponent<HpCtrl>();
        hpCtrl.initiallize();

        WeaponCtrl = gameObject.GetComponent<WeaponCtrl>();
        WeaponCtrl.initiallize();

        AnimationCtrl = gameObject.GetComponent<AnimationCtrl>();
        AnimationCtrl.initiallize();
        

        // �̺κ��� ������ ��ü�� �ڵ����� �����´�.
        interaction = gameObject.GetComponent<I_Interaction>();
        interaction.initiallize();

    }
 
    // �Է°���, ���¸ӽ�, ����, ��Ʈ�Ѱ� ����


    //#region input

    //#endregion


    //#region move

    //#endregion

    //#region state // �ִϸ��̼� ���¸ӽ� ��û

    //#endregion

    //#region hp

    //#endregion

    //#region weapon

    //#endregion

    //#region animation

    //#endregion

}
