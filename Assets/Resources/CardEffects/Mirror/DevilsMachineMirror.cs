using UnityEngine;

public class DevilsMachineMirror : CardEffect
{
    public bool activated = false;
    public int devilsMachineParts = 0;

    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats()
    {
        if (activated) {
            player.driverPower += 0.333f;
            RaceManager.Instance.CreateBonusText(.333f, 1, RaceManager.Instance.driverPowerText.gameObject, Card);
        } else {
            player.driverPower += 0.1f;
            RaceManager.Instance.CreateBonusText(.1f, 1, RaceManager.Instance.driverPowerText.gameObject, Card);


        }
    }

    public override void UpdateCardDescription()
    {
        if (activated) {
            Card.cardSchema.cardDescription = "Unleash Hell.";
        } else {
            Card.cardSchema.cardDescription = "+0.1 Driver Power. Assemble all parts of the Devil's Machine for endless power.";
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