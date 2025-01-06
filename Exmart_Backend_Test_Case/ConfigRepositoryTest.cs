    using ExMart_Backend.Data;
    using ExMart_Backend.Model;
    using ExMart_Backend.Services.Repository;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;


namespace Exmart_Backend_Test_Case
{
    public class ConfigRepositoryTests
        {
            private readonly ApplicationDBContext _db;
            private readonly ConfigRepository _configRepository;

            public ConfigRepositoryTests()
            {
                var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;

                _db = new ApplicationDBContext(options);
                _configRepository = new ConfigRepository(_db);
            }

        [OneTimeTearDownAttribute]
        public void TearDown()
        {
            _db.Database.EnsureDeleted();
            _db.Dispose();
        }
        //[Test]
        //    public async Task GetColorById_ExistingId_ReturnsColor()
        //    {
        //        // Arrange
        //        var color = new ColourMaster { ColorId = 1, ColorName = "red" };
        //        _db.ColourMaster.Add(color);
        //        await _db.SaveChangesAsync();

        //        // Act
        //        var result = await _configRepository.GetColorById(1);

        //        // Assert
        //        Assert.NotNull(result);
        //        Assert.Equals(color.ColorName, result.ColorName);
        //    }

            [Test]
            public async Task GetColorById_NonExistingId_ReturnsNull()
            {
                // Act
                var result = await _configRepository.GetColorById(999);

                // Assert
                Assert.Null(result);
            }

            //[Test]
            //public async Task GetSizeById_ExistingId_ReturnsSize()
            //{
            //    // Arrange
            //    var size = new SizeMaster { SizeId = 1, Size = "XS" };
            //    _db.SizeMaster.Add(size);
            //    await _db.SaveChangesAsync();

            //    // Act
            //    var result = await _configRepository.GetSizeById(1);

            //    // Assert
            //    Assert.NotNull(result);
            //    Assert.Equals(size.Size, result.Size);
            //}

            [Test]
            public async Task GetSizeById_NonExistingId_ReturnsNull()
            {
                // Act
                var result = await _configRepository.GetSizeById(999);

                // Assert
                Assert.Null(result);
            }
        }
    }