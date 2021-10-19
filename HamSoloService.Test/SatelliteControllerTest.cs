using HanSoloService.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HanSoloService.Test
{
	public class SatelliteControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {

        private WebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper _output;

        public SatelliteControllerTest(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
        }

        [Fact]
        public async Task POST_topsecret_two_satellites_should_return_bad_request()
        {
            // Arrange
            var client = _factory.CreateClient();

            string example = "{\"satellites\":[{\"name\":\"kenobi\", \"distance\":100.0, \"message\":[\"este\",\"\",\"\",\"mensaje\",\"\"]}," +
                "{\"name\":\"sato\", \"distance\":142.7, \"message\":[\"este\",\"\",\"un\",\"\",\"\"]}]}";
            // Act
            var response = await client.PostAsync("/topsecret", new StringContent(example, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task POST_topsecret_should_return_right_values()
        {
            // Arrange
            var client = _factory.CreateClient();

            string example = "{\"satellites\":[{\"name\":\"kenobi\", \"distance\":100.0, \"message\":[\"este\",\"\",\"\",\"mensaje\",\"\"]}," +
                "{\"name\":\"skywalker\", \"distance\":115.5, \"message\":[\"\",\"es\",\"\",\"\",\"secreto\"]}," +
                "{\"name\":\"sato\", \"distance\":142.7, \"message\":[\"este\",\"\",\"un\",\"\",\"\"]}]}";
            // Act
            var response = await client.PostAsync("/topsecret", new StringContent(example, Encoding.UTF8, "application/json"));

            var responseContent = response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("{\"position\":{\"x\":-487.29,\"y\":1557.01},\"message\":\"este es un mensaje secreto\"}", responseContent.Result);
        }

        [Fact]
        public async Task POST_topsecret_split_should_return_OK()
        {
            // Arrange

            var client = _factory.CreateClient();

            string example = "{\"distance\":100.0,\"message\":[\"este\",\"\",\"\",\"mensaje\",\"\"]}";
            // Act
            var response = await client.PostAsync("/topsecret-split/kenobi", new StringContent(example, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task POST_topsecret_split_incorrect_satellite_name_should_return_bad_request()
        {
            // Arrange
            var client = _factory.CreateClient();

            string example = "{\"distance\":100.0,\"message\":[\"este\",\"\",\"\",\"mensaje\",\"\"]}";
            // Act
            var response = await client.PostAsync("/topsecret-split/incorrect", new StringContent(example, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GET_topsecret_split_should_return_right_values()
        {
            // Arrange
            var client = _factory.CreateClient();

            string example = "{\"distance\":100.0,\"message\":[\"este\",\"\",\"\",\"mensaje\",\"\"]}";
            await client.PostAsync("/topsecret-split/kenobi", new StringContent(example, Encoding.UTF8, "application/json"));
            
            example = "{\"distance\":115.5,\"message\":[\"\",\"es\",\"\",\"\",\"secreto\"]}";
            await client.PostAsync("/topsecret-split/skywalker", new StringContent(example, Encoding.UTF8, "application/json"));

            example = "{\"distance\":142.7,\"message\":[\"este\",\"\",\"un\",\"\",\"\"]}";
            await client.PostAsync("/topsecret-split/sato", new StringContent(example, Encoding.UTF8, "application/json"));
  
            var response = await client.GetAsync("/topsecret-split");

            var responseContent = response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("{\"position\":{\"x\":-487.29,\"y\":1557.01},\"message\":\"este es un mensaje secreto\"}", responseContent.Result);
        }
    }
}
