// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.AdjustableView
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Описание настраиваемой закладки ("вьюшки") "Навигатора"
/// </summary>
[DebuggerDisplay("[{Visible}] {Caption} ({Name}) - {ObjectTypes.Count}")]
[Serializable]
public sealed class AdjustableView : IAdjustableView, ICloneable
{
  /// <summary>Название модуля по умолчанию - "Универсальный клиент"</summary>
  internal static readonly string DefModuleName = LocalizationHolder.rm.GetString("Interfaces.Client_79");
  /// <summary>Уникальное в пределах всей системы имя закладки</summary>
  private string _Name;
  /// <summary>Краткое текстовое название заладки</summary>
  private string _Caption;
  /// <summary>Более подробное текстовое описание закладки</summary>
  private string _Hint;
  /// <summary>Название модуля (плагина), который создаёт закладку</summary>
  private string _Module;
  /// <summary>
  /// Название значка закладки (из коллекции именованных значков)
  /// </summary>
  private string _ImageName;
  /// <summary>
  /// Флажок позволяет прятать или показывать данную закладку на панелях "Навигатора"
  /// </summary>
  private bool _Visible = true;
  /// <summary>
  /// Порядковый номер закладки на менеджере закладок "Навигатора"
  /// </summary>
  private int _OrderID;
  /// <summary>
  /// Список типов объектов, для которых данная закладка будет автоматически выбираться активной
  /// </summary>
  private List<int> _ObjectTypes = new List<int>();

  /// <summary>
  /// Создать экземпляр настраиваемой команды контекстного меню
  /// </summary>
  /// <param name="name">Уникальное в пределах всей системы имя закладки</param>
  /// <param name="caption">Краткое текстовое название заладки</param>
  /// <param name="visible">Флажок позволяет прятать или показывать данную закладку на панелях "Навигатора"</param>
  /// <param name="hint">Более подробное текстовое описание закладки</param>
  /// <param name="imageName">Название значка закладки (из коллекции именованных значков)</param>
  /// <param name="module">Название модуля (плагина), который создаёт закладку</param>
  /// <param name="orderID">Порядковый номер закладки на менеджере закладок "Навигатора"</param>
  public AdjustableView(
    string name,
    string caption,
    bool visible,
    string hint,
    string module,
    string imageName,
    int orderID)
  {
    this._Name = name;
    this._Caption = caption;
    this._Visible = visible;
    this._Module = module != string.Empty ? module : AdjustableView.DefModuleName;
    this._Hint = hint;
    this._ImageName = imageName;
    this._OrderID = orderID;
  }

  /// <summary>
  /// Список типов объектов, для которых данная закладка будет автоматически выбираться активной
  /// </summary>
  public List<int> ObjectTypes
  {
    get
    {
      this._ObjectTypes = this._ObjectTypes ?? new List<int>();
      return this._ObjectTypes;
    }
  }

  /// <summary>Проверить корректность полей</summary>
  public void Check()
  {
    List<int> types = new List<int>(this.ObjectTypes.Count);
    this.ObjectTypes.ForEach((Action<int>) (typeID =>
    {
      if (types.Contains(typeID))
        return;
      types.Add(typeID);
    }));
    this.ObjectTypes.Clear();
    this.ObjectTypes.AddRange((IEnumerable<int>) types);
  }

  /// <summary>Пакетная установка свойств</summary>
  /// <param name="options">Массив опций.
  /// [0] - (string)Name,
  /// [1] - (string)Caption,
  /// [2] - (string)Hint,
  /// [3] - (string)Module,
  /// [4] - (string)ImageName,
  /// [5] - (bool)Visible,
  /// [6] - (int)OrderID
  /// [7] - (List[Int32])ObjectTypes</param>
  public void BatchPropertiesSet(params object[] options)
  {
    if (options == null || options.Length == 0)
      return;
    int length = options.Length;
    if (length > 0 && options[0] != null)
      this._Name = (string) options[0];
    if (length > 1 && options[1] != null)
      this._Caption = (string) options[1];
    if (length > 2 && options[2] != null)
      this._Hint = (string) options[2];
    if (length > 3 && options[3] != null)
      this._Module = (string) options[3] != string.Empty ? (string) options[3] : AdjustableView.DefModuleName;
    if (length > 4 && options[4] != null)
      this._ImageName = (string) options[4];
    if (length > 5 && options[5] != null)
      this._Visible = (bool) options[5];
    if (length > 6 && options[6] != null)
      this._OrderID = (int) options[6];
    if (length <= 7 || options[7] == null)
      return;
    this.ObjectTypes.Clear();
    if (!(options[7] is List<int> option))
      return;
    option.ForEach((Action<int>) (typeID =>
    {
      if (this.ObjectTypes.Contains(typeID))
        return;
      this.ObjectTypes.Add(typeID);
    }));
  }

  /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (!(obj is AdjustableView adjustableView))
      return base.Equals(obj);
    return this._Name == adjustableView._Name && this._Caption == adjustableView._Caption;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    return this._Name.GetHashCode() << 16 /*0x10*/ ^ this._Caption.GetHashCode();
  }

  /// <summary>Уникальное в пределах всей системы имя закладки</summary>
  public string Name
  {
    [DebuggerStepThrough] get => this._Name;
    set => this._Name = value != string.Empty ? value : this._Name;
  }

  /// <summary>Краткое текстовое название заладки</summary>
  public string Caption
  {
    [DebuggerStepThrough] get => this._Caption;
    set => this._Caption = value;
  }

  /// <summary>Более подробное текстовое описание закладки</summary>
  public string Hint
  {
    [DebuggerStepThrough] get => this._Hint;
    set => this._Hint = value;
  }

  /// <summary>Название модуля (плагина), который создаёт закладку</summary>
  public string Module
  {
    [DebuggerStepThrough] get => this._Module;
    set => this._Module = value != string.Empty ? value : AdjustableView.DefModuleName;
  }

  /// <summary>
  /// Название значка закладки (из коллекции именованных значков)
  /// </summary>
  public string ImageName
  {
    [DebuggerStepThrough] get => this._ImageName;
    set => this._ImageName = value;
  }

  /// <summary>
  /// Флажок позволяет прятать или показывать данную закладку на панелях "Навигатора"
  /// </summary>
  public bool Visible
  {
    [DebuggerStepThrough] get => this._Visible;
    set => this._Visible = value;
  }

  /// <summary>
  /// Порядковый номер закладки на менеджере закладок "Навигатора"
  /// </summary>
  public int OrderID
  {
    [DebuggerStepThrough] get => this._OrderID;
    set => this._OrderID = value >= 0 ? value : this._OrderID;
  }

  public object Clone()
  {
    AdjustableView adjustableView = new AdjustableView(this.Name, this.Caption, this.Visible, this.Hint, this.Module, this.ImageName, this.OrderID);
    adjustableView.ObjectTypes.AddRange((IEnumerable<int>) this.ObjectTypes);
    adjustableView.Check();
    return (object) adjustableView;
  }
}
