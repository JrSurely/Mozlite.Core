﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.Mvc.TagHelpers
{
    /// <summary>
    /// 显示日期。
    /// </summary>
    [HtmlTargetElement("x:datetime")]
    public class DateTimeLengthTagHelper : TagHelperBase
    {
        private const string AttributeName = "x-date";
        /// <summary>
        /// 当前日期。
        /// </summary>
        [HtmlAttributeName(AttributeName)]
        public DateTime? Date { get; set; }

        /// <summary>
        /// 异步访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        /// .
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (Date == null)
            {
                output.SuppressOutput();
                return;
            }
            output.TagName = "span";
            var date = Date.Value;
            var now = DateTime.Now;
            var content = await output.GetChildContentAsync();
            if (date.Year != now.Year)//过去年份
            {
                output.Content.AppendHtml(date.ToString(Resources.YearDateFormat));
                output.Content.AppendHtml(content);
                return;
            }
            if (date.Month < now.Month || date > now)
            {
                output.Content.AppendHtml(date.ToString(Resources.DateFormat));
                output.Content.AppendHtml(content);
                return;
            }
            var offset = now - date;
            if (offset.TotalDays >= 1)
            {
                output.Content.AppendHtml(string.Format(Resources.DaysBefore, (int)offset.TotalDays));
                output.Content.AppendHtml(content);
                return;
            }
            if (offset.TotalHours >= 1)
            {
                output.Content.AppendHtml(string.Format(Resources.HoursBefore, (int)offset.TotalHours));
                output.Content.AppendHtml(content);
                return;
            }
            if (offset.TotalMinutes >= 1)
            {
                output.Content.AppendHtml(string.Format(Resources.MunitesBefore, (int)offset.TotalMinutes));
                output.Content.AppendHtml(content);
                return;
            }
            output.Content.AppendHtml(Resources.JustNow);
            output.Content.AppendHtml(content);
        }
    }
}
