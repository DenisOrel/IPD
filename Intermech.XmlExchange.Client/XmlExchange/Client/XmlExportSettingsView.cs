// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.Client.XmlExportSettingsView
// Assembly: Intermech.XmlExchange.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 60313882-D426-47E0-8CD2-E15037D75FF2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.XmlExchange.Client.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.Client;

/// <summary>Закладка "Настройка экспорта в XML"</summary>
internal class XmlExportSettingsView : BaseSettingsView
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Создать экземпляр класса</summary>
  public XmlExportSettingsView()
  {
    this.InitializeComponent();
    this._imgView = this._images != null ? this._images.ImageIndex("XML.imgBriefcaseExport") : -1;
  }

  /// <summary>Заголовок закладки</summary>
  public override string Caption
  {
    [DebuggerStepThrough] get => "Настройка экспорта в XML";
  }

  /// <summary>Инициализировать закладку</summary>
  /// <param name="items">Коллекция выделенных элементов пространства навигации</param>
  /// <param name="provider">Контейнер сервисов</param>
  public override void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    base.Initialize(items, provider);
  }

  /// <summary>
  /// Активировать закладку (чтение из базы данных, загрузка информации и т.п.)
  /// </summary>
  /// <param name="previousView">Предыдущая закладка</param>
  public override void Activate(IView previousView) => base.Activate(previousView);

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
  }
}
