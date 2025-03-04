using Entities;
using Moq;
using Repositories;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace TestProject
{
    public class UnitTestOrderService
    {
        [Fact]
        public async Task CheckOrderSum_ReturnsTrue_WhenOrderSumIsCorrect()
        {
            var orderItems = new List<OrderItem> { new OrderItem { ProductId = 1, Quantity = 1 } };
            var order = new Order { Id = 1, OrderSum = 100, OrderItems = orderItems };

            var product = new Product { Id = 1, Price = 100 };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(product);

            var mockOrderRepository = new Mock<IOrderRepository>();

            var orderService = new OrderService(mockOrderRepository.Object, mockProductRepository.Object);
            var result = await orderService.CheckOrderSum(order);

            Assert.True(result);
        }

        [Fact]
        public async Task CheckOrderSum_ReturnsFalse_WhenOrderSumIsIncorrect()
        {
            var orderItems = new List<OrderItem> { new OrderItem { ProductId = 1, Quantity = 1 } };
            var order = new Order { Id = 1, OrderSum = 200, OrderItems = orderItems };

            var product = new Product { Id = 1, Price = 100 };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(product);

            var mockOrderRepository = new Mock<IOrderRepository>();

            var orderService = new OrderService(mockOrderRepository.Object, mockProductRepository.Object);
            var result = await orderService.CheckOrderSum(order);

            Assert.False(result);
        }
    }
}