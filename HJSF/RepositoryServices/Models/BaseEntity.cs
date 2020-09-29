using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace HJSF.RepositoryServices.Models
{
    public class BaseEntity:IRepositoryEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
    }
}
