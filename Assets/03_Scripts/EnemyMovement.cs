﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementPeriod = .5f;
    [SerializeField] ParticleSystem kamikazeEffect;

    [SerializeField] AudioClip enemyAttackSFX;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        print("Starting Patrol !");
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }

        SelfDestruct();
    }

    private void SelfDestruct()
    {
        var vfx = Instantiate(kamikazeEffect, transform.position, Quaternion.identity);
        vfx.Play();
        AudioSource.PlayClipAtPoint(enemyAttackSFX, Camera.main.transform.position);

        Destroy(gameObject);
    }
}
