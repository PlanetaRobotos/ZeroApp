using System.Collections.Generic;
using System.Linq;
using _Project.Models;
using _Project.Models.Boards;
using _Project.Windows.BoardWidget.Views;
using Logging;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Windows.BoardWidget.Factories
{
    public interface IBoardFactory
    {
        int GridSize { get; }
        CellView CreateCell(CellView buttonPrefab, Transform parent, SymbolType symbolType);
        Sprite GetSpriteByType(SymbolType symbolType);
        void GenerateLines(GridLayoutGroup gridLayoutGroup, Transform linesParent, GameObject linePrefab);
        SymbolType[,] CreateGrid();
    }

    public class BoardFactory : IBoardFactory
    {
        private const float LineWidth = 2;

        private readonly BoardConfig _boardConfig;

        [Inject] private ICustomLogger _logger;

        public BoardFactory(BoardDatabase boardDatabase, BoardConfig boardConfig)
        {
            _boardConfig = boardConfig;

            SpriteModels = boardDatabase.SymbolSprites.ToDictionary(x => x.SymbolType, y => y);
        }

        private Dictionary<SymbolType, SymbolSpriteModel> SpriteModels { get; }

        public CellView CreateCell(CellView buttonPrefab, Transform parent, SymbolType symbolType)
        {
            CellView cellView = Object.Instantiate(buttonPrefab, parent);
            cellView.SetSymbolSprite(GetSpriteByType(symbolType), symbolType);
            return cellView;
        }

        public Sprite GetSpriteByType(SymbolType symbolType)
        {
            return SpriteModels[symbolType].SymbolSprite;
        }

        public void GenerateLines(GridLayoutGroup gridLayoutGroup, Transform linesParent, GameObject linePrefab)
        {
            float lineWidth = LineWidth;
            float cellWidth = gridLayoutGroup.cellSize.x;
            float cellHeight = gridLayoutGroup.cellSize.y;
            float spacingX = gridLayoutGroup.spacing.x;
            float spacingY = gridLayoutGroup.spacing.y;
            RectOffset padding = gridLayoutGroup.padding;

            float totalWidth = GridSize * cellWidth + (GridSize - 1) * spacingX + padding.left + padding.right;
            float totalHeight = GridSize * cellHeight + (GridSize - 1) * spacingY + padding.top + padding.bottom;

            // Create vertical lines
            for (var x = 0; x < GridSize - 1; x++)
            {
                GameObject verticalLine = Object.Instantiate(linePrefab, linesParent);
                RectTransform verticalLineRT = (RectTransform)verticalLine.transform;
                verticalLineRT.sizeDelta = new Vector2(lineWidth, totalHeight);
                verticalLineRT.anchoredPosition =
                    new Vector2(x * (cellWidth + spacingX) - cellWidth / lineWidth - spacingX / lineWidth, 0);
            }

            // Create horizontal lines
            for (var y = 0; y < GridSize - 1; y++)
            {
                GameObject horizontalLine = Object.Instantiate(linePrefab, linesParent);
                RectTransform horizontalLineRT = (RectTransform)horizontalLine.transform;
                horizontalLineRT.sizeDelta = new Vector2(totalWidth, lineWidth);
                horizontalLineRT.anchoredPosition =
                    new Vector2(0, -y * (cellHeight + spacingY) + cellHeight / lineWidth + spacingY / lineWidth);
            }
        }

        public SymbolType[,] CreateGrid()
        {
            return new SymbolType[_boardConfig.GridSize, _boardConfig.GridSize];
        }

        public int GridSize => _boardConfig.GridSize;
    }
}