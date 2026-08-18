// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.Approve
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Signs.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class Approve(UserSession uSession, DataTable objectsTable) : 
  UserActivity(uSession, objectsTable),
  IApproveActivity,
  IActivity,
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity
{
  private List<string> _alienSettingsActs;
  private bool _whatToSignLoaded;
  private WhatToSign _whatToSign;
  private List<int> _signTypeIDs;
  private bool _hasInvalidTypes;
  private RequiredSigns _requiredSigns;
  private bool? _checkResult;
  private List<long> _unsignedObjects = new List<long>();
  private SignsDataItemModel _signsDataItemModel;

  public override ActivityKind Kind => ActivityKind.Approve;

  public bool TestOnly
  {
    get
    {
      return this.ExtProps.HasFlag(ExtPropertiesFlag.Approve) && this.ExtProps.ReadBool(nameof (TestOnly));
    }
  }

  public bool GraphForType
  {
    get
    {
      return this.ExtProps.HasFlag(ExtPropertiesFlag.Approve) && this.ExtProps.ReadBool(nameof (GraphForType));
    }
  }

  public List<string> AlienSettingsActs
  {
    get
    {
      if (this._alienSettingsActs == null)
      {
        string str = this.ExtProps.HasFlag(ExtPropertiesFlag.Approve) ? this.ExtProps.Read("SettingsActs") : string.Empty;
        List<string> stringList;
        if (string.IsNullOrEmpty(str))
          stringList = new List<string>();
        else
          stringList = new List<string>((IEnumerable<string>) str.Split(','));
        this._alienSettingsActs = stringList;
      }
      return this._alienSettingsActs;
    }
  }

  public bool RequirePersonalSigns
  {
    get
    {
      return this.ExtProps.HasFlag(ExtPropertiesFlag.Approve) && this.ExtProps.ReadBool("PersonalSigns");
    }
  }

  protected internal override void Copied()
  {
    base.Copied();
    if (this.AlienSettingsActs.Count <= 0 || this._process == null)
      return;
    List<string> values = new List<string>();
    foreach (string alienSettingsAct in this.AlienSettingsActs)
    {
      string str = alienSettingsAct;
      Guid guid;
      if (this._process._objectGuidMapper.TryGetValue(new Guid(alienSettingsAct), out guid))
        str = guid.ToString();
      values.Add(str);
    }
    if (!this.ExtProps.Write("SettingsActs", string.Join(",", (IEnumerable<string>) values), ExtPropertiesFlag.Approve))
      return;
    this.ExtProps.Save((IDBObject) this);
    this._alienSettingsActs = values;
  }

  public WhatToSign WhatToSign
  {
    get
    {
      if (!this._whatToSignLoaded)
      {
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrWhatToSignID);
        if (attributeById != null)
          this._whatToSign = (WhatToSign) attributeById.AsInteger;
        this._whatToSignLoaded = true;
      }
      return this._whatToSign;
    }
  }

  private List<Guid> SignTypeGuids
  {
    get
    {
      List<Guid> signTypeGuids = new List<Guid>();
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrObjectTypesID);
      if (attributeById != null && !attributeById.IsNull)
      {
        foreach (object obj in attributeById.Values)
        {
          string g = obj.ToString();
          if (!string.IsNullOrEmpty(g))
          {
            Guid guid = new Guid(g);
            signTypeGuids.Add(guid);
          }
        }
      }
      return signTypeGuids;
    }
  }

  public List<int> SignTypeIDs
  {
    get
    {
      if (this._signTypeIDs == null)
      {
        this._signTypeIDs = new List<int>();
        this._hasInvalidTypes = false;
        foreach (Guid signTypeGuid in this.SignTypeGuids)
        {
          int objectTypeId = MetaDataHelper.GetObjectTypeID(signTypeGuid);
          if (objectTypeId > 0)
            this._signTypeIDs.Add(objectTypeId);
          else
            this._hasInvalidTypes = true;
        }
      }
      return this._signTypeIDs;
    }
  }

  public RequiredSigns RequiredSigns
  {
    get
    {
      if (this._requiredSigns == null)
        this._requiredSigns = new RequiredSigns(this.GetAttributeByID(wfConsts.AttrRequiredSignsID));
      return this._requiredSigns;
    }
  }

  private bool InternalCheckAllSigned(bool silent, bool checkAll = false, string userName = "")
  {
    if (silent && this._checkResult.HasValue)
      return this._checkResult.Value;
    this._checkResult = new bool?(true);
    if (this.Session.GetCustomService(typeof (ISignsService)) is ISignsService customService)
    {
      AttachmentList attachs = MiscFunx.ExpandAttachments(this.Session, this.Attachments);
      this._unsignedObjects.Clear();
      string str1 = this.ObjectsSigned(attachs, !silent | checkAll, customService, this.Attachments.HasInvisibleItems);
      this._checkResult = new bool?(string.IsNullOrEmpty(str1));
      if (!this._checkResult.Value && !silent)
      {
        string str2 = string.Empty;
        if (this.GraphForType && this.IndividualSettingForTypes != null)
        {
          if (this._unsignedObjects.Count > 0)
          {
            string str3 = LocalizationHolder.rm.GetString("Workflow.Server_1");
            foreach (long unsignedObject in this._unsignedObjects)
            {
              IDBObject dbObject = this.UserSession.GetObject(unsignedObject, false);
              if (dbObject != null)
              {
                str3 = $"{str3}Для типа объекта {MetaDataHelper.GetObjectName(dbObject.ObjectType)}: ";
                SignsDataItem signsDataItem = this.IndividualSettingForTypes.GetSignsDataItem(dbObject.ObjectType);
                if (signsDataItem.SignAnyGraph)
                {
                  str3 += "В любой графе";
                }
                else
                {
                  GraphsSet gset = new GraphsSet();
                  foreach (SignsGroup group in (Collection<SignsGroup>) signsDataItem.Groups)
                  {
                    GraphsCollection graphsCollection = new GraphsCollection();
                    foreach (SignsDataItemChildren child in (Collection<SignsDataItemChildren>) group.Children)
                      graphsCollection.Add(new GraphClass(child.GraphForType, child.StrongControl, false));
                    gset.Add(group.GroupID.ToString(), graphsCollection);
                  }
                  str3 += MiscFunx.GetGraphsSetCaption(gset);
                }
              }
            }
            str2 = str3 + ".";
          }
        }
        else
          str2 = this.RequiredSigns.IsEmpty ? LocalizationHolder.rm.GetString("Workflow.Server_2") : $"{LocalizationHolder.rm.GetString("Workflow.Server_1")}{MiscFunx.GetGraphsSetCaption(this.RequiredSigns.GraphsSet)}.";
        string str4 = string.IsNullOrEmpty(userName) ? string.Empty : $" Требуется подпись от пользователя '{userName}'.";
        throw new NotAllSignedException($"{LocalizationHolder.rm.GetString("Workflow.Server_3")} {str2}{str4}{LocalizationHolder.rm.GetString("Workflow.Server_4")}{str1}{sc_22131.ssp_workflow_server_22132()}");
      }
    }
    else
    {
      this._checkResult = new bool?(false);
      if (!silent)
        throw new Exception(LocalizationHolder.rm.GetString("Workflow.Server_5"));
    }
    return this._checkResult.Value;
  }

  public bool CheckAllSigned(bool silent, out HashSet<long> participantIDs, bool checkAll = false)
  {
    List<Approve> approveList = new List<Approve>();
    participantIDs = new HashSet<long>(0);
    bool flag1 = true;
    if (this.TestOnly && this.AlienSettingsActs.Count > 0)
    {
      foreach (string alienSettingsAct in this.AlienSettingsActs)
      {
        if (this.Session.GetObject(new Guid(alienSettingsAct), false) is WFActivity wfActivity)
        {
          foreach (WFActivity activity in this.Process.Activities)
          {
            if (activity.ParentActivityID == wfActivity.ObjectID)
            {
              wfActivity = activity;
              break;
            }
          }
          Approve approve = wfActivity as Approve;
          if (approve.Status != ActivityStatus.OnApproach)
          {
            approveList.Add(approve);
            flag1 = false;
          }
        }
      }
    }
    if (approveList.Count == 0)
      approveList.Add(this);
    bool flag2 = true;
    foreach (Approve approve in approveList)
    {
      if (approve.RequirePersonalSigns && approve.Status == ActivityStatus.Completed)
      {
        ParticipantList pl = new ParticipantList(this.Session);
        pl.Assign(approve.Participants);
        MiscFunx.ExpandParticipants((IDBAttributable) approve, pl);
        if (pl.EveryOne)
        {
          foreach (Participant participant in pl)
          {
            Participant part = participant;
            int lastIndex = approve.Clones.FindLastIndex((Predicate<WFActivity>) (x => x.ParticipantID == part.ID));
            if (lastIndex != -1)
            {
              WFActivity clone = approve.Clones[lastIndex];
              if (clone.Status != ActivityStatus.OnApproach)
              {
                string empty = string.Empty;
                try
                {
                  QuickObjectInfo objectInfo = this.Session.GetObjectInfo(part.ID);
                  flag2 = flag2 && ((Approve) clone).InternalCheckAllSigned(silent, checkAll, objectInfo.Empty ? string.Empty : objectInfo.Caption);
                  if (!flag2)
                  {
                    if (!checkAll)
                      break;
                  }
                }
                catch (Exception ex)
                {
                  if (!checkAll)
                    throw;
                  empty += ex.Message;
                }
                if (!string.IsNullOrEmpty(empty))
                  throw new NotAllSignedException(empty);
              }
            }
            else if (approve.ParticipantID == part.ID)
            {
              if (approve.Status != ActivityStatus.OnApproach)
              {
                string empty = string.Empty;
                try
                {
                  QuickObjectInfo objectInfo = this.Session.GetObjectInfo(part.ID);
                  flag2 = flag2 && approve.InternalCheckAllSigned(silent, checkAll, objectInfo.Empty ? string.Empty : objectInfo.Caption);
                  if (!flag2)
                  {
                    if (!checkAll)
                      break;
                  }
                }
                catch (Exception ex)
                {
                  if (!checkAll)
                    throw;
                  empty += ex.Message;
                }
                if (!string.IsNullOrEmpty(empty))
                  throw new NotAllSignedException(empty);
              }
            }
            else
            {
              flag2 = flag2 && approve.InternalCheckAllSigned(silent, checkAll);
              if (!flag2)
              {
                if (!checkAll)
                  break;
              }
            }
          }
        }
        else
        {
          int index = -1;
          WFActivity wfActivity1 = (WFActivity) null;
          if (approve.ParentActivityID != 0L)
            wfActivity1 = this.Session.GetObject(approve.ParentActivityID, false) as WFActivity;
          if (wfActivity1 != null)
          {
            wfActivity1.GetRealClones(this.Process);
            List<WFActivity> all = wfActivity1.Clones.FindAll((Predicate<WFActivity>) (x => x.Status == ActivityStatus.Completed));
            if (all.Count > 0)
            {
              if (flag1)
                all.RemoveAt(all.Count - 1);
              if (all.Count > 0)
              {
                WFActivity wfActivity2 = all.Last<WFActivity>();
                index = wfActivity1.Clones.IndexOf(wfActivity2);
              }
            }
            if (index == -1)
            {
              flag2 = flag2 && approve.InternalCheckAllSigned(silent, checkAll);
              if (!flag2)
              {
                if (!checkAll)
                  break;
              }
            }
            else
            {
              WFActivity clone = wfActivity1.Clones[index];
              QuickObjectInfo objectInfo = this.Session.GetObjectInfo(clone.ParticipantID);
              participantIDs.Add(clone.ParticipantID);
              flag2 = flag2 && ((Approve) clone).InternalCheckAllSigned(silent, checkAll, objectInfo.Empty ? string.Empty : objectInfo.Caption);
              if (!flag2)
              {
                if (!checkAll)
                  break;
              }
            }
          }
          else
          {
            flag2 = flag2 && approve.InternalCheckAllSigned(silent, checkAll);
            if (!flag2)
            {
              if (!checkAll)
                break;
            }
          }
        }
      }
      else
      {
        flag2 = flag2 && approve.InternalCheckAllSigned(silent, checkAll);
        if (!flag2)
        {
          if (!checkAll)
            break;
        }
      }
    }
    return flag2;
  }

  private string ObjectSigned(
    Attachment att,
    long[] userIDs,
    bool checkAll,
    ISignsService signsrv)
  {
    string str1 = string.Empty;
    bool isGroupingObject = att.IsGroupingObject;
    bool flag1 = this.SignTypeIDs.Count == 0;
    if (!flag1)
    {
      foreach (int signTypeId in this.SignTypeIDs)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(att.TypeID, signTypeId))
        {
          flag1 = true;
          break;
        }
      }
    }
    foreach (long userId in userIDs)
    {
      if (flag1 && (!isGroupingObject && this.WhatToSign != WhatToSign.Revisions || isGroupingObject && this.WhatToSign != WhatToSign.Documents))
      {
        long objectId = att.ObjectID;
        bool flag2;
        if (!this.RequiredSigns.IsEmpty)
        {
          if (!this.RequirePersonalSigns)
            flag2 = signsrv.CheckSigns(new long[1]
            {
              objectId
            }, this.RequiredSigns.GraphsSet, this.Session.SessionGUID, false);
          else
            flag2 = signsrv.CheckSigns(new long[1]
            {
              objectId
            }, this.RequiredSigns.GraphsSet, userId, this.Session.SessionGUID, false);
        }
        else
          flag2 = signsrv.CheckSignsEx(new long[1]
          {
            objectId
          }, this.Session.SessionGUID, userId, true);
        if (!flag2)
        {
          str1 = MiscFunx.GetObjectCaption(this.Session, objectId);
          if (!this._unsignedObjects.Contains(objectId))
            this._unsignedObjects.Add(objectId);
        }
      }
      if ((checkAll || string.IsNullOrEmpty(str1)) && att.IsGroupingObject && att.InnerList.Count > 0 && this.WhatToSign != WhatToSign.Revisions)
      {
        string str2 = this.ObjectsSigned(att.InnerList, checkAll, signsrv);
        if (!string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str1))
          str1 += "\r\n";
        str1 += str2;
      }
    }
    return str1;
  }

  private string ObjectSigned(Attachment att, bool checkAll, ISignsService signsrv)
  {
    long[] userIDs = (long[]) null;
    if (this.TestOnly && (this.RequiredSigns.IsEmpty || this.RequirePersonalSigns))
      userIDs = this.Participants.ObjectIDs.ToArray<long>();
    if (userIDs == null)
      userIDs = new long[1]{ this.ParticipantID };
    return this.ObjectSigned(att, userIDs, checkAll, signsrv);
  }

  private string ObjectsSigned(
    AttachmentList attachs,
    bool checkAll,
    ISignsService signsrv,
    bool hasInvisibleItems = false)
  {
    string empty = string.Empty;
    if (hasInvisibleItems)
      empty += LocalizationHolder.rm.GetString("InvisibleObjectCaption");
    foreach (Attachment attach in (List<Attachment>) attachs)
    {
      string str = this.GraphForType ? this.CheckAllSignedForTypes(attach, checkAll, signsrv) : this.ObjectSigned(attach, checkAll, signsrv);
      if (!string.IsNullOrEmpty(str))
      {
        if (!string.IsNullOrEmpty(empty))
          empty += "\r\n";
        empty += str;
        if (!checkAll)
          break;
      }
    }
    return empty;
  }

  internal SignsDataItemModel IndividualSettingForTypes
  {
    get
    {
      if (this._signsDataItemModel == null)
      {
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrGraphForTypeID);
        if (attributeById != null)
        {
          XmlSerializer xmlSerializer = new XmlSerializer(typeof (SignsDataItemModel));
          string s = attributeById.Value.ToString();
          if (!string.IsNullOrEmpty(s))
          {
            using (TextReader textReader = (TextReader) new StringReader(s))
            {
              this._signsDataItemModel = xmlSerializer.Deserialize(textReader) as SignsDataItemModel;
              if (this._signsDataItemModel != null)
              {
                foreach (SignsDataItem node in (Collection<SignsDataItem>) this._signsDataItemModel.Nodes)
                  node.SetChild();
              }
            }
          }
        }
      }
      return this._signsDataItemModel;
    }
  }

  private string CheckAllSignedForTypes(
    Attachment attachment,
    bool checkAll,
    ISignsService signsService)
  {
    string empty = string.Empty;
    this._signsDataItemModel = (SignsDataItemModel) null;
    SignsDataItemModel individualSettingForTypes = this.IndividualSettingForTypes;
    if (individualSettingForTypes == null || individualSettingForTypes.Nodes.Count == 0)
      return empty + "Настройки подписей для типов объекта не найдены";
    long[] numArray = (long[]) null;
    if (this.TestOnly && individualSettingForTypes.PersonalSigns)
      numArray = this.Participants.ObjectIDs.ToArray<long>();
    if (numArray == null)
      numArray = new long[1]{ this.ParticipantID };
    SignsDataItem signsDataItem = individualSettingForTypes.GetSignsDataItem(attachment.TypeID);
    if (signsDataItem != null)
    {
      if (signsDataItem.SignAnyGraph)
      {
        foreach (long userID in numArray)
          this.AddToUnSignedIfNeeded(signsService.CheckSignsEx(new long[1]
          {
            attachment.ObjectID
          }, this.Session.SessionGUID, userID, true), ref empty, attachment.ObjectID);
      }
      else
      {
        GraphsSet gSet = new GraphsSet();
        foreach (SignsGroup group in (Collection<SignsGroup>) signsDataItem.Groups)
        {
          GraphsCollection graphsCollection = new GraphsCollection();
          foreach (SignsDataItemChildren child in (Collection<SignsDataItemChildren>) group.Children)
            graphsCollection.Add(new GraphClass(child.GraphForType, child.StrongControl, false));
          gSet.Add(group.GroupID.ToString(), graphsCollection);
        }
        if (individualSettingForTypes.PersonalSigns)
        {
          foreach (long userID in numArray)
            this.AddToUnSignedIfNeeded(signsService.CheckSigns(new long[1]
            {
              attachment.ObjectID
            }, gSet, userID, this.Session.SessionGUID, false), ref empty, attachment.ObjectID);
        }
        else
          this.AddToUnSignedIfNeeded(signsService.CheckSigns(new long[1]
          {
            attachment.ObjectID
          }, gSet, this.Session.SessionGUID, false), ref empty, attachment.ObjectID);
      }
      if ((checkAll || string.IsNullOrEmpty(empty)) && attachment.IsGroupingObject && attachment.InnerList.Count > 0)
      {
        string str = this.ObjectsSigned(attachment.InnerList, checkAll, signsService);
        if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(empty))
          empty += "\r\n";
        empty += str;
      }
    }
    else if (attachment.IsGroupingObject && attachment.InnerList.Count > 0)
    {
      string str = this.ObjectsSigned(attachment.InnerList, checkAll, signsService);
      if (!string.IsNullOrEmpty(str))
        empty += "\r\n";
      empty += str;
    }
    return empty;
  }

  private void AddToUnSignedIfNeeded(bool signed, ref string errMsg, long objectID)
  {
    if (signed)
      return;
    errMsg = MiscFunx.GetObjectCaption(this.Session, objectID);
    if (this._unsignedObjects.Contains(objectID))
      return;
    this._unsignedObjects.Add(objectID);
  }

  internal override void PrepareActivity()
  {
    this.SkipParticipantsExec = this.TestOnly;
    base.PrepareActivity();
    this._autoStep = this.TestOnly | this.CheckAllSigned(!this.TestOnly, out HashSet<long> _, false);
    if (!this._autoStep)
      return;
    this.Attributes.AddAttribute(wfConsts.AutoExecuteAttributeID, false, new object[1]
    {
      (object) true
    });
  }

  public override void ValidateParticipants(ref string s)
  {
    bool flag = !this.TestOnly;
    if (!flag)
      flag = this.AlienSettingsActs.Count == 0 && (this.RequiredSigns.IsEmpty || this.RequirePersonalSigns);
    if (!flag)
      return;
    base.ValidateParticipants(ref s);
  }

  public override string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null)
  {
    string s = base.Validate(checkSubProcessSchemes, checkedSchemesList);
    IMSAttributeType atype = (IMSAttributeType) null;
    foreach (string graphs in this.RequiredSigns.GraphsSet)
    {
      foreach (GraphClass graphClass in this.RequiredSigns.GraphsSet[graphs])
      {
        if (MiscFunx.GetSignGraphCaption(graphClass.Value, ref atype, true) == null)
        {
          MiscFunx.AddNewLined(ref s, string.Format(LocalizationHolder.GetString(sc_22131.ssp_workflow_server_22133()), (object) this.Name));
          break;
        }
      }
    }
    this._signTypeIDs = this.SignTypeIDs;
    if (this._hasInvalidTypes || this.IndividualSettingForTypes != null && this.IndividualSettingForTypes.HasInvalidTypes)
      MiscFunx.AddNewLined(ref s, string.Format(LocalizationHolder.GetString(sc_22131.ssp_workflow_server_22134()), (object) this.Name));
    return s;
  }

  protected override bool NeedToSendWorkOffer(ParticipantList parts)
  {
    bool sendWorkOffer = this.Attachments.Count != 0;
    HashSet<long> participantIDs = new HashSet<long>(0);
    if (sendWorkOffer && !this.RequiredSigns.IsEmpty)
      sendWorkOffer = !this.CheckAllSigned(true, out participantIDs, false);
    if (!sendWorkOffer)
    {
      if (this.RequirePersonalSigns)
      {
        if (participantIDs == null || participantIDs.Count <= 0)
          return true;
        parts.Clear();
        foreach (long ID in participantIDs)
          parts.AddParticipant(ParticipantKind.User, ID);
        return false;
      }
      parts.Clear();
      parts.AddParticipant(ParticipantKind.User, wfConsts.SystemUserID);
      this.AllowSystemParticipant = true;
    }
    return sendWorkOffer;
  }

  internal override void NextStep(bool goNext)
  {
    try
    {
      if (goNext && !this.Flags.HasFlag((Enum) ActivityFlags.SignsChecked))
        this.CheckAllSigned(false, out HashSet<long> _, false);
      base.NextStep(goNext);
    }
    finally
    {
      if (this.Flags.HasFlag((Enum) ActivityFlags.SignsChecked))
        this.Flags ^= ActivityFlags.SignsChecked;
    }
  }

  public List<long> GetUnsignedObjects()
  {
    this.CheckAllSigned(true, out HashSet<long> _, true);
    return this._unsignedObjects;
  }
}
