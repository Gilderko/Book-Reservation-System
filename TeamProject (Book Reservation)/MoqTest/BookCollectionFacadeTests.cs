using System;
using Xunit;
using Autofac.Extras.Moq;

namespace MoqTest
{
    public class BookCollectionFacadeTests
    {
        [Fact]
        public void VerifyExistingTicketWithSmartManager()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                //Mock setup. Output of GetMockTickets() mocks the real connection, which would be provided by DataAccess
                mock.Mock<IDataAccess>()
                   .Setup(x => x.GetTickets())
                   .Returns(GetMockTickets());
                //Ticket manager with mocked DataAccess
                var cls = mock.Create<SmartTicketManager>();
                var tickets = cls.GetTickets();

                //Act
                bool ticket1 = cls.Verify(tickets[0]);
                bool ticket2 = cls.Verify(tickets[1]);

                //Assert
                Assert.True(ticket1);
                Assert.False(ticket2);
            }
        }
    }
}
