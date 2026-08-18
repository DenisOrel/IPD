// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.UserMessage
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

#nullable disable
namespace Intermech.Forums;

/// <summary>сообщение пользователя</summary>
[Serializable]
public class UserMessage
{
  /// <summary>Сообщение</summary>
  private string message = string.Empty;
  /// <summary>Заголовок</summary>
  private string caption = string.Empty;
  /// <summary>Дата отправления сообщения</summary>
  private DateTime date = DateTime.Now;
  /// <summary>Дата последнего редактирования сообщения</summary>
  private DateTime modifyDate = DateTime.MinValue;
  /// <summary>здесь будет guid</summary>
  private string userGuid = string.Empty;
  /// <summary>guid обсуждения, которому принадлежит это сообщение</summary>
  private string dicsObjectGuid = string.Empty;
  /// <summary>guid узла, опубликовавшего обcуждение</summary>
  private string siteGuid = string.Empty;
  /// <summary>guid верcии обсуждаемого объекта</summary>
  private string discussedObjectGuid = string.Empty;
  /// <summary>
  /// Какими пользователями уже было прочитано данное сообщение.
  /// Хранит Гуиды пользователей
  /// Автор тоже считается и пишется при создании сообщения.
  /// Если пустое, значит сообщение создавалось до введения этой настройки.
  /// </summary>
  private List<string> readByUsers = new List<string>();

  /// <summary>Сообщение</summary>
  public string Message
  {
    get => this.message;
    set => this.message = value;
  }

  /// <summary>Заголовок</summary>
  public string Caption
  {
    get => this.caption;
    set => this.caption = value;
  }

  /// <summary>ID пользователя, оставившего сообщение</summary>
  public string UserGuid
  {
    get => this.userGuid;
    set => this.userGuid = value;
  }

  /// <summary>Дата отправления сообщения</summary>
  public DateTime Date
  {
    get => this.date;
    set => this.date = value;
  }

  /// <summary>Дата последнего редактирования сообщения</summary>
  public DateTime ModifyDate
  {
    get => this.modifyDate;
    set => this.modifyDate = value;
  }

  /// <summary>
  /// id версии Обсуждения, которому принадлежит данное сообщение
  /// </summary>
  public string DicsObjectGuid
  {
    get => this.dicsObjectGuid;
    set => this.dicsObjectGuid = value;
  }

  /// <summary>guid верcии обсуждаемого объекта</summary>
  public string DiscussedObjectGuid
  {
    get => this.discussedObjectGuid;
    set => this.discussedObjectGuid = value;
  }

  /// <summary>
  /// Какими пользователями уже было прочитано данное сообщение
  /// Хранит Гуиды пользователей
  /// </summary>
  public List<string> ReadByUsers
  {
    get => this.readByUsers;
    set => this.readByUsers = value;
  }

  /// <summary>
  /// guid узла, опубликовавшего обcуждение,
  /// которому принадлежит это сообщение.
  /// при добавлении с текущего узла, заполнять не буду
  /// </summary>
  public string SiteGuid
  {
    get => this.siteGuid;
    set => this.siteGuid = value;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (this.modifyDate != DateTime.MinValue)
    {
      stringBuilder.Append(this.modifyDate.ToString("u", (IFormatProvider) CultureInfo.InvariantCulture));
      stringBuilder.Append(ForumsConsts.SplitterChar);
    }
    stringBuilder.Append(this.userGuid);
    stringBuilder.Append(ForumsConsts.SplitterChar);
    if (this.siteGuid != string.Empty)
    {
      stringBuilder.Append(this.siteGuid);
      stringBuilder.Append(ForumsConsts.SplitterChar);
    }
    stringBuilder.Append(this.date.ToString("u", (IFormatProvider) CultureInfo.InvariantCulture));
    stringBuilder.Append(ForumsConsts.SplitterChar);
    stringBuilder.Append(this.caption);
    stringBuilder.Append(ForumsConsts.SplitterChar);
    stringBuilder.Append(this.message);
    if (this.readByUsers.Count > 0)
    {
      stringBuilder.Append(ForumsConsts.SplitterChar);
      stringBuilder.Append(ForumsConsts.UsersMark);
      stringBuilder.Append(string.Join(ForumsConsts.SplitterForUsers.ToString() ?? "", (IEnumerable<string>) this.readByUsers));
    }
    stringBuilder.Insert(0, ForumsConsts.SplitterChar);
    return stringBuilder.ToString();
  }

  public int FromString(string codeString)
  {
    int num1 = -1;
    if (string.IsNullOrEmpty(codeString))
      return num1;
    string[] strArray1 = codeString.Split(new char[1]
    {
      ForumsConsts.SplitterChar
    }, StringSplitOptions.None);
    if (strArray1.Length < 4)
      return num1;
    int index1 = 0;
    int num2 = 0;
    if (!GuidHelper.IsGuid(strArray1[index1].ToString()))
    {
      if (strArray1[index1] == string.Empty)
        return num1;
      if (!DateTime.TryParse(strArray1[index1], (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out this.modifyDate))
        this.modifyDate = Convert.ToDateTime(strArray1[index1]);
      this.modifyDate = this.modifyDate.ToUniversalTime();
      num2 += strArray1[index1++].Length;
    }
    this.userGuid = strArray1[index1];
    int num3 = num2;
    string[] strArray2 = strArray1;
    int index2 = index1;
    int index3 = index2 + 1;
    int length1 = strArray2[index2].Length;
    int num4 = num3 + length1;
    if (GuidHelper.IsGuid(strArray1[index3].ToString()))
    {
      this.siteGuid = strArray1[index3];
      num4 += strArray1[index3++].Length;
    }
    if (!DateTime.TryParse(strArray1[index3], (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out this.date))
      this.date = Convert.ToDateTime(strArray1[index3]);
    this.date = this.date.ToUniversalTime();
    int num5 = num4;
    string[] strArray3 = strArray1;
    int index4 = index3;
    int index5 = index4 + 1;
    int length2 = strArray3[index4].Length;
    int num6 = num5 + length2;
    this.caption = strArray1[index5];
    int num7 = num6;
    string[] strArray4 = strArray1;
    int index6 = index5;
    int index7 = index6 + 1;
    int length3 = strArray4[index6].Length;
    int num8 = num7 + length3;
    this.message = strArray1[index7];
    int num9 = num8;
    string[] strArray5 = strArray1;
    int index8 = index7;
    int index9 = index8 + 1;
    int length4 = strArray5[index8].Length;
    int num10 = num9 + length4;
    if (index9 < strArray1.Length && strArray1[index9].Contains(ForumsConsts.UsersMark))
    {
      this.readByUsers = new List<string>((IEnumerable<string>) strArray1[index9].Remove(0, ForumsConsts.UsersMark.Length).Split(ForumsConsts.SplitterForUsers));
      num10 += strArray1[index9++].Length;
    }
    return num10 + ForumsConsts.Splitter.Length * index9;
  }

  public override int GetHashCode() => this.date.GetHashCode() ^ this.userGuid.GetHashCode() << 2;

  public override bool Equals(object obj)
  {
    return obj != null && obj is UserMessage && this.GetHashCode().Equals(obj.GetHashCode());
  }
}
