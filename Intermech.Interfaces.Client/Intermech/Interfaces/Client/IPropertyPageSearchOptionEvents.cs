// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IPropertyPageSearchOptionEvents
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Необязательное расширение для <see cref="T:Intermech.Interfaces.Client.IPropertyPage" />, позволяющее осуществлять
/// получение и выделение настроек
/// </summary>
public interface IPropertyPageSearchOptionEvents : IPropertyPage
{
  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  List<string> GetOptionNames();
}
