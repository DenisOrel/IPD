
// Type: Intermech.Navigator.DBObjects.TableImbaseBlobsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

[ViewDescriptionProvider(typeof (TableImbaseBlobsView.TableImbaseBlobsViewDescriptionProvider))]
internal class TableImbaseBlobsView : UserControl, IView
{
  private bool _firstEnter = true;
  private long _objID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private DataGridView dataGridView1;
  private DataGridView dataGridView2;
  private Splitter splitter1;

  public TableImbaseBlobsView() => this.InitializeComponent();

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._objID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    this._firstEnter = true;
  }

  public void Activate(IView previousView)
  {
    if (!this._firstEnter)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeByGuid = sessionKeeper.Session.GetObject(this._objID).GetAttributeByGuid(new Guid("cad00215-306c-11d8-b4e9-00304f19f545"));
      this.dataGridView1.DataSource = (object) null;
      this.dataGridView2.DataSource = (object) null;
      if (attributeByGuid != null)
      {
        IBlobReader blobReader = (IBlobReader) attributeByGuid;
        if (blobReader == null)
          return;
        BlobInformation blobInformation = blobReader.OpenBlob(0);
        try
        {
          if (blobInformation.RealFileSize > 0L)
          {
            byte[] buffer = blobReader.ReadDataBlock(0);
            if (buffer != null)
            {
              MemoryStream memoryStream1 = new MemoryStream(buffer);
              BinaryFormatter binaryFormatter = new BinaryFormatter();
              DataSet dataSet;
              if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
              {
                MemoryStream memoryStream2 = new MemoryStream();
                ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) memoryStream2, (Stream) memoryStream1);
                memoryStream2.Position = 0L;
                dataSet = (DataSet) binaryFormatter.Deserialize((Stream) memoryStream2);
              }
              else
              {
                memoryStream1.Position = 0L;
                dataSet = (DataSet) binaryFormatter.Deserialize((Stream) memoryStream1);
              }
              this.dataGridView1.DataSource = (object) dataSet.Tables[0];
              this.dataGridView2.DataSource = (object) dataSet.Tables[1];
            }
          }
        }
        finally
        {
          blobReader.CloseBlob();
        }
      }
    }
    this._firstEnter = false;
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => LocalizationHolder.rm.GetString("Client.Core_322");

  public int ImageIndex => -1;

  public int OrderID => 999;

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
    this.dataGridView1 = new DataGridView();
    this.dataGridView2 = new DataGridView();
    this.splitter1 = new Splitter();
    ((ISupportInitialize) this.dataGridView1).BeginInit();
    ((ISupportInitialize) this.dataGridView2).BeginInit();
    this.SuspendLayout();
    this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView1.Dock = DockStyle.Top;
    this.dataGridView1.Location = new Point(0, 0);
    this.dataGridView1.Name = "dataGridView1";
    this.dataGridView1.Size = new Size(650, 150);
    this.dataGridView1.TabIndex = 0;
    this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView2.Dock = DockStyle.Fill;
    this.dataGridView2.Location = new Point(0, 150);
    this.dataGridView2.Name = "dataGridView2";
    this.dataGridView2.Size = new Size(650, 263);
    this.dataGridView2.TabIndex = 1;
    this.splitter1.Dock = DockStyle.Top;
    this.splitter1.Location = new Point(0, 150);
    this.splitter1.Name = "splitter1";
    this.splitter1.Size = new Size(650, 3);
    this.splitter1.TabIndex = 2;
    this.splitter1.TabStop = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.dataGridView2);
    this.Controls.Add((Control) this.dataGridView1);
    this.Name = nameof (TableImbaseBlobsView);
    this.Size = new Size(650, 413);
    ((ISupportInitialize) this.dataGridView1).EndInit();
    ((ISupportInitialize) this.dataGridView2).EndInit();
    this.ResumeLayout(false);
  }

  private sealed class TableImbaseBlobsViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList))
        ServicesManager.GetService(typeof (INamedImageList));
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core_322"),
        ImageIndex = -1,
        OrderID = 999
      };
    }
  }
}
