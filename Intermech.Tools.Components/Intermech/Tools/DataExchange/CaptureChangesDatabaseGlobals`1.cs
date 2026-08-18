// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CaptureChangesDatabaseGlobals`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using System;

#nullable disable
namespace Intermech.Tools.DataExchange;

public static class CaptureChangesDatabaseGlobals<T>
{
  public static T GetOrCreate(CaptureChangesDatabase db, Func<T> objectConstructor)
  {
    return db != null ? CaptureChangesDatabaseGlobals<T>.GetOrCreate(db, CaptureChangesDatabaseGlobals<T>.CreateObjectTypeKey(), objectConstructor) : throw new ArgumentNullException(nameof (db));
  }

  public static T GetOrCreate(SectionEntity keyEntity, Func<T> objectConstructor)
  {
    if (keyEntity == null)
      throw new ArgumentNullException("entity");
    return CaptureChangesDatabaseGlobals<T>.GetOrCreate((CaptureChangesDatabase) keyEntity.Database, CaptureChangesDatabaseGlobals<T>.CreateEntityKey(keyEntity), objectConstructor);
  }

  public static T GetOrCreate(
    CaptureChangesDatabase db,
    string objectKey,
    Func<T> objectConstructor)
  {
    if (db == null)
      throw new ArgumentNullException(nameof (db));
    if (objectKey == null)
      throw new ArgumentNullException(nameof (objectKey));
    if (objectConstructor == null)
      throw new ArgumentNullException(nameof (objectConstructor));
    SectionEntity sectionEntity1 = db.QueryFirst((IQueryCondition) new BinaryCondition((object) CaptureChangesDatabaseGlobals<T>.HelperSection.KeyRef, BinaryOperator.Equal, (object) objectKey));
    CaptureChangesDatabaseGlobals<T>.HelperSection sectionObject;
    if (sectionEntity1 == null)
    {
      sectionObject = new CaptureChangesDatabaseGlobals<T>.HelperSection(objectKey, objectConstructor());
      SectionEntity sectionEntity2 = new SectionEntity();
      sectionEntity2.Sections.Set((object) sectionObject);
      db.Add((IEntity) sectionEntity2);
    }
    else
      sectionObject = sectionEntity1.Sections.Get<CaptureChangesDatabaseGlobals<T>.HelperSection>();
    return sectionObject.Object;
  }

  private static string CreateEntityKey(SectionEntity entity)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    if (entity.Database == null)
      throw new ArgumentException("A entity must be added to a database first.", nameof (entity));
    return CaptureChangesDatabaseGlobals<T>.CreateObjectTypeKey() + entity.UniqueId.ToString();
  }

  private static string CreateObjectTypeKey() => typeof (T).GUID.ToString("N");

  private sealed class HelperSection
  {
    public static readonly SectionPropertyReference KeyRef = new SectionPropertyReference(typeof (CaptureChangesDatabaseGlobals<T>.HelperSection), nameof (Key));
    private readonly string key;
    private readonly T obj;

    public HelperSection(string key, T obj)
    {
      this.key = key;
      this.obj = obj;
    }

    [Indexable(IndexType.Equality, true)]
    public string Key => this.key;

    public T Object => this.obj;
  }
}
