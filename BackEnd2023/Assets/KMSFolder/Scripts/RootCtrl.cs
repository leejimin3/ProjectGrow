using UnityEngine;

public struct Status
{
    public float speed;
    public Status(float speed)
    {
        this.speed= speed;
    }
}

public class RootCtrl : MonoBehaviour
{
    public Status status;

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
        status = new Status(0.5f);
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
