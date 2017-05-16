using System;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mozlite.Mvc.TagHelpers.Binders.DataBinders
{
    /// <summary>
    /// �ݹ���֡�
    /// </summary>
    public class ChildrenSyntax : Syntax
    {
        /// <summary>
        /// �ؼ��֡�
        /// </summary>
        public override string Keyword => "children";

        /// <summary>
        /// ������䡣
        /// </summary>
        /// <param name="output">�����ǩʵ����</param>
        /// <param name="code">��������顣</param>
        /// <param name="instance">��ǰʵ������</param>
        /// <param name="element">��ǰ�ڵ�ʵ����</param>
        /// <returns>�������ɵ���䡣</returns>
        public override void Parse(TagHelperOutput output, string code, object instance, CodeBlockElement element)
        {
            if (instance is IEnumerable children)
            {
                var current = element.Any() ? element as Element : element.Doc;
                foreach (var child in children)
                {
                    Render(output, child, current);
                }
            }
        }
    }
}