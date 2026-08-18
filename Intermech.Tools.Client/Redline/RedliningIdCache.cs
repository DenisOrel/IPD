// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.RedliningIdCache
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces.Data.Metadata;
using System;

#nullable disable
namespace Intermech.Redline;

internal sealed class RedliningIdCache
{
  public RedliningIdCache(MetadataResolverFactory metadataResolvers)
  {
    this.UsersType = metadataResolvers.ObjectTypeResolver(new Guid("CAD00002-306C-11D8-B4E9-00304F19F545"));
    this.UserVisibleName = metadataResolvers.AttributeTypeResolver(new Guid("CAD0001D-306C-11D8-B4E9-00304F19F545"));
  }

  internal ObjectTypeResolver UsersType { get; private set; }

  internal AttributeTypeResolver UserVisibleName { get; private set; }
}
