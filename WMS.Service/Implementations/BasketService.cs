using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Domain.ViewModels;
using WMS.Domain.ViewModels.Basket;
using WMS.Service.Interfaces;

namespace WMS.Service.Implementations
{
    public class BasketService: IBasketService
    {
        IBaseRepository<Basket> _basketRepository;
        IBaseRepository<Device> _deviceRepository;
        IBaseRepository<User> _userRepository;
        public BasketService(IBaseRepository<Basket> basketRepository, IBaseRepository<Device> deviceRepository, IBaseRepository<User> userRepository) 
        { 
            _basketRepository = basketRepository;
            _deviceRepository = deviceRepository;
            _userRepository = userRepository;
        }

        public bool ClearBasket(string name)
        {
            var user = _userRepository.GetAll().Include(c => c.Basket).FirstOrDefault(c => c.Name == name);
            var basket = _basketRepository.GetAll().Include(f => f.Devices).FirstOrDefault(b => b.Id == user.Id);
            basket.Devices.Clear();
            _basketRepository.Update(basket);
            return true;
        }

        public Basket AddToBasket(string name, long id)
        {
            var user = _userRepository.GetAll().Include(c => c.Basket).FirstOrDefault(c => c.Name == name);
            var basket = _basketRepository.GetAll().Include(f => f.Devices).FirstOrDefault(b => b.Id == user.Id);
            var device = _deviceRepository.GetAll().FirstOrDefault(c=>c.Id==id);
            basket.Devices.Add(device);
            _basketRepository.Update(basket); 
            return basket;
        }

        public Basket GetBasket(string name)
        {
            var user = _userRepository.GetAll().Include(c=> c.Basket).FirstOrDefault(c=>c.Name == name);
            var basket = _basketRepository.GetAll().Include(f=>f.Devices).FirstOrDefault(b => b.Id == user.Id);
            return basket;
        }

        public Basket DeleteFromBasket(string name, long id)
        {
            var user = _userRepository.GetAll().Include(c => c.Basket).FirstOrDefault(c => c.Name == name);
            var basket = _basketRepository.GetAll().Include(f => f.Devices).FirstOrDefault(b => b.Id == user.Id);
            var device = basket.Devices.FirstOrDefault(b=>b.Id==id);
            basket.Devices.Remove(device);
            _basketRepository.Update(basket);
            return basket;
        }

        public void MoveDevices(string name, int placeId)
        {
            var user = _userRepository.GetAll().Include(c => c.Basket).FirstOrDefault(c => c.Name == name);
            var basket = _basketRepository.GetAll().Include(f => f.Devices).FirstOrDefault(b => b.Id == user.Id);
            var devices = basket.Devices;            
            foreach ( var device in devices)
            {
                device.Histories.Add(new History
                {
                    DateTime = DateTime.Now,
                    FromPlace = device.PlaceId,
                    ToPlace = placeId,
                    Device = device,
                    _Guid = Guid.NewGuid(),
                    UserId = name
                });   
                
                device.PlaceId = placeId;
            }

            _basketRepository.Update(basket);
        }
    }
}
