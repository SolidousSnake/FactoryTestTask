using _Project.Code.Data.Enum;
using UnityEngine;

namespace _Project.Code.Data.Config
{
    [CreateAssetMenu]
    public class ResourceConfig : ScriptableObject
    {
        [field: SerializeField] public ResourceType Type { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}