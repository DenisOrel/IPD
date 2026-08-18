// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeTest
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Interfaces.AutoSelection;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

[TypeConverter(typeof (AutoSelectionNodeTestConverter))]
internal class AutoSelectionNodeTest : AutoSelectionNodeBase
{
  protected AS_Guid _objectTypeGuid;
  protected AS_Long _objectID;
  protected AS_Long _imbaseObjectID;
  protected readonly AutoSelAttrValList _defObjAttrList;
  protected AutoSelectionMode _mode;
  protected AutoSelectionTestObjectMode _objectMode;

  public AutoSelectionNodeTest(Guid objectType, long imbaseObjectId)
    : base((AutoSelectionNodeBase) null, string.Empty)
  {
    this._objectTypeGuid = new AS_Guid(objectType);
    this._imbaseObjectID = new AS_Long(imbaseObjectId);
    this._objectID = new AS_Long();
    this._defObjAttrList = new AutoSelAttrValList(AutoSelAttrTypeMode.asatObjectType, (AutoSelectionNodeItemFillAttributes) null);
  }

  public override AutoSelExecuteStatus Execute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    throw new NotSupportedException();
  }

  [Browsable(false)]
  public override string Name
  {
    get => this._name;
    set => this._name = value;
  }

  [Browsable(false)]
  public override int Order
  {
    get => this._order;
    set => this._order = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_88")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_22")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_45")]
  [TypeConverter(typeof (ObjectTypeConverter))]
  [ReadOnly(true)]
  public AS_Guid ObjectType
  {
    get => this._objectTypeGuid;
    set => this._objectTypeGuid = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_88")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_46")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_47")]
  [RefreshProperties(RefreshProperties.All)]
  public AutoSelectionTestObjectMode ObjectMode
  {
    get => this._objectMode;
    set => this._objectMode = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_88")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_48")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_49")]
  [TypeConverter(typeof (SelectionLongObjectConverter))]
  [Editor(typeof (SelectionTestObjectEditor), typeof (UITypeEditor))]
  public AS_Long ObjectID
  {
    get => this._objectID;
    set => this._objectID = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_88")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_50")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_51")]
  [TypeConverter(typeof (SelectionLongObjectConverter))]
  [ReadOnly(true)]
  public AS_Long ImbaseObjectID
  {
    get => this._imbaseObjectID;
    set => this._imbaseObjectID = value;
  }

  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_28")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_52")]
  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_53")]
  [TypeConverter(typeof (AutoSelAttrCollTypeConverter))]
  [Editor(typeof (AutoSelAttrCollEditor), typeof (UITypeEditor))]
  public AutoSelAttrValList DefObjAttrList => this._defObjAttrList;

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_88")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_54")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_55")]
  public AutoSelectionMode Mode
  {
    get => this._mode;
    set => this._mode = value;
  }
}
