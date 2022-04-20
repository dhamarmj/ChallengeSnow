using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChallengeSnow.Models.Core;
using ChallengeSnow.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChallengeSnow.Models
{
    public class Order_Manager : Interfaces.IOrderManager
    {
        private readonly DataContext _dataContext;
        private readonly MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Item, Item>();
            cfg.CreateMap<Deal_Item, Deal_Item>();
            cfg.CreateMap<Order, Order>();
        });
        Mapper _mapper;
        public Order_Manager(DataContext dc)
        {
            _dataContext = dc;
            _mapper = new Mapper(mapperConfig);
        }

        //ITEM METHODS
        #region  Items
        public async Task<Result<bool>> AddItem(Item item)
        {
            if (item.Id == Guid.Empty) item.Id = new Guid();
            else
            {
                var result = await _dataContext.Items.FindAsync(item.Id);
                if (result != null) return Result<bool>.Failure("Item exists"); // "Item Already exists";
            }

            _dataContext.Items.Add(item);
            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Error saving item");

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> RemoveItem(Guid id)
        {
            var result = await _dataContext.Items.FindAsync(id);

            if (result == null) return Result<bool>.Failure("Item doesn't exist"); // "Item doesnt exist!";

            //CALL THE ORDER REMOVE!

            _dataContext.Remove(result);

            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Failed to delete");

            return Result<bool>.Success(true);
        }

        public async Task<Result<Item>> GetItem(Guid id)
        {
            var activity = await _dataContext.Items.FindAsync(id);

            if (activity == null) return Result<Item>.Failure("Item not found");

            return Result<Item>.Success(activity);
        }
        public async Task<Result<IEnumerable<Item>>> GetItems()
        {
            return Result<IEnumerable<Item>>.Success(await _dataContext.Items.ToListAsync());
        }


        public async Task<Result<bool>> UpdateItem(Item item)
        {
            var result = await _dataContext.Items.FindAsync(item.Id);

            if (result == null) return Result<bool>.Failure("Item not found"); // "Item Already exists";

            _mapper.Map(item, result);

            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Failure to update");

            return Result<bool>.Success(true);
        }

        #endregion

        // DEAL METHODS
        #region deal_items
        public async Task<Result<bool>> AddDeal(Deal_Item item)
        {
            if (item.Start_Date > item.End_Date) return Result<bool>.Failure("Start and End Dates don't match");

            var result = _dataContext.Deal_Items.FindAsync(item.Id);

            if (result != null) return Result<bool>.Failure("Deal already exists"); // "Item Already exists";

            _dataContext.Deal_Items.Add(item);
            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Error saving Deal item");

            return Result<bool>.Success(true);
        }

        public async Task<Result<Deal_Item>> GetDeal_Items(Guid id)
        {
            var result = await _dataContext.Deal_Items.FindAsync(id);

            if (result == null) return Result<Deal_Item>.Failure("Deal not found");

            return Result<Deal_Item>.Success(result);
        }

        public async Task<Result<IEnumerable<Deal_Item>>> GetDeal_Items()
        {
            return Result<IEnumerable<Deal_Item>>.Success(await _dataContext.Deal_Items.ToListAsync());
        }
        public async Task<Result<bool>> UpdateDeal(Deal_Item item)
        {
            if (item.Start_Date > item.End_Date) return Result<bool>.Failure("Start and End Dates don't match");

            var result = await _dataContext.Deal_Items.FindAsync(item.Id);

            if (result == null) return Result<bool>.Failure("Deal doesn't exist"); // "Item Already exists";

            _mapper.Map(item, result);

            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Failure to update");

            return Result<bool>.Success(true);
        }
        public async Task<Result<bool>> RemoveDeal(Guid id)
        {
            var result = _dataContext.Deal_Items.FindAsync(id);

            if (result == null) return Result<bool>.Failure("Deal doesn't exist"); // "Item Already exists";

            //REMOVE ORDERS
            _dataContext.Remove(result);

            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Failed to delete");

            return Result<bool>.Success(true);
        }

        #endregion

        #region orders
        public async Task<Result<bool>> AddOrder(Order order)
        {
            if (order.Id == Guid.Empty) order.Id = new Guid();
            else
            {
                var result = await _dataContext.Orders.FindAsync(order.Id);

                if (result != null) return Result<bool>.Failure("Order already exists");
            }

            //var dealItem = await GetDeal_Items(order.Item_Number.Id);
            var item = await _dataContext.Items.FindAsync(order.Item_Number.Id);

            if (item == null) return Result<bool>.Failure("Item doesn't exist");
            // else if (dealItem.IsSuccess)
            // {
            //     order.Item_Number = dealItem.Value;
            //     dealItem.Value.Available_Quantity -= order.Quantity;

            //     await UpdateDeal(dealItem.Value);
            // }
            else
            {
                var newItem = item;
                newItem.Available_Quantity -= order.Quantity;
                order.Item_Number = newItem;
                order.Item_NumberId = newItem.Id;

                _mapper.Map(item, newItem);
            }

            order.Date_Created = DateTime.Now;

            _dataContext.Orders.Add(order);

            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Error saving Deal item");

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateOrder(Order order)
        {
            var result = await _dataContext.Orders.FindAsync(order.Id);

            if (result == null) return Result<bool>.Failure("Order doesn't exist");

            _mapper.Map(order, result);

            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Failure to update");

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> RemoveOrder(Guid id) // remember to update the quantity of objects!
        {
            var result = _dataContext.Orders.FindAsync(id);

            if (result == null) return Result<bool>.Failure("Order doesn't exist"); // "Item Already exists";

            _dataContext.Remove(result);

            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Failed to delete");

            return Result<bool>.Success(true);
        }

        private void RemoveOrders(Guid itemId)
        {

        }
        public async Task<Result<Order>> GetOrders(Guid id)
        {
            var result = await _dataContext.Orders.FindAsync(id);

            if (result == null) return Result<Order>.Failure("Order not found");

            return Result<Order>.Success(result);
        }

        public async Task<Result<IEnumerable<Order>>> GetOrders()
        {
            return Result<IEnumerable<Order>>.Success(await _dataContext.Orders.ToListAsync());
        }


        #endregion
        public string PrintItems()
        {
            string print = "";
            _dataContext.Items.ToList().ForEach(x =>
            {
                print += String.Format("{0}-{1}-{2} \n", x.Id, x.Available_Quantity, x.Price);
                Console.WriteLine(String.Format("{0}-{1}-{2}", x.Id, x.Available_Quantity, x.Price));
            });

            return print;
        }

        public string PrintOrders()
        {
            string print = "";
            _dataContext.Orders.ToList().ForEach(x =>
             {
                 print += String.Format("{0}-{1}-{2}-{3} \n", x.Id, x.Date_Created.ToString("MMMM dd yyyy"), x.Item_Number, x.Quantity);
                 Console.WriteLine(String.Format("{0}-{1}-{2}-{3} \n", x.Id, x.Date_Created.ToString("MMMM dd yyyy"), x.Item_Number, x.Quantity));
             });

            return print;
        }
    }
}
