// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.CompositionSelect
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.Data;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Делегат для события, генерируемого CompositionService, об запросе составов/применяемости
/// </summary>
/// <param name="userSession">Пользовательская сессия</param>
/// <param name="e"></param>
/// <returns></returns>
public delegate DataTable CompositionSelect(IUserSession userSession, CompositionSelectEventArgs e);
