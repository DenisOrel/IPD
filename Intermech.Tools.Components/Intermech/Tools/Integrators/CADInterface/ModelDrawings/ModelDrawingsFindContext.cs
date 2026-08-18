// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ModelDrawings.ModelDrawingsFindContext
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface.ModelDrawings;

/// <summary>
/// Реализует вспомогательный объект, который используется в операциях поиска чертежей по документу 3D-модели.
/// Он используется для вычисления расположения файлов чертежей на диске, если они могут находиться в каталоге,
/// отличном от каталога 3D-модели.
/// </summary>
public class ModelDrawingsFindContext
{
  private readonly string modelMasterFileName;
  private readonly string modelDirectory;

  /// <summary>Создает объект.</summary>
  internal ModelDrawingsFindContext(string modelMasterFileName)
  {
    this.modelMasterFileName = modelMasterFileName;
    this.modelDirectory = Path.GetDirectoryName(modelMasterFileName);
  }

  /// <summary>Возвращает имя мастер-файла документа 3D-модели.</summary>
  public string ModelMasterFileName => this.modelMasterFileName;

  /// <summary>
  /// Возвращает путь к каталогу, в котором находится мастер-файл документа 3D-модели. Может быть как в абсолютной, так и в относительной форме.
  /// </summary>
  public string ModelDirectory => this.modelDirectory;
}
