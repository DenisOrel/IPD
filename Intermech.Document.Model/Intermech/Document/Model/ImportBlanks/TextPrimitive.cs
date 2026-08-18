// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.TextPrimitive
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>текст</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
public class TextPrimitive(GroupPrimitive owner) : RectPrimitive(owner)
{
  /// <summary>Текст</summary>
  public string text;
  /// <summary>Тип редактора</summary>
  public EditorType editorType;
  /// <summary>Горизонтальное выравнивание текста</summary>
  public HorAlignment hTextAlign;
  /// <summary>Вертикальное выравнивание текста</summary>
  public VertAlignment vTextAlign;
  /// <summary>ориентация текста</summary>
  public TextOrient orient;
  /// <summary>Высота текста for multi-line fields</summary>
  public ushort lineHeight;
  /// <summary>Линии между строками</summary>
  public bool stringDelimiters;
  /// <summary>Имя шрифта</summary>
  public string fontName;
  /// <summary>Высота шрифта</summary>
  public int fontHeight;
  /// <summary>Ширина шрифта</summary>
  public int fontWidth;
  /// <summary>Кодовая страница</summary>
  public int charSet;
  /// <summary>Флаги шрифта </summary>
  public FontFlags flags;
  /// <summary>Смещение для чётных страниц ?</summary>
  public int evenOffset;
  /// <summary>Смещение для нечётных страниц ?</summary>
  public int addOffset;
  /// <summary>Стиль рамки</summary>
  public FrameStyle frameStyle;

  /// <summary>Текст</summary>
  public string Text
  {
    [DebuggerStepThrough] get => this.text;
  }

  /// <summary>Тип редактора</summary>
  public EditorType EditorType
  {
    [DebuggerStepThrough] get => this.editorType;
  }

  /// <summary>Горизонтальное выравнивание текста</summary>
  public HorAlignment HTextAlign
  {
    [DebuggerStepThrough] get => this.hTextAlign;
  }

  /// <summary>Вертикальное выравнивание текста</summary>
  public VertAlignment VTextAlign
  {
    [DebuggerStepThrough] get => this.vTextAlign;
  }

  /// <summary>ориентация текста</summary>
  public TextOrient Orient
  {
    [DebuggerStepThrough] get => this.orient;
  }

  /// <summary>Высота текста for multi-line fields</summary>
  public ushort LineHeight
  {
    [DebuggerStepThrough] get => this.lineHeight;
  }

  /// <summary>Линии между строками</summary>
  public bool StringDelimiters
  {
    [DebuggerStepThrough] get => this.stringDelimiters;
  }

  /// <summary>Имя шрифта</summary>
  public string FontName
  {
    [DebuggerStepThrough] get => this.fontName;
  }

  /// <summary>Высота шрифта</summary>
  public int FontHeight
  {
    [DebuggerStepThrough] get => this.fontHeight;
  }

  /// <summary>Ширина шрифта</summary>
  public int FontWidth
  {
    [DebuggerStepThrough] get => this.fontWidth;
  }

  /// <summary>Кодовая страница</summary>
  public int CharSet
  {
    [DebuggerStepThrough] get => this.charSet;
  }

  /// <summary>Флаги шрифта </summary>
  public FontFlags Flags
  {
    [DebuggerStepThrough] get => this.flags;
  }

  /// <summary>Смещение для чётных страниц ?</summary>
  public int EvenOffset
  {
    [DebuggerStepThrough] get => this.evenOffset;
  }

  /// <summary>Смещение для нечётных страниц ?</summary>
  public int AddOffset
  {
    [DebuggerStepThrough] get => this.addOffset;
  }

  /// <summary>Стиль рамки</summary>
  public FrameStyle FrameStyle
  {
    [DebuggerStepThrough] get => this.frameStyle;
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик примитивов</param>
  public override void Load(PrimitiveLoader loader)
  {
    base.Load(loader);
    BinaryReader reader = loader.Reader;
    int num = reader.ReadInt32();
    this.text = "";
    for (int index = 0; index < num; ++index)
    {
      string str = loader.LoadingVersion < 278 ? loader.ReadString() : loader.ReadStringLong();
      this.text = this.text + (index == 0 ? "" : Environment.NewLine) + str;
    }
    this.editorType = (EditorType) reader.ReadByte();
    this.hTextAlign = (HorAlignment) reader.ReadByte();
    this.vTextAlign = (VertAlignment) reader.ReadByte();
    this.orient = (TextOrient) reader.ReadByte();
    this.lineHeight = reader.ReadUInt16();
    this.stringDelimiters = reader.ReadBoolean();
    this.fontName = loader.ReadString();
    this.fontHeight = reader.ReadInt32();
    this.fontWidth = reader.ReadInt32();
    this.flags = (FontFlags) reader.ReadByte();
    this.charSet = loader.LoadingVersion < 230 || loader.CurrentPrimitiveIsLoaded ? (int) new Font(this.FontName, 8f).GdiCharSet : reader.ReadInt32();
    this.evenOffset = loader.LoadingVersion < 240 /*0xF0*/ || loader.CurrentPrimitiveIsLoaded ? 0 : reader.ReadInt32();
    this.frameStyle = loader.LoadingVersion < 262 || loader.CurrentPrimitiveIsLoaded ? FrameStyle.fsFull : (FrameStyle) reader.ReadByte();
    if (loader.LoadingVersion >= 266 && !loader.CurrentPrimitiveIsLoaded)
      this.needFrame = reader.ReadBoolean();
    else
      this.needFrame = false;
    if (loader.LoadingVersion >= 274 && !loader.CurrentPrimitiveIsLoaded)
      this.addOffset = reader.ReadInt32();
    else
      this.addOffset = 0;
  }

  private HorzAlignment HorAligmentToImDocAlignment(HorAlignment align)
  {
    switch (align)
    {
      case HorAlignment.haLeft:
        return HorzAlignment.Left;
      case HorAlignment.haCenter:
        return HorzAlignment.Center;
      case HorAlignment.haRight:
        return HorzAlignment.Right;
      default:
        return HorzAlignment.Left;
    }
  }

  private Intermech.Interfaces.Document.VertAlignment VertAligmentToImDocAlignment(
    VertAlignment align)
  {
    switch (align)
    {
      case VertAlignment.vaTop:
        return Intermech.Interfaces.Document.VertAlignment.Top;
      case VertAlignment.vaCenter:
        return Intermech.Interfaces.Document.VertAlignment.Center;
      case VertAlignment.vaBottom:
        return Intermech.Interfaces.Document.VertAlignment.Bottom;
      default:
        return Intermech.Interfaces.Document.VertAlignment.Center;
    }
  }

  /// <summary>Создать новый узел документа</summary>
  /// <returns>Узел документа</returns>
  public override DocumentTreeNode CreateNewDocumentNode(DocumentTreeNode parentDocNode)
  {
    DocumentTreeNode newDocumentNode = this.orient != TextOrient.toNormal ? (DocumentTreeNode) new LabelElement() : (DocumentTreeNode) new TextBoxElement();
    this.SetNodeId(newDocumentNode);
    parentDocNode?.AddChildNode(newDocumentNode, false, false);
    this.InitNewDocumentNode(newDocumentNode);
    return newDocumentNode;
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    if (!(node is TextData ownerNode))
      return;
    ownerNode.AssignReadOnly(this is AutoText);
    ownerNode.AssignTransparent(true, false);
    ownerNode.AssignText(this.Text, false, true, false, false, false);
    ParagraphFormat paragraphFormat1 = ownerNode.ParagraphFormat;
    ParagraphFormat paragraphFormat2 = paragraphFormat1 == null ? new ParagraphFormat() : paragraphFormat1.Clone();
    paragraphFormat2.HorzAlignment = new HorzAlignment?(this.HorAligmentToImDocAlignment(this.hTextAlign));
    paragraphFormat2.VertAlignment = new Intermech.Interfaces.Document.VertAlignment?(this.VertAligmentToImDocAlignment(this.vTextAlign));
    ownerNode.SetParagraphFormat(paragraphFormat2, false, false, true);
    CharStyle charStyle = CharStyle.Regular;
    if ((this.Flags & FontFlags.fBold) != FontFlags.fNone)
      charStyle |= CharStyle.Bold;
    if ((this.Flags & FontFlags.fItalic) != FontFlags.fNone)
      charStyle |= CharStyle.Italic;
    if ((this.Flags & FontFlags.fUnderline) != FontFlags.fNone)
      charStyle |= CharStyle.Underline;
    if ((this.Flags & FontFlags.fSuperscript) != FontFlags.fNone)
      charStyle |= CharStyle.Superscript;
    if ((this.Flags & FontFlags.fSearch) != FontFlags.fNone)
      ownerNode.SetAttributeValue("BLN.Flags", "fSearch", false, false, false);
    if (this.FontWidth != 0)
      ownerNode.SetAttributeValue("BLN.FontWidth", PrimitiveBase.BlankUnitToMm(this.FontWidth).ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    float fontSize = (float) UnitsConverter.MmToPoints(PrimitiveBase.BlankUnitToMm(this.FontHeight));
    if ((double) fontSize == 0.0)
      fontSize = 8f;
    if ((double) fontSize < 0.0)
      fontSize = -fontSize;
    CharFormat charFormat1 = ownerNode.CharFormat;
    CharFormat charFormat2;
    if (charFormat1 != null)
    {
      charFormat2 = charFormat1.Clone();
      charFormat2.FontFamily = this.FontName;
      charFormat2.FontSize = new float?(fontSize);
      charFormat2.CharStyle = charStyle;
    }
    else
      charFormat2 = new CharFormat(this.FontName, fontSize, charStyle);
    charFormat2.GdiCharSet = (byte) this.CharSet;
    ownerNode.SetCharFormat(charFormat2, false, false);
    if (ownerNode is TextBoxElement textBoxElement && textBoxElement.IsFormulaLib && this.editorType == EditorType.etSingleLine && this.AutoFillTextBox && this.Owner is Area)
    {
      textBoxElement.AssignAutoSizeWidth(true, false, false, true);
      textBoxElement.AssignMinWidth(textBoxElement.Bounds.Width, false, false, true);
    }
    if (this.LineHeight != (ushort) 0 && this.editorType == EditorType.etMultiLine)
    {
      if (this.StringDelimiters)
      {
        ownerNode.SetDefaultRowSize(PrimitiveBase.BlankUnitToMm((int) this.lineHeight), false, true, false, false);
      }
      else
      {
        paragraphFormat2.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.ExactMM);
        paragraphFormat2.AssignSpaceBetweenLines(new float?(PrimitiveBase.BlankUnitToMm((int) this.lineHeight)));
      }
    }
    if (this.id == "#")
    {
      ownerNode.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) ownerNode, BaseReferenceNodeType.ntParentDocument, "", DocumentTreeNode.AttributeName_DocPageCount), true, false, false);
      if (ownerNode.Name == null || ownerNode.Name == "")
        ownerNode.Name = this.id;
    }
    else if (this.id == "№")
    {
      ownerNode.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) ownerNode, BaseReferenceNodeType.ntParentPage, "", DocumentTreeNode.AttributeName_DocPageNumber), true, false, false);
      if (ownerNode.Name == null || ownerNode.Name == "")
        ownerNode.Name = this.id;
    }
    else if (this.id == "##")
    {
      ownerNode.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) ownerNode, BaseReferenceNodeType.ntParentDocument, "", DocumentTreeNode.AttributeName_ComplectPageCount), true, false, false);
      if (ownerNode.Name == null || ownerNode.Name == "")
        ownerNode.Name = this.id;
    }
    else if (this.id == "№№")
    {
      ownerNode.AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) ownerNode, BaseReferenceNodeType.ntParentPage, "", DocumentTreeNode.AttributeName_ComplectPageNumber), true, false, false);
      if (ownerNode.Name == null || ownerNode.Name == "")
        ownerNode.Name = this.id;
    }
    if (this.EvenOffset != 0)
      ownerNode.SetAttributeValue("BLN.EvenOffset", PrimitiveBase.BlankUnitToMm(this.EvenOffset).ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    if (this.AddOffset != 0)
      ownerNode.SetAttributeValue("BLN.AddOffset", PrimitiveBase.BlankUnitToMm(this.AddOffset).ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
    if (this.NeedFrame)
    {
      switch (this.FrameStyle)
      {
        case FrameStyle.fsColumns:
          ownerNode.Borders = (RectangleBorder) new CustomBorder(new BorderLine(BorderStyles.None, 0.2f), new BorderLine(BorderStyles.None, 0.2f), (BorderLine) null, new BorderLine((this.Flags & FontFlags.fBold) > FontFlags.fNone ? 0.5f : 0.2f), new BorderLine((this.Flags & FontFlags.fBold) > FontFlags.fNone ? 0.5f : 0.2f));
          break;
        case FrameStyle.fsSerifs:
          ownerNode.Borders = (RectangleBorder) new CustomBorder(new BorderLine(BorderStyles.None, 0.2f), new BorderLine(BorderStyles.None, 0.2f), (BorderLine) null, new BorderLine(BorderStyles.Serif, (this.Flags & FontFlags.fBold) > FontFlags.fNone ? 0.5f : 0.2f), new BorderLine(BorderStyles.Serif, (this.Flags & FontFlags.fBold) > FontFlags.fNone ? 0.5f : 0.2f));
          break;
      }
    }
    if (this.orient != TextOrient.toNormal)
    {
      switch (this.orient)
      {
        case TextOrient.toVert90:
          ownerNode.Orientation = TextOrientation.DownTop;
          break;
        case TextOrient.toReversed:
          ownerNode.Orientation = TextOrientation.UpsideDown;
          break;
        case TextOrient.toVert270:
          ownerNode.Orientation = TextOrientation.TopDown;
          break;
      }
    }
    if (!ownerNode.IsFormulaLib || ownerNode.ReadOnly || !string.IsNullOrEmpty(ownerNode.Name) || this is AutoText)
      return;
    ownerNode.Name = this.id;
  }
}
