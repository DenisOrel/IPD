// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.MsoDocumentCodec
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using Intermech.Tools.Integrators;

#nullable disable
namespace Intermech.Tools.MSOffice;

internal sealed class MsoDocumentCodec : DocumentAttributesCodec
{
  public MsoDocumentCodec()
    : base((IValueBagFormatter) new MsoDocumentFormatter())
  {
  }
}
