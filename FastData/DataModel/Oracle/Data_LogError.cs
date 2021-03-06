﻿using System;
using FastData.Property;

namespace FastData.DataModel.Oracle
{
    /// <summary>
    /// 出错日志
    /// </summary>
    /// </summary>
    [Table(Comments = "出错日志")]
    internal class Data_LogError
    {
        /// <summary>
        /// error id
        /// </summary>
        [Column(Comments = "Error id", DataType = "Char", Length = 64, IsNull = false, IsKey = true)]
        public string ErrorId { get; set; }

        /// <summary>
        /// 出错方法
        /// </summary>
        [Column(Comments = "出错的方法", DataType = "Char", IsNull = true, Length = 32)]
        public string Method { get; set; }

        /// <summary>
        /// 出错对象
        /// </summary>
        [Column(Comments = "出错对象", DataType = "Char", IsNull = true, Length = 32)]
        public string Type { get; set; }
        
        /// <summary>
        /// sql语句
        /// </summary>
        [Column(Comments = "sql语句", DataType = "Clob", IsNull = false)]
        public string Sql { get; set; }

        /// <summary>
        /// 出错内容
        /// </summary>
        [Column(Comments = "出错内容", DataType = "Clob", IsNull = false)]
        public string Content { get; set; }

        /// <summary>
        /// 增加时间
        /// </summary>
        [Column(Comments = "增加时间", DataType = "Date", IsNull = false)]
        public DateTime AddTime { get; set; }
    }
}
