//using ExMart_Backend.Data;
//using ExMart_Backend.Model;
//using ExMart_Backend.Services.Repository;

//namespace Exmart_Backend_Test_Case
//{
//    [TestFixture]
//    public class AddToCartRepoTest
//    {
//        private AddToCartRepository _addToCartRepository;
//    private List<AddToCart> _testCartList;
//        public AddToCartRepoTest()
//        {
//            _testCartList = new List<AddToCart>
//        {
//            new AddToCart { ProductId = 1, UserId = 1, Quantity = 1 },
//            new AddToCart { ProductId = 2, UserId = 1, Quantity = 2 },
//            new AddToCart { ProductId = 3, UserId = 2, Quantity = 1 }
//        };
//            DBDataInitializer.cartList = new List<AddToCart>(_testCartList);

//            _addToCartRepository = new AddToCartRepository();
//        }

//        [Test]
//        public void AddToCart_ShouldAddItemToCart()
//        {
//            // Arrange
//            var newItem = new AddToCart { ProductId = 4, UserId = 999, Quantity = 1 };

//            // Act
//            var result = _addToCartRepository.AddToCart(newItem);

//            // Assert
//            Assert.IsTrue(result);
//            Assert.Contains(newItem, DBDataInitializer.cartList);
//        }

//        [Test]
//        public async Task GetCartList_ShouldReturnAllCartItems()
//        {
//            // Act
//            var cartList = await _addToCartRepository.GetCartList();

//            // Assert
//            Assert.AreEqual(_testCartList.Count, cartList.Count);
//        }

//        [Test]
//        public void DeleteCartList_ShouldRemoveItemFromCart()
//        {
//            // Arrange
//            var productId = 1;
//            var userId = 1;

//            // Act
//            var result = _addToCartRepository.DeleteCartList(productId, userId);

//            // Assert
//            Assert.IsTrue(result);
//            Assert.IsNull(DBDataInitializer.cartList.FirstOrDefault(cart => cart.ProductId == productId && cart.UserId == userId));
//        }

//        [Test]
//        public void DeleteCartList_ShouldReturnFalseIfItemNotFound()
//        {
//            // Arrange
//            var productId = 999;
//            var userId = 999;

//            // Act
//            var result = _addToCartRepository.DeleteCartList(productId, userId);

//            // Assert
//            Assert.IsFalse(result);
//        }
//    }


  
//}

