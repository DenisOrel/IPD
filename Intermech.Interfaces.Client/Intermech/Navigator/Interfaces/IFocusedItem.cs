// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IFocusedItem
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Интерфейс сфокусированного узла в списке</summary>
public interface IFocusedItem
{
  /// <summary>Колонка сфокусированного узла</summary>
  NodeColumn FocusedColumn { get; }

  /// <summary>Идентификатор сфокусированного узла</summary>
  INodeID ItemID { get; }

  /// <summary>Путь к родительскому узлу</summary>
  NodeIDPath ParentPath { get; }

  /// <summary>Извлечь из узла данные указанного типа</summary>
  /// <param name="dataFormat">Тип данных</param>
  /// <returns>Данные указанного типа или null</returns>
  object GetItemData(Type dataFormat);

  /// <summary>Извлечь из родительского узла данные указанного типа</summary>
  /// <param name="dataFormat">Тип данных</param>
  /// <returns>Данные указанного типа или null</returns>
  object GetParentData(Type dataFormat);
}
