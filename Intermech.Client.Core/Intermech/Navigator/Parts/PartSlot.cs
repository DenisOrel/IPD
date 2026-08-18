
// Type: Intermech.Navigator.Parts.PartSlot
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;


namespace Intermech.Navigator.Parts;

/// <summary>
/// Слот, в котором размещён интерфейс для работы с дочерними элементами пространства навигации
/// </summary>
public class PartSlot : Slot<INodePart>
{
  /// <summary>Идентификатор слота</summary>
  private Guid partGuid;

  /// <summary>
  /// Создать слот, разместить в нём интерфейс для работы с дочерними элементами пространства навигации
  /// </summary>
  /// <param name="partGuid">Идентификатор слота</param>
  /// <param name="part">Интерфейс для работы с дочерними элементами пространства навигации</param>
  public PartSlot(Guid partGuid, INodePart part)
  {
    if (partGuid == Guid.Empty)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Client.Core_625"), nameof (partGuid));
    if (part == null)
      throw new ArgumentNullException(nameof (part), LocalizationHolder.rm.GetString("Client.Core_626"));
    this.partGuid = partGuid;
    this.uniqueId = PartGuidMapper.GetUniqueId(partGuid);
    this.obj = part;
  }

  /// <summary>Идентификатор слота</summary>
  public Guid Guid => this.partGuid;
}
