// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ICadmech3DServices
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.Cadmech;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Позволяет наладить взаимодействие с Cadmech.</summary>
public interface ICadmech3DServices
{
  /// <summary>Позволяет получить параметры поверхности у 3D-модели.</summary>
  /// <param name="documentId">Идентификатор версии документа 3D-модели</param>
  /// <param name="method">Метод, выполняющий чтение параметров поверхности</param>
  /// <exception cref="T:System.ArgumentException">Идентификатор версии документа не определен</exception>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на метод не может быть null</exception>
  /// <exception cref="T:System.NotSupportedException">Получение параметров поверхности для указанного документа не поддерживается</exception>
  /// <exception cref="T:System.Exception">При получении параметров поверхности для указанного документа произошла ошибка</exception>
  void UseAttInterface(long documentId, Action<IAttInterface> method);

  /// <summary>Позволяет получить параметры поверхности у 3D-модели.</summary>
  /// <typeparam name="T">Тип возвращаемого результата</typeparam>
  /// <param name="documentId">Идентификатор версии документа 3D-модели</param>
  /// <param name="method">Метод, выполняющий чтение параметров поверхности</param>
  /// <returns>Результат выполнения метода</returns>
  /// <exception cref="T:System.ArgumentException">Идентификатор версии документа не определен</exception>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на метод не может быть null</exception>
  /// <exception cref="T:System.NotSupportedException">Получение параметров поверхности для указанного документа не поддерживается</exception>
  /// <exception cref="T:System.Exception">При получении параметров поверхности для указанного документа произошла ошибка</exception>
  T UseAttInterface<T>(long documentId, Func<IAttInterface, T> method);
}
