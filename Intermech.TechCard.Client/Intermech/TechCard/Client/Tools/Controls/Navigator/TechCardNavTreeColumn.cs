// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.Navigator.TechCardNavTreeColumn
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls.Navigator;

/// <summary>
/// 
/// </summary>
public class TechCardNavTreeColumn : NavigatorTreeColumn
{
  /// <summary>Constructor</summary>
  public TechCardNavTreeColumn()
  {
  }

  /// <summary>Constructor</summary>
  /// <param name="tree"></param>
  /// <param name="column"></param>
  /// <param name="columns"></param>
  public TechCardNavTreeColumn(
    NavigatorTreeView tree,
    NodeColumn column,
    NodeColumnCollection columns)
    : base(tree, column, columns)
  {
  }
}
