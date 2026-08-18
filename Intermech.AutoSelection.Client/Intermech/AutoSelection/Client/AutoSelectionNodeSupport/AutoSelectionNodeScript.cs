// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNodeSupport.AutoSelectionNodeScript
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNodeSupport;

public class AutoSelectionNodeScript : AutoSelectionNodeCommon
{
  private AS_Guid _extScriptGuid;
  private string _extScriptCaption = string.Empty;

  private void InitializeData() => this._type = AutoSelectionNodeType.ScriptCall;

  public AutoSelectionNodeScript(AutoSelectionNodeBase ownerNode, string name)
    : base(ownerNode, name)
  {
    this._extScriptGuid = new AS_Guid();
    this.InitializeData();
  }

  public override XmlNode SaveData(XmlDocument doc)
  {
    XmlNode xmlNode = base.SaveData(doc);
    if (this._extScriptGuid != null && !this._extScriptGuid.Equals((object) Guid.Empty))
    {
      XmlAttribute attribute = doc.CreateAttribute("ScriptGuid");
      attribute.Value = this._extScriptGuid.ToString();
      xmlNode.Attributes.Append(attribute);
    }
    XmlAttribute attribute1 = doc.CreateAttribute("ScriptCaption");
    attribute1.Value = this._extScriptCaption;
    xmlNode.Attributes.Append(attribute1);
    return xmlNode;
  }

  public override AutoSelectionNodeCommon LoadData(XmlNode node)
  {
    if (node?.Attributes == null || base.LoadData(node) == null)
      return (AutoSelectionNodeCommon) null;
    XmlAttribute attribute1 = node.Attributes["ScriptGuid"];
    if (attribute1 != null)
      this._extScriptGuid = new AS_Guid(new Guid(attribute1.Value));
    XmlAttribute attribute2 = node.Attributes["ScriptCaption"];
    if (attribute2 != null)
      this._extScriptCaption = attribute2.Value;
    return (AutoSelectionNodeCommon) this;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_87")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_96")]
  [TypeConverter(typeof (SelectionGuidObjectConverter))]
  [Editor(typeof (SelectionScriptObjectEditor), typeof (UITypeEditor))]
  public AS_Guid ExtScriptGuid
  {
    get => this._extScriptGuid;
    set => this.SetExtScriptGuid(value, true);
  }

  protected internal override void CollectLinks(
    Dictionary<long, int> id2Types,
    Dictionary<Guid, int> objGuid2Types)
  {
    if (this.ExtScriptGuid == null || !(this.ExtScriptGuid.Value != Guid.Empty))
      return;
    objGuid2Types[this.ExtScriptGuid.Value] = AutoSelectionConsts.objTypeScriptID;
  }

  protected internal override void UpdateLinks(
    Dictionary<long, string> id2Caption,
    Dictionary<Guid, string> guid2Caption)
  {
    if (this.ExtScriptGuid == null || !guid2Caption.ContainsKey(this.ExtScriptGuid.Value))
      return;
    this._extScriptCaption = guid2Caption[this.ExtScriptGuid.Value];
  }

  protected override string GetShortInfo()
  {
    return this._extScriptCaption != string.Empty ? this._extScriptCaption ?? "" : base.GetShortInfo();
  }

  public override string ToString()
  {
    return this._extScriptCaption != string.Empty ? $"{EnumDescConverter.GetEnumDescription((Enum) this.Type)}({this._extScriptCaption})" : base.ToString();
  }

  protected override AutoSelExecuteStatus DoExecute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    AutoSelExecuteStatus selExecuteStatus = base.DoExecute(asSession, logRec);
    if (selExecuteStatus != AutoSelExecuteStatus.Applied)
      return selExecuteStatus;
    if (this._extScriptGuid.Value == Guid.Empty)
    {
      asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_93"));
      return AutoSelExecuteStatus.Skipped;
    }
    string scriptCode = (string) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._extScriptGuid.Value, false);
      if (dbObject == null)
      {
        asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_94"), (object) this._extScriptGuid));
        return selExecuteStatus;
      }
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00366-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid != null)
      {
        object obj = attributeByGuid.Value;
        scriptCode = obj != null ? obj.ToString().Trim() : string.Empty;
      }
    }
    if (string.IsNullOrEmpty(scriptCode))
      return selExecuteStatus;
    asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_95"));
    ServiceContainer serviceContainer = new ServiceContainer();
    serviceContainer.AddService(typeof (AutoSelectionLogRec), (object) logRec);
    serviceContainer.AddService(typeof (AutoSelectionSession), (object) asSession);
    serviceContainer.AddService(typeof (AutoSelectionNodeBase), (object) this);
    serviceContainer.AddService(typeof (AutoSelectionNodeScript), (object) this);
    serviceContainer.AddService(typeof (AutoSelectionNodeCommon), (object) this);
    List<AutoSelectionObject> serviceInstance = new List<AutoSelectionObject>();
    for (AutoSelectionNodeBase ownerNode = this.OwnerNode; ownerNode != null; ownerNode = ownerNode.OwnerNode)
    {
      foreach (AutoSelectionObject createdObject in asSession.CreatedObjectList)
      {
        if (createdObject.Node == ownerNode)
          serviceInstance.Add(createdObject);
      }
      if (serviceInstance.Count != 0)
        break;
    }
    serviceContainer.AddService(typeof (IEnumerable<AutoSelectionObject>), (object) serviceInstance);
    try
    {
      selExecuteStatus = (AutoSelExecuteStatus) ServiceUtils.GetService<ICSharpScriptExecutor>((object) ApplicationServices.Container, false).Execute(scriptCode, CSharpScriptInvocationOptions.Default, (object) asSession, (object) serviceContainer);
    }
    catch (Exception ex)
    {
      if (ex is ISimpleMessageException)
      {
        string str = Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_651.ssp_automatch_652());
        asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, str + ex.Message);
      }
      else
      {
        string str = Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_651.ssp_automatch_653());
        asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, str + ex.Message + Environment.NewLine + ex.StackTrace);
        throw;
      }
    }
    if (selExecuteStatus != AutoSelExecuteStatus.Applied && selExecuteStatus == AutoSelExecuteStatus.AbortAll)
      asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_13"));
    return selExecuteStatus;
  }

  private void SetExtScriptGuid(AS_Guid value, bool updateLinkMode)
  {
    if (object.Equals((object) this._extScriptGuid, (object) value))
      return;
    this._extScriptGuid = value;
    if (!updateLinkMode)
      return;
    AutoSelectionUtils.Common.UpdateNodesLinkCaptions(new List<AutoSelectionNodeBase>()
    {
      (AutoSelectionNodeBase) this
    });
  }
}
