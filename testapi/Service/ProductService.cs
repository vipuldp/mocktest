using System.Collections.Generic;
using System.Threading.Tasks;
using testapi.Common;

namespace testapi.Service
{
	public interface IProductService : IBaseService<Product>
	{
		//we can add this file into new folder, only for demo purpose added here
		//here we can add extra method, which are not common for other modules
		List<Product> TestMethod();
	}

	public class ProductService : BaseService<Product, InventoryContext>, IProductService
	{
		protected InventoryContext dbContext;
		public ProductService(InventoryContext context) : base(context)
		{
			this.dbContext = context;
		}

		public override Task<Product> AddData(Product objAdd)
		{
			objAdd.ProductName = objAdd.ProductName.Trim();
			objAdd.ProductDesc = objAdd.ProductDesc.Trim();

			return base.AddData(objAdd);
		}

		public override Task<string> UpdateData(Product objUpdate)
		{
			objUpdate.ProductName = objUpdate.ProductName.Trim();
			objUpdate.ProductDesc = objUpdate.ProductDesc.Trim();

			return base.UpdateData(objUpdate);
		}

		//extra method
		public List<Product> TestMethod()
		{
			throw new System.NotImplementedException();
		}
	}
}
