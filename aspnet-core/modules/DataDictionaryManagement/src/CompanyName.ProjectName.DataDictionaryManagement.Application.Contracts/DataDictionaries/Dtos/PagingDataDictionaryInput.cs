﻿using Lion.Abp.Extension;

namespace CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class PagingDataDictionaryInput : PagingBase
    {
        public string Filter { get; set; }
    }
}