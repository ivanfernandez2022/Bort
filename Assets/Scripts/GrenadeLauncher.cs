using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    [SerializeField] GameObject grenadePrefab; 
    [SerializeField] private GameObject pointB;

    //private Transform pointA; 
    private bool moveMark;
    private Vector3 initialPoint;

    private void Start()
    {
        initialPoint = pointB.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveMark = !moveMark;
            if (!moveMark)
            {
                LaunchGrenade();
                pointB.transform.position = initialPoint;
            }
        }

        if (moveMark)
        {
            pointB.transform.position += Vector3.forward * 10 * Time.deltaTime;
        }
       
        
    }

   


    void LaunchGrenade()
    {
        // Instanciar la granada y configurar su trayectoria
        GameObject grenade = Instantiate(grenadePrefab, transform.position, Quaternion.identity);
        GrenadeController grenadeController = grenade.GetComponent<GrenadeController>();
        if (grenadeController != null)
        {
            grenadeController.SetTrajectory(pointB.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        // Dibujar una línea entre los puntos A y B para visualizar la trayectoria
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, pointB.transform.position);
    }
}
