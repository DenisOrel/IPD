
// Type: Intermech.Tools.Integrators.IIntegratorServer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Tools.Integrators
{
    /// <summary>
    /// Серверная служба, обслуживающая интеграторы с CAD-системами и другими приложениями.
    /// </summary>
    public interface IIntegratorServer
    {
      /// <summary>Создает объект интегратора в базе IPS.</summary>
      /// <param name="id">Идентификатор интегратора</param>
      /// <param name="xmlData">Конфигурационные данные интегратора</param>
      /// <returns>Описание созданного интегратора</returns>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор или конфигурация интегратора</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе создания объекта интегратора произошла ошибка</exception>
      IntegratorObject CreateIntegrator(Guid id, string xmlData);

      /// <summary>Удаляет объект интегратора из базы IPS.</summary>
      /// <param name="id">Идентификатор интегратора</param>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор интегратора</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе удаления объекта интегратора произошла ошибка</exception>
      void RemoveIntegrator(Guid id);

      /// <summary>Возвращает конфигурационные данные интегратора.</summary>
      /// <param name="id">Идентификатор интегратора</param>
      /// <returns>Xml-конфигурация интегратора</returns>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор интегратора</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе получения конфигурации интегратора произошла ошибка</exception>
      string GetIntegratorData(Guid id);

      /// <summary>
      /// Записывает конфигурационные данные интегратора в базу IPS.
      /// </summary>
      /// <param name="id">Идентификатор интегратора</param>
      /// <param name="xmlData">Xml-конфигурация интегратора</param>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор или конфигурация интегратора</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе записи конфигурации интегратора произошла ошибка</exception>
      void SetIntegratorData(Guid id, string xmlData);

      /// <summary>
      /// Проверяет существование объекта интегратора в базе IPS.
      /// </summary>
      /// <param name="id">Идентификатор интегратора</param>
      /// <returns>Признак наличия в базе IPS объекта интегратора</returns>
      bool IsIntegratorExists(Guid id);

      /// <summary>
      /// Возвращает описания существующих в базе IPS объектов интеграторов.
      /// </summary>
      /// <returns>Описания существующих интеграторов</returns>
      /// <exception cref="T:Intermech.KernelException">В процессе получения списка существующих интеграторов произошла ошибка</exception>
      List<IntegratorObject> GetIntegrators();

      /// <summary>
      /// Возвращает краткое описание интегратора по идентификатору.
      /// </summary>
      /// <param name="id">Идентификатор интегратора</param>
      /// <returns>Краткое описание интегратора</returns>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор интегратора</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе получения сведений об интеграторе произошла ошибка</exception>
      IntegratorObject GetIntegrator(Guid id);

      /// <summary>
      /// Возвращает детальное описание интегратора по идентификатору.
      /// </summary>
      /// <param name="id">Идентификатор интегратора</param>
      /// <returns>Детальное описание интегратора</returns>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор интегратора</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе получения сведений об интеграторе произошла ошибка</exception>
      IntegratorDetails GetIntegratorDetails(Guid id);

      /// <summary>
      /// С помощью типа объекта определяет интегратор, который должен использоваться для работы с объектами этого типа.
      /// Если для данного типа объектов интегратор не назначен, то метод вернет null.
      /// </summary>
      /// <param name="objectType">Идентификатор типа объекта</param>
      /// <returns>Описание интегратора или null</returns>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объекта</exception>
      IntegratorObject Lookup(int objectType);

      /// <summary>
      /// С помощью xpath-выражения находит все интеграторы в базе IPS, конфигурационные данные удовлетворяют указанному выражению.
      /// </summary>
      /// <param name="xpath">XPath-выражение для поиска интеграторов</param>
      /// <param name="firstMatchOnly">Признак, что поиск нужно прекратить после нахождения первого подходящего интегратора</param>
      /// <returns>Список найденных интеграторов и частей их конфигурационных данных</returns>
      /// <exception cref="T:System.ArgumentException">Не задано xpath-выражение</exception>
      List<LookupResult> Lookup(string xpath, bool firstMatchOnly);

      /// <summary>
      /// С помощью xpath-выражения находит подходящую часть конфигурационных данных интегратора.
      /// </summary>
      /// <param name="xpath">XPath-выражение для поиска интеграторов</param>
      /// <param name="integratorId">Идентификатор интегратора</param>
      /// <returns>Найденная часть конфигурационных данных интегратора</returns>
      /// <exception cref="T:System.ArgumentException">Не задано xpath-выражение</exception>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор интегратора</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе получения конфигурации интегратора произошла ошибка</exception>
      LookupResult Lookup(string xpath, Guid integratorId);

      /// <summary>
      /// Возвращает значение счетчика изменений в объектах интеграторов. Используется при реализации кэширования
      /// настроек интеграторов.
      /// </summary>
      long WriteSeq { get; }

      /// <summary>
      /// Заставляет службу принудительно обновить кэш настроек интеграторов.
      /// </summary>
      void ReloadCache();
    }
}
