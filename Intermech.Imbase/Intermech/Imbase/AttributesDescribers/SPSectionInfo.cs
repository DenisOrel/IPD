// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.SPSectionInfo
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal class SPSectionInfo
{
  private string _name = string.Empty;
  private string _sectionNumber = string.Empty;

  internal string Name => this._name;

  internal string SectionNumber => this._sectionNumber;

  internal SPSectionInfo(string name, string sectionNumber)
  {
    this._name = name;
    this._sectionNumber = sectionNumber;
  }

  public override string ToString() => this._name;
}
