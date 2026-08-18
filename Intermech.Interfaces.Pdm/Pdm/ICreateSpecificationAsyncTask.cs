// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.ICreateSpecificationAsyncTask
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Pdm;

/// <summary>
/// Интерфейс асинхронной задачи по созданию/обновлению спецификации по сборочному чертежу.
/// </summary>
[ComVisible(true)]
[Guid("215FDC91-B2A9-4872-8C1C-9250B3C69F1A")]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface ICreateSpecificationAsyncTask
{
  /// <summary>Возвращает признак, что задача была завершена.</summary>
  bool IsCompleted { get; }

  /// <summary>
  /// Возвращает признак, что задача была завершена из-за необработанного исключения в процессе выполнения задачи.
  /// </summary>
  bool IsFaulted { get; }

  /// <summary>
  /// Возвращает результат выполнения задачи или null, если задача еще не завершена.
  /// </summary>
  /// <returns>Содержимое файла с обновленным составом сборочного чертежа</returns>
  string TryGetStructFileContent();
}
