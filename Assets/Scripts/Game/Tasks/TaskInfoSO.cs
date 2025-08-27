using UnityEngine;

[CreateAssetMenu(fileName = "TaskInfoSO", menuName = "Scriptable Objects/TaskInfoSO", order = 1)]
public class TaskInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Steps")]
    public GameObject[] taskStepPrefabs;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
