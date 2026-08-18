// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.StructureColumns
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Archives;

/// <summary>класс для поиска атрибутов в структуре архива</summary>
public static class StructureColumns
{
  /// <summary>
  ///  добавить в коллекцию колонки для атрибутов, назначенных указанному архиву
  /// </summary>
  /// <param name="columns"></param>
  /// <param name="objectID">версия архива</param>
  /// <returns></returns>
  public static void AddStructureColumns(NodeColumnCollection columns, long objectID)
  {
    Guid structureSchemeGuid = ArchivesStructureScheme.ArchivesStructureSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    if (service == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(objectID, ConstsHolder.ArchiveStructureAttrID);
      if (objectAttributeById == null)
        return;
      foreach (object obj in objectAttributeById.Values)
      {
        string Guid = obj.ToString();
        if (!string.IsNullOrEmpty(Guid))
        {
          int attributeTypeId = MetaDataHelper.GetAttributeTypeID(Guid);
          if (attributeTypeId != -10000)
            columns.Add(service.CreateColumn(structureSchemeGuid, (object) attributeTypeId), 250);
        }
      }
    }
  }
}
