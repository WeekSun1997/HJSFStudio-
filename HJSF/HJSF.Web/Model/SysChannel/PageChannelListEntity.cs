using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJSF.Web.Model.SysChannel
{
	/// <summary>
	/// 左侧菜单
	/// </summary>
	public class PageChannelListEntity
	{
		/// <summary>
		/// 导航名称，未指定jump属性值该名称将作为链接Url
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }
		/// <summary>
		/// 导航名称
		/// </summary>
		[JsonProperty("title")]
		public string Title { get; set; }
		/// <summary>
		/// 导航图标，可以使用Font Awesome图标
		/// </summary>
		[JsonProperty("icon")]
		public string Icon { get; set; }
		/// <summary>
		/// 链接Url，指定了该属性值则优先使用该值。否则将使用name属性值拼接Url
		/// </summary>
		[JsonProperty("jump")]
		public string Jump { get; set; }
		/// <summary>
		/// 下级导航
		/// </summary>
		[JsonProperty("list")]
		public List<PageChannelListEntity> List { get; set; }

	}
}
