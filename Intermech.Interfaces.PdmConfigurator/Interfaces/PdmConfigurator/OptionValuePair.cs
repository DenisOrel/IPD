// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.OptionValuePair
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Координаты значения опции - [Guid опции x ID значения]
/// </summary>
[Serializable]
public sealed class OptionValuePair : 
  IAssignable,
  ICloneable,
  IComparable,
  IComparable<OptionValuePair>,
  IXMLStorageLoadSave
{
  /// <summary>Guid опции</summary>
  private Guid _option = Guid.Empty;
  /// <summary>ID значения опции</summary>
  private string _id = string.Empty;

  /// <summary>Guid опции</summary>
  public Guid Option
  {
    [DebuggerStepThrough] get => this._option;
  }

  /// <summary>ID значения опции</summary>
  public string ID
  {
    [DebuggerStepThrough] get => this._id;
  }

  /// <summary>Является ли ключ пустым</summary>
  public bool Empty => this.Option == Guid.Empty && string.IsNullOrEmpty(this.ID);

  /// <summary>Создать пустой экземпляр класса</summary>
  public OptionValuePair()
  {
  }

  /// <summary>Создать значение опции на основе указанного объекта</summary>
  /// <param name="source">Объект-источник</param>
  public OptionValuePair(object source) => this.Assign(source);

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="option">Guid опции</param>
  /// <param name="id">ID значения опции</param>
  public OptionValuePair(Guid option, string id)
  {
    this._option = option;
    this._id = id;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (this == obj)
      return true;
    return obj is OptionValuePair optionValuePair && this.Option == optionValuePair.Option && this.ID == optionValuePair.ID;
  }

  /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.Option.GetHashCode() << 8 ^ this.ID.GetHashCode();

  /// <summary>Описание экземпляра класса в виде строки</summary>
  /// <returns>Описание экземпляра класса в виде строки</returns>
  public override string ToString()
  {
    if (this.Option == Guid.Empty && string.IsNullOrEmpty(this.ID))
      return "[n/a]";
    StringBuilder stringBuilder = new StringBuilder();
    OptionHolder option = PdmConfiguratorCache.CacheFindOption(this.Option);
    stringBuilder.Append("[");
    stringBuilder.Append(option != null ? option.OptionCaption : "empty");
    stringBuilder.Append(" x ");
    OptionValue optionValue = option?.OptionValues.FindValue(this.ID);
    stringBuilder.Append(optionValue != null ? optionValue.Value : (!string.IsNullOrEmpty(this.ID) ? this.ID : "empty"));
    stringBuilder.Append("]");
    return stringBuilder.ToString();
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._option = Guid.Empty;
    this._id = string.Empty;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is OptionValuePair optionValuePair))
      return;
    this._option = optionValuePair.Option;
    this._id = optionValuePair.ID;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new OptionValuePair((object) this);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(object obj) => this.CompareTo(obj as OptionValuePair);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(OptionValuePair other)
  {
    if (other == null)
      return 1;
    int num = this.Option.CompareTo(other.Option);
    if (num == 0)
      num = this.ID.CompareTo(other.ID);
    return num;
  }

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (node == null || node.Name != "g" || !(xmlStorage.Services.GetService(typeof (PdmGuidMapper)) is PdmGuidMapper service))
      return;
    string attributeValue = xmlStorage.GetAttributeValue(node, "e", "");
    if (string.IsNullOrEmpty(attributeValue) || attributeValue.Length < 2 || attributeValue.IndexOf(":") <= 0)
      return;
    long int64 = StringsHelper.HexToInt64(attributeValue.Substring(0, attributeValue.IndexOf(":")));
    this._option = service[int64];
    this._id = attributeValue.Substring(attributeValue.IndexOf(":") + 1);
  }

  /// <summary>
  /// Сохранить данные в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    if (this.Empty)
      return;
    if (!(xmlStorage.Services.GetService(typeof (PdmGuidMapper)) is PdmGuidMapper serviceInstance))
    {
      serviceInstance = new PdmGuidMapper();
      xmlStorage.Services.AddService(typeof (PdmGuidMapper), (object) serviceInstance);
    }
    long num = serviceInstance[this.Option];
    XmlNode node = xmlStorage.AddNode(parentNode, "g");
    xmlStorage.SetAttributeValue(node, "e", $"{StringsHelper.IntToHex(num)}:{this._id}");
  }

  /// <summary>
  /// Добавить или заменить в списке значение для указанной опции
  /// </summary>
  /// <param name="list">Список</param>
  /// <param name="item">Значение</param>
  /// <returns>true - добавление или замена выполнены успешно</returns>
  public static bool AddOrReplace(IList<OptionValuePair> list, OptionValuePair item)
  {
    if (list == null || item == null || item.Empty)
      return false;
    int index = OptionValuePair.IndexOf(list, item.Option);
    if (index >= 0)
      list[index] = item;
    else
      list.Add(item);
    return true;
  }

  /// <summary>
  /// Проверить, есть ли в указанном списке хотя бы одно значение опции
  /// </summary>
  /// <param name="list">Список</param>
  /// <param name="option">Искомая опция</param>
  /// <returns>true - в одном из ключей найдена указанная опция</returns>
  public static bool ExistsOption(IList<OptionValuePair> list, Guid option)
  {
    if (list == null || list.Count == 0 || option == Guid.Empty)
      return false;
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index].Option == option)
        return true;
    }
    return false;
  }

  /// <summary>Найти в списке индекс значения с указанной опцией</summary>
  /// <param name="list">Список</param>
  /// <param name="option">Искомая опция</param>
  /// <returns>Индекс значения с указанной опцией или -1</returns>
  public static int IndexOf(IList<OptionValuePair> list, Guid option)
  {
    if (list == null || list.Count == 0 || option == Guid.Empty)
      return -1;
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index].Option == option)
        return index;
    }
    return -1;
  }

  /// <summary>Удалить информацию об указанной опции из списка</summary>
  /// <param name="list">Список</param>
  /// <param name="option">Опция</param>
  /// <returns>true - опция была найдена и удалена из списка</returns>
  public static bool Remove(IList<OptionValuePair> list, Guid option)
  {
    if (list == null || list.Count == 0 || option == Guid.Empty)
      return false;
    for (int index = list.Count - 1; index >= 0; --index)
    {
      if (list[index].Option == option)
      {
        list.RemoveAt(index);
        return true;
      }
    }
    return false;
  }

  /// <summary>Отыскать значение, с которым связана данная опция</summary>
  /// <param name="list">Список</param>
  /// <param name="option">Искомая опция</param>
  /// <returns>ID связанного значения или String.Empty, если значение не найдено</returns>
  public static string FindOptionValue(IList<OptionValuePair> list, Guid option)
  {
    if (list == null || list.Count == 0 || option == Guid.Empty)
      return string.Empty;
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index].Option == option)
        return list[index].ID;
    }
    return string.Empty;
  }

  /// <summary>Добавить в список значение для указанной опции</summary>
  /// <param name="list">Список</param>
  /// <param name="item">Значение</param>
  public static void AddOptionValue(IList<OptionValuePair> list, OptionValuePair item)
  {
    if (list == null || item == null || item.Empty || list.Contains(item))
      return;
    list.Add(item);
  }
}
