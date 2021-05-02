using System;
using Xunit;
using TimsLumber;
using TimsLumber.Models;
using System.Collections.Generic;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace TimsLumberTests
{
    public class OrderServiceTests : TimsLumberContextTest, IDisposable
    {

        private readonly DbConnection _connection;

        public OrderServiceTests()
            : base(
                new DbContextOptionsBuilder<TimsLumberContext>()
                    .UseSqlite(CreateInMemoryDatabase())
                    .Options)
        {
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public void Dispose() => _connection.Dispose();

        [Fact]
        public void CaluclateTaxTest1()
        {
            //Arrange
            double input = 10.00;
            double expected = 0.6;

            //Act
            double actual = OrderService.CalculateTax(input);

            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void CaluclateTaxTest2()
        {
            //Arrange
            double input = 55.92;
            double expected = 3.3552;

            //Act
            double actual = OrderService.CalculateTax(input);

            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void CaluclateTotalTest1()
        {
            //Arrange
            double subtotal = 100.00;
            double expected = 106.00;

            //Act
            double actual = OrderService.CalculateTotal(subtotal);

            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void CaluclateTotalTest2()
        {
            //Arrange
            double subtotal = 15.12;
            double expected = 16.0272;

            //Act
            double actual = OrderService.CalculateTotal(subtotal);

            //Assert
            Assert.Equal(expected, actual);

        }

        
        [Fact]
        public void FinalizeOrderPricingTest1()
        {
            //Arrange
            Order order = new Order();
            OrderItem OI1 = new OrderItem();
            OrderItem OI2 = new OrderItem();
            OI1.Cost = 10.00;
            OI2.Cost = 20.40;
            order.OrderItems = new List<OrderItem>() { OI1, OI2 };

            double expSubtotal = 30.40;
            double expTax = 1.824;
            double expTotal = 32.224;

            //Act
            Order actual = OrderService.FinalizeOrder(order);

            //Assert
            Assert.Equal(expSubtotal, actual.Subtotal, 4);
            Assert.Equal(expTax, actual.Tax, 4);
            Assert.Equal(expTotal, actual.Total, 4);

        }

        [Fact]
        public void FinalizeOrderPricingTest2()
        {
            //Arrange
            Order order = new Order();
            OrderItem OI1 = new OrderItem();
            OrderItem OI2 = new OrderItem();
            OrderItem OI3 = new OrderItem();
            OI1.Cost = 32.13;
            OI2.Cost = 23.12;
            OI3.Cost = 13.52;
            order.OrderItems = new List<OrderItem>() { OI1, OI2, OI3 };

            double expSubtotal = 68.77;
            double expTax = 4.1262;
            double expTotal = 72.8962;

            //Act
            Order actual = OrderService.FinalizeOrder(order);

            //Assert
            Assert.Equal(expSubtotal, actual.Subtotal, 4);
            Assert.Equal(expTax, actual.Tax, 4);
            Assert.Equal(expTotal, actual.Total, 4);

        }

        [Fact]
        public void GetOrderItemsTest1()
        {
            //Arrange
            AddViewModel AVM = new AddViewModel();
            AVM.LumberItemId = 3;
            AVM.Length = 15;
            
            TimsLumberContext ctx = new TimsLumberContext(ContextOptions);

            //Act
            OrderItem OI = OrderService.AddItemToOrder(AVM, ctx);

            //Assert
            Assert.Equal(3, OI.LumberItem.LumberItemId);
            Assert.Equal(15, OI.Length);
            Assert.Equal(11.25, OI.Cost);

        }

        [Fact]
        public void GetOrderItemsTest2()
        {
            //Arrange
            AddViewModel AVM = new AddViewModel();
            AVM.LumberItemId = 12;
            AVM.Length = 10;

            TimsLumberContext ctx = new TimsLumberContext(ContextOptions);

            //Act
            OrderItem OI = OrderService.AddItemToOrder(AVM, ctx);

            //Assert
            Assert.Equal(12, OI.LumberItem.LumberItemId);
            Assert.Equal(10, OI.Length);
            Assert.Equal(14.80, OI.Cost);

        }

        [Fact]
        public void EditItemsTestLengthsAreCorrect()
        {
            //Arrange

            TimsLumberContext ctx = new TimsLumberContext(ContextOptions);
            Order order = new Order();
            ctx.Add(order);
            ctx.SaveChanges();
            int id = order.OrderId;
            int[] LIds = { 1, 2, 3, 4 };
            int[] Lengths = { 10, 20, 15, 30 };
            
            
            //Act
            List<OrderItem> OIList = OrderService.EditItems(id, LIds, Lengths, ctx);
            order.OrderItems = OIList;

            //Assert
            Assert.True(order.OrderItems.Count == 4);

            Assert.Equal(10, OIList[0].Length);
            Assert.Equal(20, OIList[1].Length);
            Assert.Equal(15, OIList[2].Length);
            Assert.Equal(30, OIList[3].Length);
        }

        [Fact]
        public void EditItemsTestCostsAreCorrect()
        {
            //Arrange

            TimsLumberContext ctx = new TimsLumberContext(ContextOptions);
            Order order = new Order();
            ctx.Add(order);
            ctx.SaveChanges();
            int id = order.OrderId;
            int[] LIds = { 1, 2, 3, 4 };
            int[] Lengths = { 10, 20, 15, 30 };


            //Act
            List<OrderItem> OIList = OrderService.EditItems(id, LIds, Lengths, ctx);
            order.OrderItems = OIList;

            //Assert
            Assert.True(order.OrderItems.Count == 4);

            Assert.Equal((0.55 * 10), OIList[0].Cost);
            Assert.Equal((0.65 * 20), OIList[1].Cost);
            Assert.Equal((0.75 * 15), OIList[2].Cost);
            Assert.Equal((0.85 * 30), OIList[3].Cost);
        }

        [Fact]
        public void EditItemsTestLumberItemsAreCorrect()
        {
            //Arrange

            TimsLumberContext ctx = new TimsLumberContext(ContextOptions);
            Order order = new Order();
            ctx.Add(order);
            ctx.SaveChanges();
            int id = order.OrderId;
            int[] LIds = { 1, 2, 3, 4 };
            int[] Lengths = { 10, 20, 15, 30 };


            //Act
            List<OrderItem> OIList = OrderService.EditItems(id, LIds, Lengths, ctx);
            order.OrderItems = OIList;

            //Assert
            Assert.True(order.OrderItems.Count == 4);

            Assert.Equal(1, OIList[0].LumberItem.LumberItemId);
            Assert.Equal(2, OIList[1].LumberItem.LumberItemId);
            Assert.Equal(3, OIList[2].LumberItem.LumberItemId);
            Assert.Equal(4, OIList[3].LumberItem.LumberItemId);
        }

        [Fact]
        public void PopulateLIsTest1()
        {
            //Arrange

            TimsLumberContext ctx = new TimsLumberContext(ContextOptions);
            Order order = new Order();
            OrderItem OI1 = new OrderItem();
            OrderItem OI2 = new OrderItem();
            OI1.LumberItemId = 1;
            OI2.LumberItemId = 7;
            order.OrderItems = new List<OrderItem>() { OI1, OI2};

            LumberItem exp1 = new LumberItem { LumberItemId = 1, NominalSize = "1x2", ActualSize = "3/4\" × 1-1/2\"", PricePerFt = 0.55 };
            LumberItem exp2 = new LumberItem { LumberItemId = 7, NominalSize = "2x2", ActualSize = "1-1/2\" × 1-1/2\"", PricePerFt = 0.98 };


            //Act
            order = OrderService.PopulateLIs(order, ctx);
            List<OrderItem> OIList = new List<OrderItem>();
            
            foreach (OrderItem OI in order.OrderItems)
            {
                OIList.Add(OI);
            }

            //Assert
            Assert.True(order.OrderItems.Count == 2);
            Assert.Equal(JsonConvert.SerializeObject(exp1), JsonConvert.SerializeObject(OIList[0].LumberItem));
            Assert.Equal(JsonConvert.SerializeObject(exp2), JsonConvert.SerializeObject(OIList[1].LumberItem));
        }

        [Fact]
        public void PopulateLIsTest2()
        {
            //Arrange

            TimsLumberContext ctx = new TimsLumberContext(ContextOptions);
            Order order = new Order();
            OrderItem OI1 = new OrderItem();
            OrderItem OI2 = new OrderItem();
            OrderItem OI3 = new OrderItem();
            OI1.LumberItemId = 1;
            OI2.LumberItemId = 7;
            OI3.LumberItemId = 12;
            order.OrderItems = new List<OrderItem>() { OI1, OI2, OI3 };

            LumberItem exp1 = new LumberItem { LumberItemId = 1, NominalSize = "1x2", ActualSize = "3/4\" × 1-1/2\"", PricePerFt = 0.55 };
            LumberItem exp2 = new LumberItem { LumberItemId = 7, NominalSize = "2x2", ActualSize = "1-1/2\" × 1-1/2\"", PricePerFt = 0.98 };
            LumberItem exp3 = new LumberItem { LumberItemId = 12, NominalSize = "2x10", ActualSize = "1-1/2\" × 9-1/4\"", PricePerFt = 1.48 };


            //Act
            order = OrderService.PopulateLIs(order, ctx);
            List<OrderItem> OIList = new List<OrderItem>();

            foreach (OrderItem OI in order.OrderItems)
            {
                OIList.Add(OI);
            }

            //Assert
            Assert.True(order.OrderItems.Count == 3);
            Assert.Equal(JsonConvert.SerializeObject(exp1), JsonConvert.SerializeObject(OIList[0].LumberItem));
            Assert.Equal(JsonConvert.SerializeObject(exp2), JsonConvert.SerializeObject(OIList[1].LumberItem));
            Assert.Equal(JsonConvert.SerializeObject(exp3), JsonConvert.SerializeObject(OIList[2].LumberItem));
        }

    }
}
