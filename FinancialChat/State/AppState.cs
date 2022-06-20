using FinancialChat.Core.Entities;

namespace FinancialChat.State
{
  public class AppState
  {
    public ChatRoom? ActiveRoom { get; private set; }
    public string? CurrentUser { get; private set; } = "mgongora";
    public event Action? OnChangeActiveRoom;

    public void SetActiveRoom(ChatRoom? room)
    {
      if (room != ActiveRoom)
      {
        ActiveRoom = room;
        NotifyActiveRoomChanged();
      }
    }

    private void NotifyActiveRoomChanged() =>  OnChangeActiveRoom?.Invoke();
  }
}
