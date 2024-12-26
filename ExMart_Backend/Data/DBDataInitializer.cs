using ExMart_Backend.Model;

namespace ExMart_Backend.Data
{
    public class DBDataInitializer
    {
        List<Product> productList = new();
        List<AddToCart> cartList = new();

        private readonly ApplicationDBContext _dbcontext;
        public DBDataInitializer(ApplicationDBContext dbcontext)
        {
            _dbcontext = dbcontext;
            InitializeData();
        }

        private void InitializeData()
        {
            productList = _dbcontext.Products.ToList();
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
