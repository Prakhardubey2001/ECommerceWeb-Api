using AutoMapper;
using ECommerce.CartAPI.Models.DTO;
using ECommerce.CartAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerce.CartAPI.Data;
using ECommerce.CartAPI.Service.Iservice;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ECommerce.CartAPI.Controllers
{
    /// <summary>
    /// Controller for managing the user's shopping cart.
    /// </summary>
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ResponseDTO _responseDto;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private readonly IProductService _productService;

        public CartController(AppDbContext appDbContext, IMapper mapper, IProductService productService)
        {
            _responseDto = new ResponseDTO();
            _mapper = mapper;
            _appDbContext = appDbContext;
            _productService = productService;
        }

        /// <summary>
        /// Add the items to the user cart.
        /// </summary>
        /// <param name="cartDto">The cart DTO containing the items to be added.</param>
        [Authorize]
        [HttpPost("Add-Item-Cart")]
        public async Task<ResponseDTO> AddItemsInCart(CartDTO cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _appDbContext.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    await _appDbContext.AddAsync(cartHeader);
                    await _appDbContext.SaveChangesAsync();
                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    await _appDbContext.AddAsync(_mapper.Map<CartItems>(cartDto.CartDetails.First()));
                    await _appDbContext.SaveChangesAsync();
                }
                else
                {
                    var cartDetailsFromDb = await _appDbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(u =>
                                        u.ProductId == cartDto.CartDetails.First().ProductId &&
                                        u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if (cartDetailsFromDb == null)
                    {
                        
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        await _appDbContext.AddAsync(_mapper.Map<CartItems>(cartDto.CartDetails.First()));
                        await _appDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        if (cartDto.CartDetails.First().Count < 1)
                        {
                            _responseDto.IsSuccess = false;
                            _responseDto.Result = null;
                            _responseDto.Message = "The Count of Items to be added in the Cart cannot be less than 1";
                            return _responseDto;
                        }
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _appDbContext.Update(_mapper.Map<CartItems>(cartDto.CartDetails.First()));
                        await _appDbContext.SaveChangesAsync();
                    }
                }
                _responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }


        /// <summary>
        /// Remove items from the user's cart.
        /// </summary>
        /// <param name="cartDetailsId">The ID of the cart details to be removed.</param>
        [Authorize]
        [HttpPost("RemoveCartItems")]
        public async Task<ResponseDTO> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                CartItems cartDetails = _appDbContext.CartDetails.First(u => u.CartDetailsId == cartDetailsId);

                int totalCountofCartItems = _appDbContext.CartDetails.Where(u => u.CartHeaderId==cartDetails.CartHeaderId).Count();
                _appDbContext.CartDetails.Remove(cartDetails);
                if (totalCountofCartItems ==1)
                {
                    var cartHeaderToRemove = await _appDbContext.CartHeaders.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _appDbContext.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _appDbContext.SaveChangesAsync();
                _responseDto.Result = true;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return _responseDto;

        }


        /// <summary>
        /// Retrieve items in the user's cart.
        /// </summary>
        [Authorize]
        [HttpGet("Get-Cart")]
        public async Task<ResponseDTO> GetCartItems()
        {
            try
            {
                string userId = HttpContext.User.Claims.FirstOrDefault(c =>
                                                    c.Type == ClaimTypes.NameIdentifier)?.Value;
                var cartHeader = await  _appDbContext.CartHeaders.FirstAsync(u => u.UserId == userId);
                CartDTO cartDto = new CartDTO()
                {
                    CartHeader = _mapper.Map<CartHeaderDTO>(cartHeader)
                };
                cartDto.CartDetails = _mapper.Map<IEnumerable<CartItemsDTO>>(_appDbContext.CartDetails.Where(
                                                            u => u.CartHeaderId == cartDto.CartHeader.CartHeaderId));

                IEnumerable<ProductDTO> productDtos = await _productService.GetProducts();
                cartDto.CartHeader.CartTotal = 0;
                foreach (var item in cartDto.CartDetails)
                {
                    ProductDTO product = productDtos.FirstOrDefault(u => u.Id == item.ProductId);
                    cartDto.CartHeader.CartTotal += (item.Count * product.Price);
                }
                cartHeader.CartTotal = cartDto.CartHeader.CartTotal;
                await _appDbContext.SaveChangesAsync();
                _responseDto.Result = cartDto;

            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        /// <summary>
        /// Checkout the items in the user cart.
        /// </summary>
        [Authorize]
        [HttpPost("check-out")]
        public async Task<ResponseDTO> CheckOutCart()
        {
            try
            {
                string userId = HttpContext.User.Claims.FirstOrDefault(c =>
                                                    c.Type == ClaimTypes.NameIdentifier)?.Value;
                var cartHeader = await _appDbContext.CartHeaders.FirstAsync(u => u.UserId == userId);
                CartDTO cartDto = new CartDTO()
                {
                    CartHeader = _mapper.Map<CartHeaderDTO>(cartHeader)
                };
                cartDto.CartDetails = _mapper.Map<IEnumerable<CartItemsDTO>>(_appDbContext.CartDetails.Where(
                                                            u => u.CartHeaderId == cartDto.CartHeader.CartHeaderId));
                var carDetails = _appDbContext.CartDetails.Where(u => u.CartHeaderId == cartHeader.CartHeaderId);
                IEnumerable<ProductDTO> productDtos = await _productService.GetProducts();
                List<ProductDTO> products = new List<ProductDTO>();
                CheckOutDTO checkOutDto = new CheckOutDTO()
                {
                    CartHeaderId = cartHeader.CartHeaderId,
                    UserId = userId,
                    CartTotal = cartHeader.CartTotal
                };
                _appDbContext.CartDetails.RemoveRange(carDetails);
                await _appDbContext.SaveChangesAsync();
                _appDbContext.CartHeaders.Remove(cartHeader);
                await _appDbContext.SaveChangesAsync();
                foreach (var item in cartDto.CartDetails)
                {
                    ProductDTO product = productDtos.FirstOrDefault(u => u.Id == item.ProductId);
                    if (product != null)
                    {
                        products.Add(product);
                    }
                }
                checkOutDto.ProductDtos = products;
                _responseDto.Result = checkOutDto;
                _responseDto.Message = "The cart is checkedout successfully!!";
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }



    }
}





