// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DiskBlobStorageConnectionStringBuilder
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Kernel;

internal class DiskBlobStorageConnectionStringBuilder
{
  public const int DefaultPort = 8010;
  private const string ServerOptionName = "server";
  private const string PortOptionName = "port";
  private const string PasswordOptionName = "password";

  public DiskBlobStorageConnectionStringBuilder() => this.Port = 8010;

  public DiskBlobStorageConnectionStringBuilder(string connectionString)
    : this()
  {
    string[] source = !string.IsNullOrEmpty(connectionString) ? connectionString.Replace(" ", "").Split(new string[1]
    {
      ";"
    }, StringSplitOptions.RemoveEmptyEntries) : throw new ArgumentException("Строка подключения к файловому шкафу не задана.", nameof (connectionString));
    if (source.Length > 2)
      throw new Exception("В строке подключения к файловому шкафу содержится больше двух параметров.");
    if (((IEnumerable<string>) source).Count<string>((Func<string, bool>) (item => item.ToLower().Contains("server"))) > 1)
      throw new Exception("Некорректная строка подключения к файловому шкафу. Параметр server содержится несколько раз: " + connectionString);
    if (((IEnumerable<string>) source).Count<string>((Func<string, bool>) (item => item.ToLower().Contains("port"))) > 1)
      throw new Exception("Некорректная строка подключения к файловому шкафу. Параметр port содержится несколько раз: " + connectionString);
    foreach (string str in source)
    {
      string[] strArray = str.Split('=');
      switch (strArray[0].ToLower())
      {
        case "server":
          this.Server = strArray[1];
          break;
        case "port":
          try
          {
            this.Port = int.Parse(strArray[1]);
            break;
          }
          catch (Exception ex)
          {
            throw new Exception("В строке подключения к файловому шкафу некорректно задан параметр port с номером порта: " + str);
          }
      }
    }
  }

  public string Server { get; set; }

  public int Port { get; set; }

  public string Password { get; set; }

  public void Validate()
  {
    if (string.IsNullOrEmpty(this.Server))
      throw new Exception("В строке подключения отсутствует параметр server с именем компьютера, на котором запущена служба файлового шкафа.");
    if (this.Port < 1 || this.Port > (int) ushort.MaxValue)
      throw new Exception($"В строке подключения к файловому шкафу некорректно задан параметр {"port"} с номером порта: {this.Port}");
    if (string.IsNullOrEmpty(this.Password))
      throw new Exception("В строке подключения отсутствует параметр password с паролем, использующимся для подключения к файловому шкафу.");
  }

  public override string ToString()
  {
    return $"{"server"}={this.Server};{"port"}={this.Port};{"password"}={this.Password};";
  }
}
