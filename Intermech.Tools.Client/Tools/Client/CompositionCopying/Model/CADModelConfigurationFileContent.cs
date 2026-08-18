// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CADModelConfigurationFileContent
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CADModelConfigurationFileContent : DBObjectFileContent
{
  public CADModelConfigurationFileContent(CADConfigurationTableRow configurationTableRow)
  {
    this.ConfigurationTableRow = configurationTableRow != null ? configurationTableRow : throw new ArgumentNullException(nameof (configurationTableRow));
  }

  public override DBObjectFileContentTag Tag => DBObjectFileContentTag.CADModelConfigurationFile;

  public override bool IsCADFile => true;

  public override bool IsMainFile => false;

  public CADConfigurationTableRow ConfigurationTableRow { get; }
}
