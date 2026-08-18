// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator.FillDataTableEventArgs
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;

/// <summary>
/// Аргументы для события, возникающего перед созданием заготовки нового объекта
/// </summary>
internal sealed class FillDataTableEventArgs : EventArgs
{
  /// <summary>Создать экземпляр класса</summary>
  /// <param name="nodeQuery"></param>
  /// <param name="mapping"></param>
  /// <param name="dataTable"></param>
  public FillDataTableEventArgs(INodeQuery nodeQuery, RecordMapping mapping, DataTable dataTable)
  {
    this.NodeQuery = nodeQuery;
    this.Mapping = mapping;
    this.DataTable = dataTable;
  }

  /// <summary>
  /// 
  /// </summary>
  public INodeQuery NodeQuery { get; }

  /// <summary>
  /// 
  /// </summary>
  public RecordMapping Mapping { get; }

  /// <summary>
  /// 
  /// </summary>
  public DataTable DataTable { get; set; }
}
