using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;
    float speed;
    // �̵� ó��, �ø� ����

    Vector3 pos;

    //��������Ʈ ���� scale -1
    //SpriteRenderer playerRenderer;
   [SerializeField] Transform Target;

    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
        speed = rootCtrl.status.speed;
    }

    void Update()
    {
        //�÷��̾� �̵� ����
        if (rootCtrl.inputCtrl.GetType() == typeof(EnemyInput))
        {
            transform.position = Target.position;
        }


        //�� �̵� ����
        else if (rootCtrl.inputCtrl.GetType() == typeof(PlayerInputCtrl))
        {
            pos = Vector3.zero;

            if ((rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum)) || Input.GetAxis("Horizontal") != 0)
            {
                pos.x = rootCtrl.inputCtrl.horizontal * Time.deltaTime * speed;
                if (rootCtrl.inputCtrl.horizontal > 0)
                {
                    Target.localScale = new Vector3(1, 1, 1);
                }
                else if (rootCtrl.inputCtrl.horizontal < 0)
                {
                    Target.localScale = new Vector3(-1, 1, 1);
                }
                rootCtrl.stateCtrl.WalkState(rootCtrl.inputCtrl.horizontal, rootCtrl.inputCtrl.vertical);
            }
            else
            {
                rootCtrl.stateCtrl.IdleState();
            }

            if ((rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum)) || Input.GetAxis("Vertical") != 0)
            {
                pos.y = rootCtrl.inputCtrl.vertical * Time.deltaTime * speed;
                rootCtrl.stateCtrl.WalkState(rootCtrl.inputCtrl.horizontal, rootCtrl.inputCtrl.vertical);
            }
            else
            {
                rootCtrl.stateCtrl.IdleState();
            }
            transform.position += pos;
        }
    }
}
