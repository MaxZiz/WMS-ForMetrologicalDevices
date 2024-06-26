using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Domain.Enums
{
    public enum StatusCode
    {
        OK = 0,
        InternalServerError = 500,
        EmptyListOfObjects = 100,
        ElementNotFound = 1,
        UserNotFound = 0,
        UserAlreadyExists = 1,
        DeviceNotFound = 10,
        ToDoListNotFound = 20,

    }
}
