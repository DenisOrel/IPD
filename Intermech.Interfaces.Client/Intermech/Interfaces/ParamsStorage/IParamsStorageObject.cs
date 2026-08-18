// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ParamsStorage.IParamsStorageObject
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.ParamsStorage;

/// <summary>Объект-контейнер параметров / настроек</summary>
public interface IParamsStorageObject
{
  /// <summary>Ид. версии объекта контейнера</summary>
  /// <remarks>Без крайней необходимости не использовать</remarks>
  long ObjectID { get; }

  /// <summary>Имя объекта контейнера</summary>
  string StorageName { get; }

  /// <summary>
  /// Получение списка форм редактирования, назначеннных контейнеру
  /// </summary>
  /// <returns></returns>
  long[] GetFormDesignIDs();

  /// <summary>Назначение форм редактирования контейнеру</summary>
  /// <param name="formIDs"></param>
  void SetFormDesignIDs(long[] formIDs);

  /// <summary>
  /// Получение списка значений атрибутов, назначенных контейнеру
  /// </summary>
  AttributeValues[] GetAttributeValues();

  /// <summary>Назначение значений атрибутов контейнеру</summary>
  /// <param name="attrValues"></param>
  /// <param name="deleteNotExistingAttr">Удалять несуществующие атрибуты</param>
  void SetAttributeValues(AttributeValues[] attrValues, bool deleteNotExistingAttr);

  /// <summary>Отображение диалога с формами ввода/редактирования</summary>
  /// <remarks>Если формы не заданы - метод вернет false.
  /// Результирующие значения атрибутов сохраняются в контейнере</remarks>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="resultValues">Результитующий список значений атрибутов</param>
  /// <returns></returns>
  DialogResult ShowDialog(string caption, out AttributeValues[] resultValues);

  /// <summary>Отображение диалога с формами ввода/редактирования</summary>
  /// <remarks>Если формы не заданы - метод вернет false</remarks>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="temporaryMode">Флаг режима сохранения результирующих. False - результат не сохраняется в контейнер</param>
  /// <param name="resultValues">Результитующий список значений атрибутов</param>
  /// <returns></returns>
  DialogResult ShowDialog(string caption, bool temporaryMode, out AttributeValues[] resultValues);

  /// <summary>Отображение диалога с формами ввода/редактирования</summary>
  /// <remarks>Если формы не заданы - метод вернет false</remarks>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="temporaryMode">Флаг режима сохранения результирующих. False - результат не сохраняется в контейнер</param>
  /// <param name="paramValues">Cписок атрибутов - параметров</param>
  /// <param name="resultValues">Результитующий список значений атрибутов</param>
  /// <returns></returns>
  DialogResult ShowDialog(
    string caption,
    bool temporaryMode,
    AttributeValues[] paramValues,
    out AttributeValues[] resultValues);
}
