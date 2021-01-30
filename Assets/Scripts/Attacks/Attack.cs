using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float cooldownTime;
    [SerializeField] private int damage = 0;

    private float timer = 0;

    public GameObject targetObject = null;
    public float Range { get; set; }
    public int Damage => damage;
    public float Timer => timer;
    public float CooldownTime => cooldownTime;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public bool TryAttack()
    {
        if (timer > cooldownTime)
        {
            DoAttack();
            timer = 0;
            return true;
        }
        return false;
    }

    public virtual void DoAttack()
    {
    }
}
