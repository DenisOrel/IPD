// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ImbaseSyncReport
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Форма отчета синхронизации компонентов схемы с Imbase</summary>
internal class ImbaseSyncReport : Form
{
  private ICollection<InitialArticleData> _articles;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private WebBrowser webBrowser1;
  private MenuStrip menuStrip1;
  private ToolStripMenuItem файлToolStripMenuItem;
  private ToolStripMenuItem miSave;
  private ToolStripMenuItem miExit;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripMenuItem miPrint;
  private OpenFileDialog openFileDialog1;

  public ImbaseSyncReport(ICollection<InitialArticleData> articles)
  {
    this.InitializeComponent();
    this._articles = articles;
  }

  private bool GetArticleData(
    InitialArticleData article,
    out string posDesignation,
    out string name,
    out ImbaseSyncTypes imbaseSyncType)
  {
    name = (string) null;
    posDesignation = (string) null;
    imbaseSyncType = ImbaseSyncTypes.Unknown;
    ElectricalArticleCache electricalArticleCache = article.CustomSections.Get<ElectricalArticleCache>();
    if (electricalArticleCache.ArticleType != ArticleTypes.Component)
      return false;
    ImbaseSyncInfo imbaseSyncInfo = article.CustomSections.Get<ImbaseSyncInfo>();
    if (imbaseSyncInfo.ImbaseSyncType == ImbaseSyncTypes.Normal)
      return false;
    IElectricalComponent article1 = (IElectricalComponent) electricalArticleCache.Article;
    name = article1.PartNumber;
    posDesignation = article1.PosDesignation;
    imbaseSyncType = imbaseSyncInfo.ImbaseSyncType;
    return true;
  }

  public void Initialize()
  {
    StringBuilder stringBuilder = new StringBuilder(this.PageHeader());
    string name = (string) null;
    string posDesignation = (string) null;
    ImbaseSyncTypes imbaseSyncType = ImbaseSyncTypes.Unknown;
    foreach (InitialArticleData article in (IEnumerable<InitialArticleData>) this._articles)
    {
      if (this.GetArticleData(article, out posDesignation, out name, out imbaseSyncType))
      {
        stringBuilder.Append("<tr>");
        stringBuilder.AppendFormat("<td class=\"column1\"><!-- c1 -->{0}</td>", (object) posDesignation);
        stringBuilder.AppendFormat("<td><!-- c2 -->{0}</td>", (object) name);
        stringBuilder.AppendFormat("<td class=\"{0}\"><!-- c3 -->{1}</td>", imbaseSyncType == ImbaseSyncTypes.NotFound ? (object) "column31" : (object) "column32", (object) EnumDescConverter.GetEnumDescription((Enum) imbaseSyncType));
        stringBuilder.Append("</tr>");
      }
    }
    stringBuilder.Append(this.PageEnd());
    this.DisplayHtml(stringBuilder.ToString());
  }

  private void DisplayHtml(string html)
  {
    this.webBrowser1.Navigate("about:blank");
    try
    {
      if (this.webBrowser1.Document != (HtmlDocument) null)
        this.webBrowser1.Document.Write(string.Empty);
    }
    catch
    {
    }
    this.webBrowser1.DocumentText = html;
  }

  private string PageEnd() => "</table></body>";

  private string PageHeader()
  {
    return "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1251\" /><meta http-equiv=\"X-UA-Compatible\" content=\"IE=8\" /><style>table {width: 100%;background: black;/color: black;border-spacing: 1px;}td, th {background: white;padding: 5px;} .column1 { text-align: center; width:100px; } .column31, .column32 {width:200px;} .column31 {color: #c45911;} .column32 {color: #ff0000; } </style></head><body><table><tr><th>Позиционное обозначение</th><th>Наименование</th><th>Результат</th></tr>";
  }

  private void miExit_Click(object sender, EventArgs e) => this.Close();

  private void miPrint_Click(object sender, EventArgs e) => this.webBrowser1.ShowPrintDialog();

  private void miSave_Click(object sender, EventArgs e)
  {
    if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
      return;
    using (StreamWriter text = File.CreateText(this.openFileDialog1.FileName))
    {
      text.WriteLine("Поз. обозначение\tНаименование\tРезультат");
      StringBuilder stringBuilder = new StringBuilder(this.PageHeader());
      string name = (string) null;
      string posDesignation = (string) null;
      ImbaseSyncTypes imbaseSyncType = ImbaseSyncTypes.Unknown;
      foreach (InitialArticleData article in (IEnumerable<InitialArticleData>) this._articles)
      {
        if (this.GetArticleData(article, out posDesignation, out name, out imbaseSyncType))
        {
          text.Write(posDesignation + "\t");
          text.Write(name + "\t");
          text.WriteLine(EnumDescConverter.GetEnumDescription((Enum) imbaseSyncType));
        }
      }
      text.Flush();
      text.Close();
    }
  }

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
    this.webBrowser1 = new WebBrowser();
    this.menuStrip1 = new MenuStrip();
    this.файлToolStripMenuItem = new ToolStripMenuItem();
    this.miSave = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.miExit = new ToolStripMenuItem();
    this.miPrint = new ToolStripMenuItem();
    this.openFileDialog1 = new OpenFileDialog();
    this.menuStrip1.SuspendLayout();
    this.SuspendLayout();
    this.webBrowser1.Dock = DockStyle.Fill;
    this.webBrowser1.Location = new Point(0, 24);
    this.webBrowser1.MinimumSize = new Size(20, 20);
    this.webBrowser1.Name = "webBrowser1";
    this.webBrowser1.Size = new Size(922, 627);
    this.webBrowser1.TabIndex = 0;
    this.menuStrip1.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.файлToolStripMenuItem
    });
    this.menuStrip1.Location = new Point(0, 0);
    this.menuStrip1.Name = "menuStrip1";
    this.menuStrip1.Size = new Size(922, 24);
    this.menuStrip1.TabIndex = 1;
    this.menuStrip1.Text = "menuStrip1";
    this.файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.miSave,
      (ToolStripItem) this.miPrint,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.miExit
    });
    this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
    this.файлToolStripMenuItem.Size = new Size(48 /*0x30*/, 20);
    this.файлToolStripMenuItem.Text = "Файл";
    this.miSave.Name = "miSave";
    this.miSave.ShortcutKeys = Keys.S | Keys.Control;
    this.miSave.Size = new Size(172, 22);
    this.miSave.Text = "Сохранить";
    this.miSave.Click += new EventHandler(this.miSave_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(169, 6);
    this.miExit.Name = "miExit";
    this.miExit.Size = new Size(172, 22);
    this.miExit.Text = "Выход";
    this.miExit.Click += new EventHandler(this.miExit_Click);
    this.miPrint.Name = "miPrint";
    this.miPrint.ShortcutKeys = Keys.P | Keys.Control;
    this.miPrint.Size = new Size(172, 22);
    this.miPrint.Text = "Печать";
    this.miPrint.Click += new EventHandler(this.miPrint_Click);
    this.openFileDialog1.CheckFileExists = false;
    this.openFileDialog1.DefaultExt = "*.txt";
    this.openFileDialog1.FileName = "Результаты проверки.txt";
    this.openFileDialog1.Filter = "Текстовые файлы|*.txt";
    this.openFileDialog1.RestoreDirectory = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(922, 651);
    this.Controls.Add((Control) this.webBrowser1);
    this.Controls.Add((Control) this.menuStrip1);
    this.MainMenuStrip = this.menuStrip1;
    this.Name = nameof (ImbaseSyncReport);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Список компонентов, которые не синхронизируются с Imbase";
    this.menuStrip1.ResumeLayout(false);
    this.menuStrip1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
