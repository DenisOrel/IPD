// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Links.RelationTypeFormLink
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
internal class RelationTypeFormLink : FormLink
{
  public Guid RelationTypeGuid = Guid.Empty;
  private string _name = LocalizationHolder.rm.GetString("FormDesigner_13");
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
  protected RelationTypeFormLink()
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="relationTypeGuid">Глобальный идентификатор типа связи</param>
  public RelationTypeFormLink(Guid relationTypeGuid)
  {
    if (!(relationTypeGuid != Guid.Empty))
      return;
    IMSRelationType relationType = MetaDataHelper.GetRelationType(relationTypeGuid);
    if (relationType == null)
      return;
    this._name = relationType.Description;
    this.RelationTypeGuid = relationTypeGuid;
    this.ProviderGuid = RelationTypeFormLinkProvider.stProviderGuid;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="relationTypeID">Идентификатор типа связи</param>
  public RelationTypeFormLink(int relationTypeID)
  {
    if (relationTypeID == -1)
      return;
    IMSRelationType relationType = MetaDataHelper.GetRelationType(relationTypeID);
    if (relationType == null)
      return;
    this._name = relationType.Description;
    this.RelationTypeGuid = relationType.Guid;
    this.ProviderGuid = RelationTypeFormLinkProvider.stProviderGuid;
  }

  /// <summary>
  /// 
  /// </summary>
  public override List<int> Attributes
  {
    get
    {
      List<int> attributes = (List<int>) null;
      if (this.RelationTypeGuid != Guid.Empty)
      {
        List<IMSAttribute4RelationType> relationTypeList = MetaDataHelper.GetAttribute4RelationTypeList(this.RelationTypeGuid);
        if (relationTypeList != null)
          attributes = relationTypeList.Select<IMSAttribute4RelationType, int>((Func<IMSAttribute4RelationType, int>) (x => x.AttributeID)).ToList<int>();
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
    RelationTypeFormLink relationTypeFormLink = new RelationTypeFormLink();
    relationTypeFormLink.ProviderGuid = this.ProviderGuid;
    relationTypeFormLink.RelationTypeGuid = this.RelationTypeGuid;
    relationTypeFormLink._name = this._name;
    relationTypeFormLink._node = this._node.Clone() as TreeNode;
    relationTypeFormLink._node.Tag = (object) relationTypeFormLink;
    return (object) relationTypeFormLink;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this._name;
}
