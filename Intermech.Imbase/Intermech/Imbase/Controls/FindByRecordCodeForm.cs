// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.FindByRecordCodeForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

public class FindByRecordCodeForm : Form
{
  private IContainer components;
  private TextBox tbKey;
  private Label label1;
  private Panel panel1;
  private Button btnCancel;
  private Button btnOk;

  public FindByRecordCodeForm()
  {
    this.InitializeComponent();
    if (!Clipboard.ContainsText(TextDataFormat.Text))
      return;
    string text = Clipboard.GetText(TextDataFormat.Text);
    if (!this.PreCheckKeyStr(text))
      return;
    this.tbKey.Text = text;
  }

  public long LinkId { get; private set; }

  public long RecordId { get; private set; }

  public string RecordCodeStr { get; private set; }

  private bool PreCheckKeyStr(string keyStr)
  {
    if (string.IsNullOrEmpty(keyStr) || keyStr.Length < 2)
      return false;
    string upper = keyStr.Substring(0, 2).ToUpper();
    return upper == "I6" || upper == "IK" || Guid.TryParse(keyStr, out Guid _);
  }

  private bool ParseImbaseKey(string keyStr, out string msg, out MessageBoxIcon icon)
  {
    icon = MessageBoxIcon.Exclamation;
    msg = "Неверный формат кода записи Imbase!";
    if (keyStr.Length < 2)
      return false;
    if (keyStr.Substring(0, 2).ToUpper() == "I6")
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        keyStr = ServiceUtils.GetService<IKeyConverter>((object) sessionKeeper.Session, true).ConvertOldKey(sessionKeeper.Session, keyStr);
      if (keyStr.Substring(0, 2).ToUpper() != "IK")
      {
        icon = MessageBoxIcon.Asterisk;
        msg = "Не удалось найти запись по коду: " + keyStr;
        return false;
      }
    }
    if (keyStr.Substring(0, 2).ToUpper() == "IK")
    {
      string str1 = keyStr.Substring(2, keyStr.Length - 2);
      int length = str1.IndexOf('.');
      if (length == -1)
        return false;
      string str2 = str1.Substring(0, length);
      string s = str1.Substring(length + 1, str1.Length - (length + 1));
      Guid result1 = Guid.Empty;
      long result2;
      long result3;
      if ((long.TryParse(str2, out result2) || Guid.TryParse(str2, out result1)) && long.TryParse(s, out result3))
      {
        if (result2 != 0L)
        {
          this.LinkId = result2;
        }
        else
        {
          if (!(result1 != Guid.Empty))
            return false;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(result1);
            if (objectInfo.ObjectID == 0L)
              return false;
            this.LinkId = objectInfo.ObjectID;
          }
        }
        this.RecordId = result3;
        this.RecordCodeStr = keyStr;
        return true;
      }
    }
    else if (Guid.TryParse(keyStr, out Guid _))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService1)
        {
          string[] colsNames = new string[2]
          {
            IndexesField.F_TABKEY,
            IndexesField.F_LINK_ID
          };
          DataTable dataTable = customService1.Search(sessionKeeper.Session.SessionGUID, (List<long>) null, Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID, colsNames, keyStr.ToString(), SearchesAccuracy.Exact);
          if (dataTable != null && dataTable.Rows.Count > 0)
          {
            object[] itemArray = dataTable.Rows[0].ItemArray;
            this.RecordId = Convert.ToInt64(itemArray[0]);
            this.LinkId = Convert.ToInt64(itemArray[1]);
            return true;
          }
          msg = "Не удалось найти запись по GUID.";
          if (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
          {
            DataTable indexes = customService1.GetIndexes(sessionKeeper.Session.SessionGUID, -1L, new string[2]
            {
              IndexesField.F_ATTRIBUTE_ID,
              IndexesField.F_CATALOG_ID
            });
            if (indexes != null)
            {
              DataRow[] dataRowArray = indexes.Select($"[{indexes.Columns[0].ColumnName}]={Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID}");
              List<long> catalogsList = new List<long>((IEnumerable<long>) customService.GetCatalogsList(sessionKeeper.Session.SessionGUID));
              foreach (DataRow dataRow in dataRowArray)
              {
                long int64 = Convert.ToInt64(dataRow[1]);
                int index = catalogsList.IndexOf(int64);
                if (index >= 0)
                  catalogsList.RemoveAt(index);
              }
              if (catalogsList.Count == 0)
              {
                msg = $"{msg}{Environment.NewLine}Все содержащие таблицы Каталоги проиндексированы.{Environment.NewLine}Проверьте вводимые данные";
              }
              else
              {
                string str = this.BuildCatalogNames(sessionKeeper.Session, catalogsList);
                msg = dataRowArray.Length != 0 ? $"{msg}{Environment.NewLine}Добавьте для следующих Каталогов индекс по полю 'Глобальный идентификатор записи таблицы':{str}" : $"{msg}{Environment.NewLine}Создайте для следующих Каталогов индекс по полю 'Глобальный идентификатор записи таблицы':{str}";
              }
            }
          }
        }
      }
    }
    return false;
  }

  private string BuildCatalogNames(IUserSession session, List<long> catalogsList)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (long catalogs in catalogsList)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(catalogs);
      if (!objectInfo.Empty)
      {
        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append(objectInfo.Caption);
      }
    }
    return stringBuilder.ToString();
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    string msg;
    MessageBoxIcon icon;
    if (!this.ParseImbaseKey(this.tbKey.Text, out msg, out icon))
    {
      int num = (int) MessageBox.Show(msg, "Внимание!", MessageBoxButtons.OK, icon);
    }
    else
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
  }

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    FormStorage.SaveLayout((Control) this);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tbKey = new TextBox();
    this.label1 = new Label();
    this.panel1 = new Panel();
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.tbKey.Location = new Point(12, 25);
    this.tbKey.Name = "tbKey";
    this.tbKey.Size = new Size(256 /*0x0100*/, 20);
    this.tbKey.TabIndex = 1;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(159, 13);
    this.label1.TabIndex = 2;
    this.label1.Text = "Код  или GUID записи Imbase:";
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Controls.Add((Control) this.btnOk);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 53);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(280, 46);
    this.panel1.TabIndex = 3;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(193, 11);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOk.Location = new Point(112 /*0x70*/, 11);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(75, 23);
    this.btnOk.TabIndex = 0;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.AcceptButton = (IButtonControl) this.btnOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(280, 99);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.tbKey);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (FindByRecordCodeForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Text = "Поиск по коду записи Imbase";
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
