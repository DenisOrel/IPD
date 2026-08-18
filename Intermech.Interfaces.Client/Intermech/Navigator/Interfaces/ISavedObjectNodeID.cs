// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISavedObjectNodeID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Интерфейс ноды объекта, сохранённого не-важно-каким-способом (но например в итерации)
/// то есть уникально идентифицирует объект, который может или существовать в БД, или уже нет (удалён)
/// Параметры такого объекта требуется читать не в БД, а там, где он был сохранён</summary>
public interface ISavedObjectNodeID : IRelatedObjectNodeID, IObjectNodeID, INodeID
{
  /// <summary>Присутствует ли объект с таким идентификатором версии в БД</summary>
  bool ObjectExistInDB { get; }

  /// <summary>Присутствует ли связь с таким идентификатором версии в БД</summary>
  bool RelationExistInDB { get; }
}
