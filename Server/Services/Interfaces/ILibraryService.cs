using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Interfaces
{
    public interface ILibraryService
    {
        Task<List<Book>> GetBooksItemsAsync();
        Task<List<Jornal>> GetJornalsItemsAsync();
        Task<AbstractItem> GetItemAsync(int id);
        Task<bool> CreateBookAsync(Book book);
        Task<bool> CreateJornalAsync(Jornal jornal);
        Task<AbstractItem> DeleteItemAsync(Guid guid);
    }
}
