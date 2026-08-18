
// Type: Intermech.Search.Concretization.IConcretizationClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;


namespace Intermech.Search.Concretization;

public interface IConcretizationClientService
{
  bool CanAbstract(NodeID objectNodeID);

  void AbstractCurrentVersion(long relationID);

  void AbstractCurrentVersionInComposition(long relationID, NavigatorTreeView navigatorTreeView);

  void AbstractEntireComposition(NavigatorTreeNode navigatorTreeNode);

  bool CanConcretize(int projectTypeID, NodeID objectNodeID);

  void ConcretizeCurrentVersion(long relationID, long objectVersionID);

  void ConcretizeCurrentVersionInComposition(
    long relationID,
    long objectVersionID,
    NavigatorTreeView navigatorTreeView);

  void ConcretizeSelectedVersion(long relationID, long objectID);

  void ConcretizeSelectedVersionInComposition(
    long relationID,
    long objectID,
    NavigatorTreeView navigatorTreeView);

  void ConcretizeEntireComposition(NavigatorTreeNode navigatorTreeNode);

  void CheckVersion(NodeID objectNodeID, NavigatorTreeView navigatorTreeView);

  void AbstractComposition(NavigatorTreeNode navigatorTreeNode);

  void ConcretizeComposition(NavigatorTreeNode navigatorTreeNode);
}
