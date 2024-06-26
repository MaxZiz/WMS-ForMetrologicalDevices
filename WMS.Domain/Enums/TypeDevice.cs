using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WMS.Domain.Enums
{
    public enum TypeDevice
    {
        [Display(Name = "Мультиметр")]
        Device = 1,
        [Display(Name = "Вольтметр")]
        Cables = 2,
        [Display(Name = "Источник питания")]
        Voltage = 3,
        [Display(Name = "Магазин сопротивлений")]
        Other = 4
    }
}
