
// Type: Intermech.Navigator.Controls.IGridColumnImageList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

/// <summary>Интерфейс для списка изображений у колонки грида</summary>
public interface IGridColumnImageList : IGridCellDrawing
{
  /// <summary>
  /// Нужно ли запрещать ячейке отрисовку текста, помимо значка
  /// </summary>
  bool DrawOnlyIcon { get; set; }

  /// <summary>Список изображений</summary>
  ImageList ImageList { get; }

  /// <summary>Получить индекс изображения для указанной ячейки</summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <param name="cell">Ячейка</param>
  /// <param name="columns">Список доступных колонок</param>
  /// <param name="control">Контрол</param>
  /// <returns>-1 или индекс изображения</returns>
  int ImageIndex(INodeID nodeID, iGCell cell, NodeColumnCollection columns, iGrid control);
}
