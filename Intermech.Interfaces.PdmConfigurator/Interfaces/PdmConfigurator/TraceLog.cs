// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.TraceLog
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Interfaces.Compositions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Протокол подбора объектов в конфигурируемом составе</summary>
[Serializable]
public sealed class TraceLog : IAssignable, ICloneable
{
  /// <summary>
  /// Уникальный ключ, позволяющий размещать протокол в дополнительных параметрах запроса
  /// </summary>
  public static Guid TraceLogGuid = new Guid("{7293D8C4-8A2A-41E7-A884-F962BB98BB67}");
  /// <summary>
  /// Уникальный ключ, позволяющий разместить словарик идентификаторов версий объектов в поле Tags у экземпляров TraceLog.
  /// В составе данных версий объектов могут быть маршруты обработки.
  /// Словарик имеет следующий тип:
  /// [(Int64)F_OBJECT_ID проверяемой версии объекта] =&gt; [(RelationPath)полный путь к первой связи в составе с этим объектом].
  /// После выполнения полной раскрутки составов выполняется проверка всех версий из данного словарика.
  /// Из словарика будут удалены все версии объектов, в составе которых меньше двух маршрутов обработки.
  /// </summary>
  public static Guid ObjectsWithRoutesGuid = new Guid("{720D543F-23B0-46BF-A90C-0D6EEFFB9E49}");
  /// <summary>
  /// Уникальный ключ, позволяющий разместить список применяемостей объектов типа "Маршрут обработки" в поле Tags у экземпляров TraceLog.
  /// В списке хранятся идентификаторы типов объектов, в состав которых могут входить маршруты обработки.
  /// Данный список должен добавляться во время запуска процесса раскрутки составов, а после завершения - удаляться,
  /// чтобы не увеличивать объём информации, передаваемой по ремутингу
  /// </summary>
  public static Guid RouteApplsGuid = new Guid("{71859431-FDD2-4AB2-99D1-826A5BA80412}");
  /// <summary>
  /// Уникальный ключ, позволяющий разместить список запрещённых применяемостей объектов типа "Маршрут обработки" в поле Tags у экземпляров TraceLog.
  /// В списке хранятся идентификаторы типов объектов, в состав которых запрещено включать маршруты обработки.
  /// Данный список должен добавляться во время запуска процесса раскрутки составов, а после завершения - удаляться,
  /// чтобы не увеличивать объём информации, передаваемой по ремутингу
  /// </summary>
  public static Guid RouteDisabledApplsGuid = new Guid("{7F2C4C9F-02E9-48B3-AD79-5DFB24811D78}");
  /// <summary>
  /// Элементы протокола подбора объектов в конфигурируемом составе
  /// </summary>
  public SortedDictionary<RelationPath, TraceEntry> Items = new SortedDictionary<RelationPath, TraceEntry>();
  /// <summary>
  /// Дополнительные данные, которые могут передаваться в протоколе.
  /// Примечание. Если ключи и(или) значения поддерживают интерфейс ICloneable,
  /// то при клонировании экземпляра класса TraceLog эти значения также будут клонированы.
  /// Внимание! Ключи и значения должны быть помечены как сериализуемые!
  /// </summary>
  public HybridDictionary Tags = new HybridDictionary();

  /// <summary>Создать пустой экземпляр класса</summary>
  public TraceLog()
  {
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public TraceLog(object source) => this.Assign(source);

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this.Items.Clear();
    this.Tags.Clear();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is TraceLog traceLog))
      return;
    foreach (KeyValuePair<RelationPath, TraceEntry> keyValuePair in traceLog.Items)
      this.Items.Add(keyValuePair.Key.Clone() as RelationPath, keyValuePair.Value.Clone() as TraceEntry);
    if (traceLog.Tags == null || traceLog.Tags.Count <= 0)
      return;
    foreach (object key in (IEnumerable) traceLog.Tags.Keys)
    {
      object tag = traceLog.Tags[key];
      this.Tags.Add(key is ICloneable ? ((ICloneable) key).Clone() : key, tag is ICloneable ? ((ICloneable) tag).Clone() : tag);
    }
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>
  /// Объединить протокол с информацией из указанных протоколов
  /// </summary>
  /// <param name="logs">Добавляемые протоколы</param>
  public void Merge(params TraceLog[] logs)
  {
    if (logs == null || logs.Length == 0)
      return;
    for (int index = 0; index < logs.Length; ++index)
    {
      foreach (KeyValuePair<RelationPath, TraceEntry> keyValuePair in logs[index].Items)
      {
        if (!keyValuePair.Key.Empty && !keyValuePair.Value.Empty)
          this.Items[keyValuePair.Key.Clone() as RelationPath] = keyValuePair.Value.Clone() as TraceEntry;
      }
    }
  }

  /// <summary>
  /// Метод вызывается перед тем, как протокол будет возвращён службой трассировки составов.
  /// Предназначен для очистки протокола от вспомогательной информации
  /// </summary>
  public void Pack()
  {
    if (this.Tags == null)
      return;
    if (this.Tags.Contains((object) TraceLog.RouteApplsGuid))
      this.Tags.Remove((object) TraceLog.RouteApplsGuid);
    if (this.Tags.Contains((object) TraceLog.RouteDisabledApplsGuid))
      this.Tags.Remove((object) TraceLog.RouteDisabledApplsGuid);
    if (this.Tags.Count != 0)
      return;
    this.Tags = (HybridDictionary) null;
  }
}
