// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DocNodesBlock
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.AVS;

/// <summary>Класс хранящий связку узлов документа - обычных и экспортных, соответствующих блоку записей с одним шаблоном страниц</summary>
internal class DocNodesBlock
{
  /// <summary>Узел документа</summary>
  public TableData DocNodes;
  /// <summary>Узел документа в экспортной части</summary>
  public TableData DocNodesExp;

  /// <summary>Конструктор</summary>
  /// <param name="DocNodes">Узел документа</param>
  /// <param name="DocNodesExp">Узел документа в экспортной части</param>
  public DocNodesBlock(TableData docNodes, TableData docNodesExp)
  {
    this.DocNodes = docNodes;
    this.DocNodesExp = docNodesExp;
  }
}
