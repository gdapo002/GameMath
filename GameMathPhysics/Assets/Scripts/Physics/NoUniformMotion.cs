using UnityEngine;
using UnityEngine.InputSystem;

public class NoUniformMotion : MonoBehaviour
{
    [Header("Motion variables")]
    public GameObject MovingObject;
    public Vector3 Pos0; //x0
    public Vector3 Vel0; //v0
    public Vector3 Acc; //a

    //weekly task: - we want to hit a target that's 2 kilometers
    //away (Sunila factory) from the campus
    // - our weapon of ckoice is a 1600th century black powder cannon
    //and the initial velocity is 250 m/s
    // - simulate this using our codebase from class and find what is elevation angle needed for the cannon?
    // - are we able to hit this?
    // - if not, what is the maximum range? (or what is the minimum v0 to hit the factory?)

    [Header("Internal variable")]
    [SerializeField]
    private Vector3 vel;
    //public float Time = 0f;
    private void OnDrawGizmos()
    {
        Drawing.DrawVector(Vector3.zero, 10f * Vector3.right, 3f, 0.4f, Color.red);
        Drawing.DrawVector(Vector3.zero, 10f * Vector3.up, 3f, 0.4f, Color.green);

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(Pos0, 0.25f);

        Drawing.DrawVector(Pos0, Vel0, 3f, 0.4f, Color.darkCyan);

        Gizmos.color = Color.lightCoral;
        float delta_t = 0.010f;

        Vector3 pos = Pos0; // initial position
        Vector3 vel = Vel0; //initial velocity
        for (int i = 1; i <= 250; i++)
        {
            // x = x0 + v * t
            pos = pos + vel * delta_t;

            //v = v0 + a * t
            vel = vel + Acc * delta_t;
            Gizmos.DrawSphere(pos, 0.10f);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MovingObject.transform.position = Pos0;
        vel = Vel0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = MovingObject.transform.position;
        //Update position
        pos = pos + vel * Time.deltaTime;
        //Update velocity
        vel = vel + Acc * Time.deltaTime;

        //try to get WASD and modify velocity
        if (Keyboard.current.wKey.wasPressedThisFrame) 
        {
            vel += Vector3.up * 5f;
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            vel += Vector3.left * 5f;
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            vel += Vector3.right * 5f;
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            vel += Vector3.down * 5f;
        }

        MovingObject.transform.position = pos;
    }
}
