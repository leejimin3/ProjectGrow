using UnityEngine;

public class EnemyInput : InputCtrl
{
    


    private void Update()
    {
        //transform.position = targetPlayer.position;

        if (rootCtrl.targetTran != null)
        {
            Vector3 direction = rootCtrl.targetTran.position - transform.position;
            direction.Normalize();
          horizontal = direction.x;
        vertical = direction.y;
        }

    }

    public override void initiallize()
    {
        base.initiallize();
        //Hp Ctrl aggro reference use
        rootCtrl.aggroAction += (attacker) =>
        {
            rootCtrl.WeaponCtrl.targetTran = attacker.transform;
        };

        //
    }

    // ai�� Ÿ���� ������ �ִµ� Ÿ���� ����ɶ�����
    // weapon ctrl�� Ÿ�� Ʈ�������� �ִµ� �װ� �ֽ�ȭ ���������
    // �׼��� ����ٸ� ����� �� Ÿ���� transform�� weapon�� �־������.
}
