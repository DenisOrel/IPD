// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelAttrValList
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

[TypeConverter(typeof (AutoSelAttrCollTypeConverter))]
[Editor(typeof (AutoSelAttrCollEditor), typeof (UITypeEditor))]
[DefaultValue(null)]
public class AutoSelAttrValList : List<AutoSelAttrVal>, IAutoSelAttrType, IImsGlobalsSupport
{
  public AutoSelAttrValList(
    AutoSelAttrTypeMode typeMode,
    AutoSelectionNodeItemFillAttributes ownerObject)
  {
    this.TypeMode = typeMode;
    this.OwnerObject = ownerObject;
  }

  public virtual XmlNode Save(string nodeName, XmlDocument doc)
  {
    if (this.Count == 0)
      return (XmlNode) null;
    XmlNode element = (XmlNode) doc.CreateElement(nodeName);
    foreach (AutoSelAttr autoSelAttr in (List<AutoSelAttrVal>) this)
    {
      XmlNode newChild = autoSelAttr.Save(doc);
      if (newChild != null)
        element.AppendChild(newChild);
    }
    return element;
  }

  public virtual void Load(string nodeName, XmlNode node)
  {
    this.Clear();
    if (node == null || !node.Name.Equals(nodeName))
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      AutoSelAttrVal autoSelAttrVal = AutoSelAttrVal.Load(childNode, (IAutoSelAttrType) this);
      if (autoSelAttrVal != null)
        this.Add(autoSelAttrVal);
    }
  }

  public AutoSelAttrTypeMode TypeMode { get; }

  public AutoSelectionNodeItemFillAttributes OwnerObject { get; }

  public override bool Equals(object obj)
  {
    ICollection collection = obj as ICollection;
    return this.Count == 0 && (collection == null || collection.Count == 0) || this == obj;
  }

  public override int GetHashCode() => base.GetHashCode();

  public IEnumerable<Guid> GetMetaDataGuids(IMSGlobals type = IMSGlobals.IMSAttributeType)
  {
    return (IEnumerable<Guid>) this.CollectMetaDataGuids(type, (ICollection<Guid>) new List<Guid>());
  }

  public ICollection<Guid> CollectMetaDataGuids(IMSGlobals type, ICollection<Guid> collector)
  {
    return ((IEnumerable<AutoSelAttr>) this).CollectAttrTypeGuids(type, collector);
  }
}
