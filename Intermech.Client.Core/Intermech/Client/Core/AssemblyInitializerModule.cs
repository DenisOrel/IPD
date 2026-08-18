
// Type: Intermech.Client.Core.AssemblyInitializerModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Client.Core.ThumbnailDocs;
using Intermech.Files;
using Intermech.Redline;
using Intermech.UI;


namespace Intermech.Client.Core;

/// <summary>
/// Модуль инициализации, обеспечивающий ранний запуск сервисов клиента IPS,
/// не зависящих от серисов навигатора.
/// </summary>
public sealed class AssemblyInitializerModule : LazyInitializerModuleGroup
{
  public AssemblyInitializerModule(IInitializerModuleFactory moduleFactory)
    : base(moduleFactory)
  {
    this.Add<UIReportDisplayModule>();
    this.Add<RedliningFileServicesModule>();
    this.Add<RedliningSettingsEditorModule>();
    this.Add<FileSettingsEditorModule>();
    this.Add<FileManagerModule>();
    this.Add<SaveAllOnExitModule>();
    this.Add<RegisterPreviewExtractorsModule>();
    this.Add<RecentObjectsFromNotifyServiceModule>();
    this.Add<WorkAreaFileDeleteServiceModule>();
  }
}
