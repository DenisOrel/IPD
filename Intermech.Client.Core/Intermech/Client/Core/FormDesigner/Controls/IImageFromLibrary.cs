
// Type: Intermech.Client.Core.FormDesigner.Controls.IImageFromLibrary
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Интерфейс для объектов, у которых есть возможность загружать изображение из библиотеки изображений.
/// </summary>
public interface IImageFromLibrary
{
  /// <summary>Guid объекта "библиотечное изображение".</summary>
  Guid ImageFromLibrary { get; set; }

  /// <summary>Наименование объекта "библиотечное изображение".</summary>
  string ImageFromLibraryName { get; }

  /// <summary>ID объекта "библиотечное изображение".</summary>
  long ImageFromLibraryID { get; }
}
