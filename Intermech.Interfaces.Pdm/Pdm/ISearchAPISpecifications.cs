// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.ISearchAPISpecifications
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Pdm;

/// <summary>
/// Необязательный интерфейс расширения SearchAPI, предоставляющий неблокирующие методы создания спецификаций по сборочному чертежу.
/// </summary>
[ComVisible(true)]
[Guid("732A7298-CAEB-4E65-9C45-F0B97F6B5E4D")]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface ISearchAPISpecifications : ISearchAPI
{
  /// <summary>
  /// Создает по сборочному чертежу сборочную единицу, ее исполнения и спецификацию.
  /// Редактирование спецификации выполняется в неблокирующем асинхронном режиме.
  /// </summary>
  /// <param name="dwgPath">Полный путь к чертежу</param>
  /// <param name="inpFieldLayout">Разметка входного файла</param>
  /// <param name="outFieldLayout">Разметка выходного файла</param>
  /// <param name="structFileContent">Содержимое файла с составом сборочного чертежа</param>
  /// <param name="passportData">Пасспорт чертежа</param>
  /// <returns>Асинхронная задача, позволяющая дождаться окончания редактирования спецификации и
  /// получить обновленное содержимое файла с составом сборочного чертежа</returns>
  /// <remarks>
  /// В оригинале Cadmech обменивался данными с Search с помощью файлов. Здесь немного не так: операции
  /// с файлами и их кодировками возложены на imaterial.arx. В этот метод приходит содержимое этих файлов
  /// в виде одной строки.
  /// </remarks>
  ICreateSpecificationAsyncTask CreateSpecificationAsync(
    string dwgPath,
    string inpFieldLayout,
    string outFieldLayout,
    string structFileContent,
    string passportData);
}
