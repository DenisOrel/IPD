
// Type: Intermech.Client.Core.BlackWidthService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace Intermech.Client.Core;

/// <summary>служба для работы с настройками цвета и толщины линий DWG </summary>
public class BlackWidthService : IBlackWidthService
{
  /// <summary>объект для потокобезопасного доступа</summary>
  private object SyncRoot = new object();
  /// <summary>имя секции в настройках для настроек цвета и толщины линий DWG </summary>
  public readonly string BLACKWIDTHS_SECTION = "BlackWidthsSection";
  /// <summary>
  /// имя секции для хранения непосредственного описания всех настроек цвета и толщины линий DWG .
  /// В настройках описание хранится  в формате
  /// имя параметра - порядковый номер
  /// значение - {used? "1":"0"}|{acadIndex}|{width}
  /// </summary>
  public readonly string BLACKWIDTHS_SETTINGS = "BlackWidthsSettings";
  /// <summary>все цвета привести к чёрному</summary>
  private readonly string ALLCOLORTOBLACK = nameof (AllColorToBlack);
  private BlackWidthService.BlackWidthsSection data = new BlackWidthService.BlackWidthsSection();

  /// <summary>все цвета привести к чёрному</summary>
  public bool AllColorToBlack
  {
    get => this.data.AllColorToBlack;
    set => this.data.AllColorToBlack = value;
  }

  /// <summary>получить по индексу сам класс настроек толщины для цвета в Acad</summary>
  /// <param name="index">индекс цвета в Acad(1..255)</param>
  /// <returns>класс настроек толщины</returns>
  public ColorWidth this[byte index] => this.data.Array[(int) index];

  public static string SerializeXmlToObject<T>(T toSerialize)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (StreamWriter streamWriter = new StreamWriter((Stream) memoryStream, Encoding.UTF8))
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));
        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[1]
        {
          new XmlQualifiedName("", "")
        });
        xmlSerializer.Serialize((TextWriter) streamWriter, (object) toSerialize, namespaces);
        return Encoding.UTF8.GetString(memoryStream.ToArray());
      }
    }
  }

  public static T DeserializeXmlToObject<T>(string xml)
  {
    using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
    {
      using (StreamReader streamReader = new StreamReader((Stream) memoryStream, Encoding.UTF8))
        return (T) new XmlSerializer(typeof (T)).Deserialize((TextReader) streamReader);
    }
  }

  private void SaveBlackWidthsSection()
  {
    try
    {
      byte[] bytes = Encoding.UTF8.GetBytes(BlackWidthService.SerializeXmlToObject<BlackWidthService.BlackWidthsSection>(this.data));
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        BlobInformation config_info = new BlobInformation((long) bytes.Length, (long) bytes.Length, DateTime.Now, this.BLACKWIDTHS_SETTINGS, ArcMethods.NotPacked, string.Empty);
        sessionKeeper.Session.Configurations.WriteConfigData(config_info, bytes);
      }
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  private void LoadBlackWidthsSection()
  {
    string xml = (string) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      byte[] config_file = (byte[]) null;
      BlobInformation config_info;
      sessionKeeper.Session.Configurations.LoadConfigData(this.BLACKWIDTHS_SETTINGS, out config_info, out config_file);
      if (config_info.RealFileSize > 0L)
      {
        if ((long) config_file.Length >= config_info.PackedFileSize)
          xml = Encoding.UTF8.GetString(config_file);
      }
    }
    if (xml != null)
      this.data = BlackWidthService.DeserializeXmlToObject<BlackWidthService.BlackWidthsSection>(xml);
    else
      this.SaveBlackWidthsSection();
  }

  /// <summary>Конструктор</summary>
  public BlackWidthService()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      DataTable dataTable = configurations.ReadSection("CLIENT", this.BLACKWIDTHS_SETTINGS, sessionKeeper.Session.UserID);
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        this.AllColorToBlack = configurations.ReadBool("CLIENT", this.BLACKWIDTHS_SECTION, this.ALLCOLORTOBLACK, false, DBConfigMode.UserAndGlobal);
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          ColorWidth colorWidth = new ColorWidth(Convert.ToString(dataTable.Rows[index][1]));
          this.data.Array[(int) colorWidth.AcadIndex] = colorWidth;
        }
        DataTable table = new DataTable();
        table.Columns.Add("F_PARAM_NAME", typeof (string));
        table.Columns.Add("F_VALUE", typeof (string));
        table.AcceptChanges();
        configurations.WriteSection("CLIENT", this.BLACKWIDTHS_SETTINGS, table, sessionKeeper.Session.UserID);
      }
      this.LoadBlackWidthsSection();
    }
    this.OnChanged();
  }

  /// <summary>сохранить настройки</summary>
  public void SaveSettings()
  {
    this.SaveBlackWidthsSection();
    this.OnChanged();
  }

  /// <summary>Событие изменения на закладке</summary>
  public event EventHandler Changed;

  /// <summary>Событие будет дёргаться при необходимости</summary>
  public void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  [XmlRoot("BlackWidthsSection", Namespace = "", IsNullable = false)]
  [Serializable]
  public class BlackWidthsSection
  {
    /// <summary>все цвета привести к чёрному</summary>
    [XmlElement("IsBlack")]
    public bool AllColorToBlack { get; set; }

    /// <summary>массив настроек толщины для цвета в Acad</summary>
    [XmlArray("ColorWidths")]
    [XmlArrayItem("ColorWidth", typeof (ColorWidth))]
    public ColorWidth[] Array { get; set; }

    /// <summary>Конструктор</summary>
    public BlackWidthsSection()
    {
      this.AllColorToBlack = false;
      this.Array = new ColorWidth[256 /*0x0100*/];
      int num = 256 /*0x0100*/;
      for (int varAcadIndex = 0; varAcadIndex < num; ++varAcadIndex)
        this.Array[varAcadIndex] = new ColorWidth(false, (byte) varAcadIndex, 0.0f);
    }
  }
}
