using ExMart_Backend.Model;
using Supabase;

namespace ExMart_Backend.Data
{
    public class DBDataInitializer
    {
        public static List<Product> productList = new List<Product>();
        public static List<AddToCart> cartList = new List<AddToCart>();
        private static int _cartIdCounter = 1; // Start with 1

        public static int GetNextCartId()
        {
            return _cartIdCounter++;
        }
        private readonly ApplicationDBContext _dbContext;

        public DBDataInitializer(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            InitializeData();   
        }

        public void InitializeData()
        {
            productList = _dbContext.Products.ToList();
        }

        public List<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }
        public async Task<Product> GetProductById(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
