using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public TextMeshProUGUI barText;
    public Slider slider;

    private void Awake()
    {
        playerStatus.SanityChangedEvt += PlayerStatus_SanityChangedEvt;
        playerStatus.SanityLimitChangedEvt += PlayerStatus_SanityLimitChangedEvt;
    }

    private void PlayerStatus_SanityLimitChangedEvt(object sender, int sanityMax)
    {
        slider.maxValue = sanityMax;
        barText.text = $"SANITY - {playerStatus.Sanity} / {playerStatus.SanityLimit.y}";
    }

    private void PlayerStatus_SanityChangedEvt(object sender, int sanity)
    {
        slider.value = sanity;
        barText.text = $"SANITY - {playerStatus.Sanity} / {playerStatus.SanityLimit.y}";
    }

    private void OnDestroy()
    {
        playerStatus.SanityChangedEvt -= PlayerStatus_SanityChangedEvt;
        playerStatus.SanityLimitChangedEvt -= PlayerStatus_SanityLimitChangedEvt;
    }
}
