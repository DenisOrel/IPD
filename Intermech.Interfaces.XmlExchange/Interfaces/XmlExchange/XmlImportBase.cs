// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlImportBase
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Collections;
using Intermech.Localization.Xml;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Базовый класс</summary>
[DebuggerDisplay("{Text}")]
[Serializable]
public class XmlImportBase : IAssignable, ICloneable, IDisplayable
{
  /// <summary>Владелец объекта</summary>
  public XmlImportBase Owner;
  /// <summary>Название узла XML</summary>
  public string Name = string.Empty;
  /// <summary>Внутреннее значение XML</summary>
  public string Value = string.Empty;
  /// <summary>Коллекция атрибутов и их значений</summary>
  public IDictionary<string, object> attributes = (IDictionary<string, object>) new Dictionary<string, object>();
  /// <summary>Список дочерних узлов</summary>
  public List<XmlImportBase> Items;

  /// <summary>Базовый конструктор</summary>
  public XmlImportBase()
  {
  }

  /// <summary>Конструктор, позволяющий указать владельца</summary>
  /// <param name="owner">Объект-владелец</param>
  public XmlImportBase(XmlImportBase owner) => owner?.Add(this);

  /// <summary>
  /// Прочитать/установить значение свойства с указанным именем
  /// </summary>
  /// <param name="attrName">Имя атрибута</param>
  /// <returns>Значение атрибута или null, если атрибут с таким именем не найден</returns>
  public virtual object this[string attrName]
  {
    get => this.GetAsObject(attrName, (object) null);
    set => this.SetAsObject(attrName, value);
  }

  /// <summary>Добавить дочерний элемент</summary>
  /// <param name="item">Добавляемый элемент</param>
  public void Add(XmlImportBase item)
  {
    if (item == null)
      return;
    if (item.Owner != null && item.Owner != this)
      item.Owner.Remove(item);
    this.Items = this.Items ?? new List<XmlImportBase>();
    if (this.Items.Exists((Predicate<XmlImportBase>) (el => el == item)))
      return;
    this.Items.Add(item);
    item.Owner = this;
  }

  /// <summary>Удалить элемент</summary>
  /// <param name="item">Удаляемый элемент</param>
  public void Remove(XmlImportBase item)
  {
    if (item == null || this.Items == null)
      return;
    int index = this.Items.FindIndex((Predicate<XmlImportBase>) (el => el == item));
    if (index >= 0)
      this.Items.RemoveAt(index);
    if (item.Owner != this)
      return;
    item.Owner = (XmlImportBase) null;
  }

  /// <summary>Строка для отображения на экране</summary>
  public virtual string Text
  {
    get
    {
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        stringBuilder.Append("<");
        stringBuilder.Append(this.Name);
        stringBuilder.Append(" ");
        foreach (KeyValuePair<string, object> attribute in (IEnumerable<KeyValuePair<string, object>>) this.attributes)
        {
          stringBuilder.Append(attribute.Key);
          stringBuilder.Append("=\"");
          stringBuilder.Append(attribute.Value);
          stringBuilder.Append("\" ");
        }
        stringBuilder.Append(" />");
        return stringBuilder.ToString();
      }
    }
  }

  /// <summary>Очистить поля класса</summary>
  public virtual void Clear()
  {
    this.attributes.Clear();
    this.Name = string.Empty;
    this.Value = string.Empty;
    this.Owner = (XmlImportBase) null;
    this.Items = (List<XmlImportBase>) null;
  }

  /// <summary>
  /// Заполнить поля класса информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public virtual void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is XmlImportBase xmlImportBase))
      return;
    this.attributes = CloneHelper.Clone((object) xmlImportBase.attributes) as IDictionary<string, object>;
    this.Name = xmlImportBase.Name;
    this.Value = xmlImportBase.Value;
    this.Owner = xmlImportBase.Owner;
    this.Items = CloneHelper.Clone((object) xmlImportBase.Items) as List<XmlImportBase>;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса или null</returns>
  public virtual object Clone()
  {
    if (Activator.CreateInstance(this.GetType()) is XmlImportBase instance)
      instance.Assign((object) this);
    return instance != null ? (object) instance : throw new InvalidOperationException();
  }

  /// <summary>
  /// Получить строковое представление атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение атрибута или значение по умолчанию</returns>
  public virtual string GetAsString(string attr, string defValue)
  {
    return !this.attributes.ContainsKey(attr) ? defValue : Convert.ToString(this.attributes[attr]);
  }

  /// <summary>Установить значение атрибута как строку</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде строки</param>
  public virtual void SetAsString(string attr, string value)
  {
    this.attributes[attr] = (object) value;
  }

  /// <summary>
  /// Получить объектное представление атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение атрибута или значение по умолчанию</returns>
  public virtual object GetAsObject(string attr, object defValue)
  {
    return !this.attributes.ContainsKey(attr) ? defValue : this.attributes[attr];
  }

  /// <summary>Установить значение атрибута как объект</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде объекта</param>
  public virtual void SetAsObject(string attr, object value) => this.attributes[attr] = value;

  /// <summary>
  /// Получить представление DateTime атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение DateTime атрибута или значение по умолчанию</returns>
  public virtual DateTime GetAsDateTime(string attr, DateTime defValue)
  {
    DateTime result = defValue;
    if (this.attributes.ContainsKey(attr))
    {
      object attribute = this.attributes[attr];
      if (attribute == null || attribute == DBNull.Value || !DateTime.TryParse(attribute.ToString(), out result))
        result = defValue;
    }
    return result;
  }

  /// <summary>Установить значение атрибута как DateTime</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде DateTime</param>
  public virtual void SetAsDateTime(string attr, DateTime value)
  {
    this.attributes[attr] = (object) value;
  }

  /// <summary>
  /// Получить представление Int32 атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Int32 атрибута или значение по умолчанию</returns>
  public virtual int GetAsInt32(string attr, int defValue)
  {
    int result = defValue;
    if (this.attributes.ContainsKey(attr))
    {
      object attribute = this.attributes[attr];
      if (attribute == null || attribute == DBNull.Value || !int.TryParse(attribute.ToString(), out result))
        result = defValue;
    }
    return result;
  }

  /// <summary>Установить значение атрибута как Int32</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Int32</param>
  public virtual void SetAsInt32(string attr, int value) => this.attributes[attr] = (object) value;

  /// <summary>
  /// Получить представление Int64 атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Int64 атрибута или значение по умолчанию</returns>
  public virtual long GetAsInt64(string attr, long defValue)
  {
    long result = defValue;
    if (this.attributes.ContainsKey(attr))
    {
      object attribute = this.attributes[attr];
      if (attribute == null || attribute == DBNull.Value || !long.TryParse(attribute.ToString(), out result))
        result = defValue;
    }
    return result;
  }

  /// <summary>Установить значение атрибута как Int64</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Int64</param>
  public virtual void SetAsInt64(string attr, long value) => this.attributes[attr] = (object) value;

  /// <summary>
  /// Получить представление Double атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Double атрибута или значение по умолчанию</returns>
  public virtual double GetAsDouble(string attr, double defValue)
  {
    double result = defValue;
    if (this.attributes.ContainsKey(attr))
    {
      object attribute = this.attributes[attr];
      if (attribute == null || attribute == DBNull.Value || !double.TryParse(attribute.ToString(), out result))
        result = defValue;
    }
    return result;
  }

  /// <summary>Установить значение атрибута как Double</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Double</param>
  public virtual void SetAsDouble(string attr, double value)
  {
    this.attributes[attr] = (object) value;
  }

  /// <summary>
  /// Получить представление Guid атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Guid атрибута или значение по умолчанию</returns>
  public virtual Guid GetAsGuid(string attr, Guid defValue)
  {
    Guid asGuid = defValue;
    if (this.attributes.ContainsKey(attr))
    {
      object attribute = this.attributes[attr];
      asGuid = attribute == null || attribute == DBNull.Value || !GuidHelper.IsGuid(attribute.ToString()) ? defValue : new Guid(attribute.ToString());
    }
    return asGuid;
  }

  /// <summary>Установить значение атрибута как Guid</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Guid</param>
  public virtual void SetAsGuid(string attr, Guid value) => this.attributes[attr] = (object) value;

  /// <summary>
  /// Получить представление Boolean атрибута с указанным именем
  /// </summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="defValue">Значение по умолчанию, возвращается, если атрибута нет</param>
  /// <returns>Значение Boolean атрибута или значение по умолчанию</returns>
  public virtual bool GetAsBoolean(string attr, bool defValue)
  {
    bool result = defValue;
    if (this.attributes.ContainsKey(attr))
    {
      object attribute = this.attributes[attr];
      if (attribute == null || attribute == DBNull.Value || !bool.TryParse(attribute.ToString(), out result))
        result = defValue;
    }
    return result;
  }

  /// <summary>Установить значение атрибута как Boolean</summary>
  /// <param name="attr">Имя атрибута</param>
  /// <param name="value">Значение атрибута в виде Boolean</param>
  public virtual void SetAsBoolean(string attr, bool value)
  {
    this.attributes[attr] = (object) value;
  }

  /// <summary>Загрузить содержимое объекта из узла XML</summary>
  /// <param name="node">Узел XML</param>
  /// <returns>true - узел считал содержимое корректно</returns>
  public virtual bool Load(XElement node)
  {
    XmlImportBase owner = this.Owner;
    this.Clear();
    this.Owner = owner;
    if (node == null || node.NodeType != XmlNodeType.Element)
      return false;
    this.Name = node.Name.LocalName;
    foreach (XAttribute attribute in node.Attributes())
      this.SetAsString(attribute.Name.LocalName, attribute.Value);
    foreach (XElement element in node.Elements())
    {
      this.Items = this.Items ?? new List<XmlImportBase>();
      XmlImportBase xmlImportBase = new XmlImportBase(this);
      if (!xmlImportBase.Load(element))
        this.Remove(xmlImportBase);
    }
    if (this.Items == null)
      this.Value = node.Value;
    return true;
  }

  /// <summary>
  /// Загрузить XML из заданного атрибута указанного объекта
  /// </summary>
  /// <param name="obj">Объект</param>
  /// <param name="attrId">Идентификатор типа атрибута</param>
  /// <param name="logger"></param>
  /// <returns>Коллекция элементов XML</returns>
  public static XmlImportBase Load(IDBObject obj, int attrId, ILogger logger)
  {
    XmlImportBase xmlImportBase = new XmlImportBase();
    if (obj == null)
      return xmlImportBase;
    if (!(obj.GetAttributeByID(attrId) is IBlobReader attributeById))
      return xmlImportBase;
    Stream stream;
    try
    {
      MemoryStream inStream = new MemoryStream();
      int dataBlockSize = 262144 /*0x040000*/;
      BlobInformation blobInformation = attributeById.OpenBlob(dataBlockSize);
      long num1 = 0;
      long num2 = blobInformation.ArcMethod == ArcMethods.ZLibPacked ? blobInformation.PackedFileSize : blobInformation.RealFileSize;
      while (num1 < num2)
      {
        int num3 = num2 - num1 > (long) dataBlockSize ? dataBlockSize : (int) (num2 - num1);
        byte[] buffer = attributeById.ReadDataBlock(num3);
        num1 += (long) num3;
        inStream.Write(buffer, 0, num3);
      }
      if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
      {
        inStream.Position = 0L;
        MemoryStream outStream = new MemoryStream();
        if (inStream.Length != 0L)
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) outStream, (Stream) inStream);
        stream = (Stream) outStream;
      }
      else
        stream = (Stream) inStream;
    }
    catch (Exception ex)
    {
      Exception e = new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.XmlExchange_1"), (object) ex.Message), ex);
      if (logger == null)
        throw e;
      logger.LogException(e);
      stream = (Stream) null;
    }
    if (stream == null || stream.Length <= 0L)
      return xmlImportBase;
    stream.Position = 0L;
    try
    {
      XDocument document = XDocument.Load(stream, LoadOptions.PreserveWhitespace);
      if (document.Root != null && string.Compare(document.Root.Name.ToString(), "XMLImportSettings", StringComparison.InvariantCultureIgnoreCase) != 0)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.XmlExchange_3"), (object) "XMLImportSettings"));
      List<XmlImportBase> xmlImportBaseList = XmlImportBase.Load(document, document.Root, string.Empty);
      if (document.Root != null)
        xmlImportBase.Name = document.Root.Name.LocalName;
      if (xmlImportBaseList != null)
      {
        if (xmlImportBaseList.Count > 0)
          xmlImportBase.Items = xmlImportBaseList;
      }
    }
    catch (Exception ex)
    {
      Exception e = new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.XmlExchange_1"), (object) ex.Message), ex);
      if (logger == null)
        throw e;
      logger.LogException(e);
      xmlImportBase = new XmlImportBase();
    }
    return xmlImportBase;
  }

  /// <summary>Загрузить список из узла указанного документа</summary>
  /// <param name="document">Документ</param>
  /// <param name="node">Узел</param>
  /// <param name="nodeName">Имя узла</param>
  /// <returns>Список</returns>
  public static List<XmlImportBase> Load(XDocument document, XElement node, string nodeName)
  {
    List<XmlImportBase> xmlImportBaseList = new List<XmlImportBase>();
    if (string.IsNullOrEmpty(nodeName))
    {
      if (document != null && node?.Elements() != null)
      {
        foreach (XElement element in node.Elements())
        {
          XmlImportBase xmlImportBase = new XmlImportBase();
          if (xmlImportBase.Load(element))
            xmlImportBaseList.Add(xmlImportBase);
        }
      }
    }
    else if (document != null && node?.Elements((XName) nodeName) != null)
    {
      foreach (XElement element in node.Elements((XName) nodeName))
      {
        XmlImportBase xmlImportBase = new XmlImportBase();
        if (xmlImportBase.Load(element))
          xmlImportBaseList.Add(xmlImportBase);
      }
    }
    return xmlImportBaseList;
  }

  /// <summary>
  /// Отыскать в коллекции дочерних элементов первый узел, содержащий указанное значение заданного атрибута.
  /// Уровень вложенности определяется элементами в пути path. Например, в коллекции хранится иерархия
  /// вида:
  ///  [Level0]
  ///   [Level1]
  ///    [Level2 idAttr="idValue1"]
  ///     [Level3/]
  ///    [/Level2]
  ///    [Level2 idAttr="idValue2"]
  ///     [Level3/]
  ///    [/Level2]
  ///   [/Level1]
  ///   [Level1]
  ///    [Level2 idAttr="idValue3"]
  ///     [Level3/]
  ///    [/Level2]
  ///    [Level2 idAttr="idValue4"]
  ///     [Level3/]
  ///    [/Level2]
  ///   [/Level1]
  ///  [\Level0]
  /// 
  /// Требуется отыскать узел [Level2], содержащий idAttr="idValue3".
  /// В этом случае в path следует передать "Level0", "Level1", "Level2"
  /// </summary>
  /// <param name="idAttr">Имя идентификационного атрибута</param>
  /// <param name="idValue">Значение, по которому осуществляется поиск</param>
  /// <param name="path">Полный путь к искомым узлам</param>
  /// <returns>Первый подходящий узел</returns>
  public XmlImportBase Find(string idAttr, string idValue, params string[] path)
  {
    List<XmlImportBase> all = this.FindAll(idAttr, idValue, path);
    return all == null || all.Count <= 0 ? (XmlImportBase) null : all[0];
  }

  /// <summary>
  /// Отыскать в коллекции дочерних элементов узлы, содержащие указанное значение заданного атрибута.
  /// Уровень вложенности определяется элементами в пути path. Например, в коллекции хранится иерархия
  /// вида:
  ///  [Level0]
  ///   [Level1]
  ///    [Level2 idAttr="idValue1"]
  ///     [Level3/]
  ///    [/Level2]
  ///    [Level2 idAttr="idValue2"]
  ///     [Level3/]
  ///    [/Level2]
  ///   [/Level1]
  ///   [Level1]
  ///    [Level2 idAttr="idValue3"]
  ///     [Level3/]
  ///    [/Level2]
  ///    [Level2 idAttr="idValue4"]
  ///     [Level3/]
  ///    [/Level2]
  ///   [/Level1]
  ///  [\Level0]
  /// 
  /// Требуется отыскать узел [Level2], содержащий idAttr="idValue3".
  /// В этом случае в path следует передать "Level0", "Level1", "Level2"
  /// </summary>
  /// <param name="idAttr">Имя идентификационного атрибута</param>
  /// <param name="idValue">Значение, по которому осуществляется поиск</param>
  /// <param name="path">Полный путь к искомым узлам</param>
  /// <returns>Первый подходящий узел</returns>
  public List<XmlImportBase> FindAll(string idAttr, string idValue, params string[] path)
  {
    if (this.Items == null || this.Items.Count == 0 || string.IsNullOrEmpty(idAttr) || string.IsNullOrEmpty(idValue) || path == null || path.Length == 0)
      return (List<XmlImportBase>) null;
    idAttr = idAttr.ToLowerInvariant();
    for (int index = 0; index < path.Length; ++index)
      path[index] = path[index].ToLowerInvariant();
    int idx = 0;
    List<XmlImportBase> xmlImportBaseList1;
    for (xmlImportBaseList1 = this.Items; idx < path.Length - 1 && xmlImportBaseList1 != null; idx++)
    {
      List<XmlImportBase> xmlImportBaseList2 = new List<XmlImportBase>();
      for (int index = 0; index < xmlImportBaseList1.Count; ++index)
      {
        if (xmlImportBaseList1[index].Name.ToLowerInvariant() == path[idx] && xmlImportBaseList1[index].Items != null)
          xmlImportBaseList2.AddRange((IEnumerable<XmlImportBase>) xmlImportBaseList1[index].Items);
      }
      xmlImportBaseList1 = xmlImportBaseList2;
    }
    return xmlImportBaseList1?.FindAll((Predicate<XmlImportBase>) (item => item.Name.ToLowerInvariant() == path[idx] && Convert.ToString(item[idAttr]) == idValue));
  }

  /// <summary>
  /// Отыскать в коллекции дочерних элементов первый узел с указанным уникальным именем
  /// </summary>
  /// <param name="path">Полный путь к искомым узлам</param>
  /// <returns>Первый подходящий узел</returns>
  public XmlImportBase Find(params string[] path)
  {
    List<XmlImportBase> all = this.FindAll(path);
    return all == null || all.Count <= 0 ? (XmlImportBase) null : all[0];
  }

  /// <summary>
  /// Отыскать в коллекции дочерних элементов узлы с указанным уникальным именем
  /// </summary>
  /// <param name="path">Полный путь к искомым узлам</param>
  /// <returns>Первый подходящий узел</returns>
  public List<XmlImportBase> FindAll(params string[] path)
  {
    if (this.Items == null || this.Items.Count == 0 || path == null || path.Length == 0)
      return (List<XmlImportBase>) null;
    for (int index = 0; index < path.Length; ++index)
      path[index] = path[index].ToLowerInvariant();
    int idx = 0;
    List<XmlImportBase> xmlImportBaseList1;
    for (xmlImportBaseList1 = this.Items; idx < path.Length - 1 && xmlImportBaseList1 != null; idx++)
    {
      List<XmlImportBase> xmlImportBaseList2 = new List<XmlImportBase>();
      for (int index = 0; index < xmlImportBaseList1.Count; ++index)
      {
        if (xmlImportBaseList1[index].Name.ToLowerInvariant() == path[idx] && xmlImportBaseList1[index].Items != null)
          xmlImportBaseList2.AddRange((IEnumerable<XmlImportBase>) xmlImportBaseList1[index].Items);
      }
      xmlImportBaseList1 = xmlImportBaseList2;
    }
    return xmlImportBaseList1 == null || xmlImportBaseList1.Count == 0 ? (List<XmlImportBase>) null : xmlImportBaseList1.FindAll((Predicate<XmlImportBase>) (item => item.Name.ToLowerInvariant() == path[idx]));
  }
}
