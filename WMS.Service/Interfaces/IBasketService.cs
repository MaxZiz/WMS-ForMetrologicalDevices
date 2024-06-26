using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Domain.ViewModels;
using WMS.Domain.ViewModels.Basket;

namespace WMS.Service.Interfaces
{
    public interface IBasketService
    {
        public bool ClearBasket(string user);
        public Basket AddToBasket(string name, long deviceId);
        public Basket GetBasket(string name);
        public Basket DeleteFromBasket(string name, long deviceId);
        public void MoveDevices(string name, int placeId);
    }
}
