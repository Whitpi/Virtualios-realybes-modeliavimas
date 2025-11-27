
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 10f;
    public GameObject particles;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(SoundFXManager.instance.glassShatter, transform, 0.5f);
            Instantiate(particles, transform.position, transform.rotation);
            Die();

        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
