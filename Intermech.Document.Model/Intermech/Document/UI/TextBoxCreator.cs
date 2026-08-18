// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.TextBoxCreator
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model;
using Intermech.Interfaces.Document;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Summary description for TextBoxCreator.</summary>
public class TextBoxCreator : RectanglePageElementCreator
{
  private static Image image;

  /// <summary>Иконка для кнопки статическая версия</summary>
  public new static Image Icon
  {
    get
    {
      if (TextBoxCreator.image == null)
        TextBoxCreator.image = PageElementCreator.LoadImageFromResurcesStatic("Intermech.Document.Model.Resources.TextBoxElement.png");
      return TextBoxCreator.image;
    }
  }

  /// <summary>Иконка для кнопки</summary>
  public override Image Image
  {
    [DebuggerStepThrough] get
    {
      if (TextBoxCreator.image == null)
        TextBoxCreator.image = this.LoadImageFromResurces("Intermech.Document.Model.Resources.TextBoxElement.png");
      return TextBoxCreator.image;
    }
  }

  /// <summary>Имя элемента</summary>
  public override string Name
  {
    [DebuggerStepThrough] get => TextBoxElement.ElementTypeName;
  }

  /// <summary>Создать прямоугольный элемент</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <returns>Прямоугольный элемент</returns>
  public override DocumentTreeNode CreateRectangleElement(
    DocumentTreeNode parent,
    RectangleF bounds)
  {
    TextBoxElement element = new TextBoxElement(parent, bounds, true);
    element.InitCharFormat((CharFormat) null);
    if (ImDocumentEditorConfig.Instance.ShowGeometryDlgOnCreate && new RectangleDlg((RectangleElement) element).ShowDialog() != DialogResult.OK)
    {
      element.Remove(true, false);
      element = (TextBoxElement) null;
    }
    return (DocumentTreeNode) element;
  }
}
