// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.UserMessageSelector
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Forums;

/// <summary>для выбора сообщений</summary>
public class UserMessageSelector : IUserMessageSelector
{
  /// <summary>
  /// Разрешает пользователю выбрать
  /// сообщения из обсуждения
  /// </summary>
  /// <returns></returns>
  public object[] SelectMessages()
  {
    string description = "Выберите сообщения из обсуждений";
    List<long> objectIDs = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(ForumsConsts.forumObjectTypeID);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[0], new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
        objectIDs.Add(Convert.ToInt64(row[0]));
    }
    ListDescriptor rootDescriptor = new ListDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, ForumsConsts.forumObjectTypeID, LocalizationHolder.rm.GetString("Workflow.Design_197"), (IList) objectIDs);
    Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new SelectedMessageAnalizer(), true);
    return Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Workflow.Design_193"), description, (IDescriptor) rootDescriptor, typeof (string), SelectionOptions.SelectOtherNodes | SelectionOptions.DisableSelectFromTree);
  }
}
