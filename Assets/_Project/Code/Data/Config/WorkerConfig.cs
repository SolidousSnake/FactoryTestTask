using UnityEngine;

namespace _Project.Code.Data.Config
{
    [CreateAssetMenu]
    public class WorkerConfig : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
    }
}