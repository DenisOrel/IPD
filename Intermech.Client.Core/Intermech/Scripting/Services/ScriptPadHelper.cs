
// Type: Intermech.Scripting.Services.ScriptPadHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.Diagnostics;


namespace Intermech.Scripting.Services;

/// <summary>
/// Базовый класс для интеграции конкретного типа сценариев с IDE.
/// </summary>
public abstract class ScriptPadHelper
{
  private IScriptPadService ideService;
  private ScriptTypes scriptType;

  /// <summary>Создает объект.</summary>
  /// <param name="scriptType">Тип сценариев</param>
  protected ScriptPadHelper(ScriptTypes scriptType)
  {
    this.ideService = ServiceUtils.GetService<IScriptPadService>((object) ApplicationServices.Container, false);
    if (this.ideService == null)
      throw new Exception("Не загружен клиентский модуль расширения с IDE для сценариев (Script pad).");
    this.scriptType = scriptType;
  }

  /// <summary>Возвращает сервис IDE.</summary>
  public IScriptPadService IDEService
  {
    [DebuggerStepThrough] get => this.ideService;
  }

  /// <summary>Возвращает тип сценариев.</summary>
  public ScriptTypes ScriptType => this.scriptType;
}
