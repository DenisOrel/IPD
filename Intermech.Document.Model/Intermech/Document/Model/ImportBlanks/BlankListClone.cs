// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.BlankListClone
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Drawing;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Клон страницы</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
/// <param name="origin">Примитив</param>
public class BlankListClone(GroupClone owner, RectPrimitive origin) : GroupClone(owner, origin)
{
  /// <summary>номер страницы</summary>
  public int listNum;
  /// <summary>Положение в глобальных координатах</summary>
  public Point globalOrg;
  /// <summary>Положение в локальных координатах</summary>
  public Point wsInnerOrg;
  /// <summary>Добавлен пользователем</summary>
  public bool addedByUser;

  /// <summary>номер страницы</summary>
  public int ListNum => this.listNum;

  /// <summary>Положение в глобальных координатах в мм</summary>
  public PointF GlobalOrgMm => PrimitiveBase.BlankUnitToMm(this.globalOrg);

  /// <summary>Положение в локальных координатах в мм</summary>
  public PointF WSInnerOrgMm => PrimitiveBase.BlankUnitToMm(this.wsInnerOrg);

  /// <summary>Добавлен пользователем</summary>
  public bool AddedByUser => this.addedByUser;

  /// <summary>Загрузить</summary>
  /// <param name="ueDoc">Документ</param>
  public override void Load(UEditDocument ueDoc)
  {
    base.Load(ueDoc);
    BinaryReader reader = ueDoc.Reader;
    this.listNum = reader.ReadInt32();
    this.globalOrg.X = reader.ReadInt32();
    this.globalOrg.Y = reader.ReadInt32();
    this.wsInnerOrg.X = reader.ReadInt32();
    this.wsInnerOrg.Y = reader.ReadInt32();
    long position = reader.BaseStream.Position;
    if (reader.ReadInt32() != UEditDocument.MagicSign)
    {
      reader.BaseStream.Position = position;
      this.addedByUser = false;
    }
    else
      this.addedByUser = reader.ReadBoolean();
    ueDoc.CheckChildren((GroupClone) this, (GroupPrimitive) (this.origin as BlankList));
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    (node as Page).PageNumber = this.listNum;
  }
}
