// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.ExceptionHandler
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
///  Представляет метод, который вызывается для обработки исключительной ситуации
/// </summary>
public delegate void ExceptionHandler(object sender, ExceptionEventArgs e);
