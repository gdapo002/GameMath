using UnityEngine;

public class UniformMotion : MonoBehaviour
{
    public GameObject MovingObject;
    public Vector3 Pos0;
    public Vector3 Vel;
    public float Time = 0f;
    private void OnDrawGizmos()
    {
        Drawing.DrawVector(Vector3.zero, 10f * Vector3.right, 3f, 0.4f, Color.red);
        Drawing.DrawVector(Vector3.zero, 10f * Vector3.up, 3f, 0.4f, Color.green);

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(Pos0, 0.25f);

        Drawing.DrawVector(Vel, Pos0, 3f, 0.4f, Color.blue);
        for (int i = 1; i <= 10; i++)
        {
            //Vector3 Pos = Pos0 + Vel * (i * delta_t);
            Vector3 Pos = Pos0 + i * Vel;
            Gizmos.DrawSphere(Pos, 0.25f);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //x = x0 + v*t
        //Vector3 pos = Pos0 + Time.time * Vel;
        Vector3 pos = Pos0 + Vel * Time;
        MovingObject.transform.position = pos;
    }
}
