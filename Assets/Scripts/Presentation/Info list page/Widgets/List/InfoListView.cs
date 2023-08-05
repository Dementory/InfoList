using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace InfoList.Presentation
{
    public class InfoListView : InfoListVisualizer
    {
        [SerializeField] private VisualTreeAsset _infoTile;
        [Space]
        [SerializeField] private List<CategorySprite> _categorySprites = new List<CategorySprite>();

        private ListView _infoList;

        private List<InfoListTile> _tileList = new List<InfoListTile>();

        public override void Initialize(ListView infoList)
        {
            _infoList = infoList;
        }

        public override void UpdateList(List<DataItem> dataItems, int startIndex)
        {
            if (_tileList.Count == 0)
                _infoList.hierarchy.Clear();

            for (int i = 0; i < dataItems.Count; i++)
            {
                DataItem item = dataItems[i];
                InfoListTile infoTile = GetInfoTile(i);

                Sprite badgeSprite = GetBadge(item.Category);
                int id = startIndex + i + 1;
                infoTile.UpdateView(id, dataItems[i], badgeSprite);
            }
        }

        private InfoListTile GetInfoTile(int id)
        {
            if (_tileList.Count - 1 >= id)
            {
                return _tileList[id];
            }
            else
            {
                InfoListTile tile = new InfoListTile(_infoTile);
                _tileList.Add(tile);

                _infoList.hierarchy.Add(tile);

                return tile;
            }
        }

        private Sprite GetBadge(DataItem.CategoryType categoryType)
        {
            return _categorySprites.Where(categorySprite => categorySprite.Category == categoryType).Select(categorySprite => categorySprite.Sprite).FirstOrDefault();
        }
    }

    [System.Serializable]
    public class CategorySprite
    {
        public DataItem.CategoryType Category;
        public Sprite Sprite;
    }
}
