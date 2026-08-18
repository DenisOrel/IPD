// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSViews.DocumentFormatWrapper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.Model;
using Intermech.Interfaces.Document;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS.AVSViews;

internal class DocumentFormatWrapper(ImDocument doc) : DocumentWrapper(doc)
{
  /// <summary>Номер первой страницы документа</summary>
  [DisplayName("Разрешать форматирование")]
  [Description("Разрешать форматирование для текста, который только для чтения")]
  [Category("Разное")]
  [TypeConverter(typeof (CustomBooleanNullableConverter))]
  public bool AllowFormatingForReadOnlyText
  {
    [DebuggerStepThrough] get => this.doc.AllowFormatingForReadOnlyText;
    set => this.doc.AllowFormatingForReadOnlyText = value;
  }
}
