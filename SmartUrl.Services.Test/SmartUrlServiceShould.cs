using SmartUrl.Services.HashKey;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Options;
using SmartUrl.Repository;
using SmartUrl.Entities.Domain;
using FluentAssert;

namespace SmartUrl.Services.Test
{
    public class SmartUrlServiceShould
    {
        private const string _uriKey = "1ndas3kk5";
        private const string _url = "https:localhost";
        private static IOptions<ManagedConfig> ConfigureManagedConfig()
        {
            return Options.Create(new ManagedConfig
            {
                CollisionIterations = 10,
                KeyLength = 8,
                UniqueUrlKeyIterations = 5,
                ApplicationPath = "https://localhost:44390/{0}"
            });
        }

        [Fact]
        public async Task GetSmartUrl()
        {
            var managedConfig = ConfigureManagedConfig();
            var smartUriResponse = new SmartUrlEntity { Id = 1 };
            var dataProviderMock = new Mock<IDataProvider>();
            dataProviderMock.Setup(a => a.GetSmartUrlByKey(It.Is<string>(m => m.Equals(_uriKey))))
                .Returns(Task.FromResult(smartUriResponse));

            var sut = new HashKeyService(managedConfig, dataProviderMock.Object);


            // Act
            var result = await sut.GetSmartUrl(_uriKey);


            // Assert
            result.Id.ShouldBeEqualTo(smartUriResponse.Id);
            dataProviderMock.Verify(a => a.GetSmartUrlByKey(It.Is<string>(m => m.Equals(_uriKey))), Times.Once);
        }

        [Fact]
        public async Task CreateSmartUrlShouldReturnExistingRecord()
        {
            var managedConfig = ConfigureManagedConfig();
            var smartUriResponse = new SmartUrlEntity { Id = 1 };
            var dataProviderMock = new Mock<IDataProvider>();
            dataProviderMock.Setup(a => a.GetSmartUrlByHash(It.IsAny<string>()))
              .Returns(Task.FromResult(smartUriResponse));

            var sut = new HashKeyService(managedConfig, dataProviderMock.Object);


            // Act
            var result = await sut.CreateSmartUrl(_url);


            // Assert
            result.Id.ShouldBeEqualTo(smartUriResponse.Id);
            dataProviderMock.Verify(a => a.GetSmartUrlByHash(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateSmartUrlShouldCreateNewRecord()
        {
            var managedConfig = ConfigureManagedConfig();
            var smartUriResponse = new SmartUrlEntity { Id = 1 };
            var dataProviderMock = new Mock<IDataProvider>();
            dataProviderMock.Setup(a => a.GetSmartUrlByHash(It.IsAny<string>()))
              .Returns(Task.FromResult((SmartUrlEntity)null));
            dataProviderMock.Setup(a => a.GetSmartUrlByKey(It.Is<string>(m => m.Equals(_url))))
          .Returns(Task.FromResult(smartUriResponse));

            var sut = new HashKeyService(managedConfig, dataProviderMock.Object);


            // Act
            var result = await sut.CreateSmartUrl(_url);


            // Assert
            result.Id.ShouldBeEqualTo(smartUriResponse.Id);
            dataProviderMock.Verify(a => a.GetSmartUrlByHash(It.IsAny<string>()), Times.Once);
            dataProviderMock.Verify(a => a.GetSmartUrlByKey(It.Is<string>(m => m.Equals(_url))), Times.Once);

        }

        [Fact]
        public async Task CreateSmartUrlShouldGenerateUniqueKey()
        {
            var managedConfig = ConfigureManagedConfig();
            var smartUriResponse = new SmartUrlEntity { Url = _url };
            var dataProviderMock = new Mock<IDataProvider>();
            dataProviderMock.Setup(a => a.GetSmartUrlByHash(It.IsAny<string>()))
              .Returns(Task.FromResult((SmartUrlEntity)null));
            dataProviderMock.Setup(a => a.GetSmartUrlByKey(It.Is<string>(m => m.Equals(_url))))
          .Returns(Task.FromResult((SmartUrlEntity)null));
            dataProviderMock.Setup(a => a.GetSmartUrlByKey(It.IsAny<string>()))
             .Returns(Task.FromResult(smartUriResponse));
            dataProviderMock.Setup(a => a.Add(It.IsAny<SmartUrlEntity>()))
           .Returns(Task.FromResult(0));

            var sut = new HashKeyService(managedConfig, dataProviderMock.Object);


            // Act
            var result = await sut.CreateSmartUrl(_url);


            // Assert
            result.Url.ShouldBeEqualTo(smartUriResponse.Url);
            dataProviderMock.Verify(a => a.GetSmartUrlByHash(It.IsAny<string>()), Times.Once);
            dataProviderMock.Verify(a => a.GetSmartUrlByKey(It.IsAny<string>()), Times.Exactly(5));
            dataProviderMock.Verify(a => a.Add(It.IsAny<SmartUrlEntity>()), Times.Once);
        }

        [Fact]
        public async Task CreateSmartUrlShouldReturnNull()
        {
            var managedConfig = ConfigureManagedConfig();
            managedConfig.Value.KeyLength = 0;
            var dataProviderMock = new Mock<IDataProvider>();
            dataProviderMock.Setup(a => a.GetSmartUrlByHash(It.IsAny<string>()))
              .Returns(Task.FromResult((SmartUrlEntity)null));
            dataProviderMock.Setup(a => a.GetSmartUrlByKey(It.IsAny<string>()))
          .Returns(Task.FromResult((SmartUrlEntity)null));
            dataProviderMock.Setup(a => a.GetSmartUrlByKey(It.IsAny<string>()))
             .Returns(Task.FromResult((SmartUrlEntity)null));

            var sut = new HashKeyService(managedConfig, dataProviderMock.Object);


            // Act
            var result = await sut.CreateSmartUrl(_url);


            // Assert
            result.Id.ShouldBeEqualTo(0);
            dataProviderMock.Verify(a => a.GetSmartUrlByHash(It.IsAny<string>()), Times.Once);
            dataProviderMock.Verify(a => a.GetSmartUrlByKey(It.IsAny<string>()), Times.Once);
        }
    }
}
