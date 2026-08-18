// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeProc
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelectionNodeProc : AutoSelectionNodeCommon
{
  private AS_Guid _extProcGuid;
  private string _extProcCaption = string.Empty;

  private void InitializeData() => this._type = AutoSelectionNodeType.ProcCall;

  public AutoSelectionNodeProc(AutoSelectionNodeBase ownerNode, string name)
    : base(ownerNode, name)
  {
    this._extProcGuid = new AS_Guid();
    this.InitializeData();
  }

  public override XmlNode SaveData(XmlDocument doc)
  {
    XmlNode xmlNode = base.SaveData(doc);
    if (this._extProcGuid != null && !this._extProcGuid.Equals((object) Guid.Empty))
    {
      XmlAttribute attribute = doc.CreateAttribute("ProcGuid");
      attribute.Value = this._extProcGuid.ToString();
      xmlNode.Attributes.Append(attribute);
    }
    XmlAttribute attribute1 = doc.CreateAttribute("ProcCaption");
    attribute1.Value = this._extProcCaption;
    xmlNode.Attributes.Append(attribute1);
    return xmlNode;
  }

  public override AutoSelectionNodeCommon LoadData(XmlNode node)
  {
    if (node?.Attributes == null || base.LoadData(node) == null)
      return (AutoSelectionNodeCommon) null;
    XmlAttribute attribute1 = node.Attributes["ProcGuid"];
    if (attribute1 != null)
      this._extProcGuid = new AS_Guid(new Guid(attribute1.Value));
    XmlAttribute attribute2 = node.Attributes["ProcCaption"];
    if (attribute2 != null)
      this._extProcCaption = attribute2.Value;
    return (AutoSelectionNodeCommon) this;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_87")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_43")]
  [TypeConverter(typeof (SelectionGuidObjectConverter))]
  [Editor(typeof (SelectionRuleObjectEditor), typeof (UITypeEditor))]
  public AS_Guid ExtProcGuid
  {
    get => this._extProcGuid;
    set => this.SetExtProcGuid(value, true);
  }

  protected internal override void CollectLinks(
    Dictionary<long, int> id2Types,
    Dictionary<Guid, int> objGuid2Types)
  {
    if (this.ExtProcGuid == null || !(this.ExtProcGuid.Value != Guid.Empty))
      return;
    objGuid2Types.Add(this.ExtProcGuid.Value, AutoSelectionConsts.objTypeRuleID);
  }

  protected internal override void UpdateLinks(
    Dictionary<long, string> id2Caption,
    Dictionary<Guid, string> guid2Caption)
  {
    if (this.ExtProcGuid == null || !guid2Caption.ContainsKey(this.ExtProcGuid.Value))
      return;
    this._extProcCaption = guid2Caption[this.ExtProcGuid.Value];
  }

  protected override string GetShortInfo()
  {
    return this._extProcCaption != string.Empty ? this._extProcCaption ?? "" : base.GetShortInfo();
  }

  public override string ToString()
  {
    return this._extProcCaption != string.Empty ? $"{EnumDescConverter.GetEnumDescription((Enum) this.Type)}({this._extProcCaption})" : base.ToString();
  }

  protected override AutoSelExecuteStatus DoExecute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    AutoSelExecuteStatus selExecuteStatus1 = base.DoExecute(asSession, logRec);
    if (selExecuteStatus1 != AutoSelExecuteStatus.Applied)
      return selExecuteStatus1;
    if (this._extProcGuid.Value == Guid.Empty)
    {
      asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_10"));
      return AutoSelExecuteStatus.Skipped;
    }
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule autoSelectionRule;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._extProcGuid.Value, false);
      if (dbObject == null)
      {
        asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_11"), (object) this._extProcGuid));
        return selExecuteStatus1;
      }
      autoSelectionRule = Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule.Load(dbObject);
    }
    if (autoSelectionRule == null)
      return selExecuteStatus1;
    asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_12"));
    AutoSelExecuteStatus selExecuteStatus2 = autoSelectionRule.Execute(asSession, logRec);
    switch (selExecuteStatus2)
    {
      case AutoSelExecuteStatus.AbortAll:
        asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_13"));
        break;
    }
    return selExecuteStatus2;
  }

  public void SetExtProcGuid(AS_Guid value, bool updateLinkMode)
  {
    if (object.Equals((object) this._extProcGuid, (object) value))
      return;
    this._extProcGuid = value;
    if (!updateLinkMode)
      return;
    AutoSelectionUtils.Common.UpdateNodesLinkCaptions(new List<AutoSelectionNodeBase>()
    {
      (AutoSelectionNodeBase) this
    });
  }
}
