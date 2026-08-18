
// Type: Intermech.Navigator.Snapshots.CompareObjectWithSavedInSnapshotPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Navigator.Snapshots;

/// <summary>Содержимое итерации загружающее в её составе ноду объекта, чей актуальный состав и атрибуты сравниваются с составом
/// и атрибутом, сохранённым в итерации</summary>
public class CompareObjectWithSavedInSnapshotPart : 
  SavedInSnapshotPart,
  IContextAware,
  ISnapshotContext,
  INodePart,
  INodeItems
{
  /// <summary>Конструктор</summary>
  /// <param name="ownerServices">Контекст</param>
  /// <param name="descriptor">Дескриптор содержимого</param>
  protected CompareObjectWithSavedInSnapshotPart(
    [NotNull] IServiceProvider ownerServices,
    [NotNull] IDescriptor descriptor)
    : base(ownerServices, descriptor)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerServices">Контекст</param>
  public CompareObjectWithSavedInSnapshotPart([NotNull] IServiceProvider ownerServices)
    : this(ownerServices, (IDescriptor) CompareWithSnapshotObjectDescriptor.Create(ownerServices, 0L))
  {
  }
}
