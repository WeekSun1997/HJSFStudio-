using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Attributes;

namespace HJSF.RepositoryServices.Models
{
    public class BaseEntity : IRepositoryEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        [IgnoreUpdate]
        public DateTime? UpdateDate { get; set; }
        [IgnoreUpdate]
        public long? UpdateUserId { get; set; }
        [IgnoreUpdate]
        public string UpdateUserName { get; set; }
    }
}
