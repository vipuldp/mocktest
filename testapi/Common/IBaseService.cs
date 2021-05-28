using System.Collections.Generic;
using System.Threading.Tasks;

namespace testapi.Common
{
	public interface IBaseService<M>
	{
		Task<M> AddData(M objAdd);

		Task<string> UpdateData(M objUpdate);

		Task<string> DeleteData(int deleteId);

		Task<IEnumerable<M>> GetAllData();
	}
}
