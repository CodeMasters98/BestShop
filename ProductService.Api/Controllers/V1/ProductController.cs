using AutoMapper;
using BestShop.ProductService.Application.Contracts;
using BestShop.ProductService.Application.Dtos.Product;
using BestShop.ProductService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductService.Api.Shared.Configs;
using System.Net.Mime;

namespace ProductService.Api.Controllers.V1;


public class ProductController : BaseController
{
    private readonly IProductService _productBusiness;
    private readonly IMapper _mapper;
    private readonly CustomSetting _customSetting;

    public ProductController(IProductService productBusiness, IMapper mapper,IOptionsMonitor<CustomSetting> customSetting)
    {
        _customSetting = customSetting.CurrentValue;
        _mapper = mapper;
        _productBusiness = productBusiness;
    }
   
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get()
    {
        var products = await _productBusiness.GetProducts();
        return Ok(products);
    }

    [HttpGet]
    [Route("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var product = await _productBusiness.GetProductById(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    [Route("")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Add([FromBody] AddProductDto productDto)
    {
        var product = _mapper.Map<AddProductDto, Product>(productDto);
        var isAdded = _productBusiness.AddProduct(product);
        return Ok();
    }

    [HttpPut]
    [Route("")]
    public IActionResult Update([FromBody] UpdateProductDto productDto)
    {
        //Connect To Repo
        return Ok();
    }

    [HttpDelete]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete([FromRoute] int id)
    {

        return Ok();
    }
}
