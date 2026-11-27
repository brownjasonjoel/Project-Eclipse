using UnityEngine;
using System.Collections;

public class knife : MonoBehaviour
{

    public int damage = 10;
    public float range = 2f;
    public float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;
    public Transform cameraTransform;
    private Vector3 restPosition;
    public float thrustDistance = 0.3f;
    public float thrustDuration = 0.1f;
    public float returnDuration = 0.1f;
    private Vector3 thrustPosition;
    private Coroutine thrustCoroutine;

    public void Start()
    {
        restPosition = transform.localPosition;
        thrustPosition = restPosition + Vector3.forward * thrustDistance;
    }
    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, range))
            {
                //Debug.Log("Hit: " + hitInfo.collider.gameObject.name);
                Health targetHealth = hitInfo.collider.gameObject.GetComponent<Health>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(damage);
                }
            }
            if (thrustCoroutine != null)
            {
                StopCoroutine(thrustCoroutine);
            }
            thrustCoroutine = StartCoroutine(Thrust());
        }
    }

    private IEnumerator Thrust()
    {
        float elapsedTime = 0f;
        while (elapsedTime < thrustDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / thrustDuration;
            transform.localPosition = Vector3.Lerp(restPosition, thrustPosition, t);
            yield return null;
        }
        while (elapsedTime < thrustDuration + returnDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = (elapsedTime - thrustDuration) / returnDuration;
            transform.localPosition = Vector3.Lerp(thrustPosition, restPosition, t);
            yield return null;
        }
        transform.localPosition = restPosition;
        thrustCoroutine = null;
    }

    private void OnDisable()
    {
        if (thrustCoroutine != null)
        {
            StopCoroutine(thrustCoroutine);
            thrustCoroutine = null;
            transform.localPosition = restPosition;
        }
    }
}
