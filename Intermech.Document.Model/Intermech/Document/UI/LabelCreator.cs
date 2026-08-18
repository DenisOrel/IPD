// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.LabelCreator
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

/// <summary>Вспомогательный класс, обеспечивает интерфейс пользователя при создании метки</summary>
public class LabelCreator : RectanglePageElementCreator
{
  private static Image image;

  /// <summary>Иконка для кнопки статическая версия</summary>
  public new static Image Icon
  {
    get
    {
      if (LabelCreator.image == null)
        LabelCreator.image = PageElementCreator.LoadImageFromResurcesStatic("Intermech.Document.Model.Resources.LabelElement.png");
      return LabelCreator.image;
    }
  }

  /// <summary>Иконка для кнопки</summary>
  public override Image Image
  {
    [DebuggerStepThrough] get
    {
      if (LabelCreator.image == null)
        LabelCreator.image = this.LoadImageFromResurces("Intermech.Document.Model.Resources.LabelElement.png");
      return LabelCreator.image;
    }
  }

  /// <summary>Имя элемента</summary>
  public override string Name
  {
    [DebuggerStepThrough] get => LabelElement.ElementTypeName;
  }

  /// <summary>Создать прямоугольный элемент</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <returns>Прямоугольный элемент</returns>
  public override DocumentTreeNode CreateRectangleElement(
    DocumentTreeNode parent,
    RectangleF bounds)
  {
    LabelElement element = new LabelElement(parent, bounds, true);
    element.InitCharFormat((CharFormat) null);
    if (ImDocumentEditorConfig.Instance.ShowGeometryDlgOnCreate && new RectangleDlg((RectangleElement) element).ShowDialog() != DialogResult.OK)
    {
      element.Remove(true, false);
      element = (LabelElement) null;
    }
    return (DocumentTreeNode) element;
  }
}
