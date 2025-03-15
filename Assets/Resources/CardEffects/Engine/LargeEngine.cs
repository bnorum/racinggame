using UnityEngine;

public class LargeEngine : CardEffect
{



    public override void OnBuy()
    {
        EquipPanelManager.Instance.TransmissionSlot.isLocked = true;
    }

    public override void OnSell()
    {
        EquipPanelManager.Instance.TransmissionSlot.isLocked = false;
    }
}
