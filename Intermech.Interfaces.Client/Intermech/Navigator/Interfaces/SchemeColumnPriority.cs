// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.SchemeColumnPriority
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Приоритет схемы колонок для определения, какую колонку из набора одинаковых колонок
/// оставлять в окне по настройке отображения, если несколько разных схем возвращают
/// одну и ту же колонку
/// </summary>
[Serializable]
public enum SchemeColumnPriority
{
  /// <summary>
  /// Стандартный приоритет. В список попадает первая попавшаяся колонка
  /// </summary>
  Standard,
  /// <summary>
  /// Высокий приоритет. В список попадает первая попавшаяся колонка такого приоритета, либо заменит колонку приоритета Standard
  /// </summary>
  High,
  /// <summary>
  /// Наивысший приоритет. В список попадает первая попавшаяся колонка такого приоритета, либо заменит колонку приоритетов Standard/High
  /// </summary>
  Highest,
}
