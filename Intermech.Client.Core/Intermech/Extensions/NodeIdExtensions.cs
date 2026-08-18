
// Type: Intermech.Extensions.NodeIdExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using System.Runtime.CompilerServices;


namespace Intermech.Extensions;

/// <summary>Расширения для интерфейса INodeID</summary>
public static class NodeIdExtensions
{
  /// <summary>True если нода представляет объект или версию объекта
  /// (Категория == Intermech.Consts.CategoryObject или Intermech.Consts.CategoryObjectVersion))</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsObjectCategory([CanBeNull] this INodeID nodeID)
  {
    if (nodeID == null)
      return false;
    return nodeID.CategoryID == 2 || nodeID.CategoryID == 1;
  }
}
