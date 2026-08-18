
// Type: Intermech.Search.INavigatorClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public interface INavigatorClientService
{
  NodeColumnCollection ChangeColumns(
    NodeColumnCollection columns,
    NodeColumnCollection supportedColumns,
    NodeColumnCollection defaultColumns = null);

  NavGradientBrush GetCheckedOutBrush(long checkedOutBy, Rectangle rectangle);

  object GetCellValue(object value, NodeColumn column);

  NavGradientBrush GetCheckedOutBrush(long checkedOutBy);

  Tuple<ImageList, int> GetObjectTypeIcon(int objectTypeID);

  NodeColumn CreateNodeColumn(
    ObligatoryObjectAttributes obligatoryObjectAttribute);
}
