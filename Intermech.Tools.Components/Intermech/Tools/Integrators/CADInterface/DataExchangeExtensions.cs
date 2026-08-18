// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DataExchangeExtensions
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует пустышку для сервиса. Интеграторы могут использовать ее для создания расширений.
/// </summary>
/// <summary>Создает сервис.</summary>
/// <param name="owner">Владелец сервиса</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public class DataExchangeExtensions(IIntegrator owner) : IntegratorService(owner), IDataExchangeExtensions
{
  public virtual IDependencyFilterBehavior CreateDependencyFilterBehavior(CADSystemProxy cadProxy)
  {
    return (IDependencyFilterBehavior) null;
  }
}
