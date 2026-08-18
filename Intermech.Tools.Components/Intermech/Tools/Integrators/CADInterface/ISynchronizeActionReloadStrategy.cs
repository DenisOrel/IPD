// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ISynchronizeActionReloadStrategy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Интерфейс стратегии для переоткрытия в CAD-системе открытых файлов документов, подлежащих обновлению из базы данных IPS.
/// Реализация не должна быть thread safe.
/// </summary>
public interface ISynchronizeActionReloadStrategy
{
  void BeginOperation(List<DBObjectState> dbObjects);

  bool TryUnlockFiles();

  void EndOperation();
}
