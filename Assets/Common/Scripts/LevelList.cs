using System.Collections.Generic;
using UnityEngine;

namespace WordZuma.Common
{
    [CreateAssetMenu(fileName = "Levels", menuName = "WordZuma/AllLevels")]
    public class LevelList : ScriptableObject
    {
        public List<LevelData> Levels;
    }
}
