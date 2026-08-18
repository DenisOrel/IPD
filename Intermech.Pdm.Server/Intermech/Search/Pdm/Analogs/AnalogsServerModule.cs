// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Analogs.AnalogsServerModule
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

#nullable disable
namespace Intermech.Search.Pdm.Analogs;

public sealed class AnalogsServerModule
{
  private LazyService<IElementStatusesService> _elementStatusesService = new LazyService<IElementStatusesService>();
  private LazyService<IPluginStatusesTable> _pluginStatusesTable = new LazyService<IPluginStatusesTable>();
  private LazyService<IEventLogHelper> _eventLogHelper = new LazyService<IEventLogHelper>();
  private AnalogsFilter _analogsFilter = new AnalogsFilter();

  public void Load()
  {
    this._elementStatusesService.Value.RegisterServerPlugin(new ElementStatusesPluginDescription(32 /*0x20*/, "2B55A281-C8CE-4D0E-9F78-737301FA9369", (string) null, "Аналоги", "Статусы подобранных аналогов")
    {
      IsFlags = true
    });
    this._pluginStatusesTable.Value.AddStatus("2B55A281-C8CE-4D0E-9F78-737301FA9369", 1, AnalogsSelectionStatuses.ActualAnalog.GetDescription<AnalogsSelectionStatuses>(), this.ConvertImageToByteArray((Image) AnalogsResource.ActingAnalog));
    this._pluginStatusesTable.Value.AddStatus("2B55A281-C8CE-4D0E-9F78-737301FA9369", 4, AnalogsSelectionStatuses.Analog.GetDescription<AnalogsSelectionStatuses>(), this.ConvertImageToByteArray((Image) AnalogsResource.Analog));
    this._pluginStatusesTable.Value.AddStatus("2B55A281-C8CE-4D0E-9F78-737301FA9369", 8, AnalogsSelectionStatuses.AnalogsExist.GetDescription<AnalogsSelectionStatuses>(), this.ConvertImageToByteArray((Image) AnalogsResource.AnalogsExist));
    this._pluginStatusesTable.Value.AddStatus("2B55A281-C8CE-4D0E-9F78-737301FA9369", 2, AnalogsSelectionStatuses.PriorityOrOneAnalog.GetDescription<AnalogsSelectionStatuses>(), this.ConvertImageToByteArray((Image) AnalogsResource.PriorityAnalog));
    this._eventLogHelper.Value.GetRecordsListEvent += new GetRecordsListHandler(this.EventLogHelper_GetRecordsListEvent);
  }

  public void Unload()
  {
    this._pluginStatusesTable.Value.RemoveStatuses("2B55A281-C8CE-4D0E-9F78-737301FA9369");
    this._eventLogHelper.Value.GetRecordsListEvent -= new GetRecordsListHandler(this.EventLogHelper_GetRecordsListEvent);
  }

  private void EventLogHelper_GetRecordsListEvent(
    DataTable table,
    object sender,
    DBRecordSetParams parameters,
    IUserSession session)
  {
    if (table == null || parameters.ColumnsInfo == null || session == null)
      return;
    this._analogsFilter.Filter(session, table, parameters);
  }

  private byte[] ConvertImageToByteArray(Image image)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      image.Save((Stream) memoryStream, ImageFormat.Png);
      return memoryStream.GetBuffer();
    }
  }
}
