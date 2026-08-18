
// Type: Intermech.Search.SimilarCharacterHighlighting.ChildrenViewSimilarCharacterHighlightingComponent
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.SimilarCharacterHighlighting;

public sealed class ChildrenViewSimilarCharacterHighlightingComponent : Component
{
  private const string LatinSimilarCyrillicCharacters = "аАВсСеЕНкКмМоОрРТиуУхХ";
  private const string CyrillicSimilarLatinCharacters = "aABcCeEHkKmMoOpPTuyYxX";
  private ChildrenView _childrenView;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public ChildrenViewSimilarCharacterHighlightingComponent() => this.InitializeComponent();

  public ChildrenViewSimilarCharacterHighlightingComponent(IContainer container)
  {
    container.Add((IComponent) this);
    this.InitializeComponent();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ChildrenView ChildrenView
  {
    get => this._childrenView;
    set
    {
      if (this._childrenView == value)
        return;
      if (this._childrenView != null)
        this._childrenView.CustomDrawCellText -= new EventHandler<ChildrenView.CustomDrawCellTextEventArgs>(this.ChildrenView_CustomDrawCellText);
      this._childrenView = value;
      if (this._childrenView == null)
        return;
      this._childrenView.CustomDrawCellText += new EventHandler<ChildrenView.CustomDrawCellTextEventArgs>(this.ChildrenView_CustomDrawCellText);
    }
  }

  private void ChildrenView_CustomDrawCellText(
    object sender,
    ChildrenView.CustomDrawCellTextEventArgs e)
  {
    if (!UISettings.HighlightCyrillicSimilarLatinCharacters && !UISettings.HighlightLatinSimilarCyrillicCharacters || UISettings.AllowableForHighlightingSimilarCharactersObjectTypes == null || UISettings.AllowableForHighlightingSimilarCharactersObjectTypes.Length == 0)
      return;
    NodeColumn nodeColumn = this._childrenView.GetNodeColumn(e.Cell.ColIndex);
    if (nodeColumn == null || nodeColumn.Attribute == null || nodeColumn.Attribute.AttributeID != -50 && nodeColumn.Attribute.AttributeID != Constants.NameAttributeTypeID && nodeColumn.Attribute.AttributeID != Constants.DesignationAttributeTypeID)
      return;
    INodeID nodeIdForRow = this._childrenView.GetNodeIDForRow(e.Cell.Row);
    if (nodeIdForRow == null || !(this._childrenView.Node.GetData(nodeIdForRow, typeof (IDBTypedObjectID)) is IDBTypedObjectID data))
      return;
    List<int> source = new List<int>();
    source.Add(data.ObjectType);
    source.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeParentsID(data.ObjectType));
    if (!source.Any<int>((Func<int, bool>) (objectType => ((IEnumerable<int>) UISettings.AllowableForHighlightingSimilarCharactersObjectTypes).Contains<int>(objectType))))
      return;
    List<Tuple<Color, string>> tupleList = new List<Tuple<Color, string>>();
    int startIndex = 0;
    int length = 0;
    Color color1 = e.ForeColor;
    for (int index = 0; index < e.Cell.Text.Length; ++index)
    {
      char ch = e.Cell.Text[index];
      Color color2 = e.ForeColor;
      if (UISettings.HighlightCyrillicSimilarLatinCharacters && "aABcCeEHkKmMoOpPTuyYxX".Contains<char>(ch))
        color2 = UISettings.CyrillicSimilarLatinCharacterHighlightColor;
      else if (UISettings.HighlightLatinSimilarCyrillicCharacters && "аАВсСеЕНкКмМоОрРТиуУхХ".Contains<char>(ch))
        color2 = UISettings.LatinSimilarCyrillicCharacterHighlightColor;
      if (color1 != color2)
      {
        if (length != 0)
          tupleList.Add(new Tuple<Color, string>(color1, e.Cell.Text.Substring(startIndex, length)));
        color1 = color2;
        startIndex = index;
        length = 1;
      }
      else
        ++length;
    }
    if (length != 0)
      tupleList.Add(new Tuple<Color, string>(color1, e.Cell.Text.Substring(startIndex, length)));
    int num = 0;
    foreach (Tuple<Color, string> tuple in tupleList)
    {
      using (SolidBrush solidBrush = new SolidBrush(tuple.Item1))
        e.Graphics.DrawString(tuple.Item2, e.Font, (Brush) solidBrush, (float) (e.TextBounds.X + num), (float) e.TextBounds.Y);
      Size size = TextRenderer.MeasureText((IDeviceContext) e.Graphics, tuple.Item2, e.Font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding);
      num += size.Width + 1;
    }
    e.HasDrawn = true;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
}
