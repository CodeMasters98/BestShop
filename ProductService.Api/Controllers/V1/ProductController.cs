using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductService.Api.Business;
using ProductService.Api.Contracts;
using ProductService.Api.Dtos.Product;
using ProductService.Api.Entities;
using ProductService.Api.Shared.Configs;
using System.Net.Mime;

namespace ProductService.Api.Controllers.V1;


public class ProductController : BaseController
{
    private readonly IProductBusiness _productBusiness;
    private readonly IMapper _mapper;
    private readonly CustomSetting _customSetting;
    public ProductController(IProductBusiness productBusiness, IMapper mapper,IOptionsMonitor<CustomSetting> customSetting)
    {
        _customSetting = customSetting.CurrentValue;
        _mapper = mapper;
        _productBusiness = productBusiness;
    }
   

    [HttpGet]
    [Route("")]
    public IActionResult Get()
    {
        var products = _productBusiness.GetProducts();
        return Ok(products);
    }

    [HttpGet]
    [Route("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var product = _productBusiness.GetProductById(id);
        if (product == null)
            return NotFound();

        product.Name = $"{_customSetting.strSetting} {product.Name}";

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
