// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.CloneBase
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Базовый тип для клонов (элементов документа сделанных по примитиву (шаблону))</summary>
[Serializable]
public class CloneBase
{
  /// <summary>владелец</summary>
  public GroupClone owner;
  /// <summary>примитив</summary>
  public RectPrimitive origin;
  /// <summary>Документ</summary>
  public UEditDocument ownerDoc;
  /// <summary>local coords, relative to owner</summary>
  public Rectangle coords;
  /// <summary>needed to save relative coords</summary>
  public Point relCoords;
  /// <summary>Узел документа</summary>
  [NonSerialized]
  public DocumentTreeNode DocumentNode;
  /// <summary>flag = true, if the element is already aligned
  /// don't realign all the time!</summary>
  public bool aligned;

  /// <summary>Границы в мм</summary>
  public RectangleF BoundsMm
  {
    [DebuggerStepThrough] get => PrimitiveBase.BlankUnitToMm(this.coords);
  }

  /// <summary>в мм</summary>
  public PointF RelCoordsMm
  {
    [DebuggerStepThrough] get => PrimitiveBase.BlankUnitToMm(this.relCoords);
  }

  /// <summary>flag = true, if the element is already aligned
  /// don't realign all the time!</summary>
  public bool Aligned => this.aligned;

  /// <summary>Идентификатор</summary>
  public string Id
  {
    [DebuggerStepThrough] get => this.origin != null ? this.origin.Id : (string) null;
  }

  /// <summary>Имя</summary>
  public string Name
  {
    [DebuggerStepThrough] get => this.origin != null ? this.origin.Name : (string) null;
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец</param>
  /// <param name="origin">Примитив</param>
  public CloneBase(GroupClone owner, RectPrimitive origin)
  {
    this.owner = owner;
    this.ownerDoc = owner == null ? (UEditDocument) null : (!(owner is UEditDocument) ? owner.ownerDoc : owner as UEditDocument);
    this.origin = origin;
  }

  /// <summary>Загрузить</summary>
  /// <param name="ueDoc">Документ</param>
  public virtual void Load(UEditDocument ueDoc)
  {
    BinaryReader reader = ueDoc.Reader;
    this.coords = Rectangle.FromLTRB(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
    this.relCoords.X = reader.ReadInt32();
    this.relCoords.Y = reader.ReadInt32();
    if (ueDoc.LoadingVersion >= 110)
      this.aligned = reader.ReadBoolean();
    else
      this.aligned = false;
  }

  /// <summary>Найти узел документа соответсвующий этому клону</summary>
  /// <param name="docOwner">Родительский узел</param>
  /// <returns>Узел документа</returns>
  public DocumentTreeNode FindDocNode(DocumentTreeNode docOwner)
  {
    if (docOwner == null)
      throw new ArgumentNullException(nameof (docOwner));
    List<DocumentTreeNode> foundNodes = new List<DocumentTreeNode>();
    docOwner.Template.FindNodesFromTemplate(this.origin.DocumentNode, foundNodes);
    return foundNodes.Count > 0 ? foundNodes[0] : (DocumentTreeNode) null;
  }

  /// <summary>Найти узел документа соответсвующий этому клону</summary>
  /// <returns>Узел документа</returns>
  public DocumentTreeNode FindDocNode()
  {
    if (this.owner != null)
      return this.FindDocNode(this.owner.DocumentNode);
    return this.ownerDoc != null ? this.FindDocNode(this.ownerDoc.DocumentNode) : (DocumentTreeNode) null;
  }

  /// <summary>Создать новый узел документа</summary>
  /// <returns>Узел документа</returns>
  public virtual DocumentTreeNode CreateNewDocumentNode(DocumentTreeNode parentDocNode)
  {
    DocumentTreeNode newDocumentNode = this.DocumentNode;
    if (newDocumentNode == null && this.origin != null)
    {
      if (this.origin.DocumentNode != null && !(this is AreaClone) && !(this is BlankListClone))
        newDocumentNode = parentDocNode?.FindFirstNodeFromTemplate_Recursive(this.origin.DocumentNode);
      if (newDocumentNode == null && this.origin.DocumentNode != null)
        newDocumentNode = this.origin.DocumentNode.CloneFromTemplate(false, false);
    }
    if (parentDocNode != null && newDocumentNode?.Parent == null)
      parentDocNode.AddChildNode(newDocumentNode, false, false);
    if (newDocumentNode != null)
      this.InitNewDocumentNode(newDocumentNode);
    return newDocumentNode;
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public virtual void InitNewDocumentNode(DocumentTreeNode node)
  {
    this.DocumentNode = node;
    if (!(node is RectangleElement rectangleElement) || rectangleElement.ParentCell == null || !rectangleElement.ParentCell.IsFixedStructureArea)
      return;
    RectangleF properBounds = rectangleElement.properBounds with
    {
      X = PrimitiveBase.BlankUnitToMm(this.coords.X),
      Y = PrimitiveBase.BlankUnitToMm(this.coords.Y)
    };
    rectangleElement.AssignProperBounds(properBounds, false, false, false);
    rectangleElement.AssignBounds(rectangleElement.CalcBoundsFromProper(properBounds), false, false, false);
  }
}
