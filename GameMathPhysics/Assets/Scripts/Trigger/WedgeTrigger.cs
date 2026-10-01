using UnityEngine;
using UnityEditor;

public class WedgeTrigger : MonoBehaviour
{
    [Range(0.1f, 20f)]
    public float radius = 5f;

    [Range(0.1f, 10f)]
    public float height = 2f;

    [Range(0f, 360f)]
    public float FOVDegrees = 90f;

    [SerializeField]
    private float Threshold = 0.75f;

    public GameObject Target;
    public GameObject LookingAt;

    public bool Triggered = false;

    private bool IsWedgeTriggered()
    {
        Threshold = Mathf.Cos(Mathf.Deg2Rad * FOVDegrees / 2f);

        Vector3 trigger = transform.position;
        Vector3 target = Target.transform.position;
        Vector3 looking = LookingAt.transform.position;
        Vector3 trigger_to_target = target - trigger;
        Vector3 trigger_to_lookat = looking - trigger;

        // Защита от нулевого вектора
        if (trigger_to_target.sqrMagnitude < 0.0001f) return true;
        if (trigger_to_lookat.sqrMagnitude < 0.0001f) return false;

        // 1. Radial test (по XZ)
        Vector3 flatVector = new Vector3(trigger_to_target.x, 0, trigger_to_target.z);
        if (flatVector.magnitude > radius) return false;

        // 2. Height test (по Y)
        if (Mathf.Abs(trigger_to_target.y) > height) return false;

        // 3. LookAt test
        float dotp = Vector3.Dot(flatVector.normalized, trigger_to_lookat.normalized);
        if (dotp < Threshold) return false;

        return true;
    }

    private void OnDrawGizmos()
    {
        if (Target == null || LookingAt == null) return;

        Triggered = IsWedgeTriggered();

        Handles.color = Triggered ? Color.red : Color.green;

        Vector3 trigger = transform.position;
        Vector3 looking = LookingAt.transform.position;
        Vector3 trigger_to_lookat = looking - trigger;

        if (trigger_to_lookat.sqrMagnitude < 0.0001f) return;

        // Направление взгляда (в плоскости XZ)
        Vector3 lookDir = new Vector3(trigger_to_lookat.x, 0, trigger_to_lookat.z).normalized;

        // Углы дуги
        Quaternion rotLeft = Quaternion.AngleAxis(-FOVDegrees / 2f, Vector3.up);
        Quaternion rotRight = Quaternion.AngleAxis(FOVDegrees / 2f, Vector3.up);

        Vector3 leftDir = rotLeft * lookDir;
        Vector3 rightDir = rotRight * lookDir;

        // Верхняя и нижняя точки
        Vector3 topCenter = trigger + Vector3.up * height;
        Vector3 bottomCenter = trigger - Vector3.up * height;

        // Верхняя и нижняя дуги
        Handles.DrawWireArc(topCenter, Vector3.up, leftDir, FOVDegrees, radius);
        Handles.DrawWireArc(bottomCenter, Vector3.up, leftDir, FOVDegrees, radius);

        // Радиальные линии (верх)
        Handles.DrawLine(topCenter, topCenter + leftDir * radius);
        Handles.DrawLine(topCenter, topCenter + rightDir * radius);

        // Радиальные линии (низ)
        Handles.DrawLine(bottomCenter, bottomCenter + leftDir * radius);
        Handles.DrawLine(bottomCenter, bottomCenter + rightDir * radius);

        // Вертикальные линии по краям
        Handles.DrawLine(topCenter + leftDir * radius, bottomCenter + leftDir * radius);
        Handles.DrawLine(topCenter + rightDir * radius, bottomCenter + rightDir * radius);

        // Стрелки у LookingAt и Target
        Drawing.DrawVector(LookingAt.transform.forward * 2f, looking, 2f, 0.4f, Color.cyan);
        Drawing.DrawVector(Target.transform.forward * 2f, Target.transform.position, 2f, 0.4f, Color.yellow);

        // Вектор от триггера к цели
        Drawing.DrawVector(Target.transform.position - trigger, trigger, 2f, 0.4f, Color.darkMagenta);
    }
}