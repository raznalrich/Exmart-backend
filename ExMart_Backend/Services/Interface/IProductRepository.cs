namespace ExMart_Backend.Services.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<object>> GetProducts();
    }
}
