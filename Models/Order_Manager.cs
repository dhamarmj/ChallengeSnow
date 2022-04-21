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

        //Data Context
        private readonly DataContext _dataContext;

        //AutoMapper - for updating items/orders
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
            if (item.Id == Guid.Empty) item.Id = new Guid(); //If there is no Id create it
            else
            {
                var result = await _dataContext.Items.FindAsync(item.Id); // If there is an Id validate it
                if (result != null) return Result<bool>.Failure("Item exists"); // Format all returns as a Result
            }

            _dataContext.Items.Add(item);

            var save = await _dataContext.SaveChangesAsync() > 0; // Save to Database

            if (!save) return Result<bool>.Failure("Error saving item"); // Return Failures

            return Result<bool>.Success(true); // Return Success
        }

        public async Task<Result<bool>> RemoveItem(Guid id)
        {
            var result = await _dataContext.Items.FindAsync(id);

            if (result == null) return Result<bool>.Failure("Item doesn't exist"); // Item doesnt exist return

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

            if (result == null) return Result<bool>.Failure("Item not found"); // Item Already exists

            _mapper.Map(item, result); // map the request.Item to the Item Found in the DB

            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Failure to update");

            return Result<bool>.Success(true);
        }


        public async Task<Result<IEnumerable<ItemBase>>> GetAllItems()
        {
            return Result<IEnumerable<ItemBase>>.Success(await _dataContext.AllItems.ToListAsync());
        }


        #endregion

        // DEAL METHODS
        #region deal_items
        public async Task<Result<bool>> AddDeal(Deal_Item item)
        {
            //Validations for the date
            if (item.Start_Date > item.End_Date) return Result<bool>.Failure("Start and End Dates don't match");

            if (item.Id == Guid.Empty) item.Id = new Guid();
            else
            {
                var result = await _dataContext.Orders.FindAsync(item.Id);

                if (result != null) return Result<bool>.Failure("Order already exists");
            }

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

            if (result == null) return Result<bool>.Failure("Deal doesn't exist"); // Item Already exists

            _mapper.Map(item, result);

            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Failure to update");

            return Result<bool>.Success(true);
        }
        public async Task<Result<bool>> RemoveDeal(Guid id)
        {
            var result = await _dataContext.Deal_Items.FindAsync(id);

            if (result == null) return Result<bool>.Failure("Deal doesn't exist");

            _dataContext.Remove(result);

            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Failed to delete");

            return Result<bool>.Success(true);
        }

        #endregion

        #region orders
        public async Task<Result<bool>> AddOrder(Order order)
        {
            if (order.Id == Guid.Empty) order.Id = new Guid(); // Id does not exist
            else
            {
                var result = await _dataContext.Orders.FindAsync(order.Id); // if provided validate

                if (result != null) return Result<bool>.Failure("Order already exists");
            }

            var item = await _dataContext.AllItems.FindAsync(order.Item_NumberId); // get the item referenced

            if (item == null) return Result<bool>.Failure("Item doesn't exist"); // validate the item
            else
            {
                var newItem = item;
                //validate quantity
                if (item.Available_Quantity < order.Quantity) return Result<bool>.Failure("Quantity exceeds existence");
                else
                {
                    newItem.Available_Quantity -= order.Quantity; // Reduce Quantity
                    order.Item_Number = newItem;
                    order.Item_NumberId = newItem.Id; //Update reference in Order

                    _mapper.Map(item, newItem); // Map the item to update quantity
                }
            }

            order.Date_Created = DateTime.Now; // Set the current date

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

        public async Task<Result<bool>> RemoveOrder(Guid id)
        {
            var result = await _dataContext.Orders.FindAsync(id);

            if (result == null) return Result<bool>.Failure("Order doesn't exist"); // Item Already exists 

            var item = await _dataContext.AllItems.FindAsync(result.Item_NumberId); // Get the item reference

            var newItem = item;
            newItem.Available_Quantity += result.Quantity; // update quantity

            _mapper.Map(newItem, item); // map items

            _dataContext.Remove(result);

            var save = await _dataContext.SaveChangesAsync() > 0;

            if (!save) return Result<bool>.Failure("Failed to delete");

            return Result<bool>.Success(true);
        }

        public async Task<Result<Order>> GetOrders(Guid id)
        {
            var result = await _dataContext.Orders.FindAsync(id);

            if (result == null) return Result<Order>.Failure("Order not found");

            var item = await _dataContext.AllItems.FindAsync(result.Item_NumberId);
            result.Item_Number = item; // refresh reference of item

            return Result<Order>.Success(result);
        }

        public async Task<Result<IEnumerable<Order>>> GetOrders()
        {
            var list = await _dataContext.Orders.Include(x => x.Item_Number).ToListAsync();
            return Result<IEnumerable<Order>>.Success(list);
        }


        #endregion

        //PRINTS!!
        public async Task<string> PrintItems()
        {
            var items = await _dataContext.AllItems.ToListAsync();
            string print = "";
            items.ForEach(x =>
            {
                print += String.Format("{0}-{1}-{2}-{3} \n", x.Id, x.Description, x.Available_Quantity, x.Price);
                //Console.WriteLine(String.Format("{0}-{1}-{2}", x.Id, x.Available_Quantity, x.Price));
            });

            return print;
        }

        public async Task<string> PrintOrders()
        {
            var orders = await _dataContext.Orders.ToListAsync();
            string print = "";
            orders.ForEach(x =>
             {
                 print += String.Format("{0}-{1}-{2}-{3} \n", x.Id, x.Date_Created.ToString("MMMM dd yyyy"), x.Item_Number, x.Quantity);
                 //Console.WriteLine(String.Format("{0}-{1}-{2}-{3} \n", x.Id, x.Date_Created.ToString("MMMM dd yyyy"), x.Item_Number, x.Quantity));
             });

            return print;
        }
    }
}
