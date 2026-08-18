// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.StandaloneView.StandaloneViewDataInjectionOperation
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.StandaloneView;

/// <summary>
/// Контейнер для всех данных, относящихся к операции подготовки файла документа к автономному просмотру.
/// </summary>
public class StandaloneViewDataInjectionOperation
{
  private StandaloneViewDataInjectionParameters parameters;
  private int objectTypeId;
  private StandaloneViewData viewData;
  private StandaloneViewServiceResult result;
  private object customData;

  /// <summary>Создает объект.</summary>
  public StandaloneViewDataInjectionOperation()
  {
    this.objectTypeId = -1;
    this.viewData = new StandaloneViewData();
    this.result = new StandaloneViewServiceResult();
  }

  /// <summary>Возвращает или задает параметры операции.</summary>
  public StandaloneViewDataInjectionParameters Parameters
  {
    [DebuggerStepThrough] get => this.parameters;
    [DebuggerStepThrough] set => this.parameters = value;
  }

  /// <summary>Возвращает или задает идентификатор типа объекта.</summary>
  public int ObjectTypeId
  {
    [DebuggerStepThrough] get => this.objectTypeId;
    [DebuggerStepThrough] set => this.objectTypeId = value;
  }

  /// <summary>
  /// Возвращает контейнер с данными для внедрения в файл документа.
  /// </summary>
  public StandaloneViewData ViewData
  {
    [DebuggerStepThrough] get => this.viewData;
  }

  /// <summary>
  /// Возвращает накапливаемый результат выполнения операции.
  /// </summary>
  public StandaloneViewServiceResult Result
  {
    [DebuggerStepThrough] get => this.result;
  }

  /// <summary>
  /// Возвращает или задает произвольный объект, связанный с выполняемой операцией. Как правило,
  /// он используется в наследниках класса <see cref="T:StandaloneViewServiceBase" /> для хранения ссылки на открытый файл документа, в который производится запись.
  /// </summary>
  public object CustomData
  {
    [DebuggerStepThrough] get => this.customData;
    [DebuggerStepThrough] set => this.customData = value;
  }
}
