using System.Collections.Generic;
using UnityEngine;

public class BacteriaSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject bacteriaPrefab;

    [Header("Swarm Parent")]
    [SerializeField] private Transform swarmParent;

    [Header("Large Bacteria")]
    [SerializeField] private bool spawnLargeBacteria = true;
    [SerializeField] private Transform largeBacteriaSpawnPoint;
    [SerializeField] private float largeBacteriaScale = 8f;

    [Header("Small Bacteria")]
    [SerializeField] private int spawnCount = 50;
    [SerializeField] private Vector3 spawnArea = new Vector3(20f, 10f, 20f);
    [SerializeField] private float smallBacteriaScale = 1f;

    [Header("Spawn Spacing")]
    [SerializeField] private float minimumDistance = 3f;
    [SerializeField] private int maxSpawnAttempts = 30;

    [Header("Glow")]
    [SerializeField] private bool enableGlow = true;
    [SerializeField] private Color glowColor = Color.cyan;
    [SerializeField] private float glowIntensity = 2f;

    [Header("Swarm Rotation")]
    [SerializeField] private bool rotateSwarm = true;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float rotationSpeed = 5f;

    private readonly List<GameObject> spawnedBacteria = new();
    private GameObject largeBacteria;

    private void Awake()
    {
        ClearBacteria();
    }

    private void Update()
    {
        if (rotateSwarm && swarmParent != null)
        {
            swarmParent.Rotate(rotationAxis.normalized,
                rotationSpeed * Time.deltaTime,
                Space.World);
        }
    }

    public void SpawnBacteria()
    {
        ClearBacteria();

        if (swarmParent != null)
            swarmParent.rotation = Quaternion.identity;

        // Spawn Large Bacteria
        if (spawnLargeBacteria &&
            largeBacteriaSpawnPoint != null &&
            bacteriaPrefab != null)
        {
            largeBacteria = Instantiate(
                bacteriaPrefab,
                largeBacteriaSpawnPoint.position,
                largeBacteriaSpawnPoint.rotation,
                largeBacteriaSpawnPoint);

            largeBacteria.transform.localScale = Vector3.one * largeBacteriaScale;
            largeBacteria.SetActive(true);
        }

        // Spawn Small Bacteria
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = Vector3.zero;
            bool foundPosition = false;

            for (int attempt = 0; attempt < maxSpawnAttempts; attempt++)
            {
                Vector3 candidate = transform.position + new Vector3(
                    Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f),
                    Random.Range(-spawnArea.y / 2f, spawnArea.y / 2f),
                    Random.Range(-spawnArea.z / 2f, spawnArea.z / 2f));

                bool tooClose = false;

                foreach (GameObject obj in spawnedBacteria)
                {
                    if (obj != null &&
                        Vector3.Distance(candidate, obj.transform.position) < minimumDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    spawnPosition = candidate;
                    foundPosition = true;
                    break;
                }
            }

            if (!foundPosition)
                continue;

            GameObject bacteria = Instantiate(
                bacteriaPrefab,
                spawnPosition,
                Random.rotation,
                swarmParent);

            bacteria.transform.localScale = Vector3.one * smallBacteriaScale;

            bacteria.SetActive(true);
            spawnedBacteria.Add(bacteria);
        }
    }

    public void ClearBacteria()
    {
        foreach (GameObject obj in spawnedBacteria)
        {
            if (obj != null)
                Destroy(obj);
        }

        spawnedBacteria.Clear();

        if (largeBacteria != null)
        {
            Destroy(largeBacteria);
            largeBacteria = null;
        }
    }
}