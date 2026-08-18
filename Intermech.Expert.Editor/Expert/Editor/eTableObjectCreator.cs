// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.eTableObjectCreator
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Localization;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>
/// Класс creator для объектов "Таблицы эксперной системы"
/// </summary>
public class eTableObjectCreator : IObjectCreatorCustomService
{
  private void CreateRelations(
    int[] linkTypesID,
    long[] relatedObjIDs,
    long objID,
    DateTime startRelationTime)
  {
    if (linkTypesID.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < linkTypesID.Length; ++index)
        sessionKeeper.Session.GetRelationCollection(linkTypesID[index]).Create(relatedObjIDs[index], objID, startRelationTime);
    }
  }

  /// <summary>Конструктор</summary>
  /// <param name="ObjectTypeID"></param>
  /// <param name="TemplateObjectID"></param>
  /// <param name="RelationTypeIDs"></param>
  /// <param name="RelatedObjectIDs"></param>
  /// <param name="StartDate"></param>
  /// <param name="isVersion"></param>
  /// <returns></returns>
  public long CreateObjectDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    if (TemplateObjectID != 0L && TemplateObjectID != -1L)
    {
      string initValue = new UserPrompt().Execute(LocalizationHolder.rm.GetString("Expert.Editor_585"), LocalizationHolder.rm.GetString("Expert.Editor_210"), false);
      if (initValue == "")
        return -1;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IDBObject dbObject = session.GetObject(TemplateObjectID, false);
        if (dbObject != null)
        {
          if (dbObject is IExpertTable)
          {
            IExpertTable prototype = dbObject as IExpertTable;
            IExpertTable expertTable = session.GetObjectCollection(new Guid(ExpertObjGUIDs.ExpertTable)).Create((IDBObject) prototype) as IExpertTable;
            AttributeValues[] valuesList = new AttributeValues[1]
            {
              new AttributeValues(ExpertConsts.Consts._attrObjName, (object) initValue)
            };
            expertTable.SetAttributesValues(valuesList, false, false);
            expertTable.CommitCreation(true);
            IExpertServer customService = session.GetCustomService(typeof (IExpertServer)) as IExpertServer;
            byte[] traceInfo = (byte[]) null;
            bool flag = false;
            if (customService != null)
              flag = customService.ReflectObjUpdate(session.SessionGUID, dbObject.ObjectID, ExpertTraceFlags.None, (TempFormula) null, out traceInfo);
            if (flag)
            {
              using (RuleUpdateReport ruleUpdateReport = new RuleUpdateReport())
                ruleUpdateReport.Execute(traceInfo);
            }
            return expertTable.ObjectID;
          }
        }
      }
    }
    else
    {
      using (TableSetup tableSetup = new TableSetup())
      {
        if (tableSetup.ShowDialog().Equals((object) DialogResult.OK))
        {
          if (tableSetup.Tables != null)
          {
            if (tableSetup.Tables.Length != 0)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                IExpertTable expertTable = sessionKeeper.Session.GetObjectCollection(new Guid(ExpertObjGUIDs.ExpertTable)).Create() as IExpertTable;
                TableEditView.Save(expertTable.ObjectID, tableSetup.Tables, (TempFormula) null);
                expertTable.CommitCreation(true);
                long objectId = expertTable.ObjectID;
                switch (objectId)
                {
                  case -1:
                  case 0:
                    return expertTable.ObjectID;
                  default:
                    ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectId));
                    this.CreateRelations(RelationTypeIDs, RelatedObjectIDs, objectId, StartDate);
                    goto case -1;
                }
              }
            }
          }
        }
      }
    }
    return -1;
  }

  internal static void Attach(IObjectCreatorService service)
  {
    service.RegisterCreatorCustomService(ExpertConsts.Consts.objTable, typeof (eTableObjectCreator));
  }

  internal static void Detach(IObjectCreatorService service)
  {
    service.UnregisterCreatorCustomService(ExpertConsts.Consts.objTable, typeof (eTableObjectCreator));
  }
}
