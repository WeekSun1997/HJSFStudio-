﻿using SqlSugar;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    [SugarTable("HJSF_SysUserRoleMapping")]
    public  class HjsfSysUserRoleMapping 
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//如果是主键，此处必须指定，否则会引发InSingle(id)方法异常。
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
