// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ContinuationTablesFilter
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>
/// Класс настраиваемой логики фильтрации для выбора таблицы продолжения из подходящих.
/// </summary>
internal class ContinuationTablesFilter : TypeNodeFilter
{
  public TableElement CurrentTable { get; set; }

  public override bool CheckNode(object node)
  {
    if (!base.CheckNode(node))
      return false;
    bool flag = node is TableElement tableElement1 && tableElement1.IsStartFlowTable && tableElement1.Page == this.CurrentTable.Page;
    if (flag)
    {
      for (TableElement tableElement2 = tableElement1; tableElement2 != null && tableElement2.Page == this.CurrentTable.Page; tableElement2 = tableElement2.NextTable as TableElement)
      {
        if (tableElement2 == this.CurrentTable)
        {
          flag = false;
          break;
        }
      }
    }
    return flag;
  }
}
