using Microsoft.EntityFrameworkCore.ChangeTracking;
using Order.ApplicationCore.Contracts.IRepositories;
using Order.ApplicationCore.Contracts.IServices;
using Order.ApplicationCore.Entities;
using Order.ApplicationCore.Models.Request;
using Order.ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public class OrderServiceAsync : IOrderServiceAsync
    {
        private readonly IOrderRepositoryAsync orderRepository;
        public OrderServiceAsync(IOrderRepositoryAsync orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<int> DeleteOrder(int id)
        {
            return await orderRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrderResponseModel>> GetAllOrders()
        {
            var orders = await orderRepository.GetAllAsync();
            return orders.Select(order => new OrderResponseModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                CustomerName = order.CustomerName,
                PaymentMethodId = order.PaymentMethodId,
                PaymentName = order.PaymentName,
                ShippingAddress = order.ShippingAddress,
                ShippingMethod = order.ShippingMethod,
                BillAmount = order.BillAmount,
                OrderStatus = order.OrderStatus,
                OrderDetails = order.OrderDetails.Select(detail => new OrderDetailResponseModel
                {
                    ProductId = detail.ProductId,
                    ProductName = detail.ProductName,
                    Qty = detail.Qty,
                    Price = detail.Price,
                    Discount = detail.Discount
                }).ToList()
            });
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByCustomerId(int customerId)
        {
            var orders = await orderRepository.GetOrdersByCustomerIdAsync(customerId);
            return orders.Select(order => new OrderResponseModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                CustomerName = order.CustomerName,
                PaymentMethodId = order.PaymentMethodId,
                PaymentName = order.PaymentName,
                ShippingAddress = order.ShippingAddress,
                ShippingMethod = order.ShippingMethod,
                BillAmount = order.BillAmount,
                OrderStatus = order.OrderStatus,
                OrderDetails = order.OrderDetails.Select(detail => new OrderDetailResponseModel
                {
                    ProductId = detail.ProductId,
                    ProductName = detail.ProductName,
                    Qty = detail.Qty,
                    Price = detail.Price,
                    Discount = detail.Discount
                }).ToList()
            });
        }

        public async Task<OrderResponseModel?> GetOrderById(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                return new OrderResponseModel
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    CustomerId = order.CustomerId,
                    CustomerName = order.CustomerName,
                    PaymentMethodId = order.PaymentMethodId,
                    PaymentName = order.PaymentName,
                    ShippingAddress = order.ShippingAddress,
                    ShippingMethod = order.ShippingMethod,
                    BillAmount = order.BillAmount,
                    OrderStatus = order.OrderStatus,
                    OrderDetails = order.OrderDetails.Select(detail => new OrderDetailResponseModel
                    {
                        ProductId = detail.ProductId,
                        ProductName = detail.ProductName,
                        Qty = detail.Qty,
                        Price = detail.Price,
                        Discount = detail.Discount
                    }).ToList()
                };
            }
            return null;
        }

        public async Task<int> InsertOrder(OrderRequestModel model)
        {
            var order = new Order.ApplicationCore.Entities.Order
            {
                OrderDate = model.OrderDate,
                CustomerId = model.CustomerId,
                CustomerName = model.CustomerName,
                PaymentMethodId = model.PaymentMethodId,
                PaymentName = model.PaymentName,
                ShippingAddress = model.ShippingAddress,
                ShippingMethod = model.ShippingMethod,
                BillAmount = model.BillAmount,
                OrderStatus = model.OrderStatus
            };
            return await orderRepository.InsertAsync(order);
        }

        public async Task<int> UpdateOrder(OrderRequestModel model)
        {
            var order = new Order.ApplicationCore.Entities.Order
            {
                Id = model.Id,
                OrderDate = model.OrderDate,
                CustomerId = model.CustomerId,
                CustomerName = model.CustomerName,
                PaymentMethodId = model.PaymentMethodId,
                PaymentName = model.PaymentName,
                ShippingAddress = model.ShippingAddress,
                ShippingMethod = model.ShippingMethod,
                BillAmount = model.BillAmount,
                OrderStatus = model.OrderStatus
            };
            return await orderRepository.UpdateAsync(order);
        }
    }
}
