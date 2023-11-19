using UnityEngine;

namespace SDUnityExtension.Scripts.Logic.Filter
{
    public interface IFilter
    {
        bool Test(GameObject other);
    }
}
