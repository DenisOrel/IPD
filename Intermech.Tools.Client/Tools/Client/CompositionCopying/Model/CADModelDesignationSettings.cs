// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CADModelDesignationSettings
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CADModelDesignationSettings
{
  public CADModelDesignationSettings(
    bool indendentDesignationMode,
    string basicArticleConfigurationName)
  {
    if (string.IsNullOrEmpty(basicArticleConfigurationName))
      throw new ArgumentException("Значение не может быть пустым.", nameof (basicArticleConfigurationName));
    this.IndependentDesignationMode = indendentDesignationMode;
    this.BasicArticleConfigurationName = basicArticleConfigurationName;
  }

  public bool IndependentDesignationMode { get; }

  public string BasicArticleConfigurationName { get; }
}
