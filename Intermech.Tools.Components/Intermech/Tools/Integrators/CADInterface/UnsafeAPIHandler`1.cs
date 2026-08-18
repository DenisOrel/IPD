// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.UnsafeAPIHandler`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Делегат метода для работы с API CAD-системы. Ни один прокси-объект не должен выйти за пределы
/// этого метода.
/// </summary>
/// <typeparam name="T">Тип возвращаемого методом значения</typeparam>
/// <param name="cadProxy">Прокси-объект CAD-системы</param>
/// <returns>Результат работы метода</returns>
public delegate T UnsafeAPIHandler<T>(CADSystemProxy cadProxy);
