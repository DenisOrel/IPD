// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.OptionValue
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Класс, хранящий одно значение опции конфигуратора составов IPS
/// </summary>
[Serializable]
public sealed class OptionValue : ICloneable, IAssignable
{
  /// <summary>Уникальный ID значения опции</summary>
  private string _id;
  /// <summary>Код значения опции (уникален в списке значений опции)</summary>
  private string _code;
  /// <summary>Значение опции</summary>
  private string _value;
  /// <summary>Описание значения опции</summary>
  private string _description;
  /// <summary>Ссылка на библиотечное изображение</summary>
  private Guid _image;
  /// <summary>Флажки значения опции</summary>
  private OptionValueFlags _flags;
  /// <summary>Guid пользователя, изменившего данное значение</summary>
  private Guid _user;
  /// <summary>Дата и время последних изменений</summary>
  private DateTime _lastModified;

  /// <summary>Уникальный ID значения опции</summary>
  public string ID
  {
    [DebuggerStepThrough] get => this._id;
    set => this._id = value;
  }

  /// <summary>Код значения опции (уникален в списке значений опции)</summary>
  public string Code
  {
    [DebuggerStepThrough] get => this._code;
    set => this._code = StringsHelper.TrimString(value, 20);
  }

  /// <summary>Значение опции</summary>
  public string Value
  {
    [DebuggerStepThrough] get => this._value;
    set => this._value = StringsHelper.TrimString(value, 200);
  }

  /// <summary>Описание значения опции</summary>
  public string Description
  {
    [DebuggerStepThrough] get => this._description;
    set => this._description = StringsHelper.TrimString(value, 1000);
  }

  /// <summary>Ссылка на библиотечное изображение</summary>
  public Guid Image
  {
    [DebuggerStepThrough] get => this._image;
    set => this._image = value;
  }

  /// <summary>Флажки значения опции</summary>
  public OptionValueFlags Flags
  {
    [DebuggerStepThrough] get => this._flags;
    set => this._flags = value;
  }

  /// <summary>Guid пользователя, изменившего данное значение</summary>
  public Guid User
  {
    [DebuggerStepThrough] get => this._user;
    set => this._user = value;
  }

  /// <summary>Дата и время последних изменений</summary>
  public DateTime LastModified
  {
    [DebuggerStepThrough] get => this._lastModified;
    set => this._lastModified = value;
  }

  /// <summary>Создать пустое значение опции</summary>
  public OptionValue()
  {
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public OptionValue(object source) => this.Assign(source);

  /// <summary>Создать заполненное значение опции</summary>
  /// <param name="id">ID значения опции</param>
  /// <param name="code">Код значения опции (уникален в списке значений опции)</param>
  /// <param name="value">Значение опции</param>
  /// <param name="description">Описание значения опции</param>
  /// <param name="image">Ссылка на библиотечное изображение</param>
  /// <param name="flags">Флажки значения опции</param>
  /// <param name="user">Пользователь, изменивший данное значение</param>
  /// <param name="lastModified">Дата и время последнего изменения значения</param>
  public OptionValue(
    string id,
    string code,
    string value,
    string description,
    Guid image,
    OptionValueFlags flags,
    Guid user,
    DateTime lastModified)
  {
    this.ID = id;
    this.Code = code;
    this.Value = value;
    this.Description = description;
    this.Image = image;
    this.Flags = flags;
    this.User = user;
    this.LastModified = lastModified;
  }

  /// <summary>
  /// Создать значение опции на основе указанной кодированной строки
  /// </summary>
  /// <param name="codedValue">Значение опции в виде кодированной строки</param>
  public OptionValue(string codedValue) => this.Assign((object) codedValue);

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._id = string.Empty;
    this._code = string.Empty;
    this._value = string.Empty;
    this._description = string.Empty;
    this._image = Guid.Empty;
    this._flags = OptionValueFlags.None;
    this._user = Guid.Empty;
    this._lastModified = DateTime.UtcNow;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    switch (source)
    {
      case string _:
        this.FromString((string) source);
        break;
      case OptionValue optionValue:
        this.ID = optionValue.ID;
        this.Code = optionValue.Code;
        this.Value = optionValue.Value;
        this.Description = optionValue.Description;
        this.Image = optionValue.Image;
        this.Flags = optionValue.Flags;
        this.User = optionValue.User;
        this.LastModified = optionValue.LastModified;
        break;
    }
  }

  /// <summary>
  /// Заполнить экземпляр класса информацией из кодированной строки
  /// </summary>
  /// <param name="val">Кодированная строка</param>
  /// <returns>Позиция строки, до которой хранились значения опции</returns>
  internal int FromString(string val)
  {
    int num1 = -1;
    this.Clear();
    if (string.IsNullOrEmpty(val))
      return num1;
    string[] strArray = val.Split(Helper.Splitter, StringSplitOptions.None);
    if (strArray == null || strArray.Length < 8)
      return num1;
    this.ID = strArray[0];
    this.Code = strArray[1];
    this.Value = strArray[2];
    this.Description = strArray[3];
    this.Image = GuidHelper.IsGuid(strArray[4]) ? new Guid(strArray[4]) : Guid.Empty;
    this.Flags = (OptionValueFlags) DataSetProcessor.GetInt64Value((object) strArray[5], 0L);
    this.User = GuidHelper.IsGuid(strArray[6]) ? new Guid(strArray[6]) : Guid.Empty;
    DateTime result = DateTime.UtcNow;
    this.LastModified = !DateTime.TryParse(strArray[7], (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) ? DateTime.UtcNow : result;
    int num2 = strArray[0].Length + strArray[1].Length + strArray[2].Length + strArray[3].Length + strArray[4].Length + strArray[5].Length + strArray[6].Length + strArray[7].Length + Helper.Splitter.Length * 7;
    if (strArray.Length > 8)
      num2 += Helper.Splitter.Length;
    return num2;
  }

  /// <summary>Вернуть значение экземпляра класса в виде строки</summary>
  /// <returns>Значение экземпляра класса в виде строки</returns>
  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(this.ID);
    stringBuilder.Append(Helper.SplitterChar);
    stringBuilder.Append(this.CorrectValue(this.Code));
    stringBuilder.Append(Helper.SplitterChar);
    stringBuilder.Append(this.CorrectValue(this.Value));
    stringBuilder.Append(Helper.SplitterChar);
    stringBuilder.Append(this.CorrectValue(this.Description));
    stringBuilder.Append(Helper.SplitterChar);
    if (this.Image != Guid.Empty)
      stringBuilder.Append(this.Image.ToString());
    stringBuilder.Append(Helper.SplitterChar);
    if (this.Flags != OptionValueFlags.None)
      stringBuilder.Append(StringsHelper.IntToHex((long) this.Flags));
    stringBuilder.Append(Helper.SplitterChar);
    if (this.User != Guid.Empty)
      stringBuilder.Append(this.User.ToString());
    stringBuilder.Append(Helper.SplitterChar);
    stringBuilder.Append(this.LastModified.ToString("G", (IFormatProvider) CultureInfo.InvariantCulture));
    return stringBuilder.ToString();
  }

  /// <summary>Заменить в строке все символы '|' на ' '</summary>
  /// <param name="val">Корректируемая строка</param>
  /// <returns>Откорректированная строка</returns>
  private string CorrectValue(string val) => val.Replace(Helper.SplitterChar, ' ');

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты идентичны</returns>
  public override bool Equals(object obj)
  {
    return obj is OptionValue optionValue && this.ID == optionValue.ID;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.ID.GetHashCode();

  /// <summary>
  /// Вернуть строку, отображающую значение указанной опции в читабельном виде на экране
  /// </summary>
  /// <param name="option">Опция, которой принадлежит данное значение</param>
  /// <returns>Строка, отображающая значение указанной опции в читабельном виде на экране</returns>
  public string GetDisplayValue(OptionHolder option)
  {
    if (option == null || string.IsNullOrEmpty(this.Value))
      return string.Empty;
    switch (option.OptionDataType)
    {
      case FieldTypes.ftInteger:
        return option.GetAsInt64(this.ID).ToString();
      case FieldTypes.ftDouble:
        return option.GetAsDouble(this.ID).ToString();
      case FieldTypes.ftDateTime:
        return option.GetAsDateTime(this.ID).ToShortDateString();
      case FieldTypes.ftBoolean:
        return Helper.Bool2String(option.GetAsBoolean(this.ID));
      default:
        return option.GetAsString(this.ID);
    }
  }
}
