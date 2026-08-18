// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.FormDesignerState
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Состояние службы по работе с формами для редактирования данных
/// </summary>
[Flags]
public enum FormDesignerState
{
  /// <summary>Нет состояний</summary>
  None = 0,
  /// <summary>Открыта "Карточка"</summary>
  OpenParametersCard = 1,
  /// <summary>Открыт мастер по созданию новых объектов</summary>
  OpenObjectCreateWizard = 2,
}
