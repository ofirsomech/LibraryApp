﻿using Microsoft.EntityFrameworkCore;
using Models.Models;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Classes
{
    public class LibraryService : ILibraryService
    {
        public LibraryDbContext Context { get; set; }
        public LibraryService()
        {
            Context = new LibraryDbContext();
        }
        public async Task<bool> CreateBookAsync(Book book)
        {
            try
            {
                Context.Books.Add(book);
                await Context.SaveChangesAsync();
                return true;

            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return false;
            }

        }

        public async Task<bool> CreateJornalAsync(Jornal jornal)
        {
            try
            {
                Context.Jornals.Add(jornal);
                await Context.SaveChangesAsync();
                return true;

            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return false;
            }
        }


        public async Task<List<Book>> GetBooksItemsAsync()
        {
            try
            {
                var books = await Context.Books.ToListAsync();
                var filterBooks = books.Where(b => b.IsActive).ToList();
                return filterBooks;

            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return null;
            }
        }

        public async Task<AbstractItem> GetItemAsync(int id)
        {

            AbstractItem item = await Context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (item == null)
                item = await Context.Jornals.FirstOrDefaultAsync(j => j.Id == id);

            if (item == null)
                return null;
            else
                return item;
        }

        public async Task<List<Jornal>> GetJornalsItemsAsync()
        {
            try
            {
                var jornals = await Context.Jornals.ToListAsync();
                var filterJornals = jornals.Where(j => j.IsActive).ToList();

                return filterJornals;

            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return null;
            }
        }

        public async Task<AbstractItem> DeleteItemAsync(Guid guid)
        {
            AbstractItem item = await Context.Books.FirstOrDefaultAsync(b => b.ISBN == guid);
            if(item == null)
            {
                item = await Context.Jornals.FirstOrDefaultAsync(j => j.ISBN == guid);
            }
            if (item != null)
            {
                item.IsActive = false;
                await Context.SaveChangesAsync();
                return item;

            }
                
            else
                return null;
        }
    }
}