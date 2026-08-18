// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.NonCADDocumentContent
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal class NonCADDocumentContent : DBObjectContent
{
  public override DBObjectContentTag Tag => DBObjectContentTag.NonCADDocument;

  public override bool IsCADDocument => false;
}
