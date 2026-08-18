// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.BarCodes.BarCodeSettings
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Archives.BarCodes;

/// <summary> Различные настройки которые не имеет смысла размещать в диалогах. В старом AVS подобные настройки хранились в ini-файле </summary>
public sealed class BarCodeSettings : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private int baudRate = 9600;
  private int? newbaudRate;
  private int dataBits = 8;
  private int? newdataBits;
  private StopBitsEnum stopBits = StopBitsEnum.One;
  private StopBitsEnum? newstopBits;
  private ParityEnum parity;
  private ParityEnum? newparity;
  private OpenModeEnum openMode;
  private OpenModeEnum? newopenMode;
  private string port = "COM1";
  private string newport;
  private bool use;
  private bool? newuse;
  private static BarCodeSettings _instance;
  private object _wrapper;

  private BarCodeSettings()
  {
    this._wrapper = (object) new ClassWrapperForPropertyGrid((object) this);
  }

  /// <summary> Единственный экземпляр объекта </summary>
  [Browsable(false)]
  public static BarCodeSettings Instance
  {
    [DebuggerStepThrough] get
    {
      if (BarCodeSettings._instance == null)
        BarCodeSettings._instance = new BarCodeSettings();
      return BarCodeSettings._instance;
    }
  }

  /// <summary>Скорость передачи</summary>
  [Browsable(false)]
  public int BaudRate
  {
    [DebuggerStepThrough] get => this.baudRate;
    set
    {
      this.baudRate = value;
      this.newbaudRate = new int?();
    }
  }

  [DisplayName("Скорость передачи")]
  [Description("Скорость передачи")]
  public int BaudRateVisual
  {
    [DebuggerStepThrough] get
    {
      return !this.newbaudRate.HasValue ? this.baudRate : this.newbaudRate.Value;
    }
    set => this.newbaudRate = new int?(value);
  }

  /// <summary>Скорость передачи</summary>
  [Browsable(false)]
  public bool Use
  {
    [DebuggerStepThrough] get => this.use;
    set
    {
      this.use = value;
      this.newuse = new bool?();
    }
  }

  [DisplayName("Включить проверку штрихкодирования")]
  [Description("Включить проверку штрихкодирования")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool UseVisual
  {
    [DebuggerStepThrough] get => !this.newuse.HasValue ? this.use : this.newuse.Value;
    set => this.newuse = new bool?(value);
  }

  /// <summary>Биты данных</summary>
  [Browsable(false)]
  public int DataBits
  {
    [DebuggerStepThrough] get => this.dataBits;
    set
    {
      this.dataBits = value;
      this.newdataBits = new int?();
    }
  }

  [DisplayName("Биты данных")]
  [Description("Биты данных")]
  public int DataBitsVisual
  {
    [DebuggerStepThrough] get
    {
      return !this.newdataBits.HasValue ? this.dataBits : this.newdataBits.Value;
    }
    set => this.newdataBits = new int?(value);
  }

  /// <summary>Стоп биты</summary>
  [Browsable(false)]
  public StopBitsEnum StopBits
  {
    [DebuggerStepThrough] get => this.stopBits;
    set
    {
      this.stopBits = value;
      this.newstopBits = new StopBitsEnum?();
    }
  }

  [DisplayName("Стоп биты")]
  [Description("Стоп биты")]
  public StopBitsEnum StopBitsVisual
  {
    [DebuggerStepThrough] get
    {
      return !this.newstopBits.HasValue ? this.stopBits : this.newstopBits.Value;
    }
    set => this.newstopBits = new StopBitsEnum?(value);
  }

  /// <summary>Очередность</summary>
  [Browsable(false)]
  public ParityEnum Parity
  {
    [DebuggerStepThrough] get => this.parity;
    set
    {
      this.parity = value;
      this.newparity = new ParityEnum?();
    }
  }

  [DisplayName("Очередность")]
  [Description("Очередность")]
  public ParityEnum ParityVisual
  {
    [DebuggerStepThrough] get => !this.newparity.HasValue ? this.parity : this.newparity.Value;
    set => this.newparity = new ParityEnum?(value);
  }

  /// <summary>Способ открытия</summary>
  [Browsable(false)]
  public OpenModeEnum OpenMode
  {
    [DebuggerStepThrough] get => this.openMode;
    set
    {
      this.openMode = value;
      this.newopenMode = new OpenModeEnum?();
    }
  }

  [DisplayName("Способ открытия")]
  [Description("Способ открытия документа")]
  public OpenModeEnum OpenModeVisual
  {
    [DebuggerStepThrough] get
    {
      return !this.newopenMode.HasValue ? this.openMode : this.newopenMode.Value;
    }
    set => this.newopenMode = new OpenModeEnum?(value);
  }

  /// <summary>Порт</summary>
  [Browsable(false)]
  public string Port
  {
    [DebuggerStepThrough] get => this.port;
    set
    {
      this.port = value;
      this.newport = (string) null;
    }
  }

  [DisplayName("Порт")]
  [Description("Порт")]
  public string PortVisual
  {
    [DebuggerStepThrough] get => this.newport == null ? this.port : this.newport;
    set => this.newport = value;
  }

  /// <summary>Вернуть id раздела в хелпе для данной страницы</summary>
  [Browsable(false)]
  public string HelpTopicID => "";

  public event EventHandler Changed;

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  [Browsable(false)]
  public PropertyPageType Type
  {
    [DebuggerStepThrough] get => PropertyPageType.Object;
  }

  [Browsable(false)]
  public object Control
  {
    [DebuggerStepThrough] get => this._wrapper;
  }

  [Browsable(false)]
  public string PageName
  {
    [DebuggerStepThrough] get => "Штрихкодирование";
  }

  /// <summary>Текст заголовка (пустое значение - заголовок не отображается)</summary>
  [Browsable(false)]
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public void Apply()
  {
    if (this.newuse.HasValue)
      this.Use = this.newuse.Value;
    this.newuse = new bool?();
    if (this.newopenMode.HasValue)
      this.OpenMode = this.newopenMode.Value;
    this.newopenMode = new OpenModeEnum?();
    if (this.newbaudRate.HasValue)
      this.BaudRate = this.newbaudRate.Value;
    this.newbaudRate = new int?();
    if (this.newdataBits.HasValue)
      this.DataBits = this.newdataBits.Value;
    this.newdataBits = new int?();
    if (this.newstopBits.HasValue)
      this.StopBits = this.newstopBits.Value;
    this.newstopBits = new StopBitsEnum?();
    if (this.newparity.HasValue)
      this.Parity = this.newparity.Value;
    this.newparity = new ParityEnum?();
    if (this.newport != null)
      this.Port = this.newport;
    this.newport = (string) null;
    this.OnChanged();
  }

  public void Cancel()
  {
    this.newopenMode = new OpenModeEnum?();
    this.newuse = new bool?();
    this.newbaudRate = new int?();
    this.newdataBits = new int?();
    this.newstopBits = new StopBitsEnum?();
    this.newparity = new ParityEnum?();
    this.newport = (string) null;
  }

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }
}
