
// Type: Intermech.Interfaces.SelectionFormCommandExecHandler
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Делегат на событие, возникающие при вызове команды в диалоге редактирования условий выборки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SelectionFormCommandExecHandler(
      object sender,
      SelectionFormCommandExecEventArgs e);
}
