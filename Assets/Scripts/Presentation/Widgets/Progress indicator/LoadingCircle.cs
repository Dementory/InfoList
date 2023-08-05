using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace InfoList.Widgets
{
    public class LoadingCircle : ProgressIndicator
    {
        private bool _startProgress;

        public override void StartProgress()
        {
            base.StartProgress();

            _startProgress = true;

            UpdateRotation();
        }

        public override void StopProgress()
        {
            _startProgress = false;

            base.StopProgress();
        }

        private async void UpdateRotation()
        {
            float progress = 0;

            while (_startProgress)
            {
                visualElement.style.rotate = new Rotate(progress);
                progress += speed;

                await Task.Delay(50);
            }
        }
    }
}
