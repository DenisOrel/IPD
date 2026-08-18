
// Type: Intermech.Scripting.Services.IScriptPadService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Scripting.Common.DesignTime;
using Intermech.Scripting.Projects.DBScripts;
using System.Windows.Forms;


namespace Intermech.Scripting.Services;

/// <summary>
/// Интерфейс сервиса IDE для сценариев.
/// Реализация должна быть thread safe.
/// </summary>
public interface IScriptPadService
{
  /// <summary>Регистрирует инициализатор для типа сценариев IPS.</summary>
  /// <param name="scriptObjectTypeId">Идентификатор типа сценариев в базе данных IPS</param>
  /// <param name="initializer">Инициализатор типа сценариев</param>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="scriptObjectTypeId" /> не должен содержать неопределенных значений</exception>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="initializer" /> не должен быть равен null</exception>
  void RegisterScriptProjectInitializer(
    int scriptObjectTypeId,
    DBScriptProjectInitializer initializer);

  /// <summary>
  /// Возвращает инициализатор для указанного типа сценариев IPS.
  /// </summary>
  /// <param name="scriptObjectTypeId">Идентификатор типа сценариев в базе данных IPS</param>
  /// <returns>Инициализатор или null</returns>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="scriptObjectTypeId" /> не должен содержать неопределенных значений</exception>
  DBScriptProjectInitializer TryGetScriptProjectInitializer(int scriptObjectTypeId);

  /// <summary>
  /// Создает новый сценарий указанного типа в виде проекта IDE.
  /// </summary>
  /// <param name="scriptObjectTypeId">Идентификатор типа сценариев в базе данных IPS</param>
  /// <returns>Сценарный проект</returns>
  DBScriptProject CreateEmptyScriptProject(int scriptObjectTypeId);

  /// <summary>Открывает сценарий в виде проекта IDE.</summary>
  /// <param name="scriptObjectTypeId">Идентификатор типа сценария в базе данных IPS</param>
  /// <param name="initializeWhenEmpty">Признак необходимости инициализации проекта, если код сценария пуст</param>
  /// <returns>Сценарный проект</returns>
  DBScriptProject GetScriptProject(long scriptId, bool initializeWhenEmpty = false);

  /// <summary>
  /// Открывает сценарных проект в IDE в обычном режиме. Это недиалоговый режим работы с
  /// одновременным редактированием множества сценариев.
  /// </summary>
  /// <param name="scriptProject">Сценарный проект</param>
  /// <param name="parameters">Параметры открытия</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptProject" /> не должен быть равен null; параметр <paramref name="parameters" /> не должен быть равен null</exception>
  void OpenScriptInIDEWindow(ScriptProject scriptProject, OpenInScriptPadParameters parameters);

  /// <summary>
  /// Открывает сценарный проект в IDE в диалоговом режиме. В этом режиме создается новое модальное окно IDE,
  /// в котором будет открыт только указанный сценарий.
  /// </summary>
  /// <param name="scriptProject">Сценарный проект</param>
  /// <param name="parameters">Параметры открытия</param>
  /// <param name="ownerForm">Форма-владелец для диалога, может быть не задана</param>
  /// <returns>Результирующий сценарный проект. Он может отличаться от исходного проекта, если была разрешена команда замены текущего проекта</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptProject" /> не должен быть равен null; параметр <paramref name="parameters" /> не должен быть равен null</exception>
  ScriptProject OpenScriptInDialogMode(
    ScriptProject scriptProject,
    OpenInScriptPadParameters parameters,
    Form ownerForm = null);
}
