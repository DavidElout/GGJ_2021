using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    [SerializeField] private PlayerFireBehaviour playerFireBehaviour;

    private Slider[] sliders;
    private List<Attack> attacks = new List<Attack>();

    // Start is called before the first frame update
    void Start()
    {
        sliders = GetComponentsInChildren<Slider>();
        attacks.Add(playerFireBehaviour.ProjectileAttack);
        attacks.Add(playerFireBehaviour.MeleeAttack);
        attacks.Add(playerFireBehaviour.ShieldAttack);
        for (int i = 0; i < attacks.Count; i++)
        {
            sliders[i].maxValue = attacks[i].CooldownTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < attacks.Count; i++)
        {
            sliders[i].value = attacks[i].CooldownTime - attacks[i].Timer;
        }
    }
}
