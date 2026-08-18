
// Type: Intermech.Services.PdfPrintCenterService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Client.Core;
using Intermech.Interfaces.Client;
using Intermech.PdfPrintCenter.Connector;
using Intermech.Remoting.Ipc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization;


namespace Intermech.Services;

/// <summary>
/// Сервис подключения и связи с центром печати. Реализация является thread safe
/// </summary>
internal sealed class PdfPrintCenterService : IPdfPrintCenterService
{
  private readonly object syncRoot = new object();
  private IpcConnector<IPrintCenterSystem> printCenterConnector;
  private Func<PdfPrintCenterPDMSystem> pdmSystemFactory;
  private IApplicationStateEventsService applicationStateEventsService;
  private bool channelRegistered;

  public PdfPrintCenterService(
    Func<PdfPrintCenterPDMSystem> pdmSystemFactory,
    IApplicationStateEventsService applicationStateEventsService)
  {
    if (pdmSystemFactory == null)
      throw new ArgumentNullException(nameof (pdmSystemFactory));
    if (applicationStateEventsService == null)
      throw new ArgumentNullException(nameof (applicationStateEventsService));
    this.pdmSystemFactory = pdmSystemFactory;
    this.applicationStateEventsService = applicationStateEventsService;
    this.InitializeHandlers();
  }

  /// <summary>
  /// Добавляет pdf-документы из выбранных объектов в центр печати pdf
  /// </summary>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="documents" /> либо printCenterConnector равны null</exception>
  /// <exception cref="T:System.IO.FileNotFoundException">Не найден исполняемый файл центра печати</exception>
  public void AddFilesToPrintCenter(List<IPDMDocumentInfo> documents)
  {
    if (documents == null)
      throw new ArgumentNullException(nameof (documents));
    lock (this.syncRoot)
    {
      this.LaunchPdfPrintCenter();
      if (this.printCenterConnector == null)
        throw new ArgumentNullException("printCenterConnector");
      this.printCenterConnector.GetOrConnect().AddDocuments(documents.OfType<PDMDocumentInfo>().ToList<PDMDocumentInfo>());
    }
  }

  /// <summary>
  /// Создаёт эксземпляр класса InternalDocumentInfo согласно передаваемым данным
  /// </summary>
  /// <param name="objectName">Имя объекта</param>
  /// <param name="filePaths">Пути к pdf-файлам, относящимся к объекту</param>
  /// <exception cref="T:System.ArgumentNullException">Один из параметров равен null</exception>
  public IPDMDocumentInfo CreateDocumentInfo(string objectName, List<string> filePaths)
  {
    if (objectName == null)
      throw new ArgumentNullException(nameof (objectName));
    return filePaths != null ? (IPDMDocumentInfo) new PdfPrintCenterService.InternalDocumentInfo(objectName, filePaths) : throw new ArgumentNullException(nameof (filePaths));
  }

  /// <summary>
  /// Осуществляет подключение к центру печати pdf с помощью remoting
  /// </summary>
  /// <exception cref="T:System.IO.FileNotFoundException">Не найден исполняемый файл центра печати</exception>
  public void LaunchPdfPrintCenter()
  {
    lock (this.syncRoot)
    {
      if (!this.channelRegistered)
      {
        ChannelServices.RegisterChannel((IChannel) new IpcChannel(Process.GetCurrentProcess().Id.ToString()), false);
        this.channelRegistered = true;
        RemotingServices.Marshal((MarshalByRefObject) this.pdmSystemFactory(), "PrintCenterPDMSystem", typeof (IPDMSystem));
      }
      string executablePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PdfPrintCenter", "PdfPrintCenter.exe");
      this.printCenterConnector = new IpcConnector<IPrintCenterSystem>()
      {
        ConnectionInfo = new IpcConnectionInfo("Центр печати PDF", "PrintCenterSystem", executablePath, "/WaitForPDMSystem")
      };
      this.printCenterConnector.GetOrConnect().SetPDMSystem(new IpcConnectionInfo("IPS", "PrintCenterPDMSystem", Assembly.GetEntryAssembly().Location));
    }
  }

  private void InitializeHandlers()
  {
    this.applicationStateEventsService.Exit += new EventHandler(this.ApplicationStateEventsService_Exit);
  }

  private void ApplicationStateEventsService_Exit(object sender, EventArgs e)
  {
    if (this.printCenterConnector == null || !this.printCenterConnector.IsConnected)
      return;
    this.printCenterConnector.GetOrConnect().ClosePrintCenter();
  }

  [Serializable]
  private class InternalDocumentInfo : PDMDocumentInfo, IPDMDocumentInfo, ISerializable
  {
    public InternalDocumentInfo(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public InternalDocumentInfo(string objectName, List<string> filePaths)
      : base(objectName, filePaths)
    {
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.SetType(typeof (PDMDocumentInfo));
    }

    [SpecialName]
    string IPDMDocumentInfo.get_ObjectName() => this.ObjectName;

    [SpecialName]
    List<string> IPDMDocumentInfo.get_FilePaths() => this.FilePaths;
  }
}
