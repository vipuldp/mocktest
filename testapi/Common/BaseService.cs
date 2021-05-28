using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testapi.Common
{
	public abstract class BaseService<M, C> : IBaseService<M> where M : BaseEntity, new() where C : InventoryContext
	{
		private readonly C _context;

		protected BaseService(C context)
		{
			_context = context;
		}

		public virtual async Task<M> AddData(M objAdd)
		{
			objAdd.PKId = 0;    //for new record
			objAdd.ModifiedDate = null;    //resetting to old, if user changed it while updating any other value

			await _context.Set<M>().AddAsync(objAdd);
			_context.SaveChanges();
			return objAdd;
		}

		public virtual Task<string> UpdateData(M objUpdate)
		{
			var objEdit = _context.Products.FirstOrDefault(d => d.PKId == objUpdate.PKId);
			if (objEdit == null)
				return Task.FromResult("Data not found for updation.");
			else
			{
				objUpdate.CreatedDate = objEdit.CreatedDate;    //resetting to old, if user changed it while updating any other value
				_context.Entry(objEdit).State = EntityState.Detached;
			}

			objUpdate.ModifiedDate = DateTime.UtcNow;
			_context.Entry(objUpdate).State = EntityState.Modified;
			_context.SaveChanges();

			return Task.FromResult(string.Empty);
		}

		public Task<string> DeleteData(int deleteId)
		{
			var objDelete = _context.Products.FirstOrDefault(d => d.PKId == deleteId);
			if (objDelete == null)
				return Task.FromResult("Data not found for deletion.");

			_context.Remove(objDelete);
			_context.SaveChanges();

			return Task.FromResult(string.Empty);
		}

		public async Task<IEnumerable<M>> GetAllData()
		{
			return await _context.Set<M>().ToListAsync();
		}
	}
}
