
// Type: Intermech.Navigator.Parts.ObjectApplicabilityByClassifiersPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Navigator.Parts;

/// <summary>
/// Чать узла с применяемостью объекта по классификаторам и ручным выборкам
/// </summary>
public sealed class ObjectApplicabilityByClassifiersPart : ObjectsPart
{
  /// <summary>Конструктор</summary>
  /// <param name="objectVersionID">Идентификатор версии объекта</param>
  /// <param name="serviceProvider">Провайдер сервисов</param>
  /// <exception cref="T:System.ArgumentException"></exception>
  public ObjectApplicabilityByClassifiersPart(
    long objectVersionID,
    IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    this.ObjectVersionID = !ObjectHelper.IsUnknownObjectVersionID(objectVersionID) ? objectVersionID : throw new ArgumentException();
  }

  /// <summary>Получить идентификатор версии объекта</summary>
  /// <value>Идентификатор версии объекта</value>
  public long ObjectVersionID { get; private set; }

  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    return (INodeQuery) new ObjectApplicabilityByClassifiersQuery(this.ObjectVersionID, (INodeQuerySupport) this);
  }
}
