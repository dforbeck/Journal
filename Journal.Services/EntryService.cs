﻿using Journal.Data;
using Journal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.Services
{
    public class EntryService
    {
        private readonly Guid _userId;

        public EntryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateEntry(EntryCreate model)
        {
            var entity = new Entry()
            {
                OwnerId = _userId,
                Title = model.Title,
                Content = model.Content,
                CreatedUtc = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Entries.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //entries that belong to a certain user is next
        public IEnumerable<EntryListItem>   GetEntries()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Entries
                    .Where(e => e.OwnerId == _userId)
                    .Select(e => new EntryListItem
                        {
                        EntryId = e.EntryId,
                        Title = e.Title,
                        CreatedUtc = e.CreatedUtc
                        }
                    );
                return query.ToArray();
            }
        }
    }
}
