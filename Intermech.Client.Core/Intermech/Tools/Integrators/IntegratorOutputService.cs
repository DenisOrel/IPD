
// Type: Intermech.Tools.Integrators.IntegratorOutputService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Diagnostics;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Предоставляет стандартную реализацию сервиса интегратора для вывода сообщений в окно "Вывод". Интегратор пользуется этим сервисом для
/// общения с пользователем, когда интегратору требуется пояснить принятые им решения или выполненные действия. Класс является thread-safe.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец сервиса</param>
public sealed class IntegratorOutputService(IIntegrator owner) : 
  IntegratorService(owner),
  IIntegratorOutput,
  IIntegratorService
{
  private IOutputView oview;

  /// <summary>
  /// Возвращает или задает системный сервис для взаимодействия с окном "Вывод".
  /// Значение свойства может быть не задано.
  /// </summary>
  public IOutputView OutputView
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.oview;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.oview = value;
      }
    }
  }

  /// <summary>Выводит сообщение.</summary>
  /// <param name="text">Текст сообщения</param>
  public void WriteLine(string text)
  {
    this.RequireReadyState();
    if (this.oview == null)
      return;
    this.oview.WriteString(this.Integrator.DisplayName, text);
  }
}
