// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CADGeneralDocumentContent
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CADGeneralDocumentContent : DBObjectContent
{
  public CADGeneralDocumentContent(IAttributeCodec documentAttributeCodec)
  {
    this.DocumentAttributeCodec = documentAttributeCodec != null ? documentAttributeCodec : throw new ArgumentNullException(nameof (documentAttributeCodec));
  }

  public override DBObjectContentTag Tag => DBObjectContentTag.CADGeneralDocument;

  public override bool IsCADDocument => true;

  public IAttributeCodec DocumentAttributeCodec { get; }
}
