using ExMart_Backend.Model;

namespace ExMart_Backend.Data
{
    public class DBDataInitializer
    {
        public static List<Product> productList = new List<Product>();
        public static List<AddToCart> cartList = new List<AddToCart>();
        private readonly ApplicationDBContext _dbContext;
        public DBDataInitializer(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            InitializeData();
        }

        private void InitializeData()
        {
            productList = _dbContext.Products.ToList();
        }

        public List<Product> GetProducts()
        {
            return productList;
        }
        public async Task<Product> GetProductById(int id)
        {
            return productList.FirstOrDefault(p => p.Id == id);
        }
    }
}
