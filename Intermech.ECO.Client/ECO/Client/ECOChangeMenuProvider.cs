// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECOChangeMenuProvider
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.ECO.Client;

public class ECOChangeMenuProvider : ICommandsProvider
{
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    ECOPlugin plugin = ECOPlugin.FindPlugin();
    if (plugin == null)
      return CommandsInfo.Empty;
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    bool flag1 = true;
    bool flag2 = true;
    bool flag3 = true;
    bool flag4 = true;
    bool flag5 = true;
    bool flag6 = true;
    bool flag7 = false;
    bool flag8 = false;
    bool Annuled = false;
    bool Stamped = false;
    List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_PI);
    List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(RevHelper.idObj_PR);
    List<int> allowedTypes = plugin._allowedTypes;
    HashSet<long> longSet = new HashSet<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData1)
      {
        long num1 = itemData1.Value;
        if (!longSet.Contains(num1))
          longSet.Add(num1);
        int childTypeID = items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData ? itemData.ObjectType : -1;
        if (allowedTypes.IndexOf(childTypeID) < 0)
          flag1 = false;
        flag2 = flag2 && childrenIdRecursive1.Contains(childTypeID);
        flag3 = flag3 && childrenIdRecursive2.Contains(childTypeID);
        if (childTypeID == RevHelper.idObj_II || childTypeID == RevHelper.idObj_PI || childTypeID == RevHelper.idObj_PR)
        {
          long num2 = Math.Abs(num1);
          if (plugin.revInfoList.ContainsKey(num2))
          {
            Annuled = plugin.revInfoList[num2].Annuled;
            Stamped = plugin.revInfoList[num2].Stamped;
          }
          else
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(num1, false);
              if (dbObject != null)
              {
                IDBAttribute attributeById1 = dbObject.GetAttributeByID(RevHelper.idLinkToAnnuledPI);
                if (attributeById1 != null && attributeById1.Value != null && attributeById1.Value != DBNull.Value)
                  Annuled = true;
                else if (childTypeID == RevHelper.idObj_PI || childTypeID == RevHelper.idObj_PR)
                {
                  IDBAttribute attributeById2 = dbObject.GetAttributeByID(RevHelper.idAttrStampedByII);
                  if (attributeById2 != null && attributeById2.Value != null && attributeById2.Value != DBNull.Value)
                    Stamped = true;
                }
              }
              if (childTypeID == RevHelper.idObj_PI)
              {
                if (ECO_PICommands.GetAnnulingRevision(num1) != 0L)
                  Annuled = true;
              }
            }
            plugin.revInfoList.Add(num2, new ECOPlugin.RevInfo(num2, Annuled, Stamped));
          }
        }
        List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(childTypeID);
        parentsIdReverse.Add(childTypeID);
        flag4 = flag4 && (parentsIdReverse.Contains(RevHelper.idObj_II) || parentsIdReverse.Contains(RevHelper.idObj_PI) || parentsIdReverse.Contains(RevHelper.idObj_PR));
        flag6 = flag6 && childTypeID == RevHelper.idObjContext;
        List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(childTypeID);
        if (!objectTypeParentsId.Contains(childTypeID))
          objectTypeParentsId.Add(childTypeID);
        flag5 = flag5 && (objectTypeParentsId.Contains(plugin.idOTSpecification) || objectTypeParentsId.Contains(plugin.idOTComplex) || objectTypeParentsId.Contains(plugin.idOTComplect));
        flag7 = items.Count == 1 && childTypeID == RevHelper.idObj_II;
        flag8 = items.Count == 1 && childTypeID == RevHelper.idObj_PI;
      }
      else
      {
        flag1 = false;
        flag2 = false;
        flag3 = false;
        flag4 = false;
        flag5 = false;
      }
    }
    if ((viewStateFlags & ViewStateFlags.ReadOnly) != ViewStateFlags.None)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    if (flag1 | flag2 | flag3 | flag4)
      groupCommands.Add("EngineeringChangeOrders", new CommandInfo(0));
    if (flag1 & flag5)
      groupCommands.Add("New.SetLitera", new CommandInfo(0, new ClickEventHandler(ECOChangeCommands.ChangeLiteraCommand)));
    if (flag1)
    {
      groupCommands.Add("New.Change", new CommandInfo(0, new ClickEventHandler(ECOChangeCommands.ChangeCommand)));
      groupCommands.Add("New.Replace", new CommandInfo(0, new ClickEventHandler(ECOChangeCommands.ReplaceCommand)));
      groupCommands.Add("New.Create", new CommandInfo(0, new ClickEventHandler(ECOChangeCommands.CreateByECO)));
    }
    if (flag1 && !Annuled)
      groupCommands.Add("New.Annul", new CommandInfo(0, new ClickEventHandler(ECOChangeCommands.AnnulCommand)));
    if (flag2 && !Annuled)
      groupCommands.Add("ReplacePI", new CommandInfo(0, new ClickEventHandler(ECO_PICommands.ReplaceCommand)));
    if (Stamped && items.Count == 1 && !Annuled)
      groupCommands.Add("UnreplacePI", new CommandInfo(0, new ClickEventHandler(ECO_PICommands.UnreplaceCommand)));
    if (flag1 && items.Count == 1)
      groupCommands.Add("CreateCJRecord", new CommandInfo(0, new ClickEventHandler(ECOChangeCommands.CreateCJRecord)));
    if (flag2 && items.Count == 1 && !Annuled)
    {
      groupCommands.Add("ReplacePIWithContents", new CommandInfo(0, new ClickEventHandler(ECO_PICommands.ReplaceContentsCommand)));
      groupCommands.Add("AnnulPI", new CommandInfo(0, new ClickEventHandler(ECO_PICommands.AnnulPICommand)));
    }
    if (flag3)
    {
      groupCommands.Add("AcceptPR", new CommandInfo(0, new ClickEventHandler(ECO_PRCommands.AcceptCommand)));
      groupCommands.Add("AcceptPRWithContents", new CommandInfo(0, new ClickEventHandler(ECO_PRCommands.AcceptContentsCommand)));
    }
    if (flag4 | flag6)
    {
      groupCommands.Add("CreateLinkedECO", new CommandInfo(0, new ClickEventHandler(ECO_AnyCommands.CreateLinkedCommand)));
      groupCommands.Add("LinkToOther", new CommandInfo(0, new ClickEventHandler(ECO_AnyCommands.LinkToOtherCommand)));
      groupCommands.Add("UnlinkToOther", new CommandInfo(0, new ClickEventHandler(ECO_AnyCommands.UnLinkToOtherCommand)));
      groupCommands.Suppress("CreateInclude", 0);
      groupCommands.Suppress("CreateInclude2", 0);
    }
    if (flag7 && !Annuled)
      groupCommands.Add("IssueDI", new CommandInfo(0, new ClickEventHandler(ECO_AnyCommands.CreateDI)));
    if (flag8 && !Annuled)
      groupCommands.Add("IssueDPI", new CommandInfo(0, new ClickEventHandler(ECO_AnyCommands.CreateDPI)));
    if (flag1 && !ECOPlugin.plugin.eps.Current.ShowHidden)
      groupCommands.Add("UnhideHidden", new CommandInfo(0, new ClickEventHandler(ECO_AnyCommands.UnhideHiddenCommand)));
    return groupCommands;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }
}
