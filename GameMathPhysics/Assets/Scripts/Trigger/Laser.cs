using UnityEngine;

public class Laser : MonoBehaviour
{
    [Range(1, 400)]
    public int Iterations = 5;

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 laserDirection = transform.right;
        RaycastHit hit;

        for (int i = 0; i < Iterations; i++)
        {
            if (Physics.Raycast(origin, laserDirection, out hit))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(origin, hit.point);
                Gizmos.DrawSphere(hit.point, 0.2f);

                Drawing.DrawVector(3f * hit.normal, hit.point, 2f, 0.3f, Color.cyan);

                laserDirection = Vector3.Reflect(laserDirection, hit.normal);
                Drawing.DrawVector(laserDirection, hit.point, 2f, 0.3f, Color.red);

                origin = hit.point;
            }
            else
            {
                break;
            }
        }
    }
}
