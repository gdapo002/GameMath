using UnityEngine;
using UnityEditor;

public class LookAtTrigger : MonoBehaviour
{
    [Range(0.1f, 20f)]
    public float radius = 5f;

    [Range(0f, 360f)]
    public float FOVDegrees = 90f;

    [SerializeField]
    private float Threshold = 0.75f; // Порог для dot product

    public GameObject Target;
    public GameObject LookingAt;

    public bool Triggered = false;

    private bool IsTriggered()
    {
        // Пересчитываем порог на основе угла
        Threshold = Mathf.Cos(Mathf.Deg2Rad * FOVDegrees / 2f);

        Vector3 trigger = transform.position;
        Vector3 target = Target.transform.position;
        Vector3 looking = LookingAt.transform.position;

        Vector3 trigger_to_target = target - trigger;
        Vector3 trigger_to_lookat = looking - trigger;

        // 1. Проверка радиуса
        if (trigger_to_target.magnitude > radius)
            return false;

        // 2. Проверка угла (LookAt)
        float dotp = Vector3.Dot(trigger_to_target.normalized, trigger_to_lookat.normalized);

        return dotp >= Threshold;
    }

    private void OnDrawGizmos()
    {
        if (Target == null || LookingAt == null) return; // защита от null

        Triggered = IsTriggered(); // правильный вызов метода

        if (Triggered)
            Handles.color = Color.red;
        else
            Handles.color = Color.green;

        Handles.DrawWireDisc(transform.position, Vector3.up, radius); // radius с маленькой

        Vector3 trigger = transform.position;
        Vector3 target = Target.transform.position;
        Vector3 looking = LookingAt.transform.position;
        Vector3 trigger_to_target = target - trigger; // объявлено
        Vector3 trigger_to_lookat = looking - trigger; // объявлено

        // СТРЕЛКА У TRIGGER
        if (trigger_to_lookat.sqrMagnitude > 0.0001f)
        {
            Color arrowColor = Triggered ? Color.red : Color.blue;
            Drawing.DrawVector(trigger_to_lookat.normalized * 2f, trigger, 2f, 0.4f, arrowColor);
        }

        // СТРЕЛКА У LOOKINGAT
        Drawing.DrawVector(LookingAt.transform.forward * 2f, looking, 2f, 0.4f, Color.cyan);

        // СТРЕЛКА У TARGET
        Drawing.DrawVector(Target.transform.forward * 2f, target, 2f, 0.4f, Color.yellow);

        // Вектор от триггера к цели
        Drawing.DrawVector(trigger_to_target, trigger, 2f, 0.4f, Color.darkMagenta);

        // Границы поля зрения
        if (trigger_to_lookat.sqrMagnitude > 0.0001f)
        {
            Quaternion rotLeft = Quaternion.AngleAxis(-FOVDegrees / 2f, Vector3.up); // Quaternion
            Quaternion rotRight = Quaternion.AngleAxis(FOVDegrees / 2f, Vector3.up);

            Vector3 direction = radius * trigger_to_lookat.normalized;
            Vector3 leftDir = rotLeft * direction;
            Vector3 rightDir = rotRight * direction;

            Color col = Triggered ? Color.red : Color.green;
            Drawing.DrawVector(leftDir, trigger, 2f, 0.4f, col);
            Drawing.DrawVector(rightDir, trigger, 2f, 0.4f, col);

            Handles.DrawWireArc(trigger, Vector3.up, leftDir, FOVDegrees, radius); //правильные аргументы
        }
    }
}