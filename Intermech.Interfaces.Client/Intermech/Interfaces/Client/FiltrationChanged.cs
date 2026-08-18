// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.FiltrationChanged
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Произошла смена настроек фильтрации состава на новое значение
/// </summary>
/// <param name="NewFiltration">Ссылка на интерфейс самих настроек фильтрации состава</param>
/// <param name="FiltrationValid">Являются ли указанные настройки фильтрации состава корректными (можно ли их использовать или нет)</param>
public delegate void FiltrationChanged(IFiltrationSettings NewFiltration, bool FiltrationValid);
