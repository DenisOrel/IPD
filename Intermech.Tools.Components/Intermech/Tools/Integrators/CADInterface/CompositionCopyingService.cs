// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CompositionCopyingService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Сервис интегратора, отвечающий за поддержку копирования структуры CAD-документов
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public class CompositionCopyingService(IIntegrator owner) : 
  IntegratorService(owner),
  ICompositionCopyingService,
  IIntegratorService
{
}
