using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPIProject.Dtos;
using RESTfulAPIProject.Models;
using RESTfulAPIProject.Repositories;

namespace RESTfulAPIProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var orders = await _repository.GetAllOrdersAsync();
            var orderDTOs = _mapper.Map<IEnumerable<OrderDTO>>(orders);
            return Ok(orderDTOs);
        }

        [HttpGet]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await _repository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDTO = _mapper.Map<OrderDTO>(order);
            return Ok(orderDTO);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> AddOrder(OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            order = await _repository.AddOrderAsync(order);
            orderDTO = _mapper.Map<OrderDTO>(order);
            return CreatedAtAction(nameof(GetOrder), new { id = orderDTO.Id }, orderDTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Id)
            {
                return BadRequest();
            }
            var order = _mapper.Map<Order>(orderDTO);
            await _repository.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _repository.DeleteOrderAsync(id);
            return NoContent();
        }

    }
}