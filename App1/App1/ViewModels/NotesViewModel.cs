using App1.Models;
using App1.Services;
using App1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
        private Note _selectedNote;

        public ObservableCollection<Note> Notes { get; }
        public Command LoadNotesCommand { get; }
        public Command AddNotesCommand { get; }
        public Command<Note> NoteTapped { get; }

        public NotesViewModel()
        {
            Title = "Notes Details";
            Notes = new ObservableCollection<Note>();
            LoadNotesCommand = new Command(async () => await ExecuteLoadNotesCommand());

            NoteTapped = new Command<Note>(OnNoteSelected);

            AddNotesCommand = new Command(OnAddNote);
        }

        async Task ExecuteLoadNotesCommand()
        {
            IsBusy = true;

            try
            {
                Notes.Clear();
                //INoteDataStore noteDataStore = null;
                
                var notes = await App.Database.GetNotesAsync();
                foreach (var note in notes)
                {
                    Notes.Add(note);

                    Debug.WriteLine(note.Text +"  "+ note.ID +"  "+ note.Date.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedNote = null;
        }

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                SetProperty(ref _selectedNote, value);
                OnNoteSelected(value);
            }
        }

        private async void OnAddNote(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewNotePage));
        }

        async void OnNoteSelected(Note note)
        {
            if (note == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?{nameof(NoteDetailViewModel.ItemId)}={note.ID}");
        }
    }
}
