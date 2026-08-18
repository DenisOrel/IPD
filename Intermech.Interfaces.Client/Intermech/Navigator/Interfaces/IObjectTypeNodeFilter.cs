// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IObjectTypeNodeFilter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс, позволяющий получить список допустимых или запрещённых типов объектов для узлов деревьев
/// </summary>
public interface IObjectTypeNodeFilter
{
  /// <summary>Список разрешённых типов объектов</summary>
  List<int> EnabledObjectTypes { get; }

  /// <summary>Список запрещённых типов объектов</summary>
  List<int> DisabledObjectTypes { get; }
}
