// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.OptionObjectDescription
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Класс-контейнер, в котором хранится краткое описание объекта-опции конфигуратора составов IPS
/// (только ключевые атрибуты)
/// </summary>
public class OptionObjectDescription : ObjectVersionDescription
{
  /// <summary>Категория опции</summary>
  public long Category;
  /// <summary>Список колонок для запроса в "ядро"</summary>
  protected new static List<ColumnDescriptor> columnDescriptors = new List<ColumnDescriptor>();

  /// <summary>Создать пустой экземпляр класса</summary>
  public OptionObjectDescription()
  {
  }

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="_ID">Идентификатор объекта</param>
  /// <param name="_OBJECT_ID">Идентификатор версии объекта</param>
  /// <param name="_OBJECT_TYPE">Идентификатор типа объекта</param>
  /// <param name="_LCSTEP_ID">Шаг ЖЦ</param>
  /// <param name="_OWNER_ID">Владелец объекта</param>
  /// <param name="_CHKOUT_BY">Владелец объекта</param>
  /// <param name="_CAPTION">Заголовок</param>
  /// <param name="_F_VERSION_ID">Номер версии</param>
  /// <param name="_F_MODIFICATION_ID">Номер группы изменений</param>
  /// <param name="_F_BASE_VERSION">Признак базовой версии</param>
  /// <param name="_Options">Опции</param>
  public OptionObjectDescription(
    long _ID,
    long _OBJECT_ID,
    int _OBJECT_TYPE,
    int _LCSTEP_ID,
    long _OWNER_ID,
    long _CHKOUT_BY,
    string _CAPTION,
    long _F_VERSION_ID,
    long _F_MODIFICATION_ID,
    long _F_BASE_VERSION,
    ObjectVersionDescriptionOptions _Options)
  {
    this.F_ID = _ID;
    this.F_OBJECT_ID = _OBJECT_ID;
    this.F_OBJECT_TYPE = _OBJECT_TYPE;
    this.F_LCSTEP_ID = _LCSTEP_ID;
    this.F_OWNER_ID = _OWNER_ID;
    this.F_CHKOUT_BY = _CHKOUT_BY;
    this.CAPTION = _CAPTION;
    this.F_VERSION_ID = _F_VERSION_ID;
    this.F_MODIFICATION_ID = _F_MODIFICATION_ID;
    this.F_BASE_VERSION = _F_BASE_VERSION;
    this.Options = _Options;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из строки таблицы
  /// </summary>
  /// <param name="row">Строка таблицы с данными</param>
  public OptionObjectDescription(DataRow row) => this.Assign((object) row);

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта
  /// </summary>
  /// <param name="source">Источник информации</param>
  public OptionObjectDescription(object source) => this.Assign(source);

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта
  /// </summary>
  /// <param name="source">Объект-описатель</param>
  public OptionObjectDescription(IDBObject source) => this.Assign((object) source);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return obj is ObjectVersionDescription versionDescription && this.F_OBJECT_ID == versionDescription.F_OBJECT_ID;
  }

  /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.F_OBJECT_ID.GetHashCode();

  /// <summary>
  /// Получить представление экземпляра класса в виде строки
  /// </summary>
  /// <returns>Строковое представление экземпляра класса</returns>
  public override string ToString()
  {
    return string.Format("[{0} {ver.3}] \"{1}\" (\"{2}\")", (object) this.F_OBJECT_ID, (object) this.CAPTION, (object) MetaDataHelper.GetObjectTypeName(this.F_OBJECT_TYPE), (object) this.F_VERSION_ID);
  }

  /// <summary>
  /// Получить список колонок, необходимых для получения списка объектов
  /// </summary>
  /// <returns>Список колонок, необходимых для получения списка объектов</returns>
  public override List<ColumnDescriptor> GetColumnDescriptors()
  {
    if (OptionObjectDescription.columnDescriptors.Count != 0)
      return OptionObjectDescription.columnDescriptors;
    OptionObjectDescription.columnDescriptors.AddRange((IEnumerable<ColumnDescriptor>) base.GetColumnDescriptors());
    OptionObjectDescription.columnDescriptors.Add(new ColumnDescriptor((object) Consts.attributeCategoryLinkID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    return OptionObjectDescription.columnDescriptors;
  }

  /// <summary>Очистить экземпляр класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.Category = Consts.objectNoCategoryID;
  }

  /// <summary>Скопировать информацию из указанного объекта</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    base.Assign(source);
    switch (source)
    {
      case DataRow row:
        this.Category = DataSetProcessor.GetInt64Value(row, "cad015a4-306c-11d8-b4e9-00304f19f545", Consts.objectNoCategoryID);
        this.CalcFields();
        break;
      case OptionObjectDescription objectDescription:
        this.Category = objectDescription.Category;
        this.CalcFields();
        break;
      case IDBObject dbObject:
        IDBAttribute attributeById = dbObject.GetAttributeByID(Consts.attributeCategoryLinkID);
        if (attributeById != null)
          this.Category = DataSetProcessor.GetInt64Value(attributeById.Value, Consts.objectNoCategoryID);
        this.CalcFields();
        break;
    }
  }
}
