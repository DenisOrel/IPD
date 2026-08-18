// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSViews.AdditionalChapterPropertiesWrapper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS.AVSViews;

internal class AdditionalChapterPropertiesWrapper(AdditionalChapter section) : 
  ChapterFormatPorpertiesWrapper((Chapter) section)
{
  private AdditionalChapter Section => (AdditionalChapter) this._chapter;

  /// <summary>Наименование</summary>
  [DefaultValue("")]
  [Description("Наименование части спецификации")]
  [DisplayName("Наименование")]
  [Category("Общие")]
  public string Caption
  {
    [DebuggerStepThrough] get => this.Section.Caption;
    set
    {
      this.Section.Caption = value;
      if (this.Section.Product == null)
        return;
      for (int index = 0; index < this.Section.DocNodes.Count; ++index)
        this.Section.DocNodes[index].SetAttributeValue(Chapter.CaptionFormat_AttributeName, value);
    }
  }
}
