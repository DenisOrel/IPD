// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSViews.ChapterFormatPorpertiesWrapper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS.AVSViews;

internal class ChapterFormatPorpertiesWrapper
{
  protected Chapter _chapter;

  public ChapterFormatPorpertiesWrapper(Chapter chapter) => this._chapter = chapter;

  /// <summary>Начинать ли запись с новой страницы </summary>
  [DefaultValue(false)]
  [Description("Начинать ли раздел с новой страницы")]
  [DisplayName("C новой страницы")]
  [Category("Страницы")]
  [TypeConverter(typeof (CustomBooleanNullableConverter))]
  public bool? FromNewPage
  {
    [DebuggerStepThrough] get => this._chapter.FromNewPage;
    set => this._chapter.FromNewPage = value;
  }

  /// <summary>Игнорировать пропуски в начале страницы</summary>
  [DefaultValue(true)]
  [Description("Игнорировать пропуски в начале страницы")]
  [DisplayName("Игнорировать пропуски строк перед разделом в начале страницы")]
  [Category("Пропуск строк")]
  [TypeConverter(typeof (CustomBooleanNullableConverter))]
  public bool? NonSkipBeforeAtStartPage
  {
    [DebuggerStepThrough] get => this._chapter.NonSkipBeforeAtStartPage;
    set => this._chapter.NonSkipBeforeAtStartPage = value;
  }
}
