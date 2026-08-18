// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.DisplaySection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using System;

#nullable disable
namespace Intermech.Tools.DataExchange;

public sealed class DisplaySection
{
  private string displayName;
  private string qualifiedName;

  public string DisplayName
  {
    get => this.displayName;
    set => this.displayName = value;
  }

  public string QualifiedName
  {
    get => this.qualifiedName;
    set => this.qualifiedName = value;
  }

  public static string GetDisplayName(SectionEntity dbItem)
  {
    if (dbItem == null)
      throw new ArgumentNullException(nameof (dbItem));
    return dbItem.Sections.Get<DisplaySection>().DisplayName;
  }

  public static string GetQualifiedName(SectionEntity dbItem)
  {
    if (dbItem == null)
      throw new ArgumentNullException(nameof (dbItem));
    return dbItem.Sections.Get<DisplaySection>().QualifiedName;
  }
}
