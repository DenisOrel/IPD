// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSViews.DocumentWrapper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.Model;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS.AVSViews;

internal class DocumentWrapper
{
  protected ImDocument doc;

  public DocumentWrapper(ImDocument doc) => this.doc = doc;

  /// <summary>Номер первой страницы документа</summary>
  [DisplayName("Номер первой страницы")]
  [Description("Номер первой страницы")]
  [Category("Данные")]
  public int StartPageNumber
  {
    [DebuggerStepThrough] get => this.doc.StartPageNumber;
    set => this.doc.SetStartPageNumber(value, true, true);
  }
}
