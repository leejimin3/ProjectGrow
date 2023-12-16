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
    RootCtrl rootCtrl;
    public Status status;

    public BtnState attackState;

    public float horizontal;
    public float vertical;
    
    private void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //Add 4 keys

        if (Input.GetMouseButton(0))
        {
            attackState = BtnState.Stay;
        }
        else if(Input.GetMouseButtonDown(0))
        {
            attackState = BtnState.Down;
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            attackState = BtnState.Up; 
        }
        else
        {
            attackState = BtnState.None;
        }

        //��ȣ�ۿ�Ű
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum))
            {
                rootCtrl.interaction.interactionGrap();
            }
        }
    }

    // �Է°���, ���¸ӽ�, ����, �ٸ�, Ctrl�� �۾���û or AI ó��
    public void initiallize()
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
