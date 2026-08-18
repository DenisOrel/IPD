// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DBObjectContentExtensions
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal static class DBObjectContentExtensions
{
  public static bool IsEmpty(this DBObjectContent content)
  {
    if (content == null)
      throw new ArgumentNullException(nameof (content));
    return content.Tag == DBObjectContentTag.Empty;
  }

  public static DBObjectEmptyContent AsEmpty(this DBObjectContent content)
  {
    return content != null ? (DBObjectEmptyContent) content : throw new ArgumentNullException(nameof (content));
  }

  public static CADModelContent AsCADModel(this DBObjectContent content)
  {
    return content != null ? (CADModelContent) content : throw new ArgumentNullException(nameof (content));
  }

  public static CADGeneralDocumentContent AsCADGeneralDocument(this DBObjectContent content)
  {
    return content != null ? (CADGeneralDocumentContent) content : throw new ArgumentNullException(nameof (content));
  }

  public static NonCADDocumentContent AsNonCADDocument(this DBObjectContent content)
  {
    return content != null ? (NonCADDocumentContent) content : throw new ArgumentNullException(nameof (content));
  }
}
