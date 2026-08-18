// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Parts.TechDescriptorsPart
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.TechCard.Client.Navigator.Queries;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Parts;

/// <summary>
/// Часть элемента пространства навигации, позволяющая отобразить в своём составе элементы по списку их дескрипторов
/// </summary>
/// <summary>
/// Создать экземпляр класса, указать, требуется ли сортировка дочерних элементов
/// </summary>
/// <param name="descriptors">Список дескрипторов</param>
/// <param name="sortedQueries">Если true, то дочерние элементы будут участвовать в сортировке</param>
internal class TechDescriptorsPart(DescriptorCollection descriptors, bool sortedQueries) : 
  DescriptorsPart(descriptors, sortedQueries)
{
  /// <summary>Создать экземпляр класса</summary>
  /// <param name="descriptors">Список дескрипторов</param>
  public TechDescriptorsPart(DescriptorCollection descriptors)
    : this(descriptors, true)
  {
  }

  /// <summary>
  /// Получить интерфейс объекта-запроса к источнику данных, используемого
  /// для чтения содержимого элементов из пространства навигации
  /// </summary>
  /// <returns>Интерфейс объекта-запроса к источнику данных или null</returns>
  public override INodeQuery GetQuery()
  {
    return (INodeQuery) new TechDescriptorsQuery(this._descriptors, this._sortedQueries);
  }
}
