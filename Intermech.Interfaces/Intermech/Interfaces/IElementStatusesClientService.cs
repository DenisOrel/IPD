
// Type: Intermech.Interfaces.IElementStatusesClientService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Клиентская служба, которая позволяет считывать статусы для элементов
    /// </summary>
    public interface IElementStatusesClientService
    {
      /// <summary>
      /// Синхронизировать информацию клиентской службы с серверной частью
      /// </summary>
      /// <param name="serverSide">Интерфейс серверной службы</param>
      /// <param name="statuses">Интерфейс серверной службы</param>
      void SyncWithServerSide(IElementStatusesService serverSide, IPluginStatusesTable statuses);

      /// <summary>
      /// Загрузить настройки пользователя (например, список отключенных статусов)
      /// </summary>
      /// <param name="session">Сессия</param>
      void LoadUserSettings(IUserSession session);

      /// <summary>
      /// Сохранить настройки пользователя (например, список отключенных статусов)
      /// </summary>
      /// <param name="session">Сессия</param>
      void SaveUserSettings(IUserSession session);

      /// <summary>
      /// Текущая емкость массива бит, который требуется для всех зарегистрированных плагинов (в байтах)
      /// </summary>
      int Capacity { get; }

      /// <summary>
      /// Текущая емкость массива бит, который требуется для всех зарегистрированных плагинов (в битах)
      /// </summary>
      int CapacityInBits { get; }

      /// <summary>
      /// Коллекция пар значений [(string)Guid плагина] = [(ElementStatusesPluginDescription)Описание плагина]
      /// </summary>
      Dictionary<string, ElementStatusesPluginDescription> Plugins { get; }

      /// <summary>
      /// Список Guid плагинов, которым надо запретить добавлять свои статусы в столбец "Статусы элементов"
      /// </summary>
      List<string> DisabledPlugins { get; }

      /// <summary>Получить значок для статуса указанного плагина</summary>
      /// <param name="pluginGuid">Guid плагина</param>
      /// <param name="status">Статус</param>
      /// <returns>Значок или null</returns>
      Image GetStatusIcon(Guid pluginGuid, int status);

      /// <summary>
      /// Считать статусы указанного элемента из подмножества бит указанного плагина
      /// с учётом того, что суммарная длина статусов не превышает 16 бит
      /// </summary>
      /// <param name="pluginGuid">Guid плагина, который в данный момент выполняет чтение статусов указанного элемента</param>
      /// <param name="elementStatuses">Битовый массив всех статусов обрабатываемого элемента в виде массива байт.
      /// Часть битов принадлежит указанному плагину и должна быть считана в виде 16-битного числа</param>
      /// <returns>Статусы текущего элемента, принадлежащие указанному плагину (не больше 16 бит)</returns>
      short GetElementStatuses16(string pluginGuid, byte[] elementStatuses);

      /// <summary>
      /// Считать статусы указанного элемента из подмножества бит указанного плагина
      /// с учётом того, что суммарная длина статусов не превышает 32 бита
      /// </summary>
      /// <param name="pluginGuid">Guid плагина, который в данный момент выполняет чтение статусов указанного элемента</param>
      /// <param name="elementStatuses">Битовый массив всех статусов обрабатываемого элемента в виде массива байт.
      /// Часть битов принадлежит указанному плагину и должна быть считана в виде 32-битного числа</param>
      /// <returns>Статусы текущего элемента, принадлежащие указанному плагину (не больше 32 бит)</returns>
      int GetElementStatuses32(string pluginGuid, byte[] elementStatuses);

      int[] GetStatuses(string moduleKey, byte[] bytes);

      /// <summary>
      /// Записать статусы указанного элемента в подмножество бит указанного плагина
      /// с учётом того, что суммарная длина статусов не превышает 16 бит
      /// </summary>
      /// <param name="pluginGuid">Guid плагина, который в данный момент выполняет запись статусов указанного элемента</param>
      /// <param name="elementStatuses">Битовый массив всех статусов обрабатываемого элемента в виде массива байт.
      /// Часть битов принадлежит указанному плагину и должна быть записана из 16-битного числа</param>
      /// <param name="value">Статусы текущего элемента, принадлежащие указанному плагину (не больше 16 бит)</param>
      void SetElementStatuses16(string pluginGuid, byte[] elementStatuses, short value);

      /// <summary>
      /// Записать статусы указанного элемента в подмножество бит указанного плагина
      /// с учётом того, что суммарная длина статусов не превышает 32 бита
      /// </summary>
      /// <param name="pluginGuid">Guid плагина, который в данный момент выполняет запись статусов указанного элемента</param>
      /// <param name="elementStatuses">Битовый массив всех статусов обрабатываемого элемента в виде массива байт.
      /// Часть битов принадлежит указанному плагину и должна быть записана из 32-битного числа</param>
      /// <param name="value">Статусы текущего элемента, принадлежащие указанному плагину (не больше 16 бит)</param>
      void SetElementStatuses32(string pluginGuid, byte[] elementStatuses, int value);
    }
}
