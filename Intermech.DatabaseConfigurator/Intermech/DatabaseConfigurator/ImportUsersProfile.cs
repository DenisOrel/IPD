// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.ImportUsersProfile
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator;

internal class ImportUsersProfile : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add(nameof (ImportUsersProfile), new CommandInfo(0, new ClickEventHandler(ImportUsersProfile.Import)));
    return groupCommands;
  }

  public static void Import(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string text;
    string caption;
    if (items.Count.Equals(1))
    {
      text = LocalizationHolder.rm.GetString("DatabaseConfigurator_192");
      caption = LocalizationHolder.rm.GetString("DatabaseConfigurator_197");
    }
    else
    {
      text = LocalizationHolder.rm.GetString("DatabaseConfigurator_193");
      caption = LocalizationHolder.rm.GetString("DatabaseConfigurator_198");
    }
    if (MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals((object) DialogResult.No))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      int usersTypeId = session.IdentHelper.UsersTypeID;
      if (!(session.GetCustomService(typeof (IImportUsersProfile)) is IImportUsersProfile customService))
        return;
      long[] usersImportTo = ImportUsersProfile.GetUsersImportTo(session);
      if (usersImportTo == null || usersImportTo.Length == 0)
        return;
      bool flag1 = false;
      bool flag2 = false;
      for (int index1 = 0; index1 < items.Count; ++index1)
      {
        IDBTypedObjectID itemData = items.GetItemData(index1, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        int index2 = itemData.ObjectType;
        if (index2.Equals(usersTypeId))
        {
          long[] numArray = usersImportTo;
          for (index2 = 0; index2 < numArray.Length; ++index2)
          {
            long destUserID = numArray[index2];
            try
            {
              customService.CopyProfile(itemData.ObjectID, destUserID, false);
            }
            catch (Exception ex)
            {
              if (!flag1)
              {
                QuestionFormResult questionFormResult = QuestionForm.Show(ex.Message, LocalizationHolder.rm.GetString("DatabaseConfigurator_199"));
                flag1 = questionFormResult.Equals((object) QuestionFormResult.SkipAll);
                flag2 = questionFormResult.Equals((object) QuestionFormResult.Break);
              }
            }
            if (flag2)
              return;
          }
        }
      }
    }
  }

  private static long[] GetUsersImportTo(IUserSession session)
  {
    IDescriptor rootDescriptor = (IDescriptor) new UsersGroupsDescriptor();
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("DatabaseConfigurator_200"), LocalizationHolder.rm.GetString("DatabaseConfigurator_201"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.Default);
    if (objArray == null || objArray.Length == 0)
      return (long[]) null;
    ArrayList arrayList = new ArrayList();
    foreach (IDBTypedObjectID dbTypedObjectId in objArray)
    {
      if (dbTypedObjectId.ObjectType.Equals(session.IdentHelper.UsersTypeID))
      {
        if (!arrayList.Contains((object) dbTypedObjectId.ObjectID))
          arrayList.Add((object) dbTypedObjectId.ObjectID);
      }
      else if (dbTypedObjectId.ObjectType.Equals(session.IdentHelper.GroupsTypeID))
      {
        IDBObject dbObject = session.GetObject(dbTypedObjectId.ObjectID);
        IDBObjectType objectType = session.GetObjectType(dbTypedObjectId.ObjectType);
        DataTable dataTable = session.GetRelationCollection(objectType.DefaultRelation).ConsistFrom(new DBRecordSetParams(new ConditionStructure[0], new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
        }), dbObject.ObjectID);
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (Convert.ToInt32(row[1]).Equals(session.IdentHelper.UsersTypeID))
            {
              long int64 = Convert.ToInt64(row[0]);
              if (!arrayList.Contains((object) int64))
                arrayList.Add((object) int64);
            }
          }
        }
      }
    }
    return arrayList.ToArray(typeof (long)) as long[];
  }
}
