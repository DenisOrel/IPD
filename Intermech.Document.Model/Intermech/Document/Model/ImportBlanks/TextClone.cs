// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.TextClone
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Клон текстового примитива</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
/// <param name="origin">Примитив</param>
public class TextClone(GroupClone owner, RectPrimitive origin) : CloneBase(owner, origin)
{
  /// <summary>коллекция строк</summary>
  public StringCollection text = new StringCollection();
  /// <summary>Имя шрифта</summary>
  public string FontName;
  /// <summary>Высота шрифта</summary>
  public int? fontHeight;
  /// <summary>Ширина шрифта</summary>
  public int? fontWidth;
  /// <summary>Кодовая страница</summary>
  public int? charSet;
  /// <summary>Флаги шрифта </summary>
  public FontFlags? flags;
  /// <summary>цвет текста</summary>
  public Color textColor = Color.Black;
  /// <summary>Цвет означающий, что цвет не назначен</summary>
  public static Color NoColor = Color.FromArgb(86, 52, 18);
  /// <summary>storage for RTF info</summary>
  public MemoryStream rtfStream = new MemoryStream();
  /// <summary>Метафайл - буфер изображения</summary>
  public byte[] metafileBuffer;

  /// <summary>текст</summary>
  public string Text
  {
    [DebuggerStepThrough] get
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.text.Count; ++index)
        stringBuilder.Append(this.text[index]);
      return stringBuilder.ToString();
    }
  }

  /// <summary>Высота шрифта</summary>
  public int? FontHeight
  {
    [DebuggerStepThrough] get => this.fontHeight;
  }

  /// <summary>Ширина шрифта</summary>
  public int? FontWidth
  {
    [DebuggerStepThrough] get => this.fontWidth;
  }

  /// <summary>Кодовая страница</summary>
  public int? CharSet
  {
    [DebuggerStepThrough] get => this.charSet;
  }

  /// <summary>Флаги шрифта </summary>
  public FontFlags? Flags
  {
    [DebuggerStepThrough] get => this.flags;
  }

  /// <summary>цвет текста</summary>
  public Color TextColor => this.textColor;

  /// <summary>storage for RTF info</summary>
  public MemoryStream RtfStream => this.rtfStream;

  /// <summary>Метафайл - буфер изображения</summary>
  public byte[] MetafileBuffer => this.metafileBuffer;

  /// <summary>Загрузить</summary>
  /// <param name="ueDoc">Документ</param>
  public override void Load(UEditDocument ueDoc)
  {
    BinaryReader reader = ueDoc.Reader;
    base.Load(ueDoc);
    PrimitiveLoader.LoadStringList(this.text, reader, ueDoc.LoadingVersion);
    try
    {
      if (ueDoc.LoadingVersion >= 304)
      {
        if (!ueDoc.CurrentCloneIsLoaded)
        {
          this.textColor = PrimitiveLoader.ReadDelphiColor(reader);
          if (this.textColor == TextClone.NoColor)
            this.textColor = Color.Black;
        }
      }
    }
    catch
    {
      this.textColor = Color.Black;
    }
    if (ueDoc.CurrentCloneIsLoaded)
      return;
    long position = reader.BaseStream.Position;
    try
    {
      if (reader.ReadInt32() != UEditDocument.MagicSign)
      {
        reader.BaseStream.Position = position;
      }
      else
      {
        int num = reader.ReadInt32();
        byte[] buffer = new byte[4096 /*0x1000*/];
        int count;
        for (; num > 0; num -= count)
        {
          count = num <= 4096 /*0x1000*/ ? num : 4096 /*0x1000*/;
          reader.Read(buffer, 0, count);
          this.rtfStream.Write(buffer, 0, count);
        }
      }
    }
    catch
    {
      reader.BaseStream.Position = position;
    }
    if (ueDoc.CurrentCloneIsLoaded)
      return;
    if (ueDoc.LoadingVersion >= 312)
    {
      int count = reader.ReadInt32();
      if (count != 0)
      {
        this.metafileBuffer = new byte[count + 1];
        reader.Read(this.metafileBuffer, 0, count);
      }
    }
    if (ueDoc.CurrentCloneIsLoaded || ueDoc.LoadingVersion < 318)
      return;
    this.FontName = ueDoc.ReadString();
    this.fontHeight = new int?(reader.ReadInt32());
    this.fontWidth = new int?(reader.ReadInt32());
    this.flags = new FontFlags?((FontFlags) reader.ReadByte());
    this.charSet = new int?(reader.ReadInt32());
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    TextData textData = node as TextData;
    string text = this.Text;
    if (textData != null)
    {
      if (text.Length > 0)
        textData.AssignText(text, false, true, false, false, false);
      textData.AssignForeColor(this.TextColor, false);
      if (this.fontHeight.HasValue || this.flags.HasValue || this.charSet.HasValue || !string.IsNullOrEmpty(this.FontName))
      {
        CharFormat charFormat = textData.CharFormat?.Clone() ?? new CharFormat();
        if (!string.IsNullOrEmpty(this.FontName))
          charFormat.FontFamily = this.FontName;
        if (this.fontHeight.HasValue)
        {
          float num = (float) UnitsConverter.MmToPoints(PrimitiveBase.BlankUnitToMm(this.fontHeight.Value));
          if ((double) num == 0.0)
            num = 8f;
          if ((double) num < 0.0)
            num = -num;
          charFormat.FontSize = new float?(num);
        }
        if (this.flags.HasValue)
        {
          CharStyle charStyle = CharStyle.Regular;
          FontFlags? flags = this.flags;
          FontFlags? nullable1 = flags.HasValue ? new FontFlags?(flags.GetValueOrDefault() & FontFlags.fBold) : new FontFlags?();
          FontFlags fontFlags1 = FontFlags.fNone;
          if (!(nullable1.GetValueOrDefault() == fontFlags1 & nullable1.HasValue))
            charStyle |= CharStyle.Bold;
          flags = this.flags;
          FontFlags? nullable2 = flags.HasValue ? new FontFlags?(flags.GetValueOrDefault() & FontFlags.fItalic) : new FontFlags?();
          FontFlags fontFlags2 = FontFlags.fNone;
          if (!(nullable2.GetValueOrDefault() == fontFlags2 & nullable2.HasValue))
            charStyle |= CharStyle.Italic;
          flags = this.flags;
          FontFlags? nullable3 = flags.HasValue ? new FontFlags?(flags.GetValueOrDefault() & FontFlags.fUnderline) : new FontFlags?();
          FontFlags fontFlags3 = FontFlags.fNone;
          if (!(nullable3.GetValueOrDefault() == fontFlags3 & nullable3.HasValue))
            charStyle |= CharStyle.Underline;
          flags = this.flags;
          FontFlags? nullable4 = flags.HasValue ? new FontFlags?(flags.GetValueOrDefault() & FontFlags.fSuperscript) : new FontFlags?();
          FontFlags fontFlags4 = FontFlags.fNone;
          if (!(nullable4.GetValueOrDefault() == fontFlags4 & nullable4.HasValue))
            charStyle |= CharStyle.Superscript;
          flags = this.flags;
          FontFlags? nullable5 = flags.HasValue ? new FontFlags?(flags.GetValueOrDefault() & FontFlags.fSearch) : new FontFlags?();
          FontFlags fontFlags5 = FontFlags.fNone;
          if (!(nullable5.GetValueOrDefault() == fontFlags5 & nullable5.HasValue))
            textData.SetAttributeValue("BLN.Flags", "fSearch", false, false, false);
          charFormat.CharStyle = charStyle;
        }
        if (this.charSet.HasValue)
          charFormat.GdiCharSet = (byte) this.charSet.Value;
        textData.SetCharFormat(charFormat, false, false);
      }
      if (this.fontWidth.HasValue)
      {
        int? fontWidth = this.fontWidth;
        int num = 0;
        if (!(fontWidth.GetValueOrDefault() == num & fontWidth.HasValue))
          textData.SetAttributeValue("BLN.FontWidth", PrimitiveBase.BlankUnitToMm(this.fontWidth.Value).ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
      }
    }
    if (!(textData is TextBoxElement textBoxElement) || !this.RtfStream.CanRead)
      return;
    int length = (int) this.RtfStream.Length;
    byte[] buffer = new byte[length];
    this.rtfStream.Position = 0L;
    this.RtfStream.Read(buffer, 0, length);
    char[] chArray = new char[length];
    for (int index = 0; index < length; ++index)
      chArray[index] = (char) buffer[index];
    string rtfText = new string(chArray);
    if (text.Length <= 0 || rtfText.Length <= 0)
      return;
    textBoxElement.AssignText(text, rtfText, false, false, false);
  }
}
