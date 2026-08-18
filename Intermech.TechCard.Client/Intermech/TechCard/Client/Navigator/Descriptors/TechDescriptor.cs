// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Descriptors.TechDescriptor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.CustomNode;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.TechCard.Client.Navigator.Nodes;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Descriptors;

/// <summary>
/// "Технологический" дескриптор для создания произвольного составного узла "Навигатора"
/// </summary>
public class TechDescriptor : Descriptor
{
  /// <summary>Создать дескриптор составного узла "Навигатора"</summary>
  /// <param name="caption">Заголовок узла</param>
  /// <param name="descriptors">Коллекция дескрипторов частей узла "Навигатора"</param>
  public TechDescriptor(string caption, DescriptorCollection descriptors)
    : base(caption, descriptors)
  {
  }

  /// <summary>Создать дескриптор составного узла "Навигатора"</summary>
  /// <param name="categoryID">Категория узла</param>
  /// <param name="typeID">Тип узла</param>
  /// <param name="caption">Заголовок узла</param>
  /// <param name="descriptors">Коллекция дескрипторов частей узла "Навигатора"</param>
  public TechDescriptor(
    int categoryID,
    int typeID,
    string caption,
    DescriptorCollection descriptors)
    : base(categoryID, typeID, caption, descriptors)
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public TechDescriptor(PersistentState state)
    : base(state)
  {
  }

  /// <summary>Отобразить колонку в поле</summary>
  /// <param name="column">Колонка</param>
  /// <returns>Поле</returns>
  public override object MapColumnToField(NodeColumn column)
  {
    object field = base.MapColumnToField(column);
    if (field != null)
      return field;
    return (column.SchemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid) && column.ID.Equals((object) ObligatoryObjectAttributes.CAPTION) ? (object) "F_CAPTION" : (object) null;
  }

  /// <summary>Вернуть массив данных для указанного описания узла</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="fields">Поля, загруженные из базы данных</param>
  /// <returns>массив данных для указанного описания узла</returns>
  public override object[] GetRecordValues(INodeID nodeID, object[] fields)
  {
    object[] recordValues = base.GetRecordValues(nodeID, fields);
    for (int index = 0; index < recordValues.Length; ++index)
    {
      if (fields[index].Equals((object) ObligatoryObjectAttributes.CAPTION))
        recordValues[index] = (object) this._caption;
    }
    return recordValues;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeID) => (INode) new TechNode(this._descriptors);
}
