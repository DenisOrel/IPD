// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ScriptPad.WorkflowScriptSaveChangesBehavior
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Scripting.Common.DesignTime;
using Intermech.Scripting.Projects.DBScripts;
using Intermech.Search.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ScriptPad;

/// <summary>
/// Класс поведения сценариев Workflow во время чтение/записи в базу данных.
/// Реализация не является thread safe.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="scriptProject">Проект сценария</param>
/// <exception cref="T:System.ArgumentNullException">параметр <paramref name="scriptProject" /> не должен быть равен null</exception>
internal sealed class WorkflowScriptSaveChangesBehavior(DBScriptProject scriptProject) : 
  DBScriptSaveChangesBehavior(scriptProject)
{
  /// <summary>
  /// Обработчик события, вызывающегося перед сохранением изменений.
  /// Метод вызывается и для новых, и для измененных существующих сценариев.
  /// </summary>
  /// <param name="e">Аргументы события</param>
  public override void BeforeSave(ScriptBeforeSaveEventArgs e)
  {
    base.BeforeSave(e);
    if (this.ScriptProject.ObjectTypeId != ScriptTypeHelper.GetObjType4ScriptType(ScriptTypes.WorkflowCommon))
      return;
    e.CanSave = this.CanSaveModifiedScript();
  }

  private bool CanSaveModifiedScript()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
      if (this.ScriptProject.ObjectId == 0L)
        return true;
      ConditionStructure conditionStructure1 = new ConditionStructure(0, RelationalOperators.ConsistFrom, (object) sessionKeeper.Session.GetIDByObjectID(this.ScriptProject.ObjectId), LogicalOperators.AND, 0, false)
      {
        TypeID = (object) MetaDataHelper.GetRelationTypeID(new Guid("cad00367-306c-11d8-b4e9-00304f19f545"))
      };
      ConditionStructure conditionStructure2 = new ConditionStructure(-9, RelationalOperators.Equal, (object) MetaDataHelper.GetLCLevelID("cad00013-306c-11d8-b4e9-00304f19f545"), LogicalOperators.NONE, 0, false);
      objectCollection.LocalTypesMode = true;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        conditionStructure1,
        conditionStructure2
      }, new object[2]{ (object) -2, (object) -9 });
      DataTable dataTable = objectCollection.Select(paramSet);
      if (dataTable != null)
      {
        List<long> shemesID1 = new List<long>();
        List<long> shemesID2 = new List<long>();
        if (dataTable.Rows.Count > 1)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(Convert.ToInt64(row.ItemArray[0]), false);
            if (dbObject != null)
            {
              if (!shemesID2.Contains(dbObject.ObjectID))
                shemesID2.Add(dbObject.ObjectID);
              IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad002ce-306c-11d8-b4e9-00304f19f545"), false);
              if (attributeByGuid != null && Convert.ToInt32(row.ItemArray[1]) == MetaDataHelper.GetLCLevelID("cad00013-306c-11d8-b4e9-00304f19f545") && !shemesID1.Contains(attributeByGuid.AsInteger))
                shemesID1.Add(attributeByGuid.AsInteger);
            }
          }
        }
        if (shemesID1.Count > 1)
        {
          using (SchemesFoundForWorkflowScript forWorkflowScript = new SchemesFoundForWorkflowScript(shemesID1, "Изменения в текущем сценарии повлияют на следующие шаблоны процессов: "))
          {
            if (forWorkflowScript.ShowDialog() == DialogResult.Cancel)
              return false;
          }
        }
        else if (shemesID1.Count == 1)
        {
          if (dataTable.Rows.Count > 1)
          {
            using (SchemesFoundForWorkflowScript forWorkflowScript = new SchemesFoundForWorkflowScript(shemesID2, "Изменения в текущем сценарии повлияют на следующие действия: "))
            {
              if (forWorkflowScript.ShowDialog() == DialogResult.Cancel)
                return false;
            }
          }
        }
      }
    }
    return true;
  }

  /// <summary>
  /// Определяет, можно ли изменять имя сценария при сохранении.
  /// </summary>
  /// <returns>Признак возможности изменять имя сценария при сохранении</returns>
  protected override bool CanChangeNewScriptName()
  {
    return this.ScriptProject.ObjectTypeId != ScriptTypeHelper.GetObjType4ScriptType(ScriptTypes.WorkflowLocal);
  }
}
