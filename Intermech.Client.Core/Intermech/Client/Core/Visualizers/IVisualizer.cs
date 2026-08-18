
// Type: Intermech.Client.Core.Visualizers.IVisualizer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;


namespace Intermech.Client.Core.Visualizers;

/// <summary> Визуализатор документа </summary>
public interface IVisualizer
{
  /// <summary>Создает объект для визуализации из представленных данных</summary>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="valueIndex">Индекс файла в файловом атрибуте с множеством значений</param>
  /// <param name="fileName">Имя файла</param>
  /// <param name="data">Данные</param>
  /// <returns></returns>
  MapObject GetViewObject(long objectId, int valueIndex, string fileName, byte[] data);
}
