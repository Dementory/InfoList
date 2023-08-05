using UnityEngine.UIElements;

namespace InfoList.Widgets
{
    public abstract class ProgressIndicator
    {
        protected VisualElement visualElement;
        protected float speed;

        public virtual void Initialize(VisualElement visualElement, float speed = 50)
        {
            this.visualElement = visualElement;
            this.speed = speed;
        }

        public virtual void StartProgress()
        {
            visualElement.visible = true;
        }

        public virtual void StopProgress()
        {
            visualElement.visible = false;
        }
    }
}
