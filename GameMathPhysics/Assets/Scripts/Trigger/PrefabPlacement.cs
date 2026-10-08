using UnityEngine;

public class PrefabPlacement : MonoBehaviour
{
    public GameObject MyPrefab;       
    public float Distance = 20f;     
    public LayerMask TerrainLayer;   

    private GameObject spawnedObject;

    private void Update()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward; 

        if (Physics.Raycast(origin, direction, out hit, Distance, TerrainLayer))
        {
            Vector3 normal = hit.normal;

            Vector3 lookDirection = direction;
            
            Vector3 right = Vector3.Cross(normal, lookDirection).normalized;

            Vector3 forward = Vector3.Cross(right, normal).normalized;

            Quaternion rotation = Quaternion.LookRotation(forward, normal);

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(MyPrefab);
            }
            spawnedObject.transform.position = hit.point;
            spawnedObject.transform.rotation = rotation;

            Debug.DrawLine(origin, hit.point, Color.red);
            Debug.DrawRay(hit.point, normal * 3f, Color.green);
            Debug.DrawRay(hit.point, right * 3f, Color.magenta);
            Debug.DrawRay(hit.point, forward * 3f, Color.blue);
        }
    }
}