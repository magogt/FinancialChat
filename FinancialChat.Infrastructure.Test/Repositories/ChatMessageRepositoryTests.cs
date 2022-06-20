using FinancialChat.Core.Entities;
using FinancialChat.Infrastructure.DB;
using FinancialChat.Infrastructure.Test.DB;

namespace FinancialChat.Infrastructure.Repositories.Tests
{
  [TestClass()]
  public class ChatMessageRepositoryTests
  {
    private static ChatDbContext _ctx;
    private static ChatMessageRepository _repository;

    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
      _ctx = DBContextInitializer.GetContext();
      _repository = new ChatMessageRepository(_ctx);
    }


    [TestMethod()]
    public async Task DeleteTestAsync()
    {
      var id = 3;
      var count = await _repository.Delete(id);
      Assert.AreEqual(1, count);
      Assert.IsNull(_ctx.ChatMessages.FirstOrDefault(x => x.Id == id));
    }

    [TestMethod()]
    public async Task GetByRoomTest()
    {
      var room = 1;
      var expectedMessages = new string[] { "Hello World", "Hello Room 1" };
      var result = await _repository.GetByRoom(room);
      Assert.IsNotNull(result);
      var messages = result.Select(x => x.Message).ToArray();
      Assert.AreEqual(2, messages.Count());
      CollectionAssert.AreEqual(expectedMessages, messages);
    }

    [TestMethod()]
    public async Task InsertTest()
    {
      var message = new ChatMessage
      {
        Date = DateTime.Now,
        Message = "Hello test",
        RoomId = 2,
        User = "test"
      };
      var data = await _repository.Insert(message);
      Assert.AreEqual(4, data.Id);
    }

    [TestMethod()]
    public async Task UpdateTest()
    {
      var id = 4;
      var message = new ChatMessage
      {
        Id = id,
        Date = DateTime.Now,
        Message = "Hello test modified",
        RoomId = 2,
        User = "test-modified"
      };
      var count = await _repository.Update(message);
      Assert.AreEqual(1, count);
    }
  }
}