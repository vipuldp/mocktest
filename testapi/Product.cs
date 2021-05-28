using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testapi.Common;

namespace testapi
{
	[Table("test_vp")]
	public class Product : BaseEntity
	{
		[Required(ErrorMessage = "Product Name is required")]
		[StringLength(20, ErrorMessage = "Product Name cannot exceed {1} characters ")]
		public string ProductName { get; set; }

		[Required(ErrorMessage = "Product Description is required")]
		[StringLength(100, ErrorMessage = "{0} cannot exceed {1} characters ")]
		public string ProductDesc { get; set; }

		[Required(ErrorMessage = "Price is required")]
		[Range(1, 10000000, ErrorMessage = "{0} should range from {1} to {2}")]    //upto 10lac consider
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Quantity is required")]
		[Range(1, 1000000, ErrorMessage = "{0} should range from {1} to {2}")]  //upto 1lac consider
		public int Qty { get; set; }

	}
}
