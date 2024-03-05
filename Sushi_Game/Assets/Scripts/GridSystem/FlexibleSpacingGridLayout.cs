using UnityEngine;
using UnityEngine.UI;

public class FlexibleSpacingGridLayout : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public FitType fitType;
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;
    public bool fitSpacingX;
    public bool fitSpacingY;
    
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
           float sqrRt = Mathf.Sqrt(transform.childCount);
           rows = Mathf.CeilToInt(sqrRt);
           columns = Mathf.CeilToInt(sqrRt); 
        }
        
        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }
        
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;
        
        float spacingWidth = (parentWidth / ((float)columns - 1)) - ((cellSize.x / ((float)columns - 1)) * columns) - (padding.left / ((float)columns - 1)) - (padding.right / ((float)columns - 1));
        float spacingHeight = (parentHeight / ((float)rows - 1)) - ((cellSize.y / ((float)rows - 1)) * rows) - (padding.top / ((float)rows - 1)) - (padding.bottom / ((float)rows - 1));
        
        spacing.x = fitSpacingX ? spacingWidth : spacing.x;
        spacing.y = fitSpacingY ? spacingHeight : spacing.y;
        
        int columnCount = 0;
        int rowCount = 0;
        
        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;
            
            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }
    
    public override void CalculateLayoutInputVertical()
    {
        
    }

    public override void SetLayoutHorizontal()
    {
        
    }

    public override void SetLayoutVertical()
    {
        
    }
}
