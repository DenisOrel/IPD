
// Type: Intermech.Navigator.DBObjects.MultipleObjectsNodePart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.DBObjects;

/// <summary>Часть содержимого ноды списка нескольких объектов</summary>
internal class MultipleObjectsNodePart([NotNull] DescriptorCollection descriptors, ContentType contentType) : 
  DescriptorsPart(Intermech.Diagnostics.Check.ArgumentNotNull<DescriptorCollection>(descriptors, nameof (descriptors))),
  INodePart,
  INodeItems
{
  /// <summary>Коллекция колонок по умолчанию</summary>
  /// <returns>Коллекция колонок по умолчанию</returns>
  public override NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    if (ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service)
    {
      foreach (int type in Session.Invoke<IEnumerable<int>>((Session.SessionHandler<IEnumerable<int>>) (session => this._descriptors.OfType<Descriptor>().Select<Descriptor, int>((Func<Descriptor, int>) (descriptor => session.GetObjectInfo(descriptor.ObjectID).ObjectTypeID)).Distinct<int>())))
        columnCollection.SafeAddRange<NodeColumn>((IEnumerable<NodeColumn>) ((object) service.DefaultColumnPack[new NavigatorColumnsKey(4, type, (string) null)] ?? (object) Array.Empty<NodeColumn>()));
    }
    return !columnCollection.Any<NodeColumn>() ? Utils.CaptionAndStatesesColumns(NodeColumnSortOrder.Ascending) : columnCollection;
  }

  /// <summary>Коллекция всех поддерживаемых данным элементом  виртуальных колонок навигатора.</summary>
  /// <param name="columnSetName">Название набора колонок. String.Empty - набор колонок по умолчанию</param>
  /// <returns>Коллекция всех поддерживаемых виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetSupportedColumns(string columnSetName)
  {
    return Utils.NavigatorColumns(NodeColumnSortOrder.Ascending);
  }
}
