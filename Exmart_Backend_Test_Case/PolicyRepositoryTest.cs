using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace Exmart_Backend_Test_Case
{
    [TestFixture]
    public class PolicyRepositoryTest
    {
        private readonly ApplicationDBContext _db;
        private readonly PolicyRepo _policyRepository;

        public PolicyRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _db = new ApplicationDBContext(options);
            _policyRepository = new PolicyRepo(_db);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _db.TermsAndConditions.AddRange(new List<Policy>
        {
            new Policy { Id = 1, TndCheading = "Terms And Conditions", TndCcontent = "Trail T & C" },
            new Policy { Id = 2, TndCheading = "Payment Policy", TndCcontent = "Trail Payment Policy" },
            new Policy { Id = 3, TndCheading = "Shipping Policy", TndCcontent = "Trail Shipping Policy" }
        });

            _db.SaveChanges();
        }

        [OneTimeTearDownAttribute]
        public void TearDown()
        {
            _db.Database.EnsureDeleted();
            _db.Dispose();
        }
        [Test]
        public async Task EditPolicies_ShouldUpdatePolicy_WhenPolicyExists()
        {
            var updatedPolicy = new Policy { Id = 1, TndCcontent = "Updated T & C" };
            var result = await _policyRepository.EditPolicies(1, updatedPolicy);

            Assert.IsNotNull(result);
            Assert.AreEqual("Updated T & C", result.TndCcontent);
        }

        [Test]
        public async Task EditPolicies_ShouldReturnNull_WhenPolicyDoesNotExist()
        {
            var updatedPolicy = new Policy { Id = 4, TndCcontent = "Non-existing Policy" };
            var result = await _policyRepository.EditPolicies(4, updatedPolicy);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetPoliciesById_ShouldReturnPolicy_WhenPolicyExists()
        {
            var result = await _policyRepository.GetPoliciesById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async Task GetPoliciesById_ShouldReturnNull_WhenPolicyDoesNotExist()
        {
            var result = await _policyRepository.GetPoliciesById(4);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetPolicies_ShouldReturnAllPolicies()
        {
            var result = await _policyRepository.GetPolicies();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        //private readonly ApplicationDBContext _db;
        //private readonly PolicyRepo _policyRepository;

        //public PolicyRepositoryTest()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDBContext>()
        //            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //            .Options;

        //    _db = new ApplicationDBContext(options);
        //    _policyRepository = new PolicyRepo(_db);
        //}


        //[Test]
        //public async Task EditPolicies_ShouldUpdatePolicy_WhenPolicyExists()
        //{
        //    // Arrange
        //    Policy policy = new Policy { Id = 1, TndCcontent = "Old Content" };
        //    _db.TermsAndConditions.Add(policy);
        //    await _db.SaveChangesAsync();

        //    Policy updatedPolicy = new Policy { TndCcontent = "Updated Content" };

        //    // Act
        //    Policy result = await _policyRepository.EditPolicies(1, updatedPolicy);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equals("Updated Content", result.TndCcontent);
        //}

        //[Test]
        //public async Task EditPolicies_ShouldReturnNull_WhenPolicyDoesNotExist()
        //{
        //    // Arrange
        //    var updatedPolicy = new Policy { TndCcontent = "Updated Content" };

        //    // Act
        //    var result = await _policyRepository.EditPolicies(999, updatedPolicy);

        //    // Assert
        //    Assert.Null(result);
        //}

        //[Test]
        //public async Task GetPoliciesById_ShouldReturnPolicy_WhenPolicyExists()
        //{
        //    // Arrange
        //    var policy = new Policy { Id = 1, TndCcontent = "Content" };
        //    _db.TermsAndConditions.Add(policy);
        //    await _db.SaveChangesAsync();

        //    // Act
        //    var result = await _policyRepository.GetPoliciesById(1);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equals(1, result.Id);
        //}

        //[Test]
        //public async Task GetPoliciesById_ShouldReturnNull_WhenPolicyDoesNotExist()
        //{
        //    // Act
        //    var result = await _policyRepository.GetPoliciesById(999);

        //    // Assert
        //    Assert.Null(result);
        //}

        //[Test]
        //public async Task GetPolicies_ShouldReturnAllPolicies()
        //{
        //    // Arrange
        //    var policies = new List<Policy>
        //{
        //    new Policy { Id = 1, TndCcontent = "Content 1" },
        //    new Policy { Id = 2, TndCcontent = "Content 2" },
        //    new Policy { Id = 3, TndCcontent = "Content 3" }
        //};
        //    _db.TermsAndConditions.AddRange(policies);
        //    await _db.SaveChangesAsync();

        //    // Act
        //    var result = await _policyRepository.GetPolicies();

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equals(3, result.Count());
        //}

        
    }
}
