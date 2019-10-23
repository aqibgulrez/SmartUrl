using SmartUrl.Services.HashKey;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace SmartUrl.Services.Test
{
    public class SmartUrlServiceShould
    {
        //private readonly IShortUrlService _shortUrlService;

        //public SmartUrlServiceShould(IShortUrlService shortUrlService)
        //{
        //    _shortUrlService = shortUrlService;
        //}

        //[Fact]
        //public async Task CreateUrl()
        //{
        //    var hashKeyServiceMock = new Mock<IShortUrlService>();


        //    profileRepositoryMock
        //        .Setup(m => m.GetProfileByUserName(It.IsAny<string>()))
        //        .Returns(Task.FromResult((Profile)null));


        //    profileRepositoryMock
        //        .Setup(m => m.CreateProfile(It.IsAny<Profile>()))
        //        .Returns(Task.FromResult(0));


        //    var sut = new ProfileService(profileRepositoryMock.Object);


        //    // Act
        //    var result = await sut.CreateProfile(profile);


        //    // Assert
        //    result.ShouldBeEqualTo(profile);
        //    profileRepositoryMock.Verify(m => m.GetProfileByUserName(It.IsAny<string>()), Times.Once);
        //    profileRepositoryMock.Verify(m => m.CreateProfile(It.IsAny<Profile>()), Times.Once);
        //}
    }
}
