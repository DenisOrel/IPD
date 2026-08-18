// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.BarCodes.BarCodeListener
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Tools.LaunchActions;
using System;
using System.IO.Ports;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.BarCodes;

internal class BarCodeListener
{
  private static BarCodeListener instance;
  private SerialPort port;

  internal static BarCodeListener Instance
  {
    get
    {
      if (BarCodeListener.instance == null)
        BarCodeListener.instance = new BarCodeListener();
      return BarCodeListener.instance;
    }
  }

  public void Start()
  {
    try
    {
      if (BarCodeSettings.Instance.Use)
      {
        if (this.port != null && this.port.IsOpen)
        {
          this.port.Close();
          this.port.DataReceived -= new SerialDataReceivedEventHandler(this.port_DataReceived);
        }
        this.port = new SerialPort(BarCodeSettings.Instance.Port, BarCodeSettings.Instance.BaudRate, (Parity) BarCodeSettings.Instance.Parity, BarCodeSettings.Instance.DataBits, (StopBits) BarCodeSettings.Instance.StopBits);
        this.port.DataReceived += new SerialDataReceivedEventHandler(this.port_DataReceived);
        this.port.Open();
      }
      else
      {
        if (this.port == null || !this.port.IsOpen)
          return;
        this.port.Close();
        this.port.DataReceived -= new SerialDataReceivedEventHandler(this.port_DataReceived);
      }
    }
    catch
    {
    }
  }

  private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
  {
    try
    {
      string str = this.port.ReadLine();
      string s = "";
      for (int index = 0; index < str.Length; ++index)
      {
        if (char.IsDigit(str[index]))
          s += str[index].ToString();
      }
      long result = 0;
      if (long.TryParse(s, out result))
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(result, false) ?? sessionKeeper.Session.GetObject(-result, false);
          if (dbObject == null && s.Length > 10)
          {
            result /= 10L;
            dbObject = sessionKeeper.Session.GetObject(result, false) ?? sessionKeeper.Session.GetObject(-result, false);
          }
          if (dbObject != null)
          {
            Form openForm = Application.OpenForms[0];
            if (openForm != null && openForm.InvokeRequired)
              openForm.Invoke((Delegate) new BarCodeListener.ShowCardDelegate(this.ShowCard), (object) dbObject);
            else
              this.ShowCard(dbObject);
          }
          else
          {
            int num = (int) MessageBox.Show($"Объект ID = {(object) result} не найден");
          }
        }
      }
      else
      {
        int num1 = (int) MessageBox.Show($"Значение '{str}' не является идентификатором версии объекта");
      }
    }
    catch
    {
    }
  }

  private void ShowCard(IDBObject obj)
  {
    if (BarCodeSettings.Instance.OpenMode == OpenModeEnum.Card)
    {
      int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, obj.ObjectID);
    }
    if (BarCodeSettings.Instance.OpenMode != OpenModeEnum.Editor)
      return;
    ClientContext.LaunchActions.Launch(new LaunchParams(LaunchType.Edit, obj.ObjectID, obj.ObjectType, VersionsRuleSources.GetEditorRule()));
  }

  public delegate void ShowCardDelegate(IDBObject obj);
}
