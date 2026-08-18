
// Type: Intermech.ServiceContainerExtensions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech
{
    /// <summary>Расширения для объекта ServiceContainer и интерфейса IServiceProvider</summary>
    public static class ServiceContainerExtensions
    {
      /// <summary>Добавить сервис в контейнер сервисов</summary>
      /// <typeparam name="T">Тип помещаемого в контейнер сервиса</typeparam>
      /// <param name="serviceContainer">Контейнер сервисов</param>
      /// <param name="service">Помещаемый в контейнер сервиса</param>
      /// <param name="promote">Добавлять ли сервис так же во все родительские контейнеры</param>
      /// <returns>Исходный контейнер сервисов</returns>
      [NotNull]
      [DebuggerStepThrough]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IServiceContainer AddService<T>(
        [NotNull] this IServiceContainer serviceContainer,
        [NotNull] T service,
        bool promote = false)
      {
        serviceContainer.AddService(typeof (T), (object) service, promote);
        return serviceContainer;
      }

      /// <summary>Изъять сервис из контейнера сервисов</summary>
      /// <typeparam name="T">Тип изымаемого из контейнера сервиса</typeparam>
      /// <param name="serviceContainer">Контейнер сервисов</param>
      /// <param name="promote">Изымать ли сервис так же из всех родительских контейнеров</param>
      /// <returns>Исходный контейнер сервисов</returns>
      [NotNull]
      [DebuggerStepThrough]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IServiceContainer RemoveService<T>(
        [NotNull] this IServiceContainer serviceContainer,
        bool promote = false)
      {
        serviceContainer.RemoveService(typeof (T), promote);
        return serviceContainer;
      }

      /// <summary>Попытка извлечения сервиса из провайдера сервисов</summary>
      /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Исключительная ситуация выбрасывается в случае отсутствия у провайдера запрашиваемого
      /// сервиса (при аргументе throwExceptionIfNotFound == true)</exception>
      /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
      /// <param name="serviceProvider">Провайдер сервисов</param>
      /// <param name="throwExceptionIfNotFound">Выбрасывать ли исключительную ситуацию в случае отсутствия в провайдере запрашиваемого
      /// сервиса</param>
      /// <param name="exceptionMessageIfFail">Текст сообщения об ошибке, в выбрасываемой исключительной ситуации в случае отсутствия
      /// запрашиваемого сервиса в провайдере. Если null, то используется стандартное сообщение</param>
      /// <returns>Извлекаемый сервис</returns>
      [ContractAnnotation("throwExceptionIfNotFound:true => NotNull; throwExceptionIfNotFound:false => CanBeNull")]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T GetService<T>(
        [NotNull] this IServiceProvider serviceProvider,
        bool throwExceptionIfNotFound = true,
        [CanBeNull] string exceptionMessageIfFail = null)
      {
        object service = serviceProvider.GetService(typeof (T));
        return !(service == null & throwExceptionIfNotFound) ? (T) service : throw new KeyNotFoundException(exceptionMessageIfFail ?? typeof (T).Name);
      }

      /// <summary>Извлечение сервиса из провайдера сервисов</summary>
      /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Исключительная ситуация выбрасывается в случае отсутствия у провайдера запрашиваемого
      /// сервиса</exception>
      /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
      /// <param name="serviceProvider">Провайдер сервисов</param>
      /// <param name="exceptionMessageIfFail">Текст сообщения об ошибке, в выбрасываемой исключительной ситуации в случае отсутствия
      /// запрашиваемого сервиса в провайдере. Если null, то используется стандартное сообщение</param>
      /// <returns>Извлекаемый сервис</returns>
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T GetService<T>(
        [NotNull] this IServiceProvider serviceProvider,
        [CanBeNull] string exceptionMessageIfFail)
      {
        return (T) (serviceProvider.GetService(typeof (T)) ?? throw new KeyNotFoundException(exceptionMessageIfFail ?? typeof (T).Name));
      }

      /// <summary>Извлечение сервиса из провайдера</summary>
      /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Исключительная ситуация выбрасывается в случае отсутствия у провайдера запрашиваемого
      /// сервиса</exception>
      /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
      /// <param name="serviceProvider">Провайдер сервисов</param>
      /// <param name="service">[out] извлекаемый сервис</param>
      /// <param name="exceptionMessageIfFail">Текст сообщения об ошибке, в выбрасываемой исключительной ситуации в случае отсутствия
      /// запрашиваемого сервиса в провайдере. Если null, то используется стандартное сообщение</param>
      /// <returns>Провайдер сервисов (для построения цепочки вызовов)</returns>
      [NotNull]
      [DebuggerHidden]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IServiceProvider GetService<T>(
        [NotNull] this IServiceProvider serviceProvider,
        [NotNull] out T service,
        [CanBeNull] string exceptionMessageIfFail = null)
      {
        object service1 = serviceProvider.GetService(typeof (T));
        ref T local = ref service;
        T obj = service1 != null ? (T) service1 : throw new KeyNotFoundException(exceptionMessageIfFail ?? typeof (T).Name);
        local = obj;
        return serviceProvider;
      }

      /// <summary>Попытка извлечения сервиса из провайдера сервисов</summary>
      /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
      /// <param name="serviceProvider">Провайдер сервисов</param>
      /// <param name="service">[out] извлекаемый сервис</param>
      /// <returns>true если сервис был получен, иначе false</returns>
      [ContractAnnotation("=> true, service: notnull; => false, service: null")]
      [DebuggerStepThrough]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryGetService<T>([NotNull] this IServiceProvider serviceProvider, [CanBeNull] out T service)
      {
        object service1 = serviceProvider.GetService(typeof (T));
        service = service1 != null ? (T) service1 : default (T);
        return service1 != null;
      }

      /// <summary>Контроль того, что сервис был извлечён из провайдера в переданную переменную. Если нет (она null), то извлечь в неё сервис</summary>
      /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
      /// <param name="serviceProvider">Провайдер сервисов</param>
      /// <param name="service">[in, out] извлекаемый сервис</param>
      /// <param name="exceptionMessageIfFail">Текст сообщения об ошибке, в выбрасываемой исключительной ситуации в случае отсутствия
      /// запрашиваемого сервиса в провайдере. Если null, то используется стандартное сообщение</param>
      /// <returns>извлекаемый сервис</returns>
      [NotNull]
      [DebuggerStepThrough]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T EnsureInitialized<T>(
        [NotNull] this IServiceProvider serviceProvider,
        [CanBeNull] ref T service,
        [CanBeNull] string exceptionMessageIfFail = null)
        where T : class
      {
        if ((object) service == null)
        {
          object service1 = serviceProvider.GetService(typeof (T));
          ref T local = ref service;
          T obj = service1 != null ? (T) service1 : throw new InvalidOperationException(exceptionMessageIfFail ?? $"Provider must contains \"{typeof (T).Name}\"");
          local = obj;
        }
        return service;
      }
    }
}
