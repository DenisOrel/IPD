// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ICADPrepareNewObjectsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Интерфейс сервис интегратора с CAD-системой, предназначенный для подготовки к использованию
/// новых объектов IPS.
/// </summary>
public interface ICADPrepareNewObjectsService : IPrepareNewObjectsService
{
  /// <summary>
  /// Возвращает набор значений для записи в конфигурацию 3D-модели, чтобы удалить из нее всю информацию об изделии IPS.
  /// Метод используется при создании 3D-моделей по прототипу для очистки файлов нового документа от
  /// данных документа-прототипа.
  /// </summary>
  /// <returns>Набор значений для записи в конфигурацию 3D-модели</returns>
  ValueBag GetValuesToEraseArticleInfo();
}
