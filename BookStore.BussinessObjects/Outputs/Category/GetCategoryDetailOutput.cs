﻿using BookStore.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Outputs.Category
{
    public class GetCategoryDetailOutput : BaseOutput
    {
        public CategoryDto Category { get; set; }
    }
}
