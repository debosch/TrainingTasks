using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RewindTime
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float radius = 3f;
        [SerializeField] private float upForce = 1f;
        [SerializeField] private float power = 10f;

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
                Detonate();
        }

        private void Detonate()
        {
            var explosionPosition = transform.position;
            var colliders = Physics.OverlapSphere(explosionPosition, radius);
            foreach (var hit in colliders)
            {
                var rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
            }
        }
    }
}

