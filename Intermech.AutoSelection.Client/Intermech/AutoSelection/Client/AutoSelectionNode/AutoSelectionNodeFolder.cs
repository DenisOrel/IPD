// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeFolder
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionNode.Forms;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Expert;
using Intermech.Expert.Table;
using Intermech.Extensions.WinForms;
using Intermech.Interfaces;
using Intermech.Interfaces.Expert;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

[TypeConverter(typeof (AutoSelectionNodeFolderConverter))]
public class AutoSelectionNodeFolder : AutoSelectionNodeCommon
{
  private AutoSelectionFolderType _folderType;
  private eTable[] _expTables;

  private void InitializeData() => this._type = AutoSelectionNodeType.Folder;

  protected virtual AutoSelExecuteStatus DoExecuteSimpleFolder(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    return AutoSelExecuteStatus.Applied;
  }

  protected virtual AutoSelExecuteStatus DoExecuteDialogFolder(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec,
    bool multiSelect = false)
  {
    this.DoExecuteCheckArgs(asSession, logRec);
    if (this.ChildsNodes.Count == 0)
    {
      asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, LocalizationHolder.rm.GetString("AutoSelection.Client_7"));
      return AutoSelExecuteStatus.Skipped;
    }
    using (AutoSelectionTreeSelectForm form = new AutoSelectionTreeSelectForm(asSession.Params.ObjectID, (List<AutoSelectionNodeCommon>) this.ChildsNodes, multiSelect))
    {
      if (!form.ShowTopDialog().Equals((object) DialogResult.OK))
        return AutoSelExecuteStatus.Skipped;
      foreach (AutoSelectionNodeBase selectedNode in form.SelectedNodes)
      {
        AutoSelExecuteStatus selExecuteStatus = selectedNode.Execute(asSession, logRec);
        switch (selExecuteStatus)
        {
          case AutoSelExecuteStatus.SkipOwnerLevel:
            return AutoSelExecuteStatus.Skipped;
          case AutoSelExecuteStatus.AbortAll:
            return selExecuteStatus;
          default:
            continue;
        }
      }
      return AutoSelExecuteStatus.Applied;
    }
  }

  protected virtual AutoSelExecuteStatus DoExecuteSelectFolder(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec,
    bool multiSelect = false)
  {
    this.DoExecuteCheckArgs(asSession, logRec);
    eTable[] expTables = this.ExpTables;
    if (expTables != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        eTableCollection tableCollection = new eTableCollection(expTables);
        IExpertServer expertServerService = AutoSelectionUtils.ServiceKeeper.GetExpertServerService(sessionKeeper.Session);
        object[] Values;
        if (!expertServerService.CalcTable(expertServerService.StartTask(sessionKeeper.Session.SessionGUID), (object) tableCollection, asSession.Params.ObjectID, out Values).Equals((object) ExpertResult.OK) || Values.Length == 0)
          return AutoSelExecuteStatus.Skipped;
        ResultExpertValue resultExpertValue = Values.Cast<ResultExpertValue>().Where<ResultExpertValue>((Func<ResultExpertValue, bool>) (expValue => expValue != null)).ToList<ResultExpertValue>()[0];
        if (resultExpertValue.AttributeTypeGuid.Equals(AutosSelectConsts.ImbaseObjectLinkAttrGuid))
        {
          if (resultExpertValue.Value.ValueType.Equals((object) DataType.ObjectLink))
          {
            long int64 = Convert.ToInt64(resultExpertValue.Value.Value);
            if (int64 == 0L)
              return AutoSelExecuteStatus.Skipped;
            foreach (AutoSelectionNodeCommon childsNode in (List<AutoSelectionNodeCommon>) this.ChildsNodes)
            {
              if (childsNode is AutoSelectionNodeItemImbase selectionNodeItemImbase && selectionNodeItemImbase.ImbaseObjectID.Value == int64)
              {
                AutoSelExecuteStatus selExecuteStatus = selectionNodeItemImbase.Execute(asSession, logRec);
                switch (selExecuteStatus)
                {
                  case AutoSelExecuteStatus.Applied:
                  case AutoSelExecuteStatus.AbortAll:
                    return selExecuteStatus;
                  case AutoSelExecuteStatus.SkipOwnerLevel:
                    return AutoSelExecuteStatus.Skipped;
                  default:
                    return selExecuteStatus;
                }
              }
            }
          }
        }
      }
    }
    else
    {
      if (this.ChildsNodes.Count == 0)
        return AutoSelExecuteStatus.Skipped;
      foreach (AutoSelectionNodeBase childsNode in (List<AutoSelectionNodeCommon>) this.ChildsNodes)
      {
        AutoSelExecuteStatus selExecuteStatus = childsNode.Execute(asSession, logRec);
        switch (selExecuteStatus)
        {
          case AutoSelExecuteStatus.Applied:
          case AutoSelExecuteStatus.AbortAll:
            return selExecuteStatus;
          case AutoSelExecuteStatus.Skipped:
            continue;
          case AutoSelExecuteStatus.SkipOwnerLevel:
            return AutoSelExecuteStatus.Skipped;
          default:
            return selExecuteStatus;
        }
      }
    }
    return AutoSelExecuteStatus.Applied;
  }

  public AutoSelectionNodeFolder(AutoSelectionNodeBase ownerNode, string name)
    : base(ownerNode, name)
  {
    this.InitializeData();
  }

  public override XmlNode SaveData(XmlDocument doc)
  {
    XmlNode xmlNode = base.SaveData(doc);
    XmlNode newChild = AutoSelEnumUtils.Save("FolderType", (int) this._folderType, EnumTypeHelper.GetCaption((Enum) this._folderType), doc);
    xmlNode.AppendChild(newChild);
    if (this._expTables != null)
    {
      eTableCollection graph = new eTableCollection(this._expTables);
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter()
        {
          AssemblyFormat = FormatterAssemblyStyle.Simple
        }.Serialize((Stream) serializationStream, (object) graph);
        XmlAttribute attribute = doc.CreateAttribute("ExpTables");
        attribute.Value = Convert.ToBase64String(serializationStream.ToArray());
        xmlNode.Attributes.Append(attribute);
      }
    }
    return xmlNode;
  }

  public override AutoSelectionNodeCommon LoadData(XmlNode node)
  {
    if (node == null || base.LoadData(node) == null)
      return (AutoSelectionNodeCommon) null;
    int id;
    AutoSelEnumUtils.Load("FolderType", node, out id);
    this._folderType = (AutoSelectionFolderType) id;
    XmlAttribute attribute = node.Attributes?["ExpTables"];
    if (attribute != null)
    {
      try
      {
        using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(attribute.Value)))
        {
          if (new BinaryFormatter()
          {
            AssemblyFormat = FormatterAssemblyStyle.Simple
          }.Deserialize((Stream) serializationStream) is eTableCollection eTableCollection)
            this.ExpTables = eTableCollection.Tables;
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("AutoSelection.Client_6") + ex.Message, LocalizationHolder.rm.GetString("AutoSelection.Client_2"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return (AutoSelectionNodeCommon) null;
      }
    }
    return (AutoSelectionNodeCommon) this;
  }

  [CustomCategory("Attribute.AutoSelection.Client_87")]
  [CustomDisplayName("Attribute.AutoSelection.Client_12")]
  [CustomDescription("Attribute.AutoSelection.Client_13")]
  [RefreshProperties(RefreshProperties.All)]
  public AutoSelectionFolderType FolderType
  {
    get => this._folderType;
    set => this._folderType = value;
  }

  [CustomCategory("Attribute.AutoSelection.Client_87")]
  [CustomDisplayName("Attribute.AutoSelection.Client_14")]
  [CustomDescription("Attribute.AutoSelection.Client_15")]
  [TypeConverter(typeof (SelectionExpTableConverter))]
  [Editor(typeof (SelectionExpTableEditor), typeof (UITypeEditor))]
  public eTable[] ExpTables
  {
    get => this._expTables;
    set => this._expTables = value;
  }

  protected override AutoSelExecuteStatus DoExecute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    AutoSelExecuteStatus selExecuteStatus = base.DoExecute(asSession, logRec);
    if (selExecuteStatus != AutoSelExecuteStatus.Applied)
      return selExecuteStatus;
    switch (this.FolderType)
    {
      case AutoSelectionFolderType.SimpleFolder:
        selExecuteStatus = this.DoExecuteSimpleFolder(asSession, logRec);
        break;
      case AutoSelectionFolderType.SelectFolder:
        selExecuteStatus = this.DoExecuteSelectFolder(asSession, logRec);
        break;
      case AutoSelectionFolderType.DialogFolder:
        selExecuteStatus = this.DoExecuteDialogFolder(asSession, logRec);
        break;
      case AutoSelectionFolderType.MultiSelectFolder:
        selExecuteStatus = this.DoExecuteDialogFolder(asSession, logRec, true);
        break;
    }
    return selExecuteStatus;
  }

  protected override AutoSelExecuteStatus DoExecuteChildNodes(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    bool flag = false;
    switch (this.FolderType)
    {
      case AutoSelectionFolderType.SelectFolder:
        flag = true;
        break;
      case AutoSelectionFolderType.DialogFolder:
      case AutoSelectionFolderType.MultiSelectFolder:
        flag = true;
        break;
    }
    return flag ? AutoSelExecuteStatus.Applied : base.DoExecuteChildNodes(asSession, logRec);
  }

  protected override string GetShortInfo()
  {
    return this.FolderType.Equals((object) AutoSelectionFolderType.SimpleFolder) ? base.GetShortInfo() : this.Name;
  }

  public override string ToString()
  {
    return this.FolderType.Equals((object) AutoSelectionFolderType.SimpleFolder) ? base.ToString() : $"{EnumDescConverter.GetEnumDescription((Enum) this.FolderType)}({this.Name})";
  }
}
