using System.Collections.Generic;
using System.Linq;
using _Project.Configs.Boards;
using _Project.Scripts.Core;
using _Project.Scripts.Data;
using Logging;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Windows.HUD
{
    public interface IBoardFactory
    {
    }

    public class BoardFactory : IBoardFactory
    {
        [Inject] private ICustomLogger _logger;

        private readonly BoardDatabase _boardDatabase;
        private readonly BoardConfig _boardConfig;

        private Dictionary<SymbolType, SymbolSpriteModel> SpriteModels { get; }

        public BoardFactory(BoardDatabase boardDatabase, BoardConfig boardConfig)
        {
            _boardConfig = boardConfig;
            _boardDatabase = boardDatabase;

            SpriteModels = _boardDatabase.SymbolSprites.ToDictionary(x => x.SymbolType, y => y);
        }

        public CellView CreateCell(CellView buttonPrefab, Transform parent, SymbolType symbolType)
        {
            var cellView = Object.Instantiate(buttonPrefab, parent);
            cellView.SetSymbolSprite(GetSpriteByType(symbolType), symbolType);
            return cellView;
        }

        public Sprite GetSpriteByType(SymbolType symbolType) => 
            SpriteModels[symbolType].SymbolSprite;

        public void GenerateLines(int cols, int rows, GridLayoutGroup gridLayoutGroup, Transform linesParent, int lineWidth)
        {
            float cellWidth = gridLayoutGroup.cellSize.x;
            float cellHeight = gridLayoutGroup.cellSize.y;
            float spacingX = gridLayoutGroup.spacing.x;
            float spacingY = gridLayoutGroup.spacing.y;
            RectOffset padding = gridLayoutGroup.padding;

            float totalWidth = cols * cellWidth + (cols - 1) * spacingX + padding.left + padding.right;
            float totalHeight = rows * cellHeight + (rows - 1) * spacingY + padding.top + padding.bottom;

            // Create vertical lines
            for (int x = 0; x < cols-1; x++)
            {
                var verticalLine = Object.Instantiate(_boardDatabase.LinePrefab, linesParent);
                var verticalLineRT = (RectTransform) verticalLine.transform;
                verticalLineRT.sizeDelta = new Vector2(lineWidth, totalHeight);
                verticalLineRT.anchoredPosition =
                    new Vector2(x * (cellWidth + spacingX) - cellWidth / lineWidth - spacingX / lineWidth, 0);
            }

            // Create horizontal lines
            for (int y = 0; y < rows-1; y++)
            {
                var horizontalLine = Object.Instantiate(_boardDatabase.LinePrefab, linesParent);
                var horizontalLineRT = (RectTransform) horizontalLine.transform;
                horizontalLineRT.sizeDelta = new Vector2(totalWidth, lineWidth);
                horizontalLineRT.anchoredPosition =
                    new Vector2(0, -y * (cellHeight + spacingY) + cellHeight / lineWidth + spacingY / lineWidth);
            }
        }

        public SymbolType[,] CreateGrid() => 
            new SymbolType[_boardConfig.GridSize, _boardConfig.GridSize];
    }
}