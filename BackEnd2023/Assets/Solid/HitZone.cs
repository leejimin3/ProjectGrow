using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour, I_HitZone
{
    public RootCtrl rootCtrl;
    public RootCtrl RootCtrl => rootCtrl;
    public Faction Faction => rootCtrl.faction;
    public Collider2D hitBox;
    private void Start()
    {
        rootCtrl = GetComponentInParent<RootCtrl>();
        hitBox = GetComponent<Collider2D>();

        RootCtrl.lifeAction += () =>
        {
            hitBox.enabled = true;
            GameManager.instance.AddtoTransformlist(rootCtrl);
        };
        RootCtrl.deadAction += () =>
        {
            hitBox.enabled = false;
            GameManager.instance.DeleteTransformlist(rootCtrl);
        };
    }


    public void SetDamaged(float damage, RootCtrl attacker)
    {
        if (rootCtrl.stateCtrl.stateEnum != stateEnum.Dead && rootCtrl.stateCtrl.stateEnum != stateEnum.Stunned)
        {
            rootCtrl.hpCtrl.SetDamaged(damage, attacker);
        }
    }

    public bool CheckHitLock(RootCtrl attacker)
    {
        switch (RootCtrl.faction)
        {
            case Faction.None:
                break;
            case Faction.Player:
                if (attacker.faction == Faction.Enemy)
                {
                    return false;
                }
                break;
            case Faction.Pot:
                break;
            case Faction.Enemy:
                if (attacker.faction != Faction.Enemy)
                {
                    return false;
                }
                break;
        }
        return true;

    }
}
