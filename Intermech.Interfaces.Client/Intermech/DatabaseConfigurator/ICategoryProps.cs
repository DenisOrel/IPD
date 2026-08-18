// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.ICategoryProps
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.PropertyEditors;
using System;

#nullable disable
namespace Intermech.DatabaseConfigurator;

/// <summary>
/// Интерфейс дополнительных PropertyDescriptor-ов для регистрации в службе DatabaseConfigurator
/// </summary>
public interface ICategoryProps
{
  /// <summary>
  /// идентифицирующая строка (будет включаться в состав сообщений об ошибках в процессе работы функций интерфейса )
  /// </summary>
  string SubscriberID { get; }

  /// <summary>вернуть список свойств</summary>
  /// <param name="pdh">PropDescriptorHolder, можно получить PropDescriptorCollection - текущий список PropDescriptor'ов : только для чтения</param>
  /// <param name="category">категория</param>
  /// <param name="id">идентификатор в рамках категории</param>
  /// <returns></returns>
  PropDescriptor[] GetPropDescriptors(PropDescriptorHolder pdh, int category, object id);

  /// <summary>
  /// применить;
  /// idOld - предварительный идентификатор
  /// (для нового отрицательный, при редактировании == id, idOld подается на вход GetPropDescriptors при запросе списка полей)
  /// 
  /// Внимание! После сохранения значений у сохраняемых PropDescriptor'ов должны быть выставлены флаги ChangedValueApplied в том случае, если было сохранено изменённое значение по сравнению с предыдущим состоянием.
  /// </summary>
  /// <param name="pdh">PropDescriptorHolder, можно получить PropDescriptorCollection - текущий список PropDescriptor'ов : только для чтения</param>
  /// <param name="category">категория</param>
  /// <param name="id">идентификатор в рамках категории</param>
  /// <param name="idOld">предварительный идентификатор </param>
  /// <returns></returns>
  bool Apply(PropDescriptorHolder pdh, int category, object id, object idOld);

  /// <summary>
  /// отменить. пользовательский обработчик должен переинициализировать выдаваемые PropertyDescriptor's
  /// </summary>
  /// <param name="pdh">PropDescriptorHolder, можно получить PropDescriptorCollection - текущий список PropDescriptor'ов : только для чтения</param>
  /// <param name="category">категория</param>
  /// <param name="id">идентификатор в рамках категории</param>
  void Cancel(PropDescriptorHolder pdh, int category, object id);

  /// <summary>
  /// ретранслятор события об изменении (удалении) ( возможно даже PropertyGrid, в котором редактируются выданные properties, тогда ныжно доп. приведение типа)
  /// </summary>
  /// <param name="pdh">PropDescriptorHolder, можно получить PropDescriptorCollection - текущий список PropDescriptor'ов : только для чтения</param>
  /// <param name="category">категория</param>
  /// <param name="id">идентификатор в рамках категории</param>
  /// <param name="e">аргументы события</param>
  /// <remarks>В случае удалении параметр "e" типа DeleteIDEvenArgs</remarks>
  void ChangeEventData(PropDescriptorHolder pdh, int category, object id, EventArgs e);
}
