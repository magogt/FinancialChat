
using FinancialChat.Core.Entities;
using FinancialChat.Infrastructure.DB;
using FinancialChat.Infrastructure.Test.DB;

namespace FinancialChat.Infrastructure.Repositories.Tests
{
  [TestClass()]
  public class ChatRoomRepositoryTests
  {
    private static DbContext _ctx;
    private static ChatRoomRepository _repository;

    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
      _ctx = DBContextInitializer.GetContext();
      _repository = new ChatRoomRepository(_ctx);
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
    public async Task InsertTest()
    {
      var room = new ChatRoom
      {
        Name = "room-test"
      };
      var data = await _repository.Insert(room);
      Assert.AreEqual(4, data.Id);
    }

    [TestMethod()]
    public async Task UpdateTest()
    {
      var id = 4;
      var room = new ChatRoom
      {
        Id = id,
        Name = "room-test-modified"
      };
      var count = await _repository.Update(room);
      Assert.AreEqual(1, count);
    }
  }
}