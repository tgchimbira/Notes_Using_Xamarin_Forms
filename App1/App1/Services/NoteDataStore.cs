using App1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services
{
    public class NoteDataStore : INoteDataStore
    {
        readonly SQLiteAsyncConnection database;
        readonly List<Note> notes;

        public NoteDataStore(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Note>().Wait();
        }

        public async Task<IEnumerable<Note>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(notes);
        }
        public Task<List<Note>> GetNotesAsync()
        {
            //Get all notes.
            return database.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id)
        {
            // Get a specific note.
            return database.Table<Note>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(note);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(note);
            }

        }

        public Task<int> DeleteNoteAsync(Note note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        }
    }
}
