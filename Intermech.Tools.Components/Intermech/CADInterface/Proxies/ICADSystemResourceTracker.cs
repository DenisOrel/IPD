// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ICADSystemResourceTracker
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Интерфейс объекта для хранения информации о ресурсах приложения (COM-объекты и др.), открытых интегратором.
/// При закрытии сессии подключения к API проложения эти ресурсы будут освобождены.
/// </summary>
public interface ICADSystemResourceTracker
{
  void TrackOpenDocument(string fullPath, bool alreadyOpen);

  void TrackOpenConfiguration(IModelConfiguration modelConfiguration, bool alreadyOpen);
}
