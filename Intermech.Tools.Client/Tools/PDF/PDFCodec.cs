// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.PDFCodec
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using System;

#nullable disable
namespace Intermech.Tools.PDF;

internal sealed class PDFCodec : DocumentAttributesCodec
{
  private readonly PDFIntegratorSettingsService settingsSvc;

  public PDFCodec(IServiceProvider integrator)
    : base((IValueBagFormatter) new PDFFormatter())
  {
    this.settingsSvc = ServiceUtils.GetService<PDFIntegratorSettingsService>((object) integrator, true);
  }

  protected override StringKey GetContainerValueKey(StringKey attributeName)
  {
    if (attributeName == (StringKey) IDCache.Default.Name.Text)
      return (StringKey) "Title";
    return this.settingsSvc.GetSettings().ProcessSubject && attributeName == (StringKey) IDCache.Default.Note.Text ? (StringKey) "Subject" : base.GetContainerValueKey(attributeName);
  }
}
