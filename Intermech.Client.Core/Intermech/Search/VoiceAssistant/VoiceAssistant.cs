
// Type: Intermech.Search.VoiceAssistant.VoiceAssistant
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Search.VoiceAssistant;

public sealed class VoiceAssistant : IVoiceAssistant
{
  private List<IVoiceAssistantGrammarsProvider> _grammarsProviders = new List<IVoiceAssistantGrammarsProvider>();
  private List<IVoiceAssistantCommandsTarget> _commandsTargets = new List<IVoiceAssistantCommandsTarget>();
  private Dictionary<string, VoiceAssistantHint> _tipDictionary = new Dictionary<string, VoiceAssistantHint>();
  private SpeechRecognitionEngine _speechRecognitionEngine;
  private VoiceAssistantState _state;

  public VoiceAssistant()
  {
    this.AddHint(new VoiceAssistantHint("guid", new GrammarBuilder("гуид")));
    this.AddHint(new VoiceAssistantHint("imbase", new GrammarBuilder(new Choices(new string[2]
    {
      "имбэйс",
      "айэмбэйс"
    }))));
    this.AddHint(new VoiceAssistantHint("jt", new GrammarBuilder(new Choices(new string[2]
    {
      "джити",
      "гт"
    }))));
    this.AddHint(new VoiceAssistantHint("cadmech", new GrammarBuilder("кадмех")));
    this.AddHint(new VoiceAssistantHint("3d", new GrammarBuilder(new Choices(new string[2]
    {
      "три дэ",
      "три ди"
    }))));
    this.AddCommandsTarget((IVoiceAssistantCommandsTarget) new FormCommandsTarget());
    this.AddCommandsTarget((IVoiceAssistantCommandsTarget) new MessageBoxCommandsTarget());
    this.AddCommandsTarget((IVoiceAssistantCommandsTarget) new MainMenuCommandsTarget());
    this.AddCommandsTarget((IVoiceAssistantCommandsTarget) new NavigatorContextMenuCommandsTarget());
  }

  public void AddGrammarsProvider(IVoiceAssistantGrammarsProvider grammarsProvider)
  {
    if (grammarsProvider == null)
      throw new ArgumentNullException(nameof (grammarsProvider));
    if (this._grammarsProviders.Contains(grammarsProvider))
      return;
    this._grammarsProviders.Add(grammarsProvider);
    this.ReloadGrammars();
  }

  public void RemoveGrammarsProvider(IVoiceAssistantGrammarsProvider grammarsProvider)
  {
    if (grammarsProvider == null)
      throw new ArgumentNullException(nameof (grammarsProvider));
    if (!this._grammarsProviders.Contains(grammarsProvider))
      return;
    this._grammarsProviders.Remove(grammarsProvider);
    this.ReloadGrammars();
  }

  public IVoiceAssistantCommandsTarget ActiveCommandsTarget { get; set; }

  public void AddCommandsTarget(IVoiceAssistantCommandsTarget commandsTarget)
  {
    if (commandsTarget == null)
      throw new ArgumentNullException(nameof (commandsTarget));
    if (this._commandsTargets.Contains(commandsTarget))
      return;
    this._commandsTargets.Add(commandsTarget);
  }

  public void RemoveCommandsTarget(IVoiceAssistantCommandsTarget commandsTarget)
  {
    if (commandsTarget == null)
      throw new ArgumentNullException(nameof (commandsTarget));
    this._commandsTargets.Remove(commandsTarget);
  }

  public void AddHint(VoiceAssistantHint tip)
  {
    if (tip == null)
      throw new ArgumentNullException(nameof (tip));
    if (this._tipDictionary.ContainsKey(tip.Phrase))
      return;
    this._tipDictionary[tip.Phrase] = tip;
    this.ReloadGrammars();
  }

  public VoiceAssistantHint GetHint(string text)
  {
    if (string.IsNullOrEmpty(text))
      throw new ArgumentNullException(nameof (text));
    VoiceAssistantHint hint = (VoiceAssistantHint) null;
    this._tipDictionary.TryGetValue(text, out hint);
    return hint;
  }

  public void RemoveHint(string text)
  {
    if (string.IsNullOrEmpty(text))
      throw new ArgumentNullException(nameof (text));
    if (!this._tipDictionary.ContainsKey(text))
      return;
    this._tipDictionary.Remove(text);
    this.ReloadGrammars();
  }

  public event EventHandler StateChanged;

  public VoiceAssistantState State
  {
    get => this._state;
    private set
    {
      if (this._state == value)
        return;
      this._state = value;
      this.OnStateChanged();
    }
  }

  public void Start()
  {
    if (this.State == VoiceAssistantState.Running)
      return;
    if (this._speechRecognitionEngine == null)
    {
      try
      {
        this._speechRecognitionEngine = new SpeechRecognitionEngine();
        this._speechRecognitionEngine.SetInputToDefaultAudioDevice();
        this._speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(this.SpeechRecognitionEngine_SpeechRecognized);
        this.AddGrammarsProvider((IVoiceAssistantGrammarsProvider) new RussianGrammarsProvider());
        this.AddGrammarsProvider((IVoiceAssistantGrammarsProvider) new NavigatorContextMenuGrammarsProvider());
      }
      catch (Exception ex)
      {
        this._speechRecognitionEngine = (SpeechRecognitionEngine) null;
        switch (ex)
        {
          case NotSupportedException _:
          case PlatformNotSupportedException _:
            throw new Exception("Распознавание речи не поддерживается для данной платформы. SAPI или движок распознавания речи не найдены.");
          default:
            throw;
        }
      }
    }
    this._speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
    this.State = VoiceAssistantState.Running;
  }

  public void Stop()
  {
    if (this.State != VoiceAssistantState.Running)
      return;
    this._speechRecognitionEngine.RecognizeAsyncStop();
    this.State = VoiceAssistantState.Stopped;
  }

  private void SpeechRecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
  {
    Thread thread = new Thread((ThreadStart) (() =>
    {
      if (e.Result == null)
        return;
      try
      {
        if (this.ActiveCommandsTarget != null && this.ActiveCommandsTarget.Execute(e.Result))
          return;
        using (List<IVoiceAssistantCommandsTarget>.Enumerator enumerator = this._commandsTargets.GetEnumerator())
        {
          do
            ;
          while (enumerator.MoveNext() && !enumerator.Current.Execute(e.Result));
        }
      }
      catch (Exception ex)
      {
        ServiceLocator.Get<IOutputView>().WriteString("Ошибки", ex.Message);
      }
    }));
    thread.Name = "VoiceAssistant_SpeechRecognized";
    thread.IsBackground = true;
    thread.SetApartmentState(ApartmentState.STA);
    thread.Start();
  }

  private void ReloadGrammars()
  {
    if (this._speechRecognitionEngine == null)
      return;
    this._speechRecognitionEngine.UnloadAllGrammars();
    foreach (IVoiceAssistantGrammarsProvider grammarsProvider in this._grammarsProviders)
    {
      foreach (Grammar grammar in grammarsProvider.GetGrammars())
        this._speechRecognitionEngine.LoadGrammar(grammar);
    }
  }

  private void OnStateChanged()
  {
    EventHandler stateChanged = this.StateChanged;
    if (stateChanged == null)
      return;
    stateChanged((object) this, EventArgs.Empty);
  }
}
