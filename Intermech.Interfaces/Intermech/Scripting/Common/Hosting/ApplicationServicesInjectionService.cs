
// Type: Intermech.Scripting.Common.Hosting.ApplicationServicesInjectionService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Scripting.Common.Hosting
{
    /// <summary>
    /// Сервис исполнителя скриптов для внедрения сервисов приложения в качестве зависимостей в объекты сценариев.
    /// Реализация класса является thread safe.
    /// </summary>
    public class ApplicationServicesInjectionService : ScriptDependencyInjectionService
    {
      private IServiceProvider serviceProvider;

      /// <summary>Создает объект.</summary>
      /// <param name="serviceProvider">Провайдер сервисов приложения</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="serviceProvider" /> не должен быть равен null</exception>
      public ApplicationServicesInjectionService(IServiceProvider serviceProvider)
      {
        this.serviceProvider = serviceProvider != null ? serviceProvider : throw new ArgumentNullException(nameof (serviceProvider));
      }

      /// <summary>Возвращает значения для свойств сценария.</summary>
      /// <param name="scriptCodeKey">Уникальный идентификатор сценария</param>
      /// <param name="invocationData">Объект, описывающий обращение к сценарию</param>
      /// <param name="propertyTypes">Типы свойств сценария</param>
      /// <returns>Массив значений для свойств сценария. Длина массива значений должна совпадать с длиной <paramref name="propertyTypes" /></returns>
      protected override object[] DoResolveProperties(
        ScriptCodeKey scriptCodeKey,
        ScriptInvocationData invocationData,
        string[] propertyTypes)
      {
        object[] objArray = new object[propertyTypes.Length];
        for (int index = 0; index < propertyTypes.Length; ++index)
        {
          string propertyType = propertyTypes[index];
          Type type = Type.GetType(propertyType, false);
          objArray[index] = (!(type == (Type) null) ? this.serviceProvider.GetService(type) : throw new ScriptStructureException($"Сервис '{propertyType}' не может быть передан сценарию, так как тип сервиса неизвестен IPS.")) ?? throw new ScriptStructureException($"Сервис '{propertyType}' не может быть передан сценарию, так как экземпляр сервиса не зарегистрирован в контейнере сервисов IPS.");
        }
        return objArray;
      }
    }
}
