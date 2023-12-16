using UnityEngine;
public enum BtnState
{
    None = 0,
    Down = 1,
    Stay = 2,
    Up = 3
}

public class InputCtrl : MonoBehaviour, InitiallizeInterface
{

    public BtnState attackState;


    protected RootCtrl rootCtrl;
    public Status status;

    public float horizontal;
    public float vertical;

    // �Է°���, ���¸ӽ�, ����, �ٸ�, Ctrl�� �۾���û or AI ó��
    public virtual void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
        status = rootCtrl.status;
    }

    // OverLapsCircle
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("�Ʊ�"))
    //    {
    //        //collision.gameObject.GetComponent<Player>().enabled = false;

    //        //collision.gameObject.transform.SetParent(transform, false);
    //        //�Ʊ� �ͷ��� ��� �ű��� �������⸦ ȣ��
    //    }
    //    else if(collision.gameObject.CompareTag("��"))
    //    {
    //        //collision.gameObject.GetComponent<Enemy>().enabled = false;
    //    }
    //}
}
