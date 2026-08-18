// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.LastViewedDocumentsView
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Navigator.DBObjects;

#nullable disable
namespace Intermech.Redline;

internal class LastViewedDocumentsView : CompositionView
{
  public LastViewedDocumentsView() => this.DisableDoubleClicks = true;

  public override string Caption => "Просмотренные документы";
}
