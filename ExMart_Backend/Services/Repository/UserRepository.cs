using System.Net;
using ExMart_Backend.Data;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
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

        //public async Task<bool> AddUser(User user)
        //{
        //    try
        //    {
        //        int Id = _db.Users.OrderByDescending(u => u.Id).FirstOrDefault()?.Id + 1 ?? 1;
        //        await _db.Users.AddAsync(user);
        //        await _db.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}
    

        public async Task<bool> AddUser(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User data cannot be null.");
                }

                // Determine new user ID
                int newId = _db.Users.OrderByDescending(u => u.Id).FirstOrDefault()?.Id + 1 ?? 1;
                user.Id = newId;

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsUserExisted(int userId)
        {
            try
            {
                return await _db.Users.AnyAsync(m => m.Id == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking user existence: {ex.Message}");
                throw new Exception("An error occurred while checking user existence.", ex);
            }
        }

        public async Task<int?> ReturnIdbyEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email cannot be null or empty.", nameof(email));
                }

                var user = await _db.Users.FirstOrDefaultAsync(m => m.Email == email);
                return user?.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user ID by email: {ex.Message}");
                throw new Exception("An error occurred while retrieving user ID by email.", ex);
            }
        }

        public async Task<string?> ReturnEmailById(int id)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(m => m.Id == id);
                return user?.Email;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving email by user ID: {ex.Message}");
                throw new Exception("An error occurred while retrieving email by user ID.", ex);
            }
        }
        public async Task<string?> ReturnNameById(int id)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(m => m.Id == id);
                return user?.Name;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Name by user ID: {ex.Message}");
                throw new Exception("An error occurred while retrieving Name by user ID.", ex);
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
                ZipCode = addAddressDTO.ZipCode,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                CreatedBy = addAddressDTO.UserId
            };


            _db.UserAddresses.Add(userAddress);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserAddress>> GetUserAddress()
        {
            try
            {
                return await _db.UserAddresses
           .Select(address => new UserAddress
           {
               Id = address.Id,
               UserId = address.UserId,
               AddressType = new AddressType
               {
                   Id = address.AddressType.Id,
                   AddressTypeName = address.AddressType.AddressTypeName
               },
               IsPrimary = address.IsPrimary,
               AddressLine = address.AddressLine,
               City = address.City,
               District = address.District,
               State = address.State,
               ZipCode = address.ZipCode,
               CreatedAt = DateTime.UtcNow,
               IsActive = true,
               CreatedBy = address.UserId
           })
           .ToListAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in GetCategoriesAsync: {ex.Message}");
                throw;
            }
        }
        public Task<List<UserAddress>> GetAddressByUserId(int userId)
        {
            return _db.UserAddresses
                .Include(address => address.AddressType)
                .Where(a => a.UserId == userId && a.IsActive)
                .ToListAsync();
        }
        public async Task<UserAddress> GetAddressById(int id)
        {
            return await _db.UserAddresses
                            .Include(address => address.AddressType)
                            .FirstOrDefaultAsync(a => a.Id == id && a.IsActive);
        }
        public async Task<bool> EditAddressById(int id, AddAddressDTO editAddressDTO)
        {
            var existingAddress = await _db.UserAddresses.FindAsync(id);

            if (existingAddress == null)
            {
                return false;
            }

            existingAddress.AddressLine = editAddressDTO.AddressLine ?? existingAddress.AddressLine;
            existingAddress.City = editAddressDTO.City ?? existingAddress.City;
            existingAddress.District = editAddressDTO.District ?? existingAddress.District;
            existingAddress.State = editAddressDTO.State ?? existingAddress.State;
            existingAddress.ZipCode = editAddressDTO.ZipCode ?? existingAddress.ZipCode;
            existingAddress.IsPrimary = editAddressDTO.IsPrimary;
            existingAddress.UpdatedAt = DateTime.UtcNow;

            _db.UserAddresses.Update(existingAddress);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAddressById(int id)
        {
            var address = await _db.UserAddresses.FirstOrDefaultAsync(a => a.Id == id && a.IsActive);

            if (address == null)
            {
                return false; // Address not found or already inactive
            }

            address.IsActive = false;
            address.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        //public async Task<bool> IsUserExisted(int userId)
        //{
        //    if (await _db.Users.AnyAsync(m => m.Id == userId))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //public async Task<int?> ReturnIdbyEmail(string email)
        //{
        //    var user = await _db.Users.FirstOrDefaultAsync(m => m.Email == email);

        //    // If the user exists, return their ID, otherwise return null
        //    return user?.Id;
        //}
        //public async Task<string?> ReturnEmailById(int id)
        //{
        //    var user = await _db.Users.FirstOrDefaultAsync(m => m.Id == id);

        //    // If the user exists, return their ID, otherwise return null
        //    return user?.Email;
        //}
    }
}

