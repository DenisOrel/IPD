// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.AreaClone
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Клон рабочей области</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
/// <param name="origin">Примитив</param>
public class AreaClone(GroupClone owner, RectPrimitive origin) : GroupClone(owner, origin)
{
  /// <summary>Строк перед</summary>
  public int strBefore;
  /// <summary>Строк после</summary>
  public int strAfter;
  /// <summary>всегда с начала страницы</summary>
  public bool onPageTop;

  /// <summary>Строк перед</summary>
  public int StrBefore => this.strBefore;

  /// <summary>Строк после</summary>
  public int StrAfter => this.strAfter;

  /// <summary>всегда с начала страницы</summary>
  public bool OnPageTop => this.onPageTop;

  /// <summary>Загрузить</summary>
  /// <param name="ueDoc">Документ</param>
  public override void Load(UEditDocument ueDoc)
  {
    base.Load(ueDoc);
    BinaryReader reader = ueDoc.Reader;
    if (ueDoc.LoadingVersion < 302 || ueDoc.CurrentCloneIsLoaded)
      return;
    this.strBefore = reader.ReadInt32();
    this.strAfter = reader.ReadInt32();
    this.onPageTop = reader.ReadInt32() > 0;
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node) => base.InitNewDocumentNode(node);
}
