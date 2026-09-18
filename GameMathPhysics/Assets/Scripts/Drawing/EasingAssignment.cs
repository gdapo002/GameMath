using UnityEngine;

public enum EasingType
{
    Linear,
    EaseIn,
    EaseOut,
    EaseInOut
}

public class EasingAssignment : MonoBehaviour
{
    public GameObject[] MyGameObjects;

    public Color[] MyColorsStart;
    public Color[] MyColorsEnd;

    [Range(1f, 10f)]
    public float EasingTime = 5f;
    [Range(0f, 5f)]
    public float StartTime = 2f;

    public float MoveAmount = 5f;

    public bool Loop = true;

    private Vector3[] OriginalPositions;

    private EasingFunction.Function[] EasingFunctions;

    public EasingFunction.Ease[] Easings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OriginalPositions = new Vector3[MyGameObjects.Length];
        EasingFunctions = new EasingFunction.Function[MyGameObjects.Length];
        for (int i = 0; i < MyGameObjects.Length; i++)
        {
            OriginalPositions[i] = MyGameObjects[i].transform.position;
            EasingFunctions[i] = EasingFunction.GetEasingFunction(Easings[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < StartTime)
            return;
        else
        {
            float t = 0.0f;
            if (!Loop)
                t = Mathf.Clamp01((Time.time - StartTime) / EasingTime);
            else
                t = Mathf.Clamp01(((Time.time - StartTime) % StartTime) / EasingTime);

            for (int i = 0; i < MyGameObjects.Length; i++)
            {
                // Position
                MyGameObjects[i].transform.position =
                    OriginalPositions[i] + MoveAmount * EasingFunctions[i](0f, 1f, t) * Vector3.right;
                // Material color
                MyGameObjects[i].GetComponent<MeshRenderer>().material.color = Color.Lerp(MyColorsStart[i], MyColorsEnd[i], EasingFunctions[i](0f, 1f, t));

            }

        }

    }
}