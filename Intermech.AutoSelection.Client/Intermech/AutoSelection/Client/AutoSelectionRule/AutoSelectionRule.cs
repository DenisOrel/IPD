// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionRule;

public class AutoSelectionRule : AutoSelectionNodeBase, ICloneable
{
  private long _ruleId;
  private AutoSelectionMode _mode;
  private Guid _objType;
  private Guid _attrType = Guid.Empty;

  private void InitializeData() => this._order = 0;

  public AutoSelectionRule()
    : this(Guid.Empty)
  {
    this.InitializeData();
  }

  public AutoSelectionRule(Guid objType)
    : base((AutoSelectionNodeBase) null, string.Empty)
  {
    this._objType = objType;
  }

  public static Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule Load(
    IDBObject dbObject)
  {
    IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(AutoSelectionConsts.attrTypeData, false);
    if (attributeByGuid1 == null)
      return (Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule) null;
    using (MemoryStream aDestStream = new MemoryStream())
    {
      BlobProcReader blobProcReader;
      try
      {
        blobProcReader = new BlobProcReader(attributeByGuid1, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
        blobProcReader.ReadData();
      }
      finally
      {
        aDestStream.Close();
      }
      Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule autoSelectionRule = (Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule) null;
      if (blobProcReader.Result)
      {
        autoSelectionRule = Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule.Load(aDestStream.ToArray());
        if (autoSelectionRule == null)
          return (Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule) null;
        autoSelectionRule.RuleID = dbObject.ObjectID;
        IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(AutosSelectConsts.AutoSelectionModeAttrGuid);
        if (attributeByGuid2 != null && !attributeByGuid2.IsNull)
          autoSelectionRule.Mode = (AutoSelectionMode) EnumTypeHelper.GetEnumValue(typeof (AutoSelectionMode), attributeByGuid2.AsString, (object) AutoSelectionMode.Manual);
        IDBAttribute attributeByGuid3 = dbObject.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid3 != null && !attributeByGuid3.IsNull)
          autoSelectionRule.Name = attributeByGuid3.AsString;
        IDBAttribute attributeByGuid4 = dbObject.GetAttributeByGuid(new Guid("cad001a0-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid4 != null && !attributeByGuid4.IsNull)
          autoSelectionRule._objType = new Guid(attributeByGuid4.AsString);
        IDBAttribute attributeByGuid5 = dbObject.GetAttributeByGuid(new Guid("cad001d0-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid5 != null && !attributeByGuid5.IsNull)
          autoSelectionRule._attrType = new Guid(attributeByGuid5.AsString);
      }
      return autoSelectionRule;
    }
  }

  public static Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule Load(byte[] data)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (MemoryStream inStream = new MemoryStream(data))
    {
      if (inStream.Length != 0L)
        xmlDocument.Load((Stream) inStream);
    }
    return new Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule().LoadData(xmlDocument.FirstChild, true);
  }

  private AttributeValues GetMetadataGuidsUsedIndirectly(IMSGlobals type, Guid multiValAttrGuid)
  {
    List<Guid> guidList = new List<Guid>();
    this.CollectMetaDataGuids(type, (ICollection<Guid>) guidList);
    object[] objArray;
    if (!guidList.IsEmpty<Guid>())
      objArray = guidList.ConvertAll<object>((Converter<Guid, object>) (g => (object) g)).ToArray();
    else
      objArray = new object[1]{ (object) DBNull.Value };
    object[] initValue = objArray;
    return new AttributeValues(MetaDataHelper.GetAttributeID((object) multiValAttrGuid), (object) initValue);
  }

  private void AddMetadataGuidsUsedIndirectly(ICollection<AttributeValues> collection)
  {
    \u003C\u003Ef__AnonymousType0<IMSGlobals, string>[] dataArray = new \u003C\u003Ef__AnonymousType0<IMSGlobals, string>[3]
    {
      new
      {
        type = IMSGlobals.IMSAttributeType,
        guid = "cadd9c03-306c-11d8-b4e9-00304f19f545"
      },
      new
      {
        type = IMSGlobals.IMSObjectType,
        guid = "cad00149-306c-11d8-b4e9-00304f19f545"
      },
      new
      {
        type = IMSGlobals.IMSRelationType,
        guid = "cad0014a-306c-11d8-b4e9-00304f19f545"
      }
    };
    foreach (var data in dataArray)
    {
      AttributeValues guidsUsedIndirectly = this.GetMetadataGuidsUsedIndirectly(data.type, new Guid(data.guid));
      collection.Add(guidsUsedIndirectly);
    }
  }

  public virtual void Save(IDBObject dbObject, IUserSession session)
  {
    if (dbObject == null)
      return;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(AutoSelectionConsts.attrTypeData);
    if (attributeTypeId == -10000)
      return;
    int attributeId1 = MetaDataHelper.GetAttributeID((object) AutosSelectConsts.AutoSelectionModeAttrGuid);
    int attributeId2 = MetaDataHelper.GetAttributeID((object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
    List<AttributeValues> collection = new List<AttributeValues>()
    {
      new AttributeValues(attributeId1, (object) EnumTypeHelper.GetCaption((Enum) this.Mode)),
      new AttributeValues(attributeId2, (object) this.Name)
    };
    this.AddMetadataGuidsUsedIndirectly((ICollection<AttributeValues>) collection);
    dbObject.SetAttributesValues(collection.ToArray());
    using (MemoryStream aSourceStream = new MemoryStream(this.Save()))
    {
      try
      {
        aSourceStream.Position = 0L;
        BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, "AutoSelectionRule.xml", ArcMethods.ZLibPacked, string.Empty);
        new BlobProcWriter(dbObject.ObjectID, AttributableElements.Object, attributeTypeId, 0, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData(session, false);
      }
      finally
      {
        aSourceStream.Close();
      }
    }
  }

  public virtual byte[] Save()
  {
    XmlDocument doc = new XmlDocument();
    XmlNode newChild = this.SaveData(doc);
    doc.AppendChild(newChild);
    using (MemoryStream outStream = new MemoryStream())
    {
      doc.Save((Stream) outStream);
      return outStream.ToArray();
    }
  }

  public virtual XmlNode SaveData(XmlDocument doc)
  {
    XmlNode element = (XmlNode) doc.CreateElement(nameof (AutoSelectionRule));
    foreach (AutoSelectionNodeCommon childsNode in (List<AutoSelectionNodeCommon>) this.ChildsNodes)
      element.AppendChild(childsNode.SaveData(doc));
    return element;
  }

  public virtual Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule LoadData(
    XmlNode node)
  {
    return this.LoadData(node, false);
  }

  public virtual Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule LoadData(
    XmlNode node,
    bool updateLinkMode)
  {
    if (node == null || !node.Name.Equals(nameof (AutoSelectionRule)))
      return (Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule) null;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name.Equals("Node"))
      {
        AutoSelectionNodeCommon selNode = AutoSelectionNodeCommon.Load((AutoSelectionNodeBase) this, childNode);
        if (selNode != null)
          this.ChildsNodes.Add(selNode);
      }
    }
    if (updateLinkMode)
      AutoSelectionUtils.Common.UpdateNodesLinkCaptions(this.CollectChildNodes(true));
    return this;
  }

  public override AutoSelExecuteStatus Execute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    this.DoExecuteCheckArgs(asSession, logRec);
    AutoSelectionLogRec autoSelectionLogRec = asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_17"), (object) this.Name));
    if (this.ChildsNodes.Count == 0)
    {
      asSession.SelectionLog.AddRec(autoSelectionLogRec, (AutoSelectionNodeBase) null, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_18"));
      return AutoSelExecuteStatus.Skipped;
    }
    asSession.SelectionLog.AddRec(autoSelectionLogRec, (AutoSelectionNodeBase) null, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_19"));
    AutoSelExecuteStatus selExecuteStatus1 = AutoSelExecuteStatus.Applied;
    foreach (AutoSelectionNodeBase childsNode in (List<AutoSelectionNodeCommon>) this.ChildsNodes)
    {
      AutoSelExecuteStatus selExecuteStatus2 = childsNode.Execute(asSession, autoSelectionLogRec);
      switch (selExecuteStatus2)
      {
        case AutoSelExecuteStatus.AbortAll:
          asSession.SelectionLog.AddRec(autoSelectionLogRec, (AutoSelectionNodeBase) null, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_13"));
          return selExecuteStatus2;
        default:
          continue;
      }
    }
    asSession.SelectionLog.AddRec(autoSelectionLogRec, (AutoSelectionNodeBase) null, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_20"));
    return selExecuteStatus1;
  }

  public object Clone()
  {
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule autoSelectionRule = Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule.Load(this.Save());
    autoSelectionRule.RuleID = this.RuleID;
    autoSelectionRule.Name = this.Name;
    autoSelectionRule.Mode = this.Mode;
    autoSelectionRule._objType = this.ObjectType;
    autoSelectionRule._attrType = this.AttributeType;
    autoSelectionRule._order = this.Order;
    return (object) autoSelectionRule;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_56")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_57")]
  [ReadOnly(true)]
  public long RuleID
  {
    get => this._ruleId;
    set => this._ruleId = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_56")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_58")]
  public override string Name
  {
    get => this._name;
    set => this._name = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_56")]
  [ReadOnly(true)]
  public override int Order
  {
    get => this._order;
    set => this._order = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_56")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_59")]
  [TypeConverter(typeof (EnumDescConverter))]
  public AutoSelectionMode Mode
  {
    get => this._mode;
    set => this._mode = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_56")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_22")]
  [TypeConverter(typeof (ObjectTypeConverter))]
  [ReadOnly(true)]
  public Guid ObjectType => this._objType;

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_56")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_60")]
  [TypeConverter(typeof (AttributeTypeConverter))]
  [ReadOnly(true)]
  public Guid AttributeType
  {
    get => this._attrType;
    set => this._attrType = value;
  }
}
