
// Type: Intermech.Extensions.INodeIdExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Runtime.CompilerServices;


namespace Intermech.Extensions;

public static class INodeIdExtensions
{
  /// <summary>С помощью внешней ф-ии получить значение поля NodeID у нитерфейса ноды.
  /// Выполняет проверки на null и чтобы класс реализации был NodeID, иначе возвращает значение по-умолчанию</summary>
  /// <typeparam name="T">Тип значений запрашиваемого поля</typeparam>
  /// <param name="iNodeID">The iNodeID to act on</param>
  /// <param name="getValue">внешняя ф-ия получения значения поля у NodeID</param>
  /// <param name="defaultValue">значение по-умолчанию</param>
  /// <returns>Полученное из внешней ф-ии getValue значение поля NodeID,
  /// либо, если интерфейс ноды null, либо он не NodeID, - вернёт переданное значение по умолчанию</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetNodeIdParam<T>([NotNull] this INodeID iNodeID, [NotNull] Func<NodeID, T> getValue, T defaultValue)
  {
    return iNodeID is NodeID nodeId ? getValue(nodeId) : defaultValue;
  }

  /// <summary>С помощью внешней ф-ии получить значение поля NodeID у нитерфейса ноды.
  /// Выполняет проверки на null и чтобы класс реализации был NodeID, иначе сгенерирует exception</summary>
  /// <exception cref="T:System.Exception">Если интерфейс ноды == null или если он не NodeID</exception>
  /// <typeparam name="T">Тип значений запрашиваемого поля</typeparam>
  /// <param name="iNodeID">The iNodeID to act on</param>
  /// <param name="getValue">внешняя ф-ия получения значения поля у NodeID</param>
  /// <returns>Полученное из внешней ф-ии getValue значение поля NodeID</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetNodeIdParam<T>([NotNull] this INodeID iNodeID, [NotNull] Func<NodeID, T> getValue)
  {
    return getValue(Intermech.Diagnostics.Check.Is<NodeID>((object) iNodeID, nameof (iNodeID)));
  }

  /// <summary>Получить идентификатор версии объекта</summary>
  /// <exception cref="T:System.Exception">Если throwException = true, и интерфейс ноды == null или если он не NodeID</exception>
  /// <param name="throwException">Если true, то если интерфейс ноды == null, если он не NodeID, будет сгенерирован exception, если false,
  /// то в описанных случаях будет возвращён Consts.UnknownObjectId</param>
  /// <returns>Идентификатор версии объекта, либо, если throwException == false, и интерфейс ноды == null или если он не NodeID, вернёт
  /// Consts.UnknownObjectId</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long GetObjVerID([NotNull] this INodeID iNodeID, bool throwException = true)
  {
    return !throwException ? iNodeID.GetNodeIdParam<long>(new Func<NodeID, long>(INodeIdExtensions.GetObjectVerID), 0L) : iNodeID.GetNodeIdParam<long>(new Func<NodeID, long>(INodeIdExtensions.GetObjectVerID));
  }

  /// <summary>Получить идентификатор типа объекта</summary>
  /// <exception cref="T:System.Exception">Если throwException = true, и интерфейс ноды == null или если он не NodeID</exception>
  /// <param name="throwException">Если true, то если интерфейс ноды == null, если он не NodeID, будет сгенерирован exception, если false,
  /// то в описанных случаях будет возвращён Consts.UnknownObjectTypeId</param>
  /// <returns>Идентификатор типа объекта, либо, если throwException == false, и интерфейс ноды == null или если он не NodeID, вернёт
  /// Consts.UnknownObjectTypeId</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetObjTypeID([NotNull] this INodeID iNodeID, bool throwException = true)
  {
    return !throwException ? iNodeID.GetNodeIdParam<int>(new Func<NodeID, int>(INodeIdExtensions.GetObjectTypeID), -1) : iNodeID.GetNodeIdParam<int>(new Func<NodeID, int>(INodeIdExtensions.GetObjectTypeID));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static long GetObjectVerID([NotNull] NodeID nodeID) => nodeID.ObjectID;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static int GetObjectTypeID([NotNull] NodeID nodeID) => nodeID.ObjectTypeID;
}
