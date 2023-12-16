using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputCtrl : InputCtrl
{



    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //Add 4 keys

        if (Input.GetMouseButton(0))
        {
            attackState = BtnState.Stay;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            attackState = BtnState.Down;
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
}
