﻿using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);
        Task<IEnumerable<object>> GetProducts();
        Task<Product> AddProductAsync(Product product);
        Task<bool> DeactivateProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsByNameAsync(string productName);
    }
}
