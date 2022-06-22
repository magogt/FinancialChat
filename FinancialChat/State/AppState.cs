using FinancialChat.Core.Entities;

namespace FinancialChat.State
{
  public class AppState
  {
    public AppState(IHttpContextAccessor ctx)
    {
      CurrentUser = ctx.HttpContext?.User?.Identity?.Name ?? "";
    }
    public ChatRoom? ActiveRoom { get; private set; }
    public string? CurrentUser { get; private set; } 
    public event Action<ChatRoom?, ChatRoom>? OnChangeActiveRoom;

    public void SetActiveRoom(ChatRoom? room)
    {
      if (room != null &&room != ActiveRoom)
      {
        var previous = ActiveRoom;
        ActiveRoom = room;
        NotifyActiveRoomChanged(previous, room);
      }
    }
    private void NotifyActiveRoomChanged(ChatRoom? previous, ChatRoom current) =>  OnChangeActiveRoom?.Invoke(previous, current);

  }
}
