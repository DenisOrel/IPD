// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.Extensions.VisualNodeExtensions
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.Document.Model.UI.Extensions;

public static class VisualNodeExtensions
{
  public static void UpdatePageElementChildPosition(
    this VisualNode parent,
    DocumentTreeNode node,
    int oldChildIndex,
    int newChildIndex)
  {
    PageElementUI pageElementUi = (PageElementUI) null;
    if (parent is Page page)
      pageElementUi = (PageElementUI) page.PageUI;
    else if (parent is TableElement tableElement)
      pageElementUi = tableElement.PageUI;
    pageElementUi?.PageElementUIs.Insert(newChildIndex, pageElementUi.PageElementUIs[oldChildIndex]);
  }
}
