using AutoMapper;
using ECommerceProductAPI.Data;
using ECommerceProductAPI.Model.DTO;
using ECommerceProductAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProductAPI.Controllers
{
    /// <summary>
    /// Controller for managing products.
    /// </summary>
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbcontext;
        private readonly IMapper _mapper;
        private ResponseDTO _response;

        public ProductController(AppDbContext appDbcontext, IMapper mapper)
        {
            _appDbcontext = appDbcontext;
            _mapper = mapper;
             _response = new ResponseDTO();
        }
        /// <summary>
        /// To Get all products.
        /// </summary>
        [HttpGet]
        public async Task<ResponseDTO> GetAll()
        {
            try
            {
                IEnumerable<Product> objList = await _appDbcontext.Products.ToListAsync();
                _response.Result = _mapper.Map<List<ProductDTO>>(objList);
                _response.Message="All Available Products";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }
        /// <summary>
        /// Retrieve products by the name.
        /// </summary>
        /// <param name="code">The name or part of the name of the product.</param>
        [HttpGet]
        [Route("GetByName/{code}")]
        public ResponseDTO GetByName(string code)
        {
            try
            {

            var products = _appDbcontext.Products
            .Where(u => u.ProductName.ToLower().Contains(code.ToLower()))
            .ToList();

                if (products.Count > 0)
                {
                    _response.Result = products.Select(prodObj => _mapper.Map<ProductDTO>(prodObj)).ToList();
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "No products found with the provided name.";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        /// <summary>
        /// Retrieve a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        [HttpGet("{id:int}")]
        public async Task<ResponseDTO> GetById(int id)
        {
            try
            {
                Product obj = await _appDbcontext.Products.FirstOrDefaultAsync(x => x.Id == id);
                _response.Result = _mapper.Map<ProductDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        /// <summary>
        /// To Add a new product.
        /// </summary>
        /// <param name="addProductDto">The DTO containing the product details to be added.</param>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ResponseDTO> AddProduct([FromBody] AddProductDTO addProductDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(addProductDto);
                await _appDbcontext.Products.AddAsync(obj);
                await _appDbcontext.SaveChangesAsync();
                _response.Result = _mapper.Map<ProductDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        /// <summary>
        /// To Update an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to be updated.</param>
        /// <param name="updateProductDto">The DTO containing the updated product details.</param>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<ResponseDTO> Update(int id, [FromBody] ProductUpdateDTO updateProductDto)
        {
            try
            {
                Product product = await _appDbcontext.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = $"The Product does not exist with Id - {id}";
                }
                else
                {
                    product.ProductName = updateProductDto.ProductName;
                    product.ProductDescription = updateProductDto.ProductDescription;
                    product.Price = updateProductDto.Price;
                    product.Specification = updateProductDto?.Specification;
                    await _appDbcontext.SaveChangesAsync();
                    _response.Result = _mapper.Map<ProductDTO>(product);
                }

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }
        /// <summary>
        /// To Delete a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to be deleted.</param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ResponseDTO> Delete(int id)
        {
            try
            {
                Product obj = await _appDbcontext.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (obj == null)
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = $"The Product does not exist with Id - {id}";
                }
                else
                {
                    _appDbcontext.Products.Remove(obj);
                    await _appDbcontext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }


    }
}
