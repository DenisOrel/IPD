// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.AdjustableViews
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>Коллекция настраиваемых закладок "Навигатора"</summary>
[Serializable]
public sealed class AdjustableViews : List<AdjustableView>, ICloneable
{
  /// <summary>
  /// Символ в начале строки с названием закладки, который говорит о том,
  /// что имя закладки может начинаться с этой строки
  /// </summary>
  public static char Wildcard = Convert.ToChar("@");
  /// <summary>Guid настроек роли</summary>
  public static string RoleSettingsGuid = "{3B6DE34A-59CD-4F3D-80B0-4EC65D5CA88B}";
  /// <summary>Guid настроек пользователя</summary>
  public static string UserSettingsGuid = "{11FB5231-4A88-4564-89D1-E7F977A51A42}";

  /// <summary>Отыскать в коллекции закладку с указанным именем.</summary>
  /// <param name="name">Уникальное в пределах системы имя закладки</param>
  /// <returns>null, если закладка не найдена</returns>
  public AdjustableView FindView(string name)
  {
    if (name == string.Empty)
      return (AdjustableView) null;
    for (int index = 0; index < this.Count; ++index)
    {
      string name1 = this[index].Name;
      if ((int) name1[0] != (int) AdjustableViews.Wildcard)
      {
        if (name1 == name)
          return this[index];
      }
      else if (name.StartsWith(name1) || name.StartsWith(name1.Substring(1)))
        return this[index];
    }
    return (AdjustableView) null;
  }

  /// <summary>
  /// Добавить новую (обновить существующую) настраиваемую закладку ("вьюшку") "Навигатора" в коллекцию
  /// </summary>
  /// <param name="name">Уникальное в пределах всей системы имя закладки</param>
  /// <param name="caption">Краткое текстовое название заладки</param>
  /// <param name="hint">Более подробное текстовое описание закладки</param>
  /// <param name="module">Название модуля (плагина), который создаёт закладку</param>
  /// <param name="imageName">Название значка закладки (из коллекции именованных значков)</param>
  /// <param name="visible">Флажок позволяет прятать или показывать данную закладку на панелях "Навигатора"</param>
  /// <param name="orderID">Порядковый номер закладки на менеджере закладок "Навигатора"</param>
  /// <returns>Ссылка на новую настраиваемую закладку</returns>
  public AdjustableView Add(
    string name,
    string caption,
    string hint,
    string module,
    string imageName,
    bool visible,
    int orderID)
  {
    AdjustableView view = this.FindView(name);
    if (view != null)
    {
      view.BatchPropertiesSet(null, (object) caption, (object) hint, (object) module, (object) imageName, (object) visible, (object) orderID);
      return view;
    }
    if (name == string.Empty || name == null)
      return (AdjustableView) null;
    AdjustableView adjustableView = new AdjustableView(name, caption, visible, hint, module, imageName, orderID);
    this.Add(adjustableView);
    return adjustableView;
  }

  /// <summary>
  /// Синхронизация настроек закладки с другой коллекцией настроек
  /// </summary>
  /// <param name="source">Источник</param>
  public void Assign(AdjustableViews source)
  {
    if (source == null)
      return;
    for (int index = 0; index < source.Count; ++index)
    {
      AdjustableView adjustableView1 = source[index];
      AdjustableView view = this.FindView(adjustableView1.Name);
      if (view == null)
      {
        this.Add((AdjustableView) adjustableView1.Clone());
      }
      else
      {
        AdjustableView adjustableView2 = view;
        object[] objArray = new object[8];
        objArray[5] = (object) adjustableView1.Visible;
        objArray[6] = (object) adjustableView1.OrderID;
        objArray[7] = (object) adjustableView1.ObjectTypes;
        adjustableView2.BatchPropertiesSet(objArray);
        view.Check();
      }
    }
  }

  public void BatchPropertiesSet(params object[] options)
  {
    if (options == null || options.Length == 0)
      return;
    for (int index = 0; index < this.Count; ++index)
      this[index].BatchPropertiesSet(options);
  }

  /// <summary>
  /// Выполнить синхронизацию с настройками закладок у указанной роли
  /// </summary>
  /// <param name="RoleID">Идентификатор роли</param>
  public void SyncWithRoleSettings(long RoleID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
        return;
      if (!(customService.GetRoleSettingsObject(RoleID, (object) AdjustableViews.RoleSettingsGuid) is byte[] roleSettingsObject))
      {
        customService.LoadRolesSettings((object) sessionKeeper.Session.SessionGUID);
        roleSettingsObject = customService.GetRoleSettingsObject(RoleID, (object) AdjustableViews.RoleSettingsGuid) as byte[];
      }
      AdjustableViews source = (AdjustableViews) null;
      if (roleSettingsObject != null)
      {
        try
        {
          MemoryStream memoryStream = new MemoryStream(roleSettingsObject);
          MemoryStream outStream = new MemoryStream();
          if (ZLibStreamHelper.UnpackStream((Stream) memoryStream, (Stream) outStream) > 0L)
            memoryStream = outStream;
          else
            memoryStream.Seek(0L, SeekOrigin.Begin);
          try
          {
            source = new BinaryFormatter().Deserialize((Stream) memoryStream) as AdjustableViews;
          }
          catch
          {
            source = (AdjustableViews) null;
          }
        }
        catch
        {
          source = (AdjustableViews) null;
        }
      }
      object[] objArray = new object[7];
      objArray[5] = (object) true;
      this.BatchPropertiesSet(objArray);
      this.Assign(source);
    }
  }

  /// <summary>
  /// Сохранить настройки закладок в настройки указанной роли
  /// </summary>
  /// <param name="RoleID">Идентификатор роли</param>
  public void SaveToRoleSettings(long RoleID)
  {
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        try
        {
          new BinaryFormatter().Serialize((Stream) memoryStream, (object) this);
          ZLibStreamHelper.PackStream((Stream) memoryStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
          customService.SetRoleSettingsObject(RoleID, (object) AdjustableViews.RoleSettingsGuid, (object) outStream.ToArray());
        }
        catch
        {
        }
      }
    }
  }

  /// <summary>
  /// Выполнить синхронизацию с настройками команд меню у указанного пользователя
  /// </summary>
  /// <param name="UserID">Идентификатор пользователя</param>
  public void SyncWithUserSettings(long UserID)
  {
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    byte[] buffer = customService[UserID, (object) AdjustableViews.UserSettingsGuid] as byte[];
    AdjustableViews source = (AdjustableViews) null;
    if (buffer != null)
    {
      try
      {
        MemoryStream memoryStream = new MemoryStream(buffer);
        MemoryStream outStream = new MemoryStream();
        long num = ZLibStreamHelper.UnpackStream((Stream) memoryStream, (Stream) outStream);
        if (num > 0L)
        {
          memoryStream.Close();
          memoryStream = outStream;
        }
        else
          memoryStream.Seek(0L, SeekOrigin.Begin);
        try
        {
          source = new BinaryFormatter().Deserialize((Stream) memoryStream) as AdjustableViews;
        }
        catch
        {
          source = (AdjustableViews) null;
        }
        finally
        {
          if (num > 0L)
          {
            outStream.Close();
          }
          else
          {
            memoryStream.Close();
            outStream.Close();
          }
        }
      }
      catch
      {
        source = (AdjustableViews) null;
      }
    }
    object[] objArray = new object[7];
    objArray[5] = (object) true;
    this.BatchPropertiesSet(objArray);
    this.Assign(source);
  }

  /// <summary>
  /// Сохранить настройки закладок в настройки указанного пользователя
  /// </summary>
  /// <param name="UserID">Идентификатор пользователя</param>
  public void SaveToUserSettings(long UserID)
  {
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        try
        {
          new BinaryFormatter().Serialize((Stream) memoryStream, (object) this);
          ZLibStreamHelper.PackStream((Stream) memoryStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
          customService[UserID, (object) AdjustableViews.UserSettingsGuid] = (object) outStream.ToArray();
        }
        catch
        {
        }
      }
    }
  }

  /// <summary>Создать копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public object Clone()
  {
    AdjustableViews adjustableViews = new AdjustableViews();
    for (int index = 0; index < this.Count; ++index)
      adjustableViews.Add(this[index].Clone() as AdjustableView);
    return (object) adjustableViews;
  }
}
