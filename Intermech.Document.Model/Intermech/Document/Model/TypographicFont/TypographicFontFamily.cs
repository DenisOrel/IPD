// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.TypographicFont.TypographicFontFamily
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.TypographicFont;

[DebuggerDisplay("{ToString(),nq}")]
public sealed class TypographicFontFamily
{
  private static List<TypographicFontFamily> familyList;

  public static List<TypographicFontFamily> InstalledFamiliesList
  {
    get
    {
      return TypographicFontFamily.familyList ?? (TypographicFontFamily.familyList = TypographicFontFamily.CreateFamilyList());
    }
  }

  private static List<TypographicFontFamily> CreateFamilyList()
  {
    Dictionary<string, List<Intermech.Document.Model.TypographicFont.TypographicFont>> dictionary = new Dictionary<string, List<Intermech.Document.Model.TypographicFont.TypographicFont>>();
    foreach (string installedFontFile in (IEnumerable<string>) Intermech.Document.Model.TypographicFont.TypographicFont.GetInstalledFontFiles())
    {
      if (File.Exists(installedFontFile))
      {
        foreach (Intermech.Document.Model.TypographicFont.TypographicFont typographicFont in Intermech.Document.Model.TypographicFont.TypographicFont.FromFile(installedFontFile))
        {
          List<Intermech.Document.Model.TypographicFont.TypographicFont> typographicFontList;
          if (!dictionary.TryGetValue(typographicFont.Family, out typographicFontList))
            dictionary.Add(typographicFont.Family, typographicFontList = new List<Intermech.Document.Model.TypographicFont.TypographicFont>());
          typographicFontList.Add(typographicFont);
        }
      }
    }
    List<TypographicFontFamily> familyList = new List<TypographicFontFamily>();
    foreach (KeyValuePair<string, List<Intermech.Document.Model.TypographicFont.TypographicFont>> keyValuePair in dictionary)
    {
      keyValuePair.Value.Sort((Comparison<Intermech.Document.Model.TypographicFont.TypographicFont>) ((a, b) => string.Compare(a.SubFamily, b.SubFamily, StringComparison.OrdinalIgnoreCase)));
      familyList.Add(new TypographicFontFamily(keyValuePair.Key, (IReadOnlyList<Intermech.Document.Model.TypographicFont.TypographicFont>) keyValuePair.Value));
    }
    familyList.Sort((Comparison<TypographicFontFamily>) ((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase)));
    return familyList;
  }

  public TypographicFontFamily(string name, IReadOnlyList<Intermech.Document.Model.TypographicFont.TypographicFont> fonts)
  {
    this.Name = name;
    this.Fonts = fonts;
  }

  /// <summary>Имя типографического семейства шрифтов.</summary>
  public string Name { get; private set; }

  /// <summary>
  /// Коллекция установленных шрифтов, принадлежащих данному типографическому семейству.
  /// </summary>
  public IReadOnlyList<Intermech.Document.Model.TypographicFont.TypographicFont> Fonts { get; private set; }

  public override string ToString() => this.Name;
}
