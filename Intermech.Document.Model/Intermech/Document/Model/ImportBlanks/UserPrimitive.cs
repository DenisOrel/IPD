// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.UserPrimitive
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Пользовательский примитив</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
public class UserPrimitive(GroupPrimitive owner) : RectPrimitive(owner)
{
  /// <summary>Имя пользовательского примитива</summary>
  public string userPrimName;

  /// <summary>Имя пользовательского примитива</summary>
  public string UserPrimName
  {
    [DebuggerStepThrough] get => this.userPrimName;
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик примитивов</param>
  public override void Load(PrimitiveLoader loader)
  {
    base.Load(loader);
    this.userPrimName = loader.ReadString();
  }

  /// <summary>Создать новый узел документа</summary>
  /// <returns>Узел документа</returns>
  public override DocumentTreeNode CreateNewDocumentNode(DocumentTreeNode parentDocNode)
  {
    return base.CreateNewDocumentNode(parentDocNode);
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node) => base.InitNewDocumentNode(node);
}
