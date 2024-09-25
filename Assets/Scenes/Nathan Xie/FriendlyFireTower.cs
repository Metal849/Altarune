using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyFireTower : Summon
{
    [SerializeField] private FriendlyFireBigProjectile projectilePrefab;
    [SerializeField] private float attackInterval;
    [SerializeField] private int minRandAngle;
    [SerializeField] private int maxRandAngle;

    private float attackTick = -0.2f;
    private GameObject player = null;
    private bool init;

    protected override void Awake(){
        base.Awake();
        if(player == null){
            player = FindObjectOfType<PlayerController>().gameObject;
        }
    }

    public override void Init() => init = true;

    // Update is called once per frame
    void Update()
    {
        Target();
    }
    private void Target(){
        if(!init) return;
        attackTick += Time.deltaTime;
        if (attackTick >= attackInterval) {
            System.Random rand = new System.Random();
            Vector3 playerVector = player.transform.position - this.transform.position;
            float angle = Vector3.SignedAngle(playerVector, transform.right, Vector3.up);
            float randomAngle = rand.Next(minRandAngle,maxRandAngle);
            FriendlyFireBigProjectile projectile = Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
            Quaternion myRotation = Quaternion.AngleAxis(-angle + randomAngle, Vector3.up);
            Vector3 startDirection = transform.right;
            Vector3 result = myRotation * startDirection;
            projectile.Launch(result);
            attackTick = 0;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Entity entity)) {
            
        }
    }
}
