using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TruckApplication.Controllers;
using TruckApplication.Models;
using TruckApplication.Models.Entity;
using Xunit;

namespace TestTruckApplication.controllers
{
    public class TrucksControllerTest
    {
        TrucksFake _trucksFake;
        public TrucksControllerTest()
        {
            _trucksFake = new TrucksFake();
        }
        [Fact]
        public async void TestGetAllType()
        {
            // Act
            var trucks = ((List<TruckEntity>)await _trucksFake.GetDataAsync());
            // Assert
            Assert.IsType<List<TruckEntity>>(trucks);
        }
        [Fact]
        public async void TestGetAll()
        {
            // Act
            var trucks = ((List<TruckEntity>)await _trucksFake.GetDataAsync());
            // Assert
            Assert.True(trucks.Count == 6);
        }
        [Fact]
        public async void TestEdit()
        {
            // Act
            // Act
            TrucksFake tf = new TrucksFake(2);
            var okResult = await _trucksFake.Save();
            // Assert
            Assert.True(okResult);
        }
        [Fact]
        public async void TestAfterEditing()
        {
            // Act
            TruckEntity truck = new TruckEntity();
            truck.Id = 2;
            truck.YearOfFactory = 2060;
            truck.YearOfModel = 2080;

            TrucksFake tf = new TrucksFake(truck);
            var okResult = await tf.Save();
            // Assert
            Assert.True(okResult);

            var truckUpdated = ((List<TruckEntity>)await tf.GetDataAsync()).Find(a => a.Id == truck.Id);
            Assert.Equal(truck.YearOfFactory, truckUpdated.YearOfFactory);
            Assert.Equal(truck.YearOfModel, truckUpdated.YearOfModel);
        }
        [Fact]
        public async void TestNew()
        {
            // Act
            var okResult = await _trucksFake.Save();
            // Assert
            Assert.True(okResult);
        }
        [Fact]
        public async void TestRemoveExists()
        {
            // Act
            TrucksFake tf = new TrucksFake(2);
            var okResult = await tf.RemoveAsync();
            // Assert
            Assert.True(okResult);
        }
        [Fact]
        public async void TestRemoveNotExists()
        {
            // Act
            TrucksFake tf = new TrucksFake(20);
            var okResult = await tf.RemoveAsync();
            // Assert
            Assert.False(okResult);
        }
    }
}
