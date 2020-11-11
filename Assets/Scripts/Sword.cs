using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float cooldownSpeed = 5f;
    public float cooldownDuration = 0.5f;
    public float attackDuration = 0.35f;
    float cooldownTimer;
    bool isAttacking;
    public float swingingSpeed = 5f;
    Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        targetRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * (isAttacking ? swingingSpeed:cooldownSpeed));
        cooldownTimer -= Time.deltaTime;
    }

    public void SwordAttack()
    {
        if(cooldownTimer > 0f)
        {
            return;
        }

        targetRotation = Quaternion.Euler(90, 0, 0);

        cooldownTimer = cooldownDuration;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;

        targetRotation = Quaternion.Euler(0, 0, 0);
    }
}
