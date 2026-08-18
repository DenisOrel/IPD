// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSViews.SpecificationSectionFormatPorpertiesWrapper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS.AVSViews;

internal class SpecificationSectionFormatPorpertiesWrapper(SpecificationSection section) : 
  ChapterFormatPorpertiesWrapper((Chapter) section)
{
  private SpecificationSection Section => (SpecificationSection) this._chapter;

  /// <summary>Пропуск строк перед записью </summary>
  [DefaultValue(null)]
  [Description("Пропуск строк перед разделом")]
  [DisplayName("Перед разделом")]
  [Category("Пропуск строк")]
  public int? SkipLinesBefore
  {
    [DebuggerStepThrough] get => this.Section.SkipLinesBefore;
    set => this.Section.SkipLinesBefore = value;
  }

  /// <summary>Пропуск строк после записи </summary>
  [DefaultValue(null)]
  [Description("Пропуск строк после заголовка раздела")]
  [DisplayName("После заголовка раздела")]
  [Category("Пропуск строк")]
  public int? SkipLinesAfter
  {
    [DebuggerStepThrough] get => this.Section.SkipLinesAfter;
    set => this.Section.SkipLinesAfter = value;
  }
}
