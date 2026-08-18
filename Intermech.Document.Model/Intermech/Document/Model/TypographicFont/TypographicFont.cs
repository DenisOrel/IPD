// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.TypographicFont.TypographicFont
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Document.Model.TypographicFont;

[DebuggerDisplay("{ToString(),nq}")]
public sealed class TypographicFont
{
  /// <summary>
  /// Имя типографского семейства шрифтов. Выводит "Arial" для "Arial Black" и "Arial Narrow", что позволяет им группироваться с другими шрифтами "Arial", несмотря на разницу в именах.
  /// Стандартный диалог выбора шрифта группирует шрифты по типографическому семейству.
  /// </summary>
  public string Family { get; private set; }

  /// <summary>
  /// Имя типографского подсемейства шрифтов. Выводит "Black" и "Narrow" для "Arial Black" и "Arial Narrow," вместе с "Regular", "Bold" и т.д.
  /// </summary>
  public string SubFamily { get; private set; }

  /// <summary>
  /// Формальное имя шрифта. Together with the style indicators (e.g. Bold and Italic), this is what uniquely identifies the font to Windows.
  /// Используйте значение этого свойства для создания экземпляра System.Drawing.Font.
  /// </summary>
  public string Name { get; private set; }

  /// <summary>Возвращает системный вес шрифта.</summary>
  public TypographicFontWeight Weight { get; private set; }

  /// <summary>
  /// Истина, если этот шрифт изначально поддерживает жирное начертание.
  /// </summary>
  public bool Bold { get; private set; }

  /// <summary>
  /// Истина, если этот шрифт изначально поддерживает курсив.
  /// </summary>
  public bool Italic { get; private set; }

  /// <summary>
  /// Истина, если этот шрифт изначально поддерживает стиль oblique.
  /// </summary>
  public bool Oblique { get; private set; }

  /// <summary>
  /// Истина, если этот шрифт изначально поддерживает стил underlined.
  /// </summary>
  public bool Underlined { get; private set; }

  /// <summary>
  /// Истина, если этот шрифт изначально поддерживает стил negative.
  /// </summary>
  public bool Negative { get; private set; }

  /// <summary>
  /// Истина, если этот шрифт изначально поддерживает стиль outline.
  /// </summary>
  public bool Outlined { get; private set; }

  /// <summary>
  /// Истина, если этот шрифт изначально поддерживает стиль overstruck.
  /// </summary>
  public bool Strikeout { get; private set; }

  /// <summary>
  /// Символы для этого шрифта имеют стандартное начертание/стиль.
  /// </summary>
  public bool Regular { get; private set; }

  /// <summary>Возвращает путь к файлу-контейнеру шрифта.</summary>
  public string FileName { get; private set; }

  private TypographicFont(
    string family,
    string subFamily,
    string name,
    TypographicFontWeight weight,
    bool bold,
    bool italic,
    bool oblique,
    bool underlined,
    bool negative,
    bool outlined,
    bool strikeout,
    bool regular,
    string fileName)
  {
    this.Family = family;
    this.SubFamily = subFamily;
    this.Name = name;
    this.Weight = weight;
    this.Bold = bold;
    this.Italic = italic;
    this.Underlined = underlined;
    this.Negative = negative;
    this.Outlined = outlined;
    this.Strikeout = strikeout;
    this.Regular = regular;
    this.FileName = fileName;
    this.Oblique = oblique;
  }

  public override string ToString()
  {
    return this.SubFamily != null ? $"{this.Family} {this.SubFamily}" : this.Family;
  }

  /// <summary>
  /// Возвращает кэшированный список всех OpenType шрифтов, установленных в системе, включая TTF, OTF и TTC форматы.
  /// </summary>
  public static IReadOnlyList<string> GetInstalledFontFiles()
  {
    List<string> installedFontFiles = new List<string>();
    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
    RegistryKey registryKey1 = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Fonts");
    RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Fonts");
    List<string> stringList = new List<string>();
    try
    {
      if (registryKey1 != null)
        stringList.AddRange((IEnumerable<string>) registryKey1.GetValueNames());
      if (registryKey2 != null)
        stringList.AddRange((IEnumerable<string>) registryKey2.GetValueNames());
      foreach (string name in stringList)
      {
        if (!(registryKey1?.GetValue(name) is string str1))
          str1 = registryKey2?.GetValue(name) as string;
        string str2 = str1;
        if (!string.IsNullOrWhiteSpace(str2))
        {
          if (!Path.IsPathRooted(str2))
            str2 = Path.Combine(folderPath, str2);
          installedFontFiles.Add(str2);
        }
      }
    }
    finally
    {
      registryKey1?.Dispose();
      registryKey2?.Dispose();
    }
    return (IReadOnlyList<string>) installedFontFiles;
  }

  /// <summary>
  /// Разбирает файл-контейнер OpenType шрифта. Поддерживаются форматы TTF, OTF и TTC.
  /// </summary>
  public static Intermech.Document.Model.TypographicFont.TypographicFont[] FromFile(string filename)
  {
    try
    {
      return Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.Read(filename);
    }
    catch
    {
      return new Intermech.Document.Model.TypographicFont.TypographicFont[0];
    }
  }

  private static class FontReader
  {
    public static Intermech.Document.Model.TypographicFont.TypographicFont[] Read(string filename)
    {
      using (FileStream input = new FileStream(filename, FileMode.Open, FileAccess.Read))
      {
        using (Intermech.Document.Model.TypographicFont.TypographicFont.BigEndianBinaryReader br = new Intermech.Document.Model.TypographicFont.TypographicFont.BigEndianBinaryReader((Stream) input))
        {
          if (Encoding.ASCII.GetString(br.ReadBytes(4)) == "ttcf")
          {
            int num = (int) br.ReadUInt32();
            uint[] numArray = new uint[(int) br.ReadUInt32()];
            for (int index = 0; index < numArray.Length; ++index)
              numArray[index] = br.ReadUInt32();
            List<Intermech.Document.Model.TypographicFont.TypographicFont> typographicFontList = new List<Intermech.Document.Model.TypographicFont.TypographicFont>();
            for (int index = 0; index < numArray.Length; ++index)
            {
              Intermech.Document.Model.TypographicFont.TypographicFont openTypeStream = Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.ParseOpenTypeStream(br, (long) numArray[index], filename);
              if (openTypeStream != null)
                typographicFontList.Add(openTypeStream);
            }
            return typographicFontList.ToArray();
          }
          Intermech.Document.Model.TypographicFont.TypographicFont openTypeStream1 = Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.ParseOpenTypeStream(br, 0L, filename);
          Intermech.Document.Model.TypographicFont.TypographicFont[] typographicFontArray;
          if (openTypeStream1 != null)
            typographicFontArray = new Intermech.Document.Model.TypographicFont.TypographicFont[1]
            {
              openTypeStream1
            };
          else
            typographicFontArray = new Intermech.Document.Model.TypographicFont.TypographicFont[0];
          return typographicFontArray;
        }
      }
    }

    private static Intermech.Document.Model.TypographicFont.TypographicFont ParseOpenTypeStream(
      Intermech.Document.Model.TypographicFont.TypographicFont.BigEndianBinaryReader br,
      long streamOffset,
      string filename)
    {
      br.BaseStream.Seek(streamOffset, SeekOrigin.Begin);
      switch (br.ReadUInt32())
      {
        case 65536 /*0x010000*/:
        case 1330926671:
          ushort num1 = br.ReadUInt16();
          int num2 = (int) br.ReadUInt16();
          int num3 = (int) br.ReadUInt16();
          int num4 = (int) br.ReadUInt16();
          Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.FamilyNamesInfo? nullable1 = new Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.FamilyNamesInfo?();
          Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.OS2Info? nullable2 = new Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.OS2Info?();
          for (int index = 0; index < (int) num1; ++index)
          {
            string str = Encoding.ASCII.GetString(br.ReadBytes(4));
            int num5 = (int) br.ReadUInt32();
            uint offset = br.ReadUInt32();
            int num6 = (int) br.ReadUInt32();
            long position = br.BaseStream.Position;
            switch (str)
            {
              case "name":
                nullable1 = new Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.FamilyNamesInfo?(Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.ReadFamilyNames(br, offset));
                break;
              case "OS/2":
                nullable2 = new Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.OS2Info?(Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.ReadOS2(br, offset));
                break;
            }
            if (!nullable1.HasValue || !nullable2.HasValue)
              br.BaseStream.Seek(position, SeekOrigin.Begin);
            else
              break;
          }
          return !nullable1.HasValue || !nullable2.HasValue ? (Intermech.Document.Model.TypographicFont.TypographicFont) null : new Intermech.Document.Model.TypographicFont.TypographicFont(nullable1.Value.TypographicFamily, nullable1.Value.TypographicSubfamily == string.Empty ? (string) null : nullable1.Value.TypographicSubfamily, nullable1.Value.FontName, nullable2.Value.Weight, (nullable2.Value.Style & TypographicFontStyle.Bold) != 0, (nullable2.Value.Style & TypographicFontStyle.Italic) != 0, (nullable2.Value.Style & TypographicFontStyle.Oblique) != 0, (nullable2.Value.Style & TypographicFontStyle.Underscore) != 0, (nullable2.Value.Style & TypographicFontStyle.Negative) != 0, (nullable2.Value.Style & TypographicFontStyle.Outlined) != 0, (nullable2.Value.Style & TypographicFontStyle.Strikeout) != 0, (nullable2.Value.Style & TypographicFontStyle.Regular) != 0, filename);
        default:
          return (Intermech.Document.Model.TypographicFont.TypographicFont) null;
      }
    }

    private static Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.FamilyNamesInfo ReadFamilyNames(
      Intermech.Document.Model.TypographicFont.TypographicFont.BigEndianBinaryReader br,
      uint offset)
    {
      br.BaseStream.Seek((long) offset, SeekOrigin.Begin);
      int num1 = (int) br.ReadUInt16();
      ushort num2 = br.ReadUInt16();
      ushort num3 = br.ReadUInt16();
      string str1 = (string) null;
      string typographicSubfamily1 = (string) null;
      string typographicFamily = (string) null;
      string typographicSubfamily2 = (string) null;
      for (int index = 0; index < (int) num2; ++index)
      {
        PlatformId platformId = (PlatformId) br.ReadUInt16();
        int num4 = (int) br.ReadUInt16();
        ushort num5 = br.ReadUInt16();
        NameId nameId = (NameId) br.ReadUInt16();
        ushort numBytes = br.ReadUInt16();
        ushort num6 = br.ReadUInt16();
        switch (platformId)
        {
          case PlatformId.Unicode:
            switch (nameId)
            {
              case NameId.FontFamilyName:
              case NameId.FontSubfamilyName:
              case NameId.TypographicFamilyName:
              case NameId.TypographicSubfamilyName:
                long position = br.BaseStream.Position;
                br.BaseStream.Seek((long) (offset + (uint) num3 + (uint) num6), SeekOrigin.Begin);
                string str2 = Encoding.BigEndianUnicode.GetString(br.ReadBytes((int) numBytes));
                br.BaseStream.Seek(position, SeekOrigin.Begin);
                switch (nameId)
                {
                  case NameId.FontFamilyName:
                    str1 = str2;
                    continue;
                  case NameId.FontSubfamilyName:
                    typographicSubfamily1 = str2;
                    continue;
                  case NameId.TypographicFamilyName:
                    typographicFamily = str2;
                    continue;
                  case NameId.TypographicSubfamilyName:
                    typographicSubfamily2 = str2;
                    continue;
                  default:
                    continue;
                }
              default:
                continue;
            }
          case PlatformId.Windows:
            if (num5 != (ushort) 1033)
              break;
            goto case PlatformId.Unicode;
        }
      }
      return typographicFamily != null ? new Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.FamilyNamesInfo(typographicFamily, typographicSubfamily2, str1) : new Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.FamilyNamesInfo(str1, typographicSubfamily1, str1);
    }

    private static Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.OS2Info ReadOS2(
      Intermech.Document.Model.TypographicFont.TypographicFont.BigEndianBinaryReader br,
      uint offset)
    {
      br.BaseStream.Seek((long) (offset + 4U), SeekOrigin.Begin);
      int weight = (int) br.ReadUInt16();
      br.BaseStream.Seek(56L, SeekOrigin.Current);
      int style = (int) br.ReadUInt16();
      return new Intermech.Document.Model.TypographicFont.TypographicFont.FontReader.OS2Info((TypographicFontWeight) weight, (TypographicFontStyle) style);
    }

    private struct FamilyNamesInfo(
      string typographicFamily,
      string typographicSubfamily,
      string fontName)
    {
      public readonly string TypographicFamily = typographicFamily;
      public readonly string TypographicSubfamily = typographicSubfamily;
      public readonly string FontName = fontName;
    }

    private struct OS2Info(TypographicFontWeight weight, TypographicFontStyle style)
    {
      public readonly TypographicFontWeight Weight = weight;
      public readonly TypographicFontStyle Style = style;
    }
  }

  private sealed class BigEndianBinaryReader : IDisposable
  {
    private readonly Stream input;
    private readonly bool leaveOpen;

    public Stream BaseStream => this.input;

    public BigEndianBinaryReader(Stream input, bool leaveOpen = false)
    {
      this.input = input != null ? input : throw new ArgumentNullException(nameof (input));
      this.leaveOpen = leaveOpen;
    }

    public ushort ReadUInt16() => (ushort) (this.input.ReadByte() << 8 | this.input.ReadByte());

    public uint ReadUInt32()
    {
      return (uint) (this.input.ReadByte() << 24 | this.input.ReadByte() << 16 /*0x10*/ | this.input.ReadByte() << 8 | this.input.ReadByte());
    }

    public byte[] ReadBytes(int numBytes)
    {
      byte[] buffer = new byte[numBytes];
      if (this.input.Read(buffer, 0, numBytes) != numBytes)
        throw new EndOfStreamException();
      return buffer;
    }

    public void Dispose()
    {
      if (this.leaveOpen)
        return;
      this.BaseStream.Dispose();
    }
  }
}
