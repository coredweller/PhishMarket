using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace WantedListFulfillmentService
{
    class Program
    {
        static void Main(string[] args)
        {

            var wantedListService = new WantedListService(Ioc.GetInstance<IWantedListRepository>());

            var wantedLists = wantedListService.GetAllActiveWantedLists();

            
        }
    }
}
