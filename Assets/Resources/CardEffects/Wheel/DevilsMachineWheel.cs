using UnityEngine;

public class DevilsMachineWheel : CardEffect
{
    public bool activated = false;
    public int devilsMachineParts = 0;

    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats()
    {
        if (activated) {
            player.maxSpeed += 666f;
            player.acceleration += 666f;
            RaceManager.Instance.CreateBonusText(666f, 1, RaceManager.Instance.speedText.gameObject, Card);
            RaceManager.Instance.CreateBonusText(666f, 1, RaceManager.Instance.accelerationText.gameObject, Card);
        } else {
            player.maxSpeed += 5f;
            player.acceleration += 5f;
            RaceManager.Instance.CreateBonusText(5f, 1, RaceManager.Instance.speedText.gameObject, Card);
            RaceManager.Instance.CreateBonusText(5f, 1, RaceManager.Instance.accelerationText.gameObject, Card);
        }
    }

    public override void UpdateCardDescription()
    {
        if (activated) {
            Card.cardSchema.cardDescription = "Unleash Hell.";
        } else {
            Card.cardSchema.cardDescription = "+5 Speed, +5 Acceleration. Assemble all parts of the Devil's Machine for endless power.";
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