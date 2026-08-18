
// Type: Intermech.Navigator.Controls.NavigatorTreeNodeLinksComparer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Navigator.Controls;

/// <summary>Кастомное сравнение нод сравнивающее не по NodeID (у NavigatorTreeNode перекрыт Equals и сравнение идёт по NodeID),
/// а по ссылке на объект. Позволяет например нормально запихивать в Dictionary несколько разных нод с одинаковым NodeID</summary>
public class NavigatorTreeNodeLinksComparer : IEqualityComparer<NavigatorTreeNode>
{
  public bool Equals(NavigatorTreeNode x, NavigatorTreeNode y) => x == y;

  public int GetHashCode(NavigatorTreeNode obj) => obj.GetHashCode();
}
