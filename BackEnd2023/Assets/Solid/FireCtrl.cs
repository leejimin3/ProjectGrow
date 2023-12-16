using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    private Item_Weapon itemWeapon;
    private Transform fireTran;

    public static Dictionary<SeedKind, ObjectPooling<BulletCtrl>> bulletPoolDic = new Dictionary<SeedKind, ObjectPooling<BulletCtrl>>();
    public BulletCtrl bullet;

    private void Awake()
    {
        if (fireTran == null)
        {
            fireTran = this.transform;
        }
    }
    public void setWeapon(Item_Weapon itemWeapon)
    {
        this.itemWeapon = itemWeapon;
        bullet = itemWeapon.weaponInfo.bulletprefab.GetComponent<BulletCtrl>();
    }
    public void Fire(RootCtrl rootCtrl)
    {
        Vector2 dic = (rootCtrl.WeaponCtrl.targetTran.position - fireTran.position);
        float dis = dic.magnitude;
        dic = dic.normalized;
        //źȲ ����, �ش� �������� �߻�
        BulletCtrl newBullet = null;
        if (bulletPoolDic.ContainsKey(itemWeapon.weaponKind) == false)
        {
            bulletPoolDic[itemWeapon.weaponKind] = new ObjectPooling<BulletCtrl>();
            bulletPoolDic[itemWeapon.weaponKind].Initialize(bullet, null, 10);
        }
        newBullet = bulletPoolDic[itemWeapon.weaponKind].GetObject(bullet);
        newBullet.dic = dic;
        newBullet.range = itemWeapon.weaponInfo.AttackDistance;//��Ÿ�
        newBullet.speed = 1f;
        //itemWeapon.weaponInfo.AttackSpeed;//�ʴ� ���ݼӵ�
        newBullet.damage = itemWeapon.weaponInfo.AttackPoint;
        newBullet.attacker = rootCtrl;
        newBullet.gameObject.SetActive(true);
        newBullet.transform.position = fireTran.position;
        newBullet.transform.eulerAngles = SolidUtility.getAngle2D(dic);
        newBullet.enable();
    }
}
