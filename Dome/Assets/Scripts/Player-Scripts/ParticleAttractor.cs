using UnityEngine;

public class ParticleAttractor : MonoBehaviour
{
    public float speed = 10f; // Speed of the particles
    public string targetTag = "EnergyTarget"; // Tag of the target object
    public float delayBeforeAiming = 1f; // Delay before particles start aiming at the target
    public float particleLifetime = 2f; // Lifetime of particles in seconds

    private ParticleSystem particleSystem;
    private Transform targetTransform;
    private ParticleSystem.Particle[] particles;
    private float delayTimer = 0f;
    private bool isAiming = false;

    void Start()
    {
        // Get the ParticleSystem component
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem == null)
        {
            Debug.LogError("No ParticleSystem found on this GameObject.");
            return;
        }

        // Set the lifetime of the particles
        var mainModule = particleSystem.main;
        mainModule.startLifetime = particleLifetime;

        // Find the target object by tag
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject == null)
        {
            Debug.LogError("No GameObject with tag " + targetTag + " found.");
            return;
        }

        targetTransform = targetObject.transform;

        // Initialize particle array
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];

        // Play the particle system
        particleSystem.Play();
    }

    void Update()
    {
        if (targetTransform == null)
        {
            Debug.LogError("TargetTransform is not set.");
            return;
        }

        // Timer to start aiming
        if (!isAiming)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= delayBeforeAiming)
            {
                isAiming = true;
            }
        }

        if (isAiming)
        {
            // Get particles from the system
            int numParticles = particleSystem.GetParticles(particles);

            // Update particles' velocities to move towards the target
            for (int i = 0; i < numParticles; i++)
            {
                Vector3 particlePosition = particles[i].position;
                Vector3 particleDirection = (targetTransform.position - particlePosition).normalized;

                // Calculate new velocity to move particles towards the target
                particles[i].velocity = particleDirection * speed;
            }

            // Apply the updated particles
            particleSystem.SetParticles(particles, numParticles);
        }
    }
}
