
// Type: Intermech.Diagnostics.RemoteExceptionData
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// <para>Контейнер для дополнительных технических сведений об объекте исключения. Этот контейнер заполняется
    /// сервером приложений и содержит сведения, которые не могут быть получены клиентом самостоятельно в момент
    /// обработки исключения.
    /// </para>
    /// <para>Используется для хранения информации о точном месте падения исключения, а также других сведений,
    /// предназначенных для улучшения диагностики ошибок у пользователей.
    /// </para>
    /// </summary>
    [Serializable]
    public class RemoteExceptionData : ISerializable
    {
      private static readonly string stackTraceKey = nameof (stackTrace);
      [NonSerialized]
      private ICollection<RemoteExceptionDataBuilder> builders;
      private static readonly string remoteExceptionDataKey = nameof (RemoteExceptionData);
      private bool isUnderConstruction;
      private string stackTrace;

      /// <summary>Создает объект.</summary>
      public RemoteExceptionData()
      {
      }

      /// <summary>
      /// Возвращает или задает признак, что процесс заполнения контейнера техническими сведениями еще не завершен.
      /// </summary>
      public bool IsUnderConstruction
      {
        [DebuggerStepThrough] get => this.isUnderConstruction;
        [DebuggerStepThrough] set => this.isUnderConstruction = value;
      }

      /// <summary>
      /// Возвращает или задает стек вызова, сохраненный при падении исключения на сервере приложений.
      /// </summary>
      public string StackTrace
      {
        [DebuggerStepThrough] get => this.stackTrace;
        [DebuggerStepThrough] set => this.stackTrace = value;
      }

      /// <summary>Создает объект.</summary>
      /// <param name="info">Сериализованное представление объекта</param>
      /// <param name="context">Контекст сериализации</param>
      protected RemoteExceptionData(SerializationInfo info, StreamingContext context)
      {
        this.stackTrace = info.GetString(RemoteExceptionData.stackTraceKey);
      }

      /// <summary>Выполняет сериализацию объекта.</summary>
      /// <param name="info">Сериализованное представление объекта</param>
      /// <param name="context">Контекст сериализации</param>
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (this.IsUnderConstruction)
        {
          this.RunBuilders();
          this.IsUnderConstruction = false;
        }
        info.AddValue(RemoteExceptionData.stackTraceKey, (object) this.stackTrace);
      }

      /// <summary>
      /// Добавляет новый построитель для ленивого заполнения RemoteExceptionData.
      /// </summary>
      /// <param name="builder">Объект построителя</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="builder" /> не должен быть равен null</exception>
      public void AddBuilder(RemoteExceptionDataBuilder builder)
      {
        if (builder == null)
          throw new ArgumentNullException(nameof (builder));
        if (this.builders == null)
          this.builders = (ICollection<RemoteExceptionDataBuilder>) new List<RemoteExceptionDataBuilder>();
        if (this.builders.Contains(builder))
          return;
        this.builders.Add(builder);
      }

      private void RunBuilders()
      {
        if (this.builders == null)
          return;
        foreach (RemoteExceptionDataBuilder builder in (IEnumerable<RemoteExceptionDataBuilder>) this.builders)
          builder.Build();
      }

      /// <summary>
      /// Читает контейнер с техническими сведениями из объекта исключения, если он был предварительно туда записан.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <returns>Объект контейнера или null</returns>
      /// <exception cref="T:System.ArgumentNullException">exception</exception>
      public static RemoteExceptionData TryGet(Exception exception)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        return exception.Data[(object) RemoteExceptionData.remoteExceptionDataKey] as RemoteExceptionData;
      }

      /// <summary>
      /// Записывает контейнер с техническими сведениями в объект исключения.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <param name="data">Контейнер с техническими сведениями</param>
      /// <exception cref="T:System.ArgumentNullException">exception</exception>
      public static void Set(Exception exception, RemoteExceptionData data)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        if (data != null)
          exception.Data[(object) RemoteExceptionData.remoteExceptionDataKey] = (object) data;
        else
          exception.Data.Remove((object) RemoteExceptionData.remoteExceptionDataKey);
      }
    }
}
