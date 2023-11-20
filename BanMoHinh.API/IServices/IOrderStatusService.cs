﻿using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IOrderStatusService
    {
        public Task<bool> Create(OrderStatusVM item);

        public Task<bool> Delete(Guid id);

        public Task<List<OrderStatus>> GetAll();

        public Task<OrderStatus> GetItem(Guid id);

        public Task<bool> Update(Guid id, OrderStatusVM item);
    }
}
