using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astespawn : MonoBehaviour
{
    public Asteroide asteroidePrefab;
    public float spawnDistance = 12f;
    public float spawnRate = 1f;
    public int amountPerSpawn = 1;
    [Range(0f, 45f)]
    public float trajectoryVariance = 15f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < amountPerSpawn; i++)
        {
            //Creacion random de los asteroides, tomando como base el centro del juego
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            // Cambio de trayectoria del asteroide
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Clonacion del asteroide con diferente tamano
            Asteroide asteroide = Instantiate(this.asteroidePrefab, spawnPoint, rotation);
            asteroide.size = Random.Range(asteroide.minSize, asteroide.maxSize);
            asteroide.SetTrajectory(rotation * -spawnDirection);

            // direccion del spawnear
            Vector2 trajectory = rotation * -spawnDirection;

        }
    }
}
