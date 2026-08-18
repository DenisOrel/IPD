// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.PDFIntegratorSettings
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Localization;
using Intermech.PropertyEditors;
using Intermech.Tools.Integrators.Simple;
using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.PDF;

internal sealed class PDFIntegratorSettings : SingleFileSettings
{
  private bool processSubject;

  public PDFIntegratorSettings() => this.processSubject = true;

  protected override SingleFileSettings CreateClone()
  {
    return (SingleFileSettings) new PDFIntegratorSettings();
  }

  protected override void FillClone(SingleFileSettings clonedObj)
  {
    base.FillClone(clonedObj);
    ((PDFIntegratorSettings) clonedObj).processSubject = this.processSubject;
  }

  [CustomCategory("SR_1")]
  [CustomDisplayName("SR_2")]
  [CustomDescription("SR_3")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool ProcessSubject
  {
    get => this.processSubject;
    set => this.processSubject = value;
  }
}
