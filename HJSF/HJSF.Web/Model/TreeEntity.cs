using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJSF.Web.Model
{
	/// <summary>
	/// 模块树形实体
	/// </summary>
    public class TreeEntity
    {
		/// <summary>
		/// 编号
		/// </summary>
		[JsonProperty("id")]
		public long Id { get; set; }
		/// <summary>
		/// 文本
		/// </summary>
		[JsonProperty("label")]
		public string Label { get; set; }
		/// <summary>
		/// 是否禁用
		/// </summary>
		[JsonProperty("disabled")]
		public bool Disabled { get; set; }
		/// <summary>
		/// 是否选中
		/// </summary>
		[JsonProperty("checked")]
		public bool Checked { get; set; }

		/// <summary>
		/// 节点是否初始展开
		/// </summary>
		[JsonProperty("spread")]
		public bool Spread { get; set; }

		/// <summary>
		/// 下级模块
		/// </summary>
		[JsonProperty("children")]
		public List<TreeEntity> Children { get; set; }
	}
}
