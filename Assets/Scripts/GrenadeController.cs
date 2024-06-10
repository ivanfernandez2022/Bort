using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    private Vector3 target; // Punto de destino
    public float arcHeight = 5f; // Altura del arco
    public float speed = 10f; // Velocidad de la granada

    public void SetTrajectory(Vector3 destination)
    {
        target = destination;
    }

    private void Update()
    {
        // Calcular la posición de la granada en función del tiempo
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        // Aplicar arco a la trayectoria
        float distanceToTarget = Vector3.Distance(transform.position, target);
        transform.position += CalculateArcPoint(transform.position, target, arcHeight, distanceToTarget);
    }

    // Función para calcular el punto de la trayectoria con arco
    private Vector3 CalculateArcPoint(Vector3 startPoint, Vector3 endPoint, float arcHeight, float distance)
    {
        Vector3 direction = endPoint - startPoint;
        float x = direction.x;
        float y = direction.y;
        float z = direction.z;
        float gravity = Mathf.Abs(Physics.gravity.y);
        float angle = Mathf.Atan((y + Mathf.Sqrt(Mathf.Pow(distance, 2) * gravity + 2 * arcHeight * gravity)) / (distance * gravity));
        float velocity = Mathf.Sqrt(gravity * distance / Mathf.Sin(2 * angle));

        Vector3 result = direction.normalized * velocity;
        result.y = result.y + Mathf.Tan(angle) * distance * 0.5f * gravity;
        return result * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        var col = other.gameObject.GetComponent<IDamageable>();

        if (col != null)
        {
            col.OnDamage();
        }
    }
}
