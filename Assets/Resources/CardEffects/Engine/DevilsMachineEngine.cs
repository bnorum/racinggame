using System.Collections.Generic;
using UnityEngine;

public class DevilsMachineEngine : CardEffect
{
    public bool activated = false;
    public int devilsMachineParts = 0;


    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats()
    {
        if (activated) {
            player.maxSpeed += 666f;
            RaceManager.Instance.CreateBonusText(666f, 1, RaceManager.Instance.speedText.gameObject, Card);
        } else {
            player.maxSpeed += 20f;
            RaceManager.Instance.CreateBonusText(20f, 1, RaceManager.Instance.speedText.gameObject, Card);
        }
    }

    public override void UpdateCardDescription()
    {
        if (activated) {
            Card.cardSchema.cardDescription = "Unleash Hell.";
        } else {
            Card.cardSchema.cardDescription = "+20 Speed. Assemble all parts of the Devil's Machine for endless power.";
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