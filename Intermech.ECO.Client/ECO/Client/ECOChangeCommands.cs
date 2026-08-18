// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECOChangeCommands
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

internal class ECOChangeCommands
{
  public static void CreateCJRecord(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.Count != 1)
      return;
    ECOPlugin.CreateCJRec(0L, (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value);
  }

  public static void ChangeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOChangeCommands.New_Command(ECOGoal.Change, items, viewServices, additionalInfo);
  }

  public static void AnnulCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOChangeCommands.New_Command(ECOGoal.Annul, items, viewServices, additionalInfo);
  }

  public static void ChangeLiteraCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOChangeCommands.New_Command(ECOGoal.Litera, items, viewServices, additionalInfo);
  }

  public static void ReplaceCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOChangeCommands.New_Command(ECOGoal.Replace, items, viewServices, additionalInfo);
  }

  public static void CreateByECO(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOChangeCommands.New_Command(ECOGoal.Creation, items, viewServices, additionalInfo);
  }

  internal static void New_Command(
    ECOGoal goal,
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return;
    List<long> longList1 = new List<long>();
    QuestionFormResult questionFormResult = QuestionFormResult.Skip;
    long num1 = -1;
    for (int index = 0; index < items.Count; ++index)
    {
      long num2 = (items.GetItemData(index, typeof (IDBObjectID)) as IDBObjectID).Value;
      try
      {
        long linkContNum = -1;
        plugin.GoalAllowed(num2, goal, ref linkContNum);
        longList1.Add(num2);
        if (goal == ECOGoal.Annul)
        {
          long modificationId;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            modificationId = sessionKeeper.Session.GetObject(num2).ModificationID;
          if (modificationId != num1)
          {
            if (num1 != -1L)
            {
              if (modificationId != -1L)
              {
                int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_258"), LocalizationHolder.rm.GetString("ECO.Client_147"), MessageBoxButtons.OK);
                return;
              }
            }
            else
              num1 = modificationId;
          }
        }
      }
      catch (Exception ex)
      {
        if (questionFormResult != QuestionFormResult.SkipAll)
        {
          questionFormResult = AdditionalCommandProviderExceptionForm.Show(ex);
          if (questionFormResult == QuestionFormResult.Break)
            return;
        }
      }
    }
    if (longList1.Count <= 0)
      return;
    if (goal == ECOGoal.Annul && num1 != -1L)
      ECOPlugin.RevObjectCreator.linkedNumber = num1;
    string objCapt = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(longList1[0]);
      if (!objectInfo.Empty)
        objCapt = objectInfo.Caption;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < longList1.Count; ++index)
      {
        IDBObject dbObject1 = sessionKeeper.Session.GetObject(longList1[index]);
        if (dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout && longList1[index] >= 0L)
        {
          IDBObject dbObject2 = dbObject1.CheckOut(false);
          if (dbObject2 != null)
            longList1[index] = dbObject2.ObjectID;
        }
      }
    }
    Intermech.ECO.Client.ECO eco = plugin.CreateECO(-1, goal, objCapt, out bool _, true, longList1);
    if (eco == null)
      return;
    Hashtable synchroTab = goal == ECOGoal.Litera ? new Hashtable() : (Hashtable) null;
    if (goal == ECOGoal.Litera)
    {
      List<long> synchroList = new List<long>();
      int index = 0;
      while (index < longList1.Count)
      {
        long num4 = longList1[index];
        if (ECOPlugin.GetSynchroParents(num4, synchroList))
        {
          if (synchroList.Count == 1)
          {
            synchroTab.Add((object) num4, (object) synchroList);
          }
          else
          {
            ChooseSynchroDlg chooseSynchroDlg = new ChooseSynchroDlg();
            if (chooseSynchroDlg.Execute(synchroList, num4))
            {
              List<long> longList2 = chooseSynchroDlg.ComposeChosenList();
              if (longList2.Count > 0)
                synchroTab.Add((object) num4, (object) longList2);
            }
            else
            {
              longList1.RemoveAt(index);
              continue;
            }
          }
        }
        ++index;
      }
    }
    ECOEditorForm ecoEditorForm = plugin.CreateECOEditorForm(eco, false, true, true, false);
    IncludeGoal includeGoal = new IncludeGoal();
    if (!includeGoal.Execute(goal, longList1, "", (List<long>) null, (List<long>) null, eco.revType))
      return;
    if (includeGoal.goal == ECOGoal.Litera)
      ecoEditorForm.ECO.litera = includeGoal.litera;
    if (synchroTab != null && synchroTab.Count == 0)
      synchroTab = (Hashtable) null;
    ecoEditorForm.NewAttachItems(longList1, goal, includeGoal.schemaId, includeGoal.selLCStepId, includeGoal.separateChanges, synchroTab: synchroTab);
  }
}
