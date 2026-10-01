using UnityEngine;
using UnityEditor;

public class RadialTrigger : MonoBehaviour
{
    [Range(0.1f, 20f)]
    public float radius = 5f;

    public bool Triggered = false;
    public GameObject Target;

    private bool IsTriggered()
    {
        Vector3 trigger = transform.position;
        Vector3 target = Target.transform.position;
        Vector3 trigger_to_target = target - trigger;

        return trigger_to_target.magnitude <= radius;
    }

    private void OnDrawGizmos()
    {
        if (Target == null) return;

        Triggered = IsTriggered();

        if (Triggered)
            Handles.color = Color.red;
        else
            Handles.color = Color.green;

        // Рисуем круг радиуса
        Handles.DrawWireDisc(transform.position, Vector3.up, radius);

        Vector3 trigger = transform.position;
        Vector3 target = Target.transform.position;
        Vector3 trigger_to_target = target - trigger;

        // Рисуем векторы
        Drawing.DrawVector(trigger_to_target, trigger, 2f, 0.4f, Color.darkMagenta);
        Drawing.DrawVector(trigger_to_target.normalized, trigger, 2f, 0.4f, Color.white);
    }
}