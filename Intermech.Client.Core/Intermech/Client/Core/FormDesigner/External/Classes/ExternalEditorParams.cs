
// Type: Intermech.Client.Core.FormDesigner.External.Classes.ExternalEditorParams
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Xml;


namespace Intermech.Client.Core.FormDesigner.External.Classes;

/// <summary>Параметры внешнего редактора.</summary>
public class ExternalEditorParams
{
  private string _path = string.Empty;
  private string _additionalCommands = string.Empty;
  private SendMethod _send = SendMethod.File;
  private ReceiveMethod _receive = ReceiveMethod.File;
  private string _swapFile = string.Empty;
  private bool _lockControl;
  private bool _sendAllAttributes;

  /// <summary>Путь к исполняемому файлу.</summary>
  public string Path
  {
    get => this._path;
    set
    {
      this._path = value;
      this.DoModified();
    }
  }

  /// <summary>Параметры командной строки.</summary>
  public string Command
  {
    get => this._additionalCommands;
    set
    {
      this._additionalCommands = value;
      this.DoModified();
    }
  }

  /// <summary>Метод передачи данных.</summary>
  public SendMethod Send
  {
    get => this._send;
    set
    {
      this._send = value;
      this.DoModified();
    }
  }

  /// <summary>Метод получения данных.</summary>
  public ReceiveMethod Receive
  {
    get => this._receive;
    set
    {
      this._receive = value;
      this.DoModified();
    }
  }

  /// <summary>
  /// Временный файл для обмена данными (если обмен идет через файл).
  /// </summary>
  public string SwapFile
  {
    get => this._swapFile;
    set
    {
      this._swapFile = value;
      this.DoModified();
    }
  }

  /// <summary>
  /// Блокировать контрол (изменять данные можно только через внешний редактор).
  /// </summary>
  public bool LockControl
  {
    get => this._lockControl;
    set
    {
      this._lockControl = value;
      this.DoModified();
    }
  }

  /// <summary>Пересылать все параметры.</summary>
  public bool SendAllAttributes
  {
    get => this._sendAllAttributes;
    set
    {
      this._sendAllAttributes = value;
      this.DoModified();
    }
  }

  /// <summary>Событие на изменение данных.</summary>
  public event EventHandler OnModified;

  /// <summary>Генерация события об изменении данных.</summary>
  protected void DoModified()
  {
    if (this.OnModified == null)
      return;
    this.OnModified((object) this, EventArgs.Empty);
  }

  /// <summary>Загрузка из Xml.</summary>
  /// <param name="node">XmlNode с данными</param>
  public bool Load(XmlNode node)
  {
    bool flag = false;
    if (node.Name == nameof (ExternalEditorParams))
    {
      foreach (XmlNode childNode in node.ChildNodes)
      {
        switch (childNode.Name)
        {
          case "Commands":
            this._additionalCommands = childNode.InnerText;
            continue;
          case "LockControl":
            this._lockControl = Convert.ToBoolean(childNode.InnerText);
            continue;
          case "Path":
            this._path = childNode.InnerText;
            continue;
          case "Receive":
            this._receive = (ReceiveMethod) EnumTypeHelper.GetEnumValue(typeof (ReceiveMethod), childNode.InnerText, (object) ReceiveMethod.File);
            continue;
          case "Send":
            this._send = (SendMethod) EnumTypeHelper.GetEnumValue(typeof (SendMethod), childNode.InnerText, (object) SendMethod.File);
            continue;
          case "SendAllAttributes":
            this._sendAllAttributes = Convert.ToBoolean(childNode.InnerText);
            continue;
          case "Swap":
            this._swapFile = childNode.InnerText;
            continue;
          default:
            continue;
        }
      }
      this.DoModified();
      flag = true;
    }
    return flag;
  }

  /// <summary>Загрузка через статический метод.</summary>
  /// <param name="node">XmlNode c данными</param>
  /// <returns>параметры внешнего редактора</returns>
  public static ExternalEditorParams LoadParams(XmlNode node)
  {
    ExternalEditorParams externalEditorParams = new ExternalEditorParams();
    return !externalEditorParams.Load(node) ? (ExternalEditorParams) null : externalEditorParams;
  }

  /// <summary>Сохранение в Xml.</summary>
  /// <param name="doc"></param>
  /// <returns></returns>
  public void Save(XmlDocument doc)
  {
    XmlNode element1 = (XmlNode) doc.CreateElement(nameof (ExternalEditorParams));
    XmlNode element2 = (XmlNode) doc.CreateElement("Path");
    element2.InnerText = this._path;
    element1.AppendChild(element2);
    XmlNode element3 = (XmlNode) doc.CreateElement("Commands");
    element3.InnerText = this._additionalCommands;
    element1.AppendChild(element3);
    XmlNode element4 = (XmlNode) doc.CreateElement("Send");
    element4.InnerText = EnumTypeHelper.GetCaption((Enum) this._send);
    element1.AppendChild(element4);
    XmlNode element5 = (XmlNode) doc.CreateElement("Receive");
    element5.InnerText = EnumTypeHelper.GetCaption((Enum) this._receive);
    element1.AppendChild(element5);
    XmlNode element6 = (XmlNode) doc.CreateElement("Swap");
    element6.InnerText = this._swapFile;
    element1.AppendChild(element6);
    XmlNode element7 = (XmlNode) doc.CreateElement("LockControl");
    element7.InnerText = Convert.ToString(this._lockControl);
    element1.AppendChild(element7);
    XmlNode element8 = (XmlNode) doc.CreateElement("SendAllAttributes");
    element8.InnerText = Convert.ToString(this._sendAllAttributes);
    element1.AppendChild(element8);
    doc.AppendChild(element1);
  }
}
