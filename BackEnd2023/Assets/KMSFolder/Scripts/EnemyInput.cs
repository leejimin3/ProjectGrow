using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : InputCtrl
{
    //hit �ǰݽ� �ൿ �߰��ϱ� + ���� �ִϸ��̼�
    
    [SerializeField]Transform targetPlayer;
    private void Update()
    {
        //transform.position = targetPlayer.position;
    }
}
