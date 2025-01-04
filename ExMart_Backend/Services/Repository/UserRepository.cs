using System.Net;
using ExMart_Backend.Data;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDBContext _db;
        public UserRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                int Id = _db.Users.OrderByDescending(u => u.Id).FirstOrDefault()?.Id + 1 ?? 1;
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task AddAddress(AddAddressDTO addAddressDTO)
        {
            var userAddress = new UserAddress
            {
                UserId = addAddressDTO.UserId,
                AddressTypeId = addAddressDTO.AddressTypeId,
                IsPrimary = addAddressDTO.IsPrimary,
                AddressLine = addAddressDTO.AddressLine,
                City = addAddressDTO.City,
                District = addAddressDTO.District,
                State = addAddressDTO.State,
                ZipCode = addAddressDTO.ZipCode
            };


            _db.UserAddresses.Add(userAddress);
            await _db.SaveChangesAsync();
        }



        public Task<List<UserAddress>> GetAddressByUserId(int userId)
        {
            return _db.UserAddresses
                .Include(address => address.AddressType)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> EditAddressById(int id, AddAddressDTO editAddressDTO)
        {
            var existingAddress = await _db.UserAddresses.FindAsync(id);

            if (existingAddress == null)
            {
                return false; // Address not found
            }

            // Update fields
            existingAddress.AddressLine = editAddressDTO.AddressLine ?? existingAddress.AddressLine;
            existingAddress.City = editAddressDTO.City ?? existingAddress.City;
            existingAddress.District = editAddressDTO.District ?? existingAddress.District;
            existingAddress.State = editAddressDTO.State ?? existingAddress.State;
            existingAddress.ZipCode = editAddressDTO.ZipCode ?? existingAddress.ZipCode;
            existingAddress.IsPrimary = editAddressDTO.IsPrimary;

            _db.UserAddresses.Update(existingAddress);
            await _db.SaveChangesAsync();

            return true; // Address updated successfully
        }

    }
}
