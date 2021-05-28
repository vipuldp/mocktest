using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using testapi.Common;
using testapi.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService productService;

		public ProductController(IProductService _productService)
		{
			productService = _productService;
		}

		private const string ExceptionMsg = "Error occured. Please contact sysadmin.";

		// GET: api/<ProductController>
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				return Ok(await productService.GetAllData());
			}
			catch (Exception ex)
			{
				ErrorMessage.WriteExceptionLog(ex);
				return BadRequest(ExceptionMsg);
			}
		}

		// POST api/<ProductController>
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Product addProject)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var res = await productService.AddData(addProject);
				if (res.PKId > 0)
					return Ok("Product added.");
				else
					return BadRequest("Product not added.");
			}
			catch (Exception ex)
			{
				ErrorMessage.WriteExceptionLog(ex);
				return BadRequest(ExceptionMsg);
			}
		}

		// PUT api/<ProductController>/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] Product updProject)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				updProject.PKId = id;

				var res = productService.UpdateData(updProject);
				if (string.IsNullOrEmpty(res.Result))
					return Ok("Product updated.");
				else
					return BadRequest(res.Result);
			}
			catch (Exception ex)
			{
				ErrorMessage.WriteExceptionLog(ex);
				return BadRequest(ExceptionMsg);
			}
		}

		// DELETE api/<ProductController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				var res = productService.DeleteData(id);
				if (string.IsNullOrEmpty(res.Result))
					return Ok("Product deleted.");
				else
					return BadRequest(res.Result);
			}
			catch (Exception ex)
			{
				ErrorMessage.WriteExceptionLog(ex);
				return BadRequest(ExceptionMsg);
			}
		}
	}
}
