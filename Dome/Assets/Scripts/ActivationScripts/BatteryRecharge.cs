using UnityEngine;

public class BatteryRecharge : MonoBehaviour
{
    public GameObject fullBatteryPrefab; // Assign the full battery prefab in the inspector
    public float rechargeTime = 120f; // 2 minutes recharge time

    private float rechargeTimer = 0f;
    private bool isRecharging = false;

    void Start()
    {
        // Start the recharge timer
        isRecharging = true;
        rechargeTimer = rechargeTime;
    }

    void Update()
    {
        if (isRecharging)
        {
            // Countdown the recharge timer
            rechargeTimer -= Time.deltaTime;

            if (rechargeTimer <= 0f)
            {
                RechargeBattery();
            }
        }
    }

    void RechargeBattery()
    {
        // Instantiate the full battery at the current position and rotation
        Instantiate(fullBatteryPrefab, transform.position, transform.rotation);

        // Destroy the empty battery
        Destroy(gameObject);
    }
}
