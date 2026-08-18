// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.IVirtualTreeItem
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls.VirtualTree;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

/// <summary>Интерфейс строки дерева</summary>
public interface IVirtualTreeItem
{
  /// <summary>Получить список дочерних узлов</summary>
  /// <returns></returns>
  List<IVirtualTreeItem> GetTreeChildren();

  /// <summary>Родительский элемент</summary>
  IVirtualTreeItem ParentItem { get; set; }

  /// <summary>Получить данные строки</summary>
  /// <param name="data">Данные</param>
  void GetRowData(RowData data);

  /// <summary>Получить данные ячейки</summary>
  /// <param name="column">Колонка для которой получаются данные</param>
  /// <param name="data"></param>
  void GetCellData(AVSColumn column, CellData data);

  /// <summary>Разрешить показывать строку в дереве</summary>
  /// <returns></returns>
  bool CanTreeShow();

  /// <summary>
  /// Строка является заголовком - одна ячейка на всю строку
  /// </summary>
  bool HeaderRow { get; }
}
