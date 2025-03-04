using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class IntegrationTestOrderService : IClassFixture<DataBaseFixture>
    {
        private readonly DataBaseFixture _dbFixture;

        public IntegrationTestOrderService()
        {
            _dbFixture = new();
        }

        [Fact]
        public async Task Post_OrderWithValidSum_AddsOrderToDatabase()
        {
            // Arrange
            var _orderRepository = new OrderRepository(_dbFixture.Context);
            var _productRepository = new ProductRepository(_dbFixture.Context);
            var _orderService = new OrderService(_orderRepository, _productRepository);
            var _userRepository = new UserRepository(_dbFixture.Context);

            var user = new User { UserName = "leah11@gmail.com", Password = "hfJG!@32h", FirstName = "Leah", LastName = "Shmuelevits" };
            var dbUser = await _userRepository.Post(user);
            await _dbFixture.Context.SaveChangesAsync();

            var category = new Category { CategoryName = "Category1" };
            await _dbFixture.Context.Categories.AddAsync(category);
            await _dbFixture.Context.SaveChangesAsync();

            var product = new Product { ProductName = "Product1", Price = 100, CategoryId = category.Id };
            await _dbFixture.Context.Products.AddAsync(product);
            await _dbFixture.Context.SaveChangesAsync();

            var orderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = product.Id, Quantity = 1 }
            };
            var order = new Order
            {
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                OrderSum = 100.0f,
                UserId = dbUser.UserId,
                OrderItems = orderItems
            };

            // Act
            var result = await _orderService.Post(order);
            await _dbFixture.Context.SaveChangesAsync();

            // Assert
            var addedOrder = await _dbFixture.Context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == result.Id);

            Assert.NotNull(addedOrder);
            Assert.Equal(order.OrderDate, addedOrder.OrderDate);
            Assert.Equal(order.OrderSum, addedOrder.OrderSum);
            Assert.Equal(order.UserId, addedOrder.UserId);
            Assert.Equal(order.OrderItems.Count, addedOrder.OrderItems.Count);
            _dbFixture.Dispose();
        }

        [Fact]
        public async Task Post_OrderWithInvalidSum_DoesNotAddOrderToDatabase()
        {
            // Arrange
            var _orderRepository = new OrderRepository(_dbFixture.Context);
            var _productRepository = new ProductRepository(_dbFixture.Context);
            var _orderService = new OrderService(_orderRepository, _productRepository);
            var _userRepository = new UserRepository(_dbFixture.Context);

            var user = new User { UserName = "leah11@gmail.com", Password = "hfJG!@32h", FirstName = "Leah", LastName = "Shmuelevits" };
            var dbUser = await _userRepository.Post(user);
            await _dbFixture.Context.SaveChangesAsync();

            var category = new Category { CategoryName = "Category1" };
            await _dbFixture.Context.Categories.AddAsync(category);
            await _dbFixture.Context.SaveChangesAsync();

            var product = new Product { ProductName = "Product1", Price = 100, CategoryId = category.Id };
            await _dbFixture.Context.Products.AddAsync(product);
            await _dbFixture.Context.SaveChangesAsync();

            var orderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = product.Id, Quantity = 1 }
            };
            var order = new Order
            {
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                OrderSum = 200.0f, // Invalid sum
                UserId = dbUser.UserId,
                OrderItems = orderItems
            };

            // Act
            var result = await _orderService.Post(order);
            await _dbFixture.Context.SaveChangesAsync();

            // Assert
            if (result != null)
            {
                var addedOrder = await _dbFixture.Context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == result.Id);

                Assert.Null(addedOrder);
            }
            else
            {
                Assert.Null(result);
            }

            _dbFixture.Dispose();
        }
    }
}