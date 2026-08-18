
// Type: Intermech.Client.Core.CompositionView.ButtonsServiceBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.CompositionView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Базовый сервис для кнопок</summary>
internal abstract class ButtonsServiceBase
{
  /// <summary>
  /// Кеш зарегестрированных типов вида: Guid типа объекта -&gt; Перечень настроенных кнопок
  /// </summary>
  protected Dictionary<Guid, List<CVButtonBase>> _cache = new Dictionary<Guid, List<CVButtonBase>>();

  /// <summary>Загрузка информации сервиса</summary>
  /// <param name="commonData">Признак "общих" данных</param>
  protected virtual void LoadFromBase(bool commonData)
  {
    this._cache.Clear();
    XmlDocument xmlDocument = (XmlDocument) null;
    byte[] config_file = (byte[]) null;
    if (commonData)
    {
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ICompositionViewServer)) is ICompositionViewServer customService)
        config_file = customService.LoadButtonsSettings();
    }
    else
      (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).LoadConfigData("CompositionViewButtons", out BlobInformation _, out config_file);
    if (config_file != null && config_file.Length != 0)
    {
      xmlDocument = new XmlDocument();
      xmlDocument.Load((Stream) new MemoryStream(config_file));
    }
    if (xmlDocument == null)
      return;
    foreach (XmlNode childNode1 in xmlDocument.DocumentElement.ChildNodes)
    {
      if (childNode1.Name.Equals("ForType"))
      {
        XmlAttribute attribute = childNode1.Attributes["Guid"];
        if (attribute != null)
        {
          Guid objTypeGuid;
          try
          {
            objTypeGuid = new Guid(attribute.Value);
          }
          catch
          {
            objTypeGuid = Guid.Empty;
          }
          if (!objTypeGuid.Equals(Guid.Empty))
          {
            foreach (XmlNode childNode2 in childNode1.ChildNodes)
            {
              CVButtonBase button = CVButtonBase.Load(childNode2);
              if (button != null)
                this.AddButton(objTypeGuid, button);
            }
          }
        }
      }
    }
  }

  /// <summary>Сохранение информации сервиса</summary>
  /// <param name="commonData">Признак "общих" данных</param>
  protected virtual void SaveToBase(bool commonData)
  {
    XmlDocument xmlDocument = new XmlDocument();
    XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Buttons");
    xmlDocument.AppendChild(element1);
    foreach (KeyValuePair<Guid, List<CVButtonBase>> keyValuePair in this._cache)
    {
      XmlNode element2 = (XmlNode) xmlDocument.CreateElement("ForType");
      XmlAttribute attribute = xmlDocument.CreateAttribute("Guid");
      attribute.Value = keyValuePair.Key.ToString();
      element2.Attributes.Append(attribute);
      foreach (CVButtonBase cvButtonBase in keyValuePair.Value)
      {
        if (cvButtonBase.Node == null)
          cvButtonBase.Save(element2);
        else
          element2.AppendChild(xmlDocument.ImportNode(cvButtonBase.Node, true));
      }
      element1.AppendChild(element2);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        xmlDocument.Save((Stream) outStream);
        byte[] array = outStream.ToArray();
        if (commonData)
        {
          if (!(sessionKeeper.Session.GetCustomService(typeof (ICompositionViewServer)) is ICompositionViewServer customService))
            return;
          customService.SaveButtonsSettings(array);
        }
        else
        {
          BlobInformation config_info = new BlobInformation((long) array.Length, (long) array.Length, DateTime.Now, "CompositionViewButtons", ArcMethods.NotPacked, string.Empty);
          sessionKeeper.Session.Configurations.WriteConfigData(config_info, array);
        }
      }
    }
  }

  /// <summary>Загрузка информации сервиса</summary>
  public abstract void LoadFromBase();

  /// <summary>Сохранение информации сервиса</summary>
  public abstract void SaveToBase();

  /// <summary>Поиск элементов по гл. идентификатору типа объекта</summary>
  /// <param name="objTypeGuid"></param>
  /// <returns></returns>
  public List<CVButtonBase> GetButtonsList(Guid objTypeGuid)
  {
    return this._cache.ContainsKey(objTypeGuid) ? this._cache[objTypeGuid] : new List<CVButtonBase>();
  }

  /// <summary>Регистрация кнопок в сервисе</summary>
  /// <param name="objTypeGuid"></param>
  /// <param name="button"></param>
  public void AddButton(Guid objTypeGuid, CVButtonBase button)
  {
    if (objTypeGuid.Equals(Guid.Empty) || button == null)
      return;
    this.AddButton(objTypeGuid, new List<CVButtonBase>((IEnumerable<CVButtonBase>) new CVButtonBase[1]
    {
      button
    }));
  }

  /// <summary>Регистрация кнопок в сервисе</summary>
  /// <param name="objTypeGuid"></param>
  /// <param name="buttons"></param>
  public void AddButton(Guid objTypeGuid, List<CVButtonBase> buttons)
  {
    if (objTypeGuid.Equals(Guid.Empty) || buttons == null || buttons.Count == 0)
      return;
    List<CVButtonBase> cvButtonBaseList;
    if (!this._cache.TryGetValue(objTypeGuid, out cvButtonBaseList))
    {
      cvButtonBaseList = new List<CVButtonBase>();
      this._cache.Add(objTypeGuid, cvButtonBaseList);
    }
    cvButtonBaseList.AddRange((IEnumerable<CVButtonBase>) buttons);
  }

  /// <summary>Удаление элемента</summary>
  /// <param name="objTypeGuid"></param>
  /// <param name="button"></param>
  public void RemoveButton(Guid objTypeGuid, CVButtonBase button)
  {
    List<CVButtonBase> cvButtonBaseList;
    if (!this._cache.TryGetValue(objTypeGuid, out cvButtonBaseList))
      return;
    cvButtonBaseList.Remove(button);
    if (!cvButtonBaseList.Count.Equals(0))
      return;
    this._cache.Remove(objTypeGuid);
  }

  /// <summary>Удаление элементов указанного типа объекта</summary>
  /// <param name="objTypeGuid"></param>
  public void ClearButtons(Guid objTypeGuid)
  {
    if (!this._cache.ContainsKey(objTypeGuid))
      return;
    this._cache.Remove(objTypeGuid);
  }
}
