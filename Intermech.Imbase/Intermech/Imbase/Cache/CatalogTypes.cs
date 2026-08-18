// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Cache.CatalogTypes
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Cache;

public class CatalogTypes
{
  private static ArrayList _types;

  static CatalogTypes() => CatalogTypes.Refresh();

  public static void Refresh()
  {
    if (CatalogTypes._types == null)
      CatalogTypes._types = new ArrayList();
    else
      CatalogTypes._types.Clear();
    ArrayList types = CatalogTypes._types;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(Intermech.Imbase.Consts.CatalogTypeAttID, false);
      if (attributeType == null)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) attributeType.GetPossibleValues().Rows)
        types.Add((object) row[attributeType.ValueFieldName].ToString());
    }
  }

  public static int GetType(string name)
  {
    return CatalogTypes._types.IndexOf((object) name) != -1 ? name.GetHashCode() : throw new ArgumentException("Catalog type name not found", nameof (name));
  }

  public static string GetName(int hashCode)
  {
    foreach (string type in CatalogTypes._types)
    {
      if (type.GetHashCode() == hashCode)
        return type;
    }
    return string.Empty;
  }

  public static string[] Names => (string[]) CatalogTypes._types.ToArray(typeof (string));
}
