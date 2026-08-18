// Decompiled with JetBrains decompiler
// Type: Intermech.Project.IDBProjectMessageCollection
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.Project;

/// <summary>Интерфейс фабрики объектов "IMProject mail notification"</summary>
[DBObjectTypeHandler("cadd91f6-306c-11d8-b4e9-00304f19f545", true)]
public interface IDBProjectMessageCollection : 
  IDBObjectCollection,
  IDBRecords,
  IDBSessionable,
  IDBAttributableCollection
{
  /// <summary>Создает заготовку почтового сообщения IMProject</summary>
  [NotNull]
  IDBProjectMessage Create();

  /// <summary>Создает заготовку почтового сообщения IMProject на основе прототипа prototype</summary>
  [NotNull]
  IDBProjectMessage Create([NotNull] IDBProjectMessage prototype);

  /// <summary>Создает заготовку почтового сообщения IMProject на основе прототипа prototype</summary>
  [NotNull]
  IDBProjectMessage Create(long prototypeID);
}
