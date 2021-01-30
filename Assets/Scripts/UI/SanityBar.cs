using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public Slider slider;

    private void Start()
    {
        playerStatus.SanityChangedEvt += PlayerStatus_SanityChangedEvt;
        playerStatus.SanityLimitChangedEvt += PlayerStatus_SanityLimitChangedEvt;
    }

    private void PlayerStatus_SanityLimitChangedEvt(object sender, int sanityMax)
    {
        slider.maxValue = sanityMax;
    }

    private void PlayerStatus_SanityChangedEvt(object sender, int sanity)
    {
        slider.value = sanity;
    }

    private void OnDestroy()
    {
        playerStatus.SanityChangedEvt -= PlayerStatus_SanityChangedEvt;
        playerStatus.SanityLimitChangedEvt -= PlayerStatus_SanityLimitChangedEvt;
    }
}
