using App1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App1.Services
{
    public interface INoteDataStore
    {
        /*
        Task<int> DeleteNoteAsync(Note note);
        Task<Note> GetNoteAsync(int id);
        Task<List<Note>> GetNotesAsync();
        Task<int> SaveNoteAsync(Note note);
        */
         Task<int> SaveNoteAsync(Note item);
       // Task<bool> UpdateItemAsync(Note item);
        Task<int> DeleteNoteAsync(Note note);
         Task<Note> GetNoteAsync(int id);
        Task<List<Note>> GetNotesAsync();
        //Task<IEnumerable<Note>> GetNotesAsync(bool forceRefresh = false);
        
    }
}