using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Online_Shop.Tests
{
    public class OrderApiControllerTest
    {
        moqData _moqdata;
        public OrderApiControllerTest()
        {
            _moqdata = new moqData();
        }

        [Fact]
        public void Success_Create_Order_Test()
        {
            //Arrange
            //var mediator = new Mock<IOrderCommandModel>();

            //CreateOrderCommand createOrderCommand = new CreateOrderCommand { };  //with fake data

            //OrderCommands commands = new OrderCommands(mediator.Object);

            //Act
            //var result = await commands.Handle(createOrderCommand, new System.Threading.CancellationToken());

            //Asert
            //Do the assertion
            //var expectedResult = mockDataBase.Value.OutputData;
            // result.StatusCode.Should().Be(expectedResult.StatusCode);
            // exceptionResult.Result.Status.Should().Be(expectedResult.Result.Status);
            // exceptionResult.Result.Message.Should().Contain(expectedResult.Result.Message);
            // exceptionResult.Data.Should().BeNull();
        }
    }
}
