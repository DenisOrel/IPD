// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.IContextServicesStack`1
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces;

/// <summary>Интерфейс стека однотипных сервисов в локальном контексте.
/// Позволяет организовывать работу однотивных сервисов, находящихся во вложенных друг в
/// друга контейнерах. Например "фильтрация команд контекстного меню должна осуществляться контролом, а так же всеми
/// контролами, в которые он вложен (поддерживающих сервис фильтрации команд)"</summary>
/// <typeparam name="ServiceType">Тип сервиса</typeparam>
public interface IContextServicesStack<ServiceType>
{
  /// <summary>Перечисление сервисов (соотв. сервис данного контекста и всех контекстов, в которые он входит)</summary>
  IEnumerable<ServiceType> Enumeration { get; }
}
