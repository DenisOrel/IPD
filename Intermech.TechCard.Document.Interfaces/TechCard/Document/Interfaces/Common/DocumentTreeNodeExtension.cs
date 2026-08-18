// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Common.DocumentTreeNodeExtension
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Interfaces.Document;
using Intermech.TechCard.Document.Interfaces.Configs.Common;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Common;

public static class DocumentTreeNodeExtension
{
  public static bool IsInWorkspace(this DocumentTreeNode item)
  {
    return item.Parent is TableData parent && parent.TopLevelTable.IsPageFlow;
  }

  public static DocumentConfigElementType ToConfigElementType(this DocumentTreeNode item)
  {
    switch (item)
    {
      case ImDocumentData _:
        return DocumentConfigElementType.Document;
      case TableData tableData:
        RectangleElement rectangleElement;
        if (tableData.IsRow && (TableData) (rectangleElement = (RectangleElement) tableData) != null && (rectangleElement.Parent is TableData parent ? (parent.IsPageFlow ? 1 : 0) : 0) != 0)
          return DocumentConfigElementType.Variant;
        break;
      case TextData _:
        return DocumentConfigElementType.TextField;
      case ContainerData _:
        return DocumentConfigElementType.PictureField;
    }
    return DocumentConfigElementType.Unknown;
  }
}
