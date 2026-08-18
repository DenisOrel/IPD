
// Type: Intermech.Tools.Integrators.IntegratorSettingsService`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Memoization;
using Intermech.Tools.Settings;
using System;
using System.Xml;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует основу для сервиса интегратора, обеспечивающего чтение настроек интегратора. В этом классе
/// реализовано получение настроек интегратора с сервера, их декодирование и кэширование.
/// </summary>
public abstract class IntegratorSettingsService<TSettings> : 
  IntegratorService,
  IIntegratorSettingsService,
  IIntegratorService,
  IPersistentIntegratorSettingsService
  where TSettings : class, ISettingsObject
{
  private IntegratorSettingsCacheManager integratorSettingsCacheManager;
  private IntegratorSettingsCodec settingsCodec;
  private IntegratorSettingsValidator settingsValidator;
  private object resetSeq;
  private DateTime lastServerCheck;
  private DateTime lastWriteTimeUtc;
  private TSettings settingsObject;
  private SimpleStateMonitor ownStateMonitor;
  private IStateMonitor fullStateMonitor;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Объект интегратора с приложением</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
  public IntegratorSettingsService(IIntegrator owner)
    : base(owner)
  {
    this.integratorSettingsCacheManager = ServiceUtils.GetService<IntegratorSettingsCacheManager>((object) ApplicationServices.Container, true);
    this.resetSeq = this.integratorSettingsCacheManager.ResetMonitor.WriterSeqNum;
    this.lastServerCheck = DateTime.MinValue;
    this.lastWriteTimeUtc = DateTime.MinValue;
    this.ownStateMonitor = new SimpleStateMonitor(false);
    this.fullStateMonitor = (IStateMonitor) new CompositeStateMonitor(new IStateMonitor[2]
    {
      (IStateMonitor) this.ownStateMonitor,
      this.integratorSettingsCacheManager.ResetMonitor
    });
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.settingsCodec = this.CreateSettingsCodec();
    if (this.settingsCodec == null)
      throw new InvalidOperationException("The settings codec can't be null.");
    this.settingsValidator = this.CreateSettingsValidator();
    if (this.settingsValidator == null)
      throw new InvalidOperationException("The settings validator can't be null.");
  }

  protected abstract IntegratorSettingsCodec CreateSettingsCodec();

  protected abstract IntegratorSettingsValidator CreateSettingsValidator();

  /// <summary>
  /// Возвращает объект настроек интегратора.
  /// При первом вызове этого метода выполняется кэширование настроек интегратора. Кэш настроек автоматически сбрасывается при их изменении в базе IPS.
  /// </summary>
  /// <returns>Объект настроек интегратора</returns>
  /// <exception cref="T:System.Exception">Объект с настройками еще не создан в базе IPS, либо содержит ошибки</exception>
  public TSettings GetSettings()
  {
    this.RequireReadyState();
    this.CheckServer();
    return this.settingsObject;
  }

  /// <summary>
  /// Возвращает объект настроек интегратора.
  /// При первом вызове этого метода выполняется кэширование настроек интегратора. Кэш настроек автоматически сбрасывается при их изменении в базе IPS.
  /// </summary>
  /// <returns>Объект настроек интегратора</returns>
  /// <exception cref="T:System.Exception">Объект с настройками еще не создан в базе IPS, либо содержит ошибки</exception>
  ISettingsObject IIntegratorSettingsService.GetSettingsObject()
  {
    return (ISettingsObject) this.GetSettings();
  }

  /// <summary>
  /// Возвращает монитор состояния для настроек интегратора. С его помощью можно определить момент переполучения сервисом настроек с сервера приложений IPS.
  /// </summary>
  /// <returns>Монитор состояния для настроек интегратора</returns>
  public IStateMonitor GetSettingsStateMonitor()
  {
    this.RequireReadyState();
    return this.fullStateMonitor;
  }

  /// <summary>
  /// Выполняет преобразование объекта с настройками в xml-документ.
  /// </summary>
  /// <param name="settingsObject">Объект с настройками</param>
  /// <returns>Настройки в форме xml-документа</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на объект с настройками не может быть null</exception>
  public XmlDocument EncodeSettings(ISettingsObject settingsObject)
  {
    this.RequireReadyState();
    return this.settingsCodec.Encode(settingsObject);
  }

  /// <summary>
  /// Выполняет преобразование xml-документа в объект с настройками.
  /// </summary>
  /// <param name="settingsXml">Настройки в форме xml-документа</param>
  /// <returns>Объект с настройками</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на xml-документ не могжет быть null</exception>
  /// <exception cref="T:System.NotSupportedException">Неизвестная версия формата xml-документа</exception>
  public ISettingsObject DecodeSettings(XmlDocument data)
  {
    this.RequireReadyState();
    return this.settingsCodec.Decode(data);
  }

  /// <summary>Выполняет проверку корректности настроек.</summary>
  /// <param name="settingsObject">Объект с настройками</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект с настройками не может быть null</exception>
  /// <exception cref="T:System.Exception">Настройки содержат ошибку</exception>
  public void ValidateSettings(ISettingsObject settingsObject, SettingsValidatorContext context)
  {
    this.RequireReadyState();
    this.settingsValidator.Validate(settingsObject, context);
  }

  /// <summary>
  /// Проверяет наличие кэшированного объекта настроек интегратора и его валидность. При необходимости
  /// переполучает настройки с сервера приложений.
  /// </summary>
  protected void CheckServer()
  {
    lock (this.Integrator.SyncRoot)
    {
      if (!this.integratorSettingsCacheManager.ResetMonitor.AnyWritersSince(this.resetSeq) && !(DateTime.Now - this.lastServerCheck > this.integratorSettingsCacheManager.ServerCheckPeriod))
        return;
      DateTime now = DateTime.Now;
      object writerSeqNum = this.integratorSettingsCacheManager.ResetMonitor.WriterSeqNum;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IIntegratorServer service = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true);
        if (!service.IsIntegratorExists(this.Integrator.Id))
          throw new IntegratorNotInstalledException(this.Integrator.DisplayName);
        IntegratorDetails integratorDetails = service.GetIntegratorDetails(this.Integrator.Id);
        if (integratorDetails.LastWriteTimeUtc > this.lastWriteTimeUtc)
        {
          XmlDocument data = new XmlDocument();
          data.LoadXml(service.GetIntegratorData(this.Integrator.Id));
          ISettingsObject settingsObject = this.DecodeSettings(data);
          this.ValidateSettings(settingsObject, SettingsValidatorContext.Generic);
          if ((object) this.settingsObject != null)
            this.ResetCache();
          this.settingsObject = (TSettings) settingsObject;
          this.lastWriteTimeUtc = integratorDetails.LastWriteTimeUtc;
          this.ownStateMonitor.UpdateState();
        }
      }
      this.resetSeq = writerSeqNum;
      this.lastServerCheck = now;
    }
  }

  /// <summary>
  /// Вызывается в случае получения с сервера нового объекта настроек. Может использоваться для очистки
  /// кэшированных сведений, созданных на основе старого объекта настроек.
  /// </summary>
  protected virtual void ResetCache()
  {
  }
}
