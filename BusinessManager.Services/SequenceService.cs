using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Services
{
    public class SequenceService : ISequenceService
    {
        public int? GetNextSequenceNumber(IRepository<Sequence> sequenceContext, string sequenceName)
        {
            int? result = null;
            Sequence sequence;
            DataContext dataContext = new DataContext();
            DbSet<Sequence> dbSet = dataContext.Set<Sequence>();

            var query = from m in dbSet
                        select m;
            try
            {
                // get Sequence.Id for Invoice model
                sequence = query.ToList().Where(s => s.SequenceName == sequenceName).Select(c => new Sequence
                {
                    Id = c.Id,
                    SequenceNumber = c.SequenceNumber + 1,
                    ModifiedAt = DateTime.Now
                }).Single();

                // fetch database record to update
                var sequenceToUpdate = sequenceContext.Find(sequence.Id);
                if (sequenceToUpdate != null)
                {
                    sequenceToUpdate.ModifiedAt = sequence.ModifiedAt;
                    if (sequenceToUpdate.SequenceNumber == sequence.SequenceNumber - 1) // check for this to make sure no collision happened from since getting the Id in previous step
                    {
                        sequenceToUpdate.SequenceNumber = sequence.SequenceNumber;
                        sequenceContext.Update(sequenceToUpdate);
                        sequenceContext.Commit();
                        result = sequenceToUpdate.SequenceNumber;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                dataContext.Dispose();
            }

            return result;
        }
    }
}
