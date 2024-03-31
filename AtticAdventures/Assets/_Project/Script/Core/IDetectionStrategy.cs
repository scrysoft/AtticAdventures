using AtticAdventures.Utilities;
using UnityEngine;

namespace AtticAdventures.Core
{
    public interface IDetectionStrategy
    {
        bool Execute(Transform player, Transform detector, CountdownTimer timer);
    }
}