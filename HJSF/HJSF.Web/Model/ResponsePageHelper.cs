using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJSF.Web.Model
{
	/// <summary>
	/// 数据查询分页返回信息类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ResponsePageHelper<T>
	{
		/// <summary>
		/// 总记录数
		/// </summary>
		[JsonProperty("count")]
		public int Count { get; set; }
		/// <summary>
		/// 当前页
		/// </summary>
		[JsonProperty("page")]
		public int Page { get; set; }
		/// <summary>
		/// 每页记录数
		/// </summary>
		[JsonProperty("limit")]
		public int Limit { get; set; }
		/// <summary>
		/// 总页数
		/// </summary>
		[JsonProperty("pageCount")]
		public int PageCount { get; set; }
		/// <summary>
		/// 返回数据结果
		/// </summary>
		[JsonProperty("data")]
		public List<T> Data { get; set; }

		/// <summary>
		/// 其他附带内容
		/// </summary>
		[JsonProperty("param")]
		public dynamic Param { get; set; }
		/// <summary>
		/// 返回状态
		/// </summary>
		public Enum.ResponseCode Code { get; set; }
		/// <summary>
		/// 返回信息
		/// </summary>
		public string Msg { get; set; }
		/// <summary>
		/// 初始化数据查询分页返回信息类
		/// </summary>
		public ResponsePageHelper(List<T> data, int total, int page = 1, int limit = 10)
		{
			this.Page = page;
			this.Limit = limit;
			this.Count = total;
			if (null == data)
			{
				this.Code = Enum.ResponseCode.Error;
				this.Msg = "ERROR";
				this.Data = new List<T>();
			}
			else
			{
				this.PageCount = (int)Math.Ceiling(total / (double)limit);
				this.Data = data;
				this.Code = Enum.ResponseCode.Success;
				this.Msg = "SUCCESS";
			}
		}
	}

}
