// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.ArticleDocumentationTrait
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class ArticleDocumentationTrait : DBObjectGraphTrait
{
  private bool isBasedOnCADModel;
  private string externalKey;
  private string cadConfigurationName;

  public ArticleDocumentationTrait()
  {
    this.isBasedOnCADModel = false;
    this.externalKey = string.Empty;
    this.cadConfigurationName = string.Empty;
  }

  public bool IsBasedOnCADModel
  {
    get => this.isBasedOnCADModel;
    set => this.isBasedOnCADModel = value;
  }

  public string ExternalKey
  {
    get => this.externalKey;
    set
    {
      this.externalKey = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  public string CADConfigurationName
  {
    get => this.cadConfigurationName;
    set
    {
      this.cadConfigurationName = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }
}
