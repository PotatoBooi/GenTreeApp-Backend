﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenTreeApp_API.Domain.Contracts;
using GenTreeApp_API.Domain.Data;
using GenTreeApp_API.Domain.Models;

namespace GenTreeApp_API.Domain.Repository
{
    public class PersonRepository : RepositoryBase<TreeDbContext,Person>,IPersonRepository
    {
        public PersonRepository(TreeDbContext entities) : base(entities)
        {
        }
    }
}