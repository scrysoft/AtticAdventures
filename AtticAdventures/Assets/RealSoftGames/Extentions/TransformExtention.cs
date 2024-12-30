///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 28/07/2019 11:47
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames
{
    public static class TransformExtension
    {
        // Generic method to find a GameObject with a specific name in the transform hierarchy.
        public static Transform FindDeepChild(this Transform parent, string name)
        {
            foreach (Transform child in parent)
            {
                if (child.name == name)
                    return child;
                var result = child.FindDeepChild(name);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}