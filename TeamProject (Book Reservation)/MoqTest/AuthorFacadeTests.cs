using Autofac.Extras.Moq;
using DAL.Entities;
using Infrastructure;
using System;
using Xunit;


namespace MoqTest
{
    public class AuthorFacadeTests
    {
        [Fact]
        public void VerifyExistingTicketWithSmartManager()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                //Mock setup. Output of GetMockTickets() mocks the real connection, which would be provided by DataAccess
                mock.Mock<IRepository<Author>>();
                   
                   

                //Ticket manager with mocked DataAccess
               

                //Act
                

                //Assert
                
            }
        }
    }
}
