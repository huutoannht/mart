﻿using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.Interfaces
{
    public interface IKeHoachCTDataService : IRepository<KeHoachCT>
    {
        List<KeHoachCT> GetCurWeekKHCT();
    }
}