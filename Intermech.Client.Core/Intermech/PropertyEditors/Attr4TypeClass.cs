
// Type: Intermech.PropertyEditors.Attr4TypeClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.Diagnostics;


namespace Intermech.PropertyEditors;

/// <summary>
/// Базовый класс для реализации аттрибутов для абстрактных классов
/// </summary>
/// <remarks>Наследуемся от DBPropDescriptorHolder для возможности раширения
/// интрерфейса
/// </remarks>
/// <summary>Конструктор</summary>
/// <param name="idValue"></param>
public class Attr4TypeClass(object idValue) : DBPropDescriptorHolder(idValue)
{
  /// <summary>
  /// 
  /// </summary>
  /// <remarks>для получения списка мастер-атрибутов извне</remarks>
  protected EventsHolder.GetListDelegate _getMasterList;
  /// <summary>
  /// 
  /// </summary>
  public object Tag;

  /// <summary>Конструктор</summary>
  /// <remarks>Для совмечтимости со старым кодом</remarks>
  public Attr4TypeClass()
    : this((object) null)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  public EventsHolder.GetListDelegate GetMasterList
  {
    [DebuggerStepThrough] get => this._getMasterList;
    [DebuggerStepThrough] set => this._getMasterList = value;
  }

  /// <summary>Наименование типа аттрибутав</summary>
  public virtual string AttributeName
  {
    [DebuggerStepThrough] get => string.Empty;
  }

  /// <summary>Ид. типа аттрибута</summary>
  public virtual int AttributeID
  {
    [DebuggerStepThrough] get => 0;
  }

  /// <summary>Формула, по которой рассчитывается атрибут</summary>
  public virtual string Formula
  {
    [DebuggerStepThrough] get => string.Empty;
  }
}
