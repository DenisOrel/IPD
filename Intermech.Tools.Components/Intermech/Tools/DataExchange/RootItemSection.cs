// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.RootItemSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using System;

#nullable disable
namespace Intermech.Tools.DataExchange;

internal sealed class RootItemSection
{
  public static readonly SectionPropertyReference EntryPointRef = new SectionPropertyReference(typeof (RootItemSection), nameof (EntryPoint));
  private readonly bool entryPoint;
  private bool handled;

  public RootItemSection(bool entryPoint) => this.entryPoint = entryPoint;

  [Indexable(IndexType.Equality, false)]
  public bool EntryPoint => this.entryPoint;

  public bool Handled
  {
    get => this.handled;
    set => this.handled = value;
  }

  public static EntitySet GetRootItems(CaptureChangesDatabase db)
  {
    return db != null ? db.Query((IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (RootItemSection))) : throw new ArgumentNullException(nameof (db));
  }

  public static SectionEntity GetEntryPoint(CaptureChangesDatabase db)
  {
    return db != null ? db.QueryFirst((IQueryCondition) new BinaryCondition((object) RootItemSection.EntryPointRef, BinaryOperator.Equal, (object) true)) : throw new ArgumentNullException(nameof (db));
  }

  public static bool IsEntryPoint(SectionEntity entity)
  {
    RootItemSection rootItemSection = entity != null ? entity.Sections.Get<RootItemSection>((RootItemSection) null) : throw new ArgumentNullException(nameof (entity));
    return rootItemSection != null && rootItemSection.EntryPoint;
  }
}
