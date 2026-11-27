using UnityEngine;
using System.Collections;

public class pistol : MonoBehaviour
{
    public int damage = 20;
    public float range = 100f;
    public float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;
    private float lineDuration = 0.05f;
    public Transform cameraTransform;
    public Transform muzzleTransform;
    public LineRenderer lineRenderer;
    public int maxAmmo = 6;
    [SerializeField] private int currentAmmo;
    public int CurrentAmmo => currentAmmo; //read only
    public int maxReserveAmmo = 18;
    [SerializeField]  private int currentReserveAmmo = 12;
    public int CurrentRAmmo => currentReserveAmmo; //read only
    private Coroutine tracerCoroutine;

    void Start()
    {
        lineRenderer.enabled = false;
        currentAmmo = maxAmmo;
    }

    public void Attack()
    {
        if (currentAmmo > 0)
        {
            if (Time.time >= nextAttackTime)
            {
                currentAmmo -= 1;
                nextAttackTime = Time.time + attackCooldown;
                Vector3 traceEnd = cameraTransform.position + cameraTransform.forward * range;
                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, range))
                {
                    traceEnd = hitInfo.point;
                    Health targetHealth = hitInfo.collider.gameObject.GetComponent<Health>();
                    if (targetHealth != null)
                    {
                        targetHealth.TakeDamage(damage);
                    }
                }
                lineRenderer.SetPosition(0, muzzleTransform.position);
                lineRenderer.SetPosition(1, traceEnd);
                if (tracerCoroutine != null)
                {
                    StopCoroutine(tracerCoroutine);
                }
                tracerCoroutine = StartCoroutine(ShowTracer());

            }
        }
    }

    public void Reload()
    {
        if (currentReserveAmmo > 0 && currentAmmo < maxAmmo)
        {
            int ammoNeeded = maxAmmo - currentAmmo;
            int ammoToReload = Mathf.Min(ammoNeeded, currentReserveAmmo);
            currentAmmo += ammoToReload;
            currentReserveAmmo -= ammoToReload;

        }
    }

    public void ReceiveAmmo(int ammo)
    {
        if (ammo < 0) //negative ammo is not allowed
        {
            Debug.LogWarning("Negative ammo recieved and is not allowed");
            return;
        }

        currentReserveAmmo += Mathf.Min(ammo, maxReserveAmmo - currentReserveAmmo); //uses Mathf.Min to prevent overammo
    }

    private IEnumerator ShowTracer()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(lineDuration);
        lineRenderer.enabled = false;
        tracerCoroutine = null;
    }

    private void OnDisable()
    {
        if (tracerCoroutine != null)
        {
            StopCoroutine(tracerCoroutine);
            tracerCoroutine = null;
            lineRenderer.enabled = false;
        }
    }

}
