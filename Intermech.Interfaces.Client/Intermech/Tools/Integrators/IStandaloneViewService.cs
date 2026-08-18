// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IStandaloneViewService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Сервис интегратора, отвечающий за внедрение в файлы документов сведений, необходимых для режима автономного просмотра.
/// Эти сведения включают в себя информацию об актуальных подписях документа, контрольной сумме файла,
/// атрибутах документа, заполняемых после согласования документа, и др.
/// </summary>
public interface IStandaloneViewService : IIntegratorService
{
  /// <summary>
  /// Записывает в файл документа сведения для автономного просмотра.
  /// </summary>
  /// <param name="parameters">Параметры выполнения операции</param>
  /// <returns>Результат выполнения операции</returns>
  /// <exception cref="T:ArgumentNullException">parameters</exception>
  /// <exception cref="T:ArgumentException">Параметры операции содержат некорректные данные</exception>
  StandaloneViewServiceResult InjectViewData(StandaloneViewDataInjectionParameters parameters);
}
