// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.SizeTypeNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class SizeTypeNode : XMLPropertyNode<long>
{
  private readonly FieldTypes _fieldType;

  public SizeTypeNode(IUserSession session, XmlNode node, FieldTypes fieldType)
    : base(session, node, "F_SIZE_TYPE", false)
  {
    this._fieldType = fieldType;
    this.ReadValue(session, node);
  }

  protected override long GetValue(IUserSession session, string nodeAttributeValue)
  {
    if (nodeAttributeValue != string.Empty)
    {
      if (this._fieldType == FieldTypes.ftMeasured && GuidHelper.IsGuid(nodeAttributeValue))
      {
        IDBObject dbObject = session.GetObject(new Guid(nodeAttributeValue), false);
        if (dbObject != null)
          return dbObject.ObjectID;
      }
      else
      {
        if (!GuidHelper.IsGuid(nodeAttributeValue))
          return (long) Convert.ToInt32(nodeAttributeValue);
        if (this._fieldType == FieldTypes.ftObjectLink)
        {
          IDBObjectType objectType = session.GetObjectType(new Guid(nodeAttributeValue), false);
          if (objectType != null)
            return (long) objectType.ObjectType;
        }
      }
    }
    return 0;
  }
}
