// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.ScriptCheckerIDCache
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces.Data.Metadata;
using System;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class ScriptCheckerIDCache
{
  public ScriptCheckerIDCache(MetadataResolverFactory metadataResolvers)
  {
    this.ScriptsBaseType = metadataResolvers.ObjectTypeResolver(new Guid("CAD0036A-306C-11D8-B4E9-00304F19F545"));
    this.ScriptCode = metadataResolvers.AttributeTypeResolver(new Guid("CAD00366-306C-11D8-B4E9-00304F19F545"));
    this.HtmlReports = metadataResolvers.ObjectTypeResolver(new Guid("CADD99D2-306C-11D8-B4E9-00304F19F545"));
    this.Name = metadataResolvers.AttributeTypeResolver(new Guid("CAD00020-306C-11D8-B4E9-00304F19F545"));
  }

  public ObjectTypeResolver ScriptsBaseType { get; private set; }

  public AttributeTypeResolver ScriptCode { get; private set; }

  public ObjectTypeResolver HtmlReports { get; private set; }

  public AttributeTypeResolver Name { get; private set; }
}
