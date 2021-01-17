using System.Threading.Tasks;
using LibraryManagementAPI.Core.Exceptions;
using LibraryManagementAPI.Core.Repositories.Interfaces;
using LibraryManagementAPI.Core.Resources;
using LibraryManagementAPI.Core.Services.Interfaces;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> AddAsync(User user)
        {
            if (await _userRepo.GetByEmailAsync(user.Email) != null)
            {
                throw new ApiException(MessagesResource.USER_ALREADY_EXISTS);
            }
            return await _userRepo.AddAsync(user);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userRepo.GetByEmailAsync(email);
        }

        public async Task<User> UpdateAsync(User user)
        {
            return await _userRepo.UpdateAsync(user);
        }
    }
}
