using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class UnitTestOrderRepository
    {
        [Fact]
        public async Task GetById_ReturnsOrder()
        {
            var user = new User { UserId = 1, UserName = "Leah", Password = "Aa!123dsA" };
            var orderItems = new List<OrderItem> { new OrderItem {  ProductId = 1, Quantity = 2 } };
            var order = new Order { Id = 1, User = user, OrderItems = orderItems };

            var mockContext = new Mock<ManagerDbContext>();
            var orders = new List<Order>() { order };
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);

            var orderRepository = new OrderRepository(mockContext.Object);
            var result = await orderRepository.GetById(1);

            Assert.Equal(order, result);
        }

        [Fact]
        public async Task GetById_OrderNotFound_ReturnsNull()
        {
            var user = new User { UserId = 1, UserName = "Leah", Password = "Aa!123dsA" };
            var orderItems = new List<OrderItem> { new OrderItem {  ProductId = 1, Quantity = 2 } };
            var order = new Order { Id = 1, User = user, OrderItems = orderItems };

            var mockContext = new Mock<ManagerDbContext>();
            var orders = new List<Order>() { order };
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);

            var orderRepository = new OrderRepository(mockContext.Object);
            var result = await orderRepository.GetById(2);

            Assert.Null(result);
        }
    }
}