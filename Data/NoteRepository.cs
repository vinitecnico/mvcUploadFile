using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mvcUploadFile.Interfaces;
using mvcUploadFile.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace mvcUploadFile.Data {
    public class NoteRepository : INoteRepository {
        private readonly Context _context = null;
        public NoteRepository (IOptions<Settings> settings) {
            _context = new Context (settings);
        }

        public async Task AddNote (Note item) {
            try {
                await _context.Notes.InsertOneAsync(item);
            } catch (Exception ex) {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Note>> GetAllNotes () {
            try
            {
                return await _context.Notes.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Task<Note> GetNote (string id) {
            throw new NotImplementedException ();
        }

        public Task<IEnumerable<Note>> GetNote (string bodyText, DateTime updatedFrom, long headerSizeLimit) {
            throw new NotImplementedException ();
        }

        public Task<bool> RemoveAllNotes () {
            throw new NotImplementedException ();
        }

        public Task<bool> RemoveNote (string id) {
            throw new NotImplementedException ();
        }

        public Task<bool> UpdateNote (string id, string body) {
            throw new NotImplementedException ();
        }

        public Task<bool> UpdateNoteDocument (string id, string body) {
            throw new NotImplementedException ();
        }
    }
}