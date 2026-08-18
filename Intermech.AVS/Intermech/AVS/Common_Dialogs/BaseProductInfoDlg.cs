// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.BaseProductInfoDlg
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

/// <summary>
/// Базовый класс для диалоговых форм редактирования заголовка исполнения.
/// </summary>
public abstract class BaseProductInfoDlg : Form
{
  protected string originDesignation = "";
  protected string originNumber = "";

  /// <summary>Обозначение исполнения</summary>
  public virtual string ProductDesignation { get; set; }

  /// <summary>Номер исполнения</summary>
  public virtual string ProductNumber { get; set; }

  protected DataTable Model { get; private set; }

  public virtual string ProductCaption { get; }

  /// <summary>Валидация</summary>
  protected virtual bool ValidateInput()
  {
    bool flag1 = this.originDesignation == this.ProductCaption;
    bool flag2 = this.originNumber == this.ProductNumber;
    bool flag3 = this.Model == null || flag1 & flag2;
    if (!flag3)
    {
      if (!flag1)
        flag1 = this.Model.Select($"CAPTION='{this.ProductCaption}'").Length == 0;
      if (!flag1)
      {
        int num1 = (int) MessageBox.Show($"Исполнение \"{this.ProductCaption}\" уже существует", "Переименование исполнения", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        if (!flag2)
          flag2 = this.Model.Select($"NUMBER='{this.ProductNumber}'").Length == 0;
        if (!flag2)
        {
          int num2 = (int) MessageBox.Show($"Номер исполнения \"{this.ProductNumber}\" уже используется", "Переименование исполнения", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      flag3 = flag1 & flag2;
    }
    return flag3;
  }

  protected override void OnFormClosing(FormClosingEventArgs e)
  {
    base.OnFormClosing(e);
    e.Cancel = this.DialogResult == DialogResult.OK && !this.ValidateInput();
  }

  public static DialogResult Execute<T>(
    string caption,
    DataTable model,
    ref string productDesignation,
    ref string productNumber)
    where T : BaseProductInfoDlg, new()
  {
    T obj1 = new T();
    obj1.Text = caption;
    obj1.Model = model;
    T obj2 = obj1;
    obj2.ProductNumber = productNumber;
    obj2.ProductDesignation = productDesignation;
    int num = (int) obj2.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    productDesignation = obj2.ProductCaption;
    productNumber = obj2.ProductNumber;
    return (DialogResult) num;
  }
}
