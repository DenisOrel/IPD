// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchivesTopObjectsPart
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Archives;

/// <summary>Часть для обновления узла "Все архивы"</summary>
public class ArchivesTopObjectsPart : TopObjectsPart
{
  /// <summary>Конструктор</summary>
  /// <param name="objTypeID"></param>
  /// <param name="services"></param>
  public ArchivesTopObjectsPart(int objTypeID, IServiceProvider services)
    : base(objTypeID, services)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objTypeID"></param>
  /// <param name="condition"></param>
  /// <param name="services"></param>
  public ArchivesTopObjectsPart(
    int objTypeID,
    ConditionStructure condition,
    IServiceProvider services)
    : base(objTypeID, condition, services)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objTypeID"></param>
  /// <param name="conditions"></param>
  /// <param name="services"></param>
  public ArchivesTopObjectsPart(
    int objTypeID,
    ConditionStructure[] conditions,
    IServiceProvider services)
    : base(objTypeID, conditions, services)
  {
  }

  /// <summary>Возвращает анализатор обновления</summary>
  /// <param name="capabilities"></param>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  /// <returns></returns>
  public override IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    if (e is DBObjectsEventArgs objectsEventArgs)
    {
      switch (objectsEventArgs.EventName)
      {
        case "ObjectsCreated":
          if (capabilities.CanAppend)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.SimpleRelationTypeID);
              List<long> objIDs = new List<long>();
              foreach (long objectId in (IEnumerable<long>) objectsEventArgs.ObjectIDs)
              {
                DataTable dataTable = relationCollection.EntersInVersion(new DBRecordSetParams(1), objectId);
                if (dataTable != null && dataTable.Rows.Count.Equals(0))
                  objIDs.Add(objectId);
              }
              if (objIDs.Count > 0)
                return (IUpdateAnalyser) new ObjectsCreatedAnalyser((IList<long>) objIDs);
              break;
            }
          }
          break;
        case "ObjectsChanged":
          return (IUpdateAnalyser) new ObjectsChangedAnalyser(objectsEventArgs.ObjectIDs);
        case "ObjectsRemoved":
          return (IUpdateAnalyser) new ObjectsRemovedAnalyser(objectsEventArgs.ObjectIDs);
      }
    }
    return (IUpdateAnalyser) null;
  }
}
