using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchShop.Data.Models;

namespace WatchShop.Data.ViewModels
{
    public class SelectedThuongHieu
    {
        public ThuongHieu ThuongHieu { get; set; }
        public bool IsSelected { get; set; }
    }
}