// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.SaveAsEventHandler
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Делегат для обработки событий перед и после сохранения файлов</summary>
public delegate void SaveAsEventHandler(object sender, SaveAsEventHandlerArgs e);
