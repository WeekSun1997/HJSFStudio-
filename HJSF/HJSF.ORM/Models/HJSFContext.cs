using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HJSF.ORM.Models
{
    public partial class HJSFContext : DbContext
    {
        public HJSFContext()
        {
        }

        public HJSFContext(DbContextOptions<HJSFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HjsfFlowCode> HjsfFlowCode { get; set; }
        public virtual DbSet<HjsfFlowEditField> HjsfFlowEditField { get; set; }
        public virtual DbSet<HjsfFlowFeedBack> HjsfFlowFeedBack { get; set; }
        public virtual DbSet<HjsfFlowFeedbackMsg> HjsfFlowFeedbackMsg { get; set; }
        public virtual DbSet<HjsfFlowHistory> HjsfFlowHistory { get; set; }
        public virtual DbSet<HjsfFlowInstance> HjsfFlowInstance { get; set; }
        public virtual DbSet<HjsfFlowLine> HjsfFlowLine { get; set; }
        public virtual DbSet<HjsfFlowLineTemp> HjsfFlowLineTemp { get; set; }
        public virtual DbSet<HjsfFlowNode> HjsfFlowNode { get; set; }
        public virtual DbSet<HjsfFlowNodeTemp> HjsfFlowNodeTemp { get; set; }
        public virtual DbSet<HjsfFlowRecord> HjsfFlowRecord { get; set; }
        public virtual DbSet<HjsfFlowScheme> HjsfFlowScheme { get; set; }
        public virtual DbSet<HjsfFlowSchemeCategory> HjsfFlowSchemeCategory { get; set; }
        public virtual DbSet<HjsfOrg> HjsfOrg { get; set; }
        public virtual DbSet<HjsfSysChannel> HjsfSysChannel { get; set; }
        public virtual DbSet<HjsfSysMessage> HjsfSysMessage { get; set; }
        public virtual DbSet<HjsfSysRecord> HjsfSysRecord { get; set; }
        public virtual DbSet<HjsfSysRole> HjsfSysRole { get; set; }
        public virtual DbSet<HjsfSysRoleChannelMapping> HjsfSysRoleChannelMapping { get; set; }
        public virtual DbSet<HjsfSysUser> HjsfSysUser { get; set; }
        public virtual DbSet<HjsfSysUserInfo> HjsfSysUserInfo { get; set; }
        public virtual DbSet<HjsfSysUserOrgMapping> HjsfSysUserOrgMapping { get; set; }
        public virtual DbSet<HjsfSysUserPositionMapping> HjsfSysUserPositionMapping { get; set; }
        public virtual DbSet<HjsfSysUserRoleMapping> HjsfSysUserRoleMapping { get; set; }
        public virtual DbSet<ReportContractView> ReportContractView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HJSF;User ID=sa;Password=123.com;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HjsfFlowCode>(entity =>
            {
                entity.ToTable("HJSF_FlowCode");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");
            });

            modelBuilder.Entity<HjsfFlowEditField>(entity =>
            {
                entity.ToTable("HJSF_FlowEditField");

                entity.Property(e => e.Content).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HjsfFlowFeedBack>(entity =>
            {
                entity.ToTable("HJSF_FlowFeedBack");

                entity.HasComment("流程节点回退记录");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasComment("审批时间");

                entity.Property(e => e.ActionType).HasComment("执行动作：0提交，1回退，2终止");

                entity.Property(e => e.BackType).HasComment("退回类型");

                entity.Property(e => e.CallBack)
                    .HasMaxLength(200)
                    .HasComment("回调链接");

                entity.Property(e => e.Client)
                    .HasMaxLength(500)
                    .HasComment("客户端");

                entity.Property(e => e.FlowCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("流程编码");

                entity.Property(e => e.FlowId).HasComment("流程编号");

                entity.Property(e => e.FlowNodeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("节点编码");

                entity.Property(e => e.From)
                    .HasMaxLength(50)
                    .HasComment("上级节点");

                entity.Property(e => e.Ip)
                    .HasMaxLength(20)
                    .HasComment("IP地址");

                entity.Property(e => e.Message)
                    .HasMaxLength(200)
                    .HasComment("审批意见");

                entity.Property(e => e.NodeId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("节点标识");

                entity.Property(e => e.NodeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("节点名称");

                entity.Property(e => e.NodeRemark)
                    .HasMaxLength(200)
                    .HasComment("备注");

                entity.Property(e => e.NodeStatus).HasComment("节点状态：0待审批，1审批中，2已退回，3已拒绝，4已审批，5已取消");

                entity.Property(e => e.NodeType).HasComment("节点类别：1开始节点，2过程节点，3结束节点");

                entity.Property(e => e.OrderNumber).HasComment("执行顺序");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasComment("提交时间");

                entity.Property(e => e.To)
                    .HasMaxLength(50)
                    .HasComment("下级节点");

                entity.Property(e => e.UserCode)
                    .HasMaxLength(50)
                    .HasComment("审批者编号");

                entity.Property(e => e.UserId).HasComment("审批者Id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasComment("审批者姓名");

                entity.Property(e => e.UserType).HasComment("审批者类型");
            });

            modelBuilder.Entity<HjsfFlowFeedbackMsg>(entity =>
            {
                entity.ToTable("HJSF_FlowFeedbackMsg");

                entity.HasIndex(e => e.CreateDate)
                    .HasName("Index_CreateDate");

                entity.HasIndex(e => e.Status)
                    .HasName("Index_Status");

                entity.HasIndex(e => new { e.CreateDate, e.Status })
                    .HasName("Index_CreateDate_Status");

                entity.HasIndex(e => new { e.Status, e.CreateDate })
                    .HasName("Index_Status_CreateDate");

                entity.Property(e => e.Client).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ip).HasMaxLength(20);

                entity.Property(e => e.Remark).IsRequired();

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUserName).HasMaxLength(50);
            });

            modelBuilder.Entity<HjsfFlowHistory>(entity =>
            {
                entity.ToTable("HJSF_FlowHistory");

                entity.HasIndex(e => e.CreateDate)
                    .HasName("Index_CreateDate");

                entity.HasIndex(e => e.Status)
                    .HasName("Index_Status");

                entity.HasIndex(e => new { e.CreateDate, e.Status })
                    .HasName("Index_CreateDate_Status");

                entity.HasIndex(e => new { e.Status, e.CreateDate })
                    .HasName("Index_Status_CreateDate");

                entity.Property(e => e.ActionDate).HasColumnType("datetime");

                entity.Property(e => e.Client).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FlowCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FlowNodeCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ip).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUserName).HasMaxLength(50);
            });

            modelBuilder.Entity<HjsfFlowInstance>(entity =>
            {
                entity.ToTable("HJSF_FlowInstance");

                entity.HasIndex(e => e.CreateDate)
                    .HasName("Index_CreateDate");

                entity.HasIndex(e => e.Status)
                    .HasName("Index_Status");

                entity.HasIndex(e => new { e.CreateDate, e.Status })
                    .HasName("Index_CreateDate_Status");

                entity.HasIndex(e => new { e.Status, e.CreateDate })
                    .HasName("Index_Status_CreateDate");

                entity.Property(e => e.ApprovalUserCode)
                    .HasMaxLength(50)
                    .HasComment("审核人编码");

                entity.Property(e => e.ApprovalUserName)
                    .HasMaxLength(50)
                    .HasComment("审核人姓名");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FlowCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.FlowSchemeContent)
                    .IsRequired()
                    .HasComment("流程模板内容（流程设计JSON）");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUserName).HasMaxLength(50);
            });

            modelBuilder.Entity<HjsfFlowLine>(entity =>
            {
                entity.ToTable("HJSF_FlowLine");

                entity.HasComment("流程连接线");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.FlowCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("流程编码");

                entity.Property(e => e.FlowId).HasComment("流程编号");

                entity.Property(e => e.FromNodeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("上级节点编码");

                entity.Property(e => e.FromNodeId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("上级节点标识");

                entity.Property(e => e.LineId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("节点标识");

                entity.Property(e => e.ToNodeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("下级节点编码");

                entity.Property(e => e.ToNodeId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("下级节点标识");
            });

            modelBuilder.Entity<HjsfFlowLineTemp>(entity =>
            {
                entity.ToTable("HJSF_FlowLineTemp");

                entity.HasComment("流程连接线模板");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.FlowSchemeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("流程模板编码");

                entity.Property(e => e.FlowSchemeId).HasComment("流程模板编号");

                entity.Property(e => e.FromNodeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("上级节点编码");

                entity.Property(e => e.FromNodeId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("上级节点标识");

                entity.Property(e => e.LineCondition).HasComment("连接线条件");

                entity.Property(e => e.LineId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("节点标识");

                entity.Property(e => e.ToNodeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("下级节点编码");

                entity.Property(e => e.ToNodeId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("下级节点标识");
            });

            modelBuilder.Entity<HjsfFlowNode>(entity =>
            {
                entity.ToTable("HJSF_FlowNode");

                entity.HasComment("流程节点");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasComment("审批时间");

                entity.Property(e => e.ActionType).HasComment("执行动作：0提交，1回退，2终止");

                entity.Property(e => e.BackType).HasComment("退回类型");

                entity.Property(e => e.CallBack)
                    .HasMaxLength(200)
                    .HasComment("回调链接");

                entity.Property(e => e.Client)
                    .HasMaxLength(500)
                    .HasComment("客户端");

                entity.Property(e => e.FlowCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("流程编码");

                entity.Property(e => e.FlowId).HasComment("流程编号");

                entity.Property(e => e.FlowNodeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("节点编码");

                entity.Property(e => e.Ip)
                    .HasMaxLength(20)
                    .HasComment("IP地址");

                entity.Property(e => e.Message)
                    .HasMaxLength(200)
                    .HasComment("审批意见");

                entity.Property(e => e.NodeId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("节点标识");

                entity.Property(e => e.NodeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("节点名称");

                entity.Property(e => e.NodeRemark)
                    .HasMaxLength(200)
                    .HasComment("备注");

                entity.Property(e => e.NodeStatus).HasComment("节点状态：0待审批，1审批中，2已退回，3已拒绝，4已审批，5已取消");

                entity.Property(e => e.NodeType).HasComment("节点类别：1开始节点，2过程节点，3结束节点");

                entity.Property(e => e.OrderNumber).HasComment("执行顺序");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasComment("提交时间");

                entity.Property(e => e.UserCode)
                    .HasMaxLength(50)
                    .HasComment("审批者编号");

                entity.Property(e => e.UserId).HasComment("审批者Id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasComment("审批者姓名");

                entity.Property(e => e.UserType).HasComment("审批者类型");
            });

            modelBuilder.Entity<HjsfFlowNodeTemp>(entity =>
            {
                entity.ToTable("HJSF_FlowNodeTemp");

                entity.HasComment("流程节点模板");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.BackType).HasComment("退回类型");

                entity.Property(e => e.CallBack)
                    .HasMaxLength(200)
                    .HasComment("回调链接");

                entity.Property(e => e.FlowNodeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("节点编码");

                entity.Property(e => e.FlowSchemeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("流程模板编码");

                entity.Property(e => e.FlowSchemeId).HasComment("流程模板编号");

                entity.Property(e => e.NodeId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("节点标识");

                entity.Property(e => e.NodeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("节点名称");

                entity.Property(e => e.NodeRemark)
                    .HasMaxLength(200)
                    .HasComment("备注");

                entity.Property(e => e.NodeType).HasComment("节点类别：1开始节点，2过程节点，3结束节点");

                entity.Property(e => e.UserCode)
                    .HasMaxLength(50)
                    .HasComment("审批者编号");

                entity.Property(e => e.UserId).HasComment("审批者Id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasComment("审批者姓名");

                entity.Property(e => e.UserType).HasComment("审批者类型");
            });

            modelBuilder.Entity<HjsfFlowRecord>(entity =>
            {
                entity.ToTable("HJSF_FlowRecord");

                entity.HasComment("流程操作记录");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.Client)
                    .HasMaxLength(500)
                    .HasComment("客户端");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人编号");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("创建人名称");

                entity.Property(e => e.FlowId).HasComment("流程编号");

                entity.Property(e => e.Ip)
                    .HasMaxLength(20)
                    .HasComment("IP地址");

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasComment("备注");

                entity.Property(e => e.Status).HasComment("状态：0禁用、1启用、2回收");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.UpdateUserId).HasComment("修改人编号");

                entity.Property(e => e.UpdateUserName)
                    .HasMaxLength(50)
                    .HasComment("修改人名称");
            });

            modelBuilder.Entity<HjsfFlowScheme>(entity =>
            {
                entity.ToTable("HJSF_FlowScheme");

                entity.HasIndex(e => e.CreateDate)
                    .HasName("Index_CreateDate");

                entity.HasIndex(e => e.FormId)
                    .HasName("Index_FormId");

                entity.HasIndex(e => e.Sort)
                    .HasName("Index_Sort");

                entity.HasIndex(e => e.Status)
                    .HasName("Index_Status");

                entity.HasIndex(e => new { e.CreateDate, e.Status })
                    .HasName("Index_CreateDate_Status");

                entity.HasIndex(e => new { e.Status, e.CreateDate })
                    .HasName("Index_Status_CreateDate");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModelType)
                    .HasMaxLength(50)
                    .HasComment("旧流程类型（关联旧数据）");

                entity.Property(e => e.SchemeCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SchemeContent).IsRequired();

                entity.Property(e => e.SchemeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUserName).HasMaxLength(50);
            });

            modelBuilder.Entity<HjsfFlowSchemeCategory>(entity =>
            {
                entity.ToTable("HJSF_FlowSchemeCategory");

                entity.HasIndex(e => e.CreateDate)
                    .HasName("Index_CreateDate");

                entity.HasIndex(e => e.Sort)
                    .HasName("Index_Sort");

                entity.HasIndex(e => e.Status)
                    .HasName("Index_Status");

                entity.HasIndex(e => new { e.CreateDate, e.Status })
                    .HasName("Index_CreateDate_Status");

                entity.HasIndex(e => new { e.Status, e.CreateDate })
                    .HasName("Index_Status_CreateDate");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUserName).HasMaxLength(50);
            });

            modelBuilder.Entity<HjsfOrg>(entity =>
            {
                entity.ToTable("HJSF_Org");

                entity.HasComment("职能机构");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.CascadeCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("级联代码");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人编号");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("创建人名称");

                entity.Property(e => e.Ocode)
                    .HasColumnName("OCode")
                    .HasMaxLength(50)
                    .HasComment("旧编号（对应老系统数据编号）");

                entity.Property(e => e.OrgTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("组织名称");

                entity.Property(e => e.ParentId).HasComment("上级机构");

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("拼音简码");

                entity.Property(e => e.Sort).HasComment("排序");

                entity.Property(e => e.Status).HasComment("状态：0禁用、1启用、2回收");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.UpdateUserId).HasComment("修改人编号");

                entity.Property(e => e.UpdateUserName)
                    .HasMaxLength(50)
                    .HasComment("修改人名称");
            });

            modelBuilder.Entity<HjsfSysChannel>(entity =>
            {
                entity.ToTable("HJSF_SysChannel");

                entity.HasComment("系统栏目");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.ChannelLink)
                    .HasMaxLength(200)
                    .HasComment("栏目链接");

                entity.Property(e => e.ChannelName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("栏目名称");

                entity.Property(e => e.ChannelType).HasComment("栏目类型：0菜单、1按钮");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .HasComment("栏目样式");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人编号");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("创建人名称");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("事件名称（类型为按钮作为点击事件触发名称，类型为菜单作为菜单标识）");

                entity.Property(e => e.IconName)
                    .HasMaxLength(50)
                    .HasComment("栏目图标");

                entity.Property(e => e.IsShow).HasComment("是否显示");

                entity.Property(e => e.LevelNumber).HasComment("层级");

                entity.Property(e => e.LevelPath)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("栏目路径");

                entity.Property(e => e.ParentId).HasComment("上级栏目");

                entity.Property(e => e.Sort).HasComment("排列顺序");

                entity.Property(e => e.Status).HasComment("状态：0禁用、1启用、2回收");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.UpdateUserId).HasComment("修改人编号");

                entity.Property(e => e.UpdateUserName)
                    .HasMaxLength(50)
                    .HasComment("修改人名称");

                entity.Property(e => e.ViewLink)
                    .HasMaxLength(200)
                    .HasComment("视图链接");
            });

            modelBuilder.Entity<HjsfSysMessage>(entity =>
            {
                entity.ToTable("HJSF_SysMessage");

                entity.HasIndex(e => e.CreateDate)
                    .HasName("Index_CreateDate");

                entity.HasIndex(e => e.Status)
                    .HasName("Index_Status");

                entity.HasIndex(e => e.ToUserId)
                    .HasName("Index_ToUserId");

                entity.HasIndex(e => new { e.CreateDate, e.Status })
                    .HasName("Index_CreateDate_Status");

                entity.HasIndex(e => new { e.Status, e.CreateDate })
                    .HasName("Index_Status_CreateDate");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ReadDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUserName).HasMaxLength(50);
            });

            modelBuilder.Entity<HjsfSysRecord>(entity =>
            {
                entity.ToTable("HJSF_SysRecord");

                entity.HasComment("系统日志");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.ChannelName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasComment("栏目名称");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人编号");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("创建人名称");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("操作IP");

                entity.Property(e => e.IsSuccess).HasComment("操作结果（成功/失败）");

                entity.Property(e => e.LinkUrl)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("操作地址");

                entity.Property(e => e.RecordType).HasComment("日志类型，添加、修改、删除等");

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasComment("日志描述");

                entity.Property(e => e.RequestParams).HasComment("请求参数");

                entity.Property(e => e.Status).HasComment("状态：0禁用、1启用、2回收");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.UpdateUserId).HasComment("修改人编号");

                entity.Property(e => e.UpdateUserName)
                    .HasMaxLength(50)
                    .HasComment("修改人名称");
            });

            modelBuilder.Entity<HjsfSysRole>(entity =>
            {
                entity.ToTable("HJSF_SysRole");

                entity.HasComment("角色");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人编号");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("创建人名称");

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("描述");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("角色名称");

                entity.Property(e => e.RoleParent).HasComment("率属于：0 普通用户，1 系统管理员");

                entity.Property(e => e.Status).HasComment("状态：0禁用、1启用、2回收");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.UpdateUserId).HasComment("修改人编号");

                entity.Property(e => e.UpdateUserName)
                    .HasMaxLength(50)
                    .HasComment("修改人名称");
            });

            modelBuilder.Entity<HjsfSysRoleChannelMapping>(entity =>
            {
                entity.ToTable("HJSF_SysRoleChannelMapping");

                entity.HasComment("系统角色与栏目关联");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.ChannelId).HasComment("栏目编号");

                entity.Property(e => e.RoleId).HasComment("角色编号");
            });

            modelBuilder.Entity<HjsfSysUser>(entity =>
            {
                entity.ToTable("HJSF_SysUser");

                entity.HasComment("员工");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人编号");

                entity.Property(e => e.CreateUserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("创建人名称");

                entity.Property(e => e.LoginCount).HasComment("登陆次数");

                entity.Property(e => e.LoginDate)
                    .HasColumnType("datetime")
                    .HasComment("登陆时间");

                entity.Property(e => e.LoginId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("账号");

                entity.Property(e => e.LoginIp)
                    .HasMaxLength(20)
                    .HasComment("登陆IP");

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("密码");

                entity.Property(e => e.Status).HasComment("状态：0禁用、1启用、2回收");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.UpdateUserId).HasComment("修改人编号");

                entity.Property(e => e.UpdateUserName)
                    .HasMaxLength(50)
                    .HasComment("修改人名称");
            });

            modelBuilder.Entity<HjsfSysUserInfo>(entity =>
            {
                entity.ToTable("HJSF_SysUserInfo");

                entity.HasComment("员工信息");

                entity.Property(e => e.Id)
                    .HasComment("编号")
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthday)
                    .HasMaxLength(50)
                    .HasComment("出生日期");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasComment("邮箱");

                entity.Property(e => e.EmpStatus).HasComment("在职状态");

                entity.Property(e => e.IsOperator).HasComment("操作员");

                entity.Property(e => e.IsSalesMan).HasComment("业务员");

                entity.Property(e => e.OrgId).HasComment("职能机构");

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .HasComment("手机");

                entity.Property(e => e.Sex).HasComment("性别");

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("拼音简码");

                entity.Property(e => e.Signature)
                    .HasMaxLength(200)
                    .HasComment("签名");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("姓名");

                entity.Property(e => e.WeChatOpenId)
                    .HasMaxLength(50)
                    .HasComment("微信OpenId");
            });

            modelBuilder.Entity<HjsfSysUserOrgMapping>(entity =>
            {
                entity.ToTable("HJSF_SysUserOrgMapping");

                entity.HasComment("系统用户与部门关联");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.OrgId).HasComment("部门编号");

                entity.Property(e => e.UserId).HasComment("用户编号");
            });

            modelBuilder.Entity<HjsfSysUserPositionMapping>(entity =>
            {
                entity.ToTable("HJSF_SysUserPositionMapping");

                entity.HasComment("系统用户与岗位关联");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.PositionId).HasComment("岗位编号");

                entity.Property(e => e.UserId).HasComment("用户编号");
            });

            modelBuilder.Entity<HjsfSysUserRoleMapping>(entity =>
            {
                entity.ToTable("HJSF_SysUserRoleMapping");

                entity.HasComment("系统用户与角色关联");

                entity.Property(e => e.Id).HasComment("编号");

                entity.Property(e => e.RoleId).HasComment("角色编号");

                entity.Property(e => e.UserId).HasComment("用户编号");
            });

            modelBuilder.Entity<ReportContractView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ReportContractView");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Orgid).HasColumnName("orgid");

                entity.Property(e => e.ProjectId).HasColumnName("projectId");

                entity.Property(e => e.TheActualAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
