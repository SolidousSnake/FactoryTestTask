using System;
using _Project.Code.Data.Enum;

namespace _Project.Code.Provider
{
    public class CollectedResourceProvider
    {
        public event Action<ResourceType> Collected;
        public ResourceType CollectedResource { get; private set; }

        public void SetResource(ResourceType resource)
        {
            CollectedResource = resource;
            Collected?.Invoke(resource);
        }
    }
}