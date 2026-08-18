
// Type: Intermech.Tools.LaunchActions.ILaunchActionServer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Tools.LaunchActions
{
    /// <summary>
    /// Серверная служба, обслуживающая команды запуска приложений.
    /// </summary>
    public interface ILaunchActionServer
    {
      /// <summary>
      /// Создает команду запуска приложения для указанного типа объекта.
      /// </summary>
      /// <param name="objectType">Идентификатор типа объекта</param>
      /// <param name="target">Область видимости команды</param>
      /// <param name="launchType">Тип запуска команды</param>
      /// <param name="handlerId">Идентификатор обработчика команды</param>
      /// <param name="xmlData">Конфигурационные данные команды</param>
      /// <returns>Описание созданной команды</returns>
      /// <exception cref="T:System.ArgumentException">Один из аргументов метода не заполнен или равен null</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе создания объекта команды произошла ошибка</exception>
      LaunchActionInfo CreateAction(
        Guid objectType,
        ITarget target,
        LaunchType launchType,
        Guid handlerId,
        string xmlData);

      /// <summary>Удаляет команду запуска приложения.</summary>
      /// <param name="actionId">Идентификатор команды</param>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор команды</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе удаления объекта команды произошла ошибка</exception>
      void RemoveAction(Guid actionId);

      /// <summary>
      /// Записывает конфигурационные данные команды в базу IPS.
      /// </summary>
      /// <param name="actionId">Идентификатор команды</param>
      /// <param name="xmlData">Конфигурационные данные команды</param>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор или конфигурационные данные команды</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе записи конфигурации команды произошла ошибка</exception>
      void SetActionData(Guid actionId, string xmlData);

      /// <summary>
      /// Возвращает конфигурационные данные команды из базы IPS.
      /// </summary>
      /// <param name="actionId">Идентификатор команды</param>
      /// <returns>Xml-конфигурация команды</returns>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор команды</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе получения конфигурации команды произошла ошибка</exception>
      string GetActionData(Guid actionId);

      /// <summary>
      /// Возвращает описания существующих в базе IPS команд для указанного типа объекта.
      /// </summary>
      /// <param name="objectType">Идентификатор типа объекта</param>
      /// <param name="target">Область видимости команды</param>
      /// <param name="launchType">Тип запуска команды</param>
      /// <returns>Описания существующих команд</returns>
      /// <exception cref="T:System.ArgumentException">Один из аргументов метода не заполнен или равен null</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе получения списка существующих команд произошла ошибка</exception>
      List<LaunchActionInfo> GetActionList(Guid objectType, ITarget target, LaunchType launchType);

      /// <summary>Возвращает описание указанной команды.</summary>
      /// <param name="actionId">Идентификатор команды</param>
      /// <returns>Описание команды</returns>
      /// <exception cref="T:System.ArgumentException">Не задан идентификатор команды</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе получения сведений о команде произошла ошибка</exception>
      LaunchActionInfo GetActionInfo(Guid actionId);

      /// <summary>
      /// Собирает описания существующих в базе IPS команд для указанного типа объекта c учетом
      /// наследования типов объектов и вложенности областей видимости.
      /// </summary>
      /// <param name="objectType">Идентификатор типа объекта</param>
      /// <param name="target">Область видимости команды</param>
      /// <param name="launchType">Тип запуска команды</param>
      /// <returns>Описания существующих команд</returns>
      /// <exception cref="T:System.ArgumentException">Один из аргументов метода не заполнен или равен null</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе получения списка существующих команд произошла ошибка</exception>
      List<LaunchActionInfo> LookupActionList(Guid objectType, ITarget target, LaunchType launchType);

      /// <summary>
      /// Регистрирует для указанного типа объекта команду, действующую по умолчанию.
      /// </summary>
      /// <param name="objectType">Идентификатор типа объекта</param>
      /// <param name="target">Область видимости команды</param>
      /// <param name="actionId">Идентификатор команды</param>
      /// <exception cref="T:System.ArgumentException">Один из аргументов метода не заполнен или равен null</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе регистрации команды произошла ошибка</exception>
      void SetDefaultAction(Guid objectType, ITarget target, Guid actionId);

      /// <summary>
      /// Отменяет для указанного типа объекта регистрацию команды, действующей по умолчанию.
      /// </summary>
      /// <param name="objectType">Идентификатор типа объекта</param>
      /// <param name="target">Область видимости команды</param>
      /// <param name="actionId">Идентификатор команды</param>
      /// <exception cref="T:System.ArgumentException">Один из аргументов метода не заполнен или равен null</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе отмены регистрации команды произошла ошибка</exception>
      void ResetDefaultAction(Guid objectType, ITarget target, Guid actionId);

      /// <summary>
      /// Возвращает зарегистрированную команду по умолчанию, если такой команды нет, то метод вернет null.
      /// </summary>
      /// <param name="objectType">Идентификатор типа объекта</param>
      /// <param name="target">Область видимости команды</param>
      /// <param name="launchType">Тип запуска команды</param>
      /// <exception cref="T:System.ArgumentException">Один из аргументов метода не заполнен или равен null</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе получения зарегистрированной команды произошла ошибка</exception>
      LaunchActionInfo GetDefaultAction(Guid objectType, ITarget target, LaunchType launchType);

      /// <summary>
      /// Вычисляет зарегистрированную команду по умолчанию с учетом наследования типов объектов и
      /// вложенности областей видимости. Если такой команды нет, то метод вернет null.
      /// </summary>
      /// <param name="objectType">Идентификатор типа объекта</param>
      /// <param name="target">Область видимости команды</param>
      /// <param name="launchType">Тип запуска команды</param>
      /// <exception cref="T:System.ArgumentException">Один из аргументов метода не заполнен или равен null</exception>
      /// <exception cref="T:Intermech.KernelException">В процессе получения зарегистрированной команды произошла ошибка</exception>
      LaunchActionInfo LookupDefaultAction(Guid objectType, ITarget target, LaunchType launchType);
    }
}
