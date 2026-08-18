// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Links.ObjectTypeFormLink
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Links;

/// <summary>
/// 
/// </summary>
internal class ObjectTypeFormLink : FormLink
{
  public Guid ObjectTypeGuid = Guid.Empty;
  private string _name = LocalizationHolder.rm.GetString("FormDesigner_11");
  private TreeNode _node;

  /// <summary>
  /// 
  /// </summary>
  public TreeNode Node
  {
    get
    {
      if (this._node == null)
        this._node = new TreeNode(this._name)
        {
          Tag = (object) this
        };
      return this._node;
    }
    set => this._node = value;
  }

  /// <summary>Конструктор.</summary>
  protected ObjectTypeFormLink()
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="objectTypeGuid">Глобальный идентификатор типа объектов</param>
  public ObjectTypeFormLink(Guid objectTypeGuid)
  {
    if (!(objectTypeGuid != Guid.Empty))
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeGuid);
    if (objectType == null)
      return;
    this._name = objectType.ObjectTypeName;
    this.ObjectTypeGuid = objectTypeGuid;
    this.ProviderGuid = ObjectTypeFormLinkProvider.stProviderGuid;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="objectTypeID">Идентификатор типа объектов</param>
  public ObjectTypeFormLink(int objectTypeID)
  {
    if (objectTypeID == -1)
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeID);
    if (objectType == null)
      return;
    this._name = objectType.ObjectTypeName;
    this.ObjectTypeGuid = objectType.Guid;
    this.ProviderGuid = ObjectTypeFormLinkProvider.stProviderGuid;
  }

  /// <summary>
  /// 
  /// </summary>
  public override List<int> Attributes
  {
    get
    {
      List<int> attributes = (List<int>) null;
      if (this.ObjectTypeGuid != Guid.Empty)
      {
        List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(this.ObjectTypeGuid);
        if (attribute4ObjectTypeList != null)
          attributes = attribute4ObjectTypeList.Select<IMSAttribute4ObjectType, int>((Func<IMSAttribute4ObjectType, int>) (x => x.AttributeID)).ToList<int>();
      }
      return attributes;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override object Clone()
  {
    ObjectTypeFormLink objectTypeFormLink = new ObjectTypeFormLink();
    objectTypeFormLink.ProviderGuid = this.ProviderGuid;
    objectTypeFormLink.ObjectTypeGuid = this.ObjectTypeGuid;
    objectTypeFormLink._name = this._name;
    objectTypeFormLink._node = this._node.Clone() as TreeNode;
    objectTypeFormLink._node.Tag = (object) objectTypeFormLink;
    return (object) objectTypeFormLink;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this._name;
}
