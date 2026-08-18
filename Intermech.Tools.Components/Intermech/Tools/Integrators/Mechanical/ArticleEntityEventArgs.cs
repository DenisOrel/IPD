// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleEntityEventArgs
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public class ArticleEntityEventArgs : EventArgs
{
  public ArticleEntityEventArgs(SectionEntity articleEntity)
  {
    this.ArticleEntity = articleEntity != null ? articleEntity : throw new ArgumentNullException(nameof (articleEntity));
  }

  public SectionEntity ArticleEntity { get; }
}
