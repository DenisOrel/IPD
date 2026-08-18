// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CADModelContent
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CADModelContent : DBObjectContent
{
  public CADModelContent(
    CADConfigurationTable configurationTable,
    string defaultConfigurationName,
    CADModelDesignationSettings documentDesignationSettings,
    IAttributeCodec documentAttributeCodec,
    IAttributeCodec articleAttributeCodec)
  {
    if (configurationTable == null)
      throw new ArgumentNullException(nameof (configurationTable));
    if (string.IsNullOrEmpty(defaultConfigurationName))
      throw new ArgumentException("Значение не может быть пустым.", nameof (defaultConfigurationName));
    if (documentDesignationSettings == null)
      throw new ArgumentNullException(nameof (documentDesignationSettings));
    if (documentAttributeCodec == null)
      throw new ArgumentNullException(nameof (documentAttributeCodec));
    if (articleAttributeCodec == null)
      throw new ArgumentNullException(nameof (articleAttributeCodec));
    this.ConfigurationTable = configurationTable;
    this.DefaultConfigurationName = defaultConfigurationName;
    this.DocumentDesignationSettings = documentDesignationSettings;
    this.DocumentAttributeCodec = documentAttributeCodec;
    this.ArticleAttributeCodec = articleAttributeCodec;
  }

  public override DBObjectContentTag Tag => DBObjectContentTag.CADModel;

  public override bool IsCADDocument => true;

  public CADConfigurationTable ConfigurationTable { get; }

  public string DefaultConfigurationName { get; }

  public CADModelDesignationSettings DocumentDesignationSettings { get; }

  public IAttributeCodec DocumentAttributeCodec { get; }

  public IAttributeCodec ArticleAttributeCodec { get; }
}
