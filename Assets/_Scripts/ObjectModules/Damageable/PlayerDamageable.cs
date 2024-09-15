using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : Damageable {

    public event System.Action<int> OnPlayerDamage;
    [SerializeField] private int doubleDamageThreshold;

    protected override bool BaseObject_OnTryDamage(int amount, ElementType element) {
        if (!iFrameOn) {
            int processedAmount = amount > doubleDamageThreshold ? 2 : 1;
            runtimeHP.DoDamage(processedAmount);
            OnPlayerDamage?.Invoke(processedAmount);
            StartCoroutine(ISimulateIFrame());

            if (runtimeHP.Health <= 0) {
                Debug.Log("Player ded");
                ToggleIFrame(true);
                return true;
            }
        }
        return !iFrameOn;
    }

    protected override IEnumerator ISimulateIFrame() {
        iFrameOn = true;
        baseObject.SetMaterial(iFrameProperties.flashMaterial);
        Time.timeScale = 0.6f;
        yield return new WaitForSeconds(iFrameProperties.duration / 2);
        Time.timeScale = 1f;
        baseObject.ResetMaterials();
        yield return new WaitForSeconds(iFrameProperties.duration / 2);
        iFrameOn = false;
    }
}