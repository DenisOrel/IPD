// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ITreeListColumns
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.ComponentModel.Design;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс,  позволяющий управлять списком колонок дерева основного окна "Навигатора"
/// </summary>
public interface ITreeListColumns
{
  /// <summary>Дескриптор корневого узла в дереве окна "Навигатора"</summary>
  IDescriptor RootDescriptor { get; set; }

  /// <summary>Общий контейнер сервисов окна "Навигатора"</summary>
  IServiceContainer Services { get; }

  /// <summary>Список видимых колонок</summary>
  NodeColumnCollection TreeListColumns { get; set; }
}
