using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TruckApplication.Controllers;
using TruckApplication.Models;
using TruckApplication.Models.Entity;
using Xunit;

namespace TestTruckApplication.controllers
{
    public class ModelControllerTest
    {
        ModelFake _modelsFake;
        public ModelControllerTest()
        {
            _modelsFake = new ModelFake();
        }
        [Fact]
        public async void TestEdit()
        {
            // Act
            ModelFake mf = new ModelFake(2);
            var okResult = await _modelsFake.Save();
            // Assert
            Assert.True(okResult);
        }
        [Fact]
        public async void TestGetAllType()
        {
            // Act
            var models = ((List<ModelEntity>)await _modelsFake.GetDataAsync());
            // Assert
            Assert.IsType<List<ModelEntity>>(models);
        }
        [Fact]
        public async void TestGetAll()
        {
            // Act
            var models = ((List<ModelEntity>)await _modelsFake.GetDataAsync());
            // Assert
            Assert.True(models.Count == 6);
        }
        [Fact]
        public async void TestAfterEditing()
        {
            // Act
            ModelEntity model = new ModelEntity();
            model.Id = 2;
            model.Description = "Description 20";

            ModelFake mf = new ModelFake(model);
            var okResult = await mf.Save();
            // Assert
            Assert.True(okResult);

            var modelUpdated = ((List<ModelEntity>)await mf.GetDataAsync()).Find(a=>a.Id == model.Id);            
            Assert.Equal(model.Description, modelUpdated.Description);
        }
        [Fact]
        public async void TestNew()
        {
            // Act
            var okResult = await _modelsFake.Save();
            // Assert
            Assert.True(okResult);
        }
        [Fact]
        public async void TestRemoveExists()
        {
            // Act
            ModelFake mf = new ModelFake(2);
            var okResult = await mf.RemoveAsync();
            // Assert
            Assert.True(okResult);
        }
        [Fact]
        public async void TestRemoveNotExists()
        {
            // Act
            ModelFake mf = new ModelFake(20);
            var okResult = await mf.RemoveAsync();
            // Assert
            Assert.False(okResult);
        }
    }
}
