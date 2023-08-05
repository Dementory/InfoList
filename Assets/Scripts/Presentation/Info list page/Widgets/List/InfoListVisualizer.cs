using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace InfoList.Presentation
{
    public abstract class InfoListVisualizer : MonoBehaviour
    {
        public abstract void Initialize(ListView infoList);

        public abstract void UpdateList(List<DataItem> dataItems, int startIndex);
    }
}
