// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.Navigator.TechCardNavCellWidget
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Navigator.Controls;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls.Navigator;

/// <summary>
/// 
/// </summary>
/// <summary>Constructor</summary>
/// <param name="rowWidget"></param>
/// <param name="column"></param>
public class TechCardNavCellWidget(Infralution.Controls.VirtualTree.RowWidget rowWidget, Column column) : 
  NavigatorCellWidget(rowWidget, column)
{
  /// <summary>Отрисовать текст</summary>
  /// <param name="graphics">Контекст рисования</param>
  /// <param name="style">Стиль</param>
  /// <param name="printing">Идёт ли вывод на печать</param>
  protected override void PaintForeground(Graphics graphics, Style style, bool printing)
  {
    TechCardNavTreeViewControl tree = this.Tree as TechCardNavTreeViewControl;
    TechcardNavTreeNode techcardNavTreeNode = this.Row?.Item as TechcardNavTreeNode;
    bool flag = false;
    NavigatorTreeViewCheckBoxStyle checkBoxStyle1 = NavigatorTreeViewCheckBoxStyle.None;
    if (tree != null && techcardNavTreeNode != null)
    {
      if (!tree.CheckRootNode && techcardNavTreeNode.Equals((object) tree.RootNode))
      {
        checkBoxStyle1 = NavigatorTreeViewCheckBoxStyle.None;
        flag = true;
      }
      else if (techcardNavTreeNode.CheckBoxStyle < tree.CheckBoxStyle)
      {
        checkBoxStyle1 = techcardNavTreeNode.CheckBoxStyle;
        flag = true;
      }
    }
    if (flag)
    {
      NavigatorTreeViewCheckBoxStyle checkBoxStyle2 = tree.CheckBoxStyle;
      try
      {
        tree.SetCheckBoxesStyleInternal(checkBoxStyle1);
        base.PaintForeground(graphics, style, printing);
      }
      finally
      {
        tree.SetCheckBoxesStyleInternal(checkBoxStyle2);
      }
    }
    else
      base.PaintForeground(graphics, style, printing);
  }

  /// <summary>Отпущена клавиша мышки в ячейке</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnMouseUp(MouseEventArgs e)
  {
    TechCardNavTreeViewControl tree = this.Tree as TechCardNavTreeViewControl;
    TechcardNavTreeNode techcardNavTreeNode = this.Row?.Item as TechcardNavTreeNode;
    bool flag = false;
    NavigatorTreeViewCheckBoxStyle checkBoxStyle1 = NavigatorTreeViewCheckBoxStyle.None;
    if (tree != null && techcardNavTreeNode != null)
    {
      if (tree.RootNode != null && tree.RootNode.Equals((object) techcardNavTreeNode) && !tree.CheckRootNode)
      {
        checkBoxStyle1 = NavigatorTreeViewCheckBoxStyle.None;
        flag = true;
      }
      else if (techcardNavTreeNode.CheckBoxStyle < tree.CheckBoxStyle)
      {
        checkBoxStyle1 = techcardNavTreeNode.CheckBoxStyle;
        flag = true;
      }
    }
    if (flag)
    {
      NavigatorTreeViewCheckBoxStyle checkBoxStyle2 = tree.CheckBoxStyle;
      try
      {
        tree.SetCheckBoxesStyleInternal(checkBoxStyle1);
        base.OnMouseUp(e);
      }
      finally
      {
        tree.SetCheckBoxesStyleInternal(checkBoxStyle2);
      }
    }
    else
      base.OnMouseUp(e);
  }
}
