using UnityEngine;

public class WarpEffect : MonoBehaviour
{
    private ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void PlayWarp()
    {
        particles.Play();
    }
}