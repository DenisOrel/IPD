// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSViews.AVSVisualizer
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Client.Core.Visualizers;
using Intermech.Document.Client;
using Intermech.Map;
using System;

#nullable disable
namespace Intermech.AVS.AVSViews;

/// <summary>Визуализатор спецификации Интермех для Show.NET</summary>
public class AVSVisualizer : IVisualizer
{
  internal static void Initialize(IServiceProvider serviceProvider)
  {
    if (!(serviceProvider.GetService(typeof (IVisualizerService)) is IVisualizerService service))
      return;
    AVSVisualizer avsVisualizer = new AVSVisualizer();
    service.AddVisualizer("sp", (IVisualizer) avsVisualizer);
  }

  /// <summary>Создает один или несколько объектов для визуализации из представленных данных</summary>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="valueIndex">Индекс файла в файловом атрибуте с множеством значений</param>
  /// <param name="fileName">Имя файла</param>
  /// <returns>Объект для просмотра</returns>
  public MapObject GetViewObject(long objectId, int valueIndex, string fileName, byte[] data)
  {
    return (MapObject) new ImDocumentShowObject(AVSPlugin.Instance.LoadAVSDocument(objectId, true).Document);
  }
}
