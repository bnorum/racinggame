using UnityEngine;

public class DevilsMachineDash : CardEffect
{
    public bool activated = false;
    public int devilsMachineParts = 0;

    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats()
    {
        if (activated) {
            player.driverPower += 0.333f;
            RaceManager.Instance.CreateBonusText(0.333f, 1, RaceManager.Instance.driverPowerText.gameObject, Card);
        } else {
            player.driverPower += 0.2f;
            RaceManager.Instance.CreateBonusText(0.2f, 1, RaceManager.Instance.driverPowerText.gameObject, Card);

        }
    }

    public override void UpdateCardDescription()
    {
        if (activated) {
            Card.cardSchema.cardDescription = "Unleash Hell.";
        } else {
            Card.cardSchema.cardDescription = "+0.2 Driver Power. Assemble all parts of the Devil's Machine for endless power.";
        }
    }

    public override void Update()
    {
        base.Update();
        if (activated == false) {
            devilsMachineParts = 0;
            foreach (Card card in EquipPanelManager.Instance.Cards) {
                if (card.cardSchema.isDevilsMachineCard) {
                    devilsMachineParts++;
                }
            }
            if (devilsMachineParts == 8 && !Card.currentSlot.isLocked) {
                activated = true;
                Card.currentSlot.isLocked = true;
            }
        }
    }


}