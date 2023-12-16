using System.Collections;
using UnityEngine;

public class PlayerInputCtrl : InputCtrl
{
    private float reviveTime = 1f;
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //Add 4 keys


        //�̰� �ٲٸ� stack overflow ��
        if (Input.GetMouseButtonDown(0))
        {
            attackState = BtnState.Down;
        }
        else if (Input.GetMouseButton(0))
        {
            attackState = BtnState.Stay;
        }
        else if (Input.GetMouseButtonUp(0))
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
            if (rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum))
            {
                rootCtrl.interaction.interactionGrap();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            rootCtrl.interaction.interactionThrow();
        }
    }

    public override void initiallize()
    {
        base.initiallize();
        rootCtrl.deadAction += () => {
            rootCtrl.stateCtrl.stateEnum = stateEnum.Stunned;
            rootCtrl.stateCtrl.StunState();
            StartCoroutine(ExecuteLifeAction());
        };

    }
    public IEnumerator ExecuteLifeAction()
    {
        yield return new WaitForSeconds(reviveTime);
        rootCtrl.lifeAction.Invoke();
    }
}