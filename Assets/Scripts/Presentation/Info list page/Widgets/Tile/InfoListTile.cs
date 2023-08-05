using UnityEngine;
using UnityEngine.UIElements;

namespace InfoList.Presentation
{
    public class InfoListTile : VisualElement
    {
        private Label _idLabel;
        private Label _contentLabel;
        private VisualElement _badge;
        private VisualElement _highlight;

        public InfoListTile(VisualTreeAsset visualTree)
        {
            visualTree.CloneTree(this);

            _idLabel = this.Q<Label>("id_text");
            _contentLabel = this.Q<Label>("content_text");
            _badge = this.Q<VisualElement>("badge");
            _highlight = this.Q<VisualElement>("highlight");
        }

        public void UpdateView(int id, DataItem dataItem, Sprite badgeSprite)
        {
            _idLabel.text = $"{id}";
            _contentLabel.text = $"{dataItem.Description}";

            _badge.style.backgroundImage = new StyleBackground(badgeSprite);

            _highlight.visible = dataItem.Special;
        }
    }
}
